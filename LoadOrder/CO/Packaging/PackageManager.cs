#pragma warning disable 
namespace CO.Packaging {
    using CO.IO;
    using CO.PlatformServices;
    using LoadOrderShared;
    using LoadOrderTool;
    using LoadOrderTool.Data;
    using LoadOrderTool.UI;
    using LoadOrderTool.Util;
    using Mono.Cecil;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.ConstrainedExecution;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class PackageManager : SingletonLite<PackageManager>, IDataManager {
        static ConfigWrapper ConfigWrapper => ConfigWrapper.instance;
        static LoadOrderConfig Config => ConfigWrapper.Config;

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public event Action EventLoaded;

        public AssetInfo GetAsset(string path) =>
            m_Assets.FirstOrDefault(a => a.AssetPath == path);

        public class AssetInfo : IWSItem {
            private string m_Path;

            //private bool m_IsBuiltin;

            //public bool isBuiltin => m_IsBuiltin;

            public bool IsLocal => !IsWorkshop;
            public bool IsWorkshop => PublishedFileId.IsValid;

            public string AssetPath => m_Path;

            public string IncludedPath => m_Path;

            public string AssetName => Path.GetFileNameWithoutExtension(AssetPath);
            public string FileName => Path.GetFileName(AssetPath);

            string displayText_;
            public string DisplayText {
                get {
                    if (string.IsNullOrEmpty(displayText_)) {
                        displayText_ = CSAssetCache?.Name ?? SteamCache?.Name;
                        string assetFile = Path.GetFileName(AssetPath);
                        if (string.IsNullOrEmpty(displayText_))
                            displayText_ = assetFile;
                        else //if(string.IsNullOrEmpty(CSAssetCache?.Name))
                            displayText_ += $"({assetFile})";
                    }
                    return displayText_;
                }
            }

            public string Description => CSItemCache?.Description;

            string strTags_;
            public string StrTags =>
                strTags_ ??= string.Join(", ", GetTags());

            public DateTime DateUpdatedUTC => SteamCache?.DateUpdatedUTC ?? default;

            string strDateUpdated_;
            public string StrDateUpdated {
                get {
                    if (strDateUpdated_ != null)
                        return strDateUpdated_;
                    else if (DateUpdatedUTC == default)
                        return strDateUpdated_ = "";
                    else
                        return strDateUpdated_ = DateUpdatedUTC.ToLocalTime().ToString("d", CultureInfo.CurrentCulture);
                }
            }

            DateTime? dateDownloadedUTC_;
            public DateTime DateDownloadedUTC {
                get {
                    if (dateDownloadedUTC_ == null) {
                        if (File.Exists(AssetPath)) {
                            dateDownloadedUTC_ = File.GetCreationTimeUtc(AssetPath);
                        } else {
                            dateDownloadedUTC_ = default(DateTime);
                        }
                    }
                    return dateDownloadedUTC_.Value;
                }
            }

            string strDateDownloaded_;
            public string StrDateDownloaded {
                get {
                    if (strDateDownloaded_ != null)
                        return strDateDownloaded_;
                    else if (DateDownloadedUTC == default)
                        return strDateDownloaded_ = "";
                    else
                        return strDateDownloaded_ = DateDownloadedUTC.ToLocalTime().ToString("d", CultureInfo.CurrentCulture);
                }
            }

            public string StrStatus => (SteamCache == null || SteamCache.Status == default)
                ? ""
                : SteamCache.Status.ToString().SpaceCamelCase();

            public string Author => SteamCache?.Author;

            string searchText_;
            public string SearchText => searchText_ ??=
                $"{CSAssetCache?.Name} {SteamCache?.Name} {FileName} {PublishedFileId} {Author}".Trim();

            public PublishedFileId PublishedFileId => this.m_PublishedFileId;

            bool isIncludedPending_;
            public bool IsIncludedPending {
                get => isIncludedPending_;
                set {
                    if (isIncludedPending_ != value) {
                        isIncludedPending_ = value;
                        ConfigWrapper.Dirty = true;
                    }
                }
            }

            public bool IsIncluded {
                get => !ConfigAssetInfo.Excluded;
                set {
                    isIncludedPending_ = value;
                    ConfigAssetInfo.Excluded = !value;
                }
            }

            private PublishedFileId m_PublishedFileId = PublishedFileId.invalid;

            public IEnumerable<string> GetTags() =>
                CSAssetCache?.Tags ?? (SteamCache?.Tags) ?? Enumerable.Empty<string>();

            private AssetInfo() { }

            public AssetInfo(string path, bool builtin, PublishedFileId id) {
                string includedPath = ContentUtil.ToIncludedPathFull(path);
                this.m_Path = includedPath;
                //this.m_IsBuiltin = builtin;
                this.m_PublishedFileId = id;

                this.ConfigAssetInfo =
                    Config.Assets.FirstOrDefault(item => item.Path == includedPath)
                    ?? new LoadOrderShared.AssetInfo { Path = includedPath };
                this.CSAssetCache = ConfigWrapper.CSCache?.GetItem(includedPath) as CSCache.Asset;
                if (IsWorkshop)
                    this.SteamCache = ConfigWrapper.SteamCache.GetOrCreateItem(PublishedFileId);

                isIncludedPending_ = IsIncluded;
            }

            public void ResetCache() {
                if (IsWorkshop)
                    this.SteamCache = ConfigWrapper.SteamCache.GetOrCreateItem(PublishedFileId);
                this.CSAssetCache = ConfigWrapper.CSCache?.GetItem(this.IncludedPath) as CSCache.Asset;
                this.strDateDownloaded_ = null;
                this.dateDownloadedUTC_ = null;
                this.strDateUpdated_ = null;
                this.displayText_ = null;
                this.searchText_ = null;
                this.strTags_ = null;
            }

            #region metadata
            public LoadOrderShared.AssetInfo ConfigAssetInfo { get; private set; }
            public LoadOrderShared.ItemInfo ItemConfig => ConfigAssetInfo;

            public LoadOrderTool.Data.SteamCache.Item SteamCache { get; private set; }

            #endregion metadata

            public CSCache.Asset CSAssetCache { get; private set; }
            public CSCache.Item CSItemCache => CSAssetCache;

            public override string ToString() {
                return
                    $"AssetInfo: path={AssetPath} " +
                    $"included={IsIncludedPending} " +
                    $"DisplayText={DisplayText} " +
                    $"FileName={FileName}";
            }

            public void ApplyPendingValues() {
                IsIncluded = isIncludedPending_;
            }

            public void ReloadConfig() {
                //discard pending values
                IsIncluded = IsIncluded;
                ResetCache();
            }
        }

        public static readonly string packageExtension = ".crp";

        private List<AssetInfo> m_Assets = new List<AssetInfo>();

        public IEnumerable<AssetInfo> GetAssets() => m_Assets.ToArray(); // avoid race condition when list is being modified.

        public string[] GetAllTags() {
            var ret = m_Assets.SelectMany(a => a.GetTags()).Distinct().ToArray();
            Array.Sort(ret);
            return ret;
        }

        public void Load() => LoadPackages().Wait();
        public async Task LoadPackages() {
            try {
                Log.Info("Loading Assets ...", true);
                IsLoading = true;
                IsLoaded = false;

                m_Assets = new List<AssetInfo>();

                await Task.Run(LoadPackagesImpl);

                AssetDataGrid.SetProgress(90);

                Config.Assets = Config.Assets
                    .Union(m_Assets.Select(item => item.ConfigAssetInfo))
                    .ToArray();

                ConfigWrapper.Dirty = true;
            } catch (Exception ex) {
                Log.Exception(ex);
            } finally {
                IsLoaded = false;
                IsLoaded = true;
                try { EventLoaded?.Invoke(); } catch (Exception ex) { ex.Log(); }
            }

        }

        void LoadPackagesImpl() {
            //this.LoadPackages(Path.Combine(DataLocation.gameContentPath, "Maps"), PublishedFileId.invalid);
            //this.LoadPackages(Path.Combine(DataLocation.gameContentPath, "Scenarios"), PublishedFileId.invalid);
            this.LoadWorkshopPackages();
            AssetDataGrid.SetProgress(80);
            this.LoadPackages(DataLocation.stylesPath, PublishedFileId.invalid);
            this.LoadPackages(DataLocation.assetsPath, PublishedFileId.invalid);
            //this.LoadPackages(DataLocation.mapLocation, PublishedFileId.invalid);
            //this.LoadPackages(DataLocation.saveLocation, PublishedFileId.invalid);
            this.LoadPackages(DataLocation.mapThemesPath, PublishedFileId.invalid);
            //this.LoadPackages(DataLocation.scenarioLocation, PublishedFileId.invalid);
        }

        public void LoadWorkshopPackages() {
            var subscribedItems = ContentUtil.GetSubscribedItems()?.ToArray();
            if (subscribedItems != null) {
                for (int i = 0; i < subscribedItems.Length; ++i) {
                    var id = subscribedItems[i];
                    string subscribedItemPath = ContentUtil.GetSubscribedItemPath(id);
                    if (subscribedItemPath != null && Directory.Exists(subscribedItemPath)) {
                        //Log.Debug("scanned: " + subscribedItemPath);
                        LoadPackages(subscribedItemPath, id);
                        ModDataGrid.SetProgress((i * 80) / subscribedItems.Length);
                    } else {
                        //Log.Debug("directory does not exist: " + subscribedItemPath);
                    }
                }
            }
        }

        public void LoadPackages(string path, PublishedFileId id) {
            CheckFiles(path);
            try {
                foreach (string file in Directory.GetFiles(path)) {
                    try {
                        if (Path.GetExtension(file) == PackageManager.packageExtension) {
                            LoadPackage(file, id);
                        }
                    } catch (Exception ex) {
                        Log.Exception(ex);
                    }
                }
                foreach (string path2 in Directory.GetDirectories(path)) {
                    this.LoadPackages(path2, id);
                }
            } catch (Exception ex) {
                Log.Exception(ex);
            }
        }

        public static void CheckFiles(string path) {
            try {
                foreach (string file in Directory.GetFiles(path))
                    CheckFile(file);
            } catch (Exception ex) {
                Log.Exception(ex);
            }
        }

        public static void CheckFile(string fullFilePath) {
            string included = ContentUtil.ToIncludedPath(fullFilePath);
            string excluded = ContentUtil.ToExcludedPath(fullFilePath);
            if (File.Exists(included) && File.Exists(excluded)) {
                File.Move(included, excluded);
                fullFilePath = excluded;
            }
        }

        public void LoadPackage(string fullFilePath, PublishedFileId id) {
            var package = new PackageManager.AssetInfo(fullFilePath, false, id);
            m_Assets.Add(package);
        }

        public void Save() => ApplyPendingValues();
        public void ApplyPendingValues() {
            foreach (var p in GetAssets())
                p.ApplyPendingValues();
        }

        public AssetInfo GetAssetInfo(string path) =>
            this.GetAssets().FirstOrDefault(p => p.AssetPath == path);

        public void LoadFromProfile(LoadOrderProfile profile, bool replace = true) {
            foreach (var assetInfo in this.GetAssets()) {
                var assetProfile = profile.GetAsset(assetInfo.AssetPath);
                if (assetProfile != null) {
                    bool included0 = assetInfo.IsIncludedPending;
                    assetProfile.WriteTo(assetInfo);
                    if (!replace) {
                        assetInfo.IsIncludedPending |= included0;
                    }
                } else if (replace) {
                    //Log.Debug("asset profile with path not found: " + assetInfo.AssetPath);
                    assetInfo.IsIncludedPending = false;
                }
            }
        }

        public void SaveToProfile(LoadOrderProfile profile) {
            var list = new List<LoadOrderProfile.Asset>(m_Assets.Count);
            foreach (var assetInfo in m_Assets) {
                var assetProfile = new LoadOrderProfile.Asset(assetInfo);
                if (assetProfile.IsIncluded) {
                    list.Add(assetProfile);
                }
            }
            profile.Assets = list.ToArray();
        }

        public void ResetCache() {
            foreach (var assetInfo in m_Assets) {
                assetInfo.ResetCache();
            }
        }

        public void ReloadConfig() {
            foreach(var assetInfo in m_Assets) {
                assetInfo.ReloadConfig();
            }
        }
    }
}
