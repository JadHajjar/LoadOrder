#pragma warning disable 
namespace CO.Plugins {
    using CO.IO;
    using CO.PlatformServices;
    using LoadOrderTool;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.ConstrainedExecution;
    using LoadOrderTool.Util;
    using Mono.Cecil;
    using System.Threading;
    using LoadOrderShared;
    using LoadOrderTool.Data;
    using LoadOrderTool.UI;
    using System.Globalization;
    using System.Threading.Tasks;

    public class PluginManager : SingletonLite<PluginManager>, IDataManager {
        public static ConfigWrapper ConfigWrapper = ConfigWrapper.instance;
        static LoadOrderConfig Config => ConfigWrapper.Config;

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public event Action EventLoaded;

        public enum MessageType {
            Error,
            Warning,
            Message
        }

        public delegate void AddMessageHandler(PluginManager.MessageType type, string message);

        public delegate void PluginsChangedHandler();

        public PluginInfo GetPlugin(string includedPath) =>
            m_Plugins.FirstOrDefault(predicate => predicate.IncludedPath == includedPath);

        public class PluginInfo : IWSItem {
            // private Type m_UserModImplementation;
            // public List<Assembly> m_Assemblies;
            // public int assemblyCount => m_Assemblies.Count;

            private TypeDefinition m_UserModImplementation;


            private bool m_IsBuiltin;

            private bool m_Unloaded;

            private string m_Path;

            /// <summary> dir name without prefix. </summary>
            private string m_CachedName;

            /// <summary> real mod path (with prefix if any) </summary>
            public string ModPath => m_Path;
            /// <summary>the name of the directory that contains the mod</summary>
            public string dirName => new DirectoryInfo(ModPath).Name;

            public string IncludedPath {
                get {
                    if (dirName.StartsWith("_"))
                        return Path.Combine(parentDirPath, dirName.Substring(1));
                    else
                        return ModPath;
                }
            }

            private string dllPath_;

            public string dllName => userModImplementation?.Module.Assembly.Name.Name;
            public string dllPath {
                get {
                    if(dllPath_ == null) 
                        _ = userModImplementation; // also initializes dllPath_
                    return dllPath_;
                }
            }

            public bool isBuiltin => m_IsBuiltin;

            public bool IsLocal => !IsWorkshop;
            public bool IsWorkshop => PublishedFileId.IsValid;

            /// <summary>
            /// full path to the parent of the mod's directory.
            /// </summary>
            public string parentDirPath => Directory.GetParent(ModPath).FullName;

            /// <summary> dir name without prefix. </summary>
            public string name => m_CachedName;

            private string dispalyPath_;

            public string DispalyPath =>
                dispalyPath_ ??= IsWorkshop ? $"{dllName}.dll" : $"{ContentUtil.ToIncludedPath(dirName)}\\{dllName}.dll";

            string displayText_;
            public string DisplayText {
                get {
                    if (displayText_ == null) {
                        string modName = CSModCache?.Name;
                        if (modName.IsNullorEmpty())
                            modName = SteamCache?.Name;

                        if (modName.IsNullorEmpty()) {
                            modName = DispalyPath;
                        }

                        string version = UserModAssemblyVersion == null ? "" : "V" + UserModAssemblyVersion;
                        string dllDetail = $"{DispalyPath} {version}";
                        displayText_ = $"{modName} ({dllDetail})";
                        
                    }
                    return displayText_;
                }
            }

            public string Description => CSItemCache?.Description;

            public DateTime DateUpdatedUTC => SteamCache?.DateUpdatedUTC ?? default;

            string strDateUpdated_;
            public string StrDateUpdate {
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
                        if (File.Exists(dllPath)){
                            dateDownloadedUTC_ = File.GetCreationTimeUtc(dllPath);
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

            public string Author => SteamCache?.Author;

            string searchText_;
            public string SearchText => searchText_ ??=
                $"{DisplayText} {PublishedFileId} {Author}".Trim();

            public string[] DllPaths =>
                Directory.GetFiles(ModPath, "*.dll", SearchOption.AllDirectories);

            /// <summary>
            /// precondition: all dependent assemblies are loaded.
            /// </summary>
            public bool isCameraScript => GetImplementation(kCameraScript) != null;

            public bool IsHarmonyMod() =>
                name == "2040656402" || dllName == "CitiesHarmony";

            /// <summary>
            /// precondition: all dependent assemblies are loaded.
            /// </summary>
            public bool HasUserMod => userModImplementation != null;

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
                get =>
                    !dirName.StartsWith("_") &&
                    !File.Exists(Path.Combine(ModPath, ContentUtil.EXCLUDED_FILE_NAME));
                set {
                    isIncludedPending_ = value; 
                    if (value == IsIncluded)
                        return;
                    bool success;
                    if (value) {
                        success = ContentUtil.Include(m_Path);
                    } else {
                        success = ContentUtil.Exclude(m_Path);
                    }
                    if (success) {
                        // fix mod path if necessary.
                        string includedPath = ContentUtil.ToIncludedPath(m_Path);
                        dllPath_ = dllPath_.Replace(m_Path, includedPath);
                        m_Path = includedPath;
                    } else {
                        // move failed. reverse value: 
                        isIncludedPending_ = IsIncluded; 
                        var modGrid = LoadOrderWindow.Instance?.dataGridMods;
                        if(modGrid != null)
                            modGrid.GetRow(this).Cells[modGrid.CIsIncluded.Index].Value = isIncludedPending_;
                    }
                }
            }

            private PublishedFileId m_PublishedFileId = PublishedFileId.invalid;

            private PluginInfo() {
            }

            public PluginInfo(string path, bool builtin, PublishedFileId id) {
                this.m_Path = path;
                this.m_CachedName = Path.GetFileNameWithoutExtension(path);
                if (m_CachedName.StartsWith("_"))
                    m_CachedName = m_CachedName.Substring(1);
                //this.m_Assemblies = new List<Assembly>();
                this.m_IsBuiltin = builtin;
                this.m_PublishedFileId = id;

                this.ModInfo =
                    Config.Mods.FirstOrDefault(item => item.Path == IncludedPath)
                    ?? new LoadOrderShared.ModInfo { Path = IncludedPath };
                this.CSModCache = ConfigWrapper.CSCache?.GetItem(IncludedPath) as CSCache.Mod;
                if (IsWorkshop)
                    this.SteamCache = ConfigWrapper.SteamCache.GetOrCreateItem(PublishedFileId);

                isIncludedPending_ = IsIncluded;
                isEnabledPending_ = isEnabled;
            }

            public void ResetCache() {
                if (IsWorkshop)
                    this.SteamCache = ConfigWrapper.SteamCache.GetOrCreateItem(PublishedFileId);
                this.CSModCache = ConfigWrapper.CSCache?.GetItem(IncludedPath) as CSCache.Mod;
                this.strDateDownloaded_ = null;
                this.dateDownloadedUTC_ = null;
                this.strDateUpdated_ = null;
                this.displayText_ = null;
                this.searchText_ = null;
            }

            public void ReloadConfig() {
                // discard pending values.
                isEnabled = isEnabled; 
                IsIncluded = IsIncluded;
                ResetCache();
            }

            //public bool ContainsAssembly(Assembly asm)
            //{
            //    for (int i = 0; i < this.m_Assemblies.Count; i++) {
            //        if (this.m_Assemblies[i] == asm) {
            //            return true;
            //        }
            //    }
            //    return false;
            //}

            //public bool ContainsAssembly(AssemblyName asmName)
            //{
            //    foreach (Assembly assembly in this.m_Assemblies) {
            //        if (assembly.GetName().FullName == asmName.FullName) {
            //            return true;
            //        }
            //    }
            //    return false;
            //}

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            public unsafe int GetLegacyHashCode(string str) {
                fixed (char* ptr = str + RuntimeHelpers.OffsetToStringData / 2) {
                    char* ptr2 = ptr;
                    char* ptr3 = ptr2 + str.Length - 1;
                    int num = 0;
                    while (ptr2 < ptr3) {
                        num = (num << 5) - num + (int)(*ptr2);
                        num = (num << 5) - num + (int)ptr2[1];
                        ptr2 += 2;
                    }
                    ptr3++;
                    if (ptr2 < ptr3) {
                        num = (num << 5) - num + (int)(*ptr2);
                    }
                    return num;
                }
            }

            bool isEnabledPending_;
            public bool IsEnabledPending {
                get => isEnabledPending_;
                set {
                    if (isEnabledPending_ != value) {
                        isEnabledPending_ = value;
                        ConfigWrapper.Dirty = true;
                    }
                }
            }
            public string savedEnabledKey_ =>
                name + GetLegacyHashCode(VirtualIncludedPath).ToString() + ".enabled";
            public SavedBool SavedEnabled => new SavedBool(savedEnabledKey_, assetStateSettingsFile, def: false, autoUpdate: true);
            public bool isEnabled {
                get => SavedEnabled.value;
                set => SavedEnabled.value = isEnabledPending_ = value;
            }

            public string VirtualIncludedPath
            {
                get
                {
                    var path = IncludedPath;

					if (!string.IsNullOrWhiteSpace(LoadOrderToolSettings.Instace.VirtualAppData))
						path = path.Replace(DataLocation.localApplicationData, LoadOrderToolSettings.Instace.VirtualAppData);

					if (!string.IsNullOrWhiteSpace(LoadOrderToolSettings.Instace.VirtualGamePath))
						path = path.Replace(DataLocation.GamePath, LoadOrderToolSettings.Instace.VirtualGamePath);

                    return path;
				}
            }


			public LoadOrderShared.ModInfo ModInfo { get; private set; }
            public LoadOrderShared.ItemInfo ItemConfig => ModInfo;

            public LoadOrderTool.Data.SteamCache.Item SteamCache { get; private set; }

            public LoadOrderShared.CSCache.Mod CSModCache { get; private set; }
            public CSCache.Item CSItemCache => CSModCache;


            public void ResetLoadOrder() => LoadOrder = LoadOrderShared.LoadOrderConfig.DefaultLoadOrder;

            public bool HasLoadOrder() => LoadOrder != LoadOrderShared.LoadOrderConfig.DefaultLoadOrder;


            public int LoadOrder {
                get => ModInfo.LoadOrder;
                set {
                    ModInfo.LoadOrder = value;
                    ConfigWrapper.Dirty = true;
                }

            }

            public override string ToString() {
                return $"PluginInfo: path={ModPath} included={IsIncludedPending} enabled={IsEnabledPending} DisplayText={DisplayText} " +
                    $"cachedName={m_CachedName}"; /*assemblies={assembliesString}"*/
            }

            public string StrStatus => (SteamCache == null ||  SteamCache.Status == default)
                ? ""
                : SteamCache.Status.ToString().SpaceCamelCase();

            //public string assembliesString {
            //    get {
            //        int assemblyCount = this.assemblyCount;
            //        string strCount = (assemblyCount > 0) ? string.Empty : "No assemblies";
            //        for (int i = 0; i < assemblyCount; i++) {
            //            AssemblyName asmName = this.m_Assemblies[i].GetName();
            //            strCount = string.Concat(new object[]
            //            {
            //                strCount,
            //                asmName.Name,
            //                "[",
            //                asmName.Version,
            //                "]"
            //            });
            //            if (i < assemblyCount - 1) {
            //                strCount += ", ";
            //            }
            //        }
            //        return strCount;
            //    }
            //}

            public TypeDefinition userModImplementation =>
                m_UserModImplementation ??= GetImplementation(PluginManager.kUserMod);

            public Version UserModAssemblyVersion => userModImplementation?.Module?.Assembly?.Name?.Version ?? default;

            public TypeDefinition GetImplementation(string fullInterfaceName) =>
                AssemblyUtil.FindImplementation(DllPaths, fullInterfaceName, out dllPath_);

            public void ApplyPendingValues() {
                IsIncluded = isIncludedPending_;
                isEnabled = isEnabledPending_;
            }
        }

        //public Type userModImplementation {
        //        get {
        //            return m_UserModImplementation =
        //                m_UserModImplementation ??
        //                GetImplementation(PluginManager.userModType);
        //        }
        //    }
        //    /// <summary>
        //    /// precondition: all dependent assemblies are loaded.            
        //    /// </summary>
        //    public Type GetImplementation(Type type)
        //    {
        //        for (int i = 0; i < this.m_Assemblies.Count; i++) {
        //            try {
        //                var asm = m_Assemblies[i];
        //                if (!asm.GetReferencedAssemblies().Any(_asm => _asm?.Name == "ICities")) {
        //                    continue;
        //                }
        //                foreach (Type type2 in this.m_Assemblies[i].GetExportedTypes()) {
        //                    if (type2.IsClass && !type2.IsAbstract) {
        //                        Type[] interfaces = type2.GetInterfaces();
        //                        if (interfaces.Contains(type)) {
        //                            return type2;
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                Log.Exception(new Exception("this can happen if not all dependencies are loaded", ex));
        //            }
        //        }
        //        return null;
        //    }
        //}

        public static readonly string kModExtension = ".dll";

        public static readonly string kSourceExtension = ".cs";

        public static bool noWorkshop;

        //private Dictionary<Assembly, string> m_AssemblyLocations = new Dictionary<Assembly, string>();

        //private Dictionary<string, PluginInfo> m_Plugins = new Dictionary<string, PluginInfo>();
        private List<PluginInfo> m_Plugins = new List<PluginInfo>();


        public static string assetStateSettingsFile => "userGameState";

        //public static Type userModType => Type.GetType("ICities.IUserMod, ICities");
        public const string kUserMod = "ICities.IUserMod";

        //public static Type cameraScriptType => Type.GetType("ICities.ICameraExtension, ICities");
        public const string kCameraScript = "ICities.ICameraExtension";


        public static event PluginManager.AddMessageHandler eventLogMessage;

        public event PluginManager.PluginsChangedHandler eventPluginsChanged;

        public event PluginManager.PluginsChangedHandler eventPluginsStateChanged;

        public IEnumerable<PluginInfo> GetMods() =>
            m_Plugins.Where(p => p.HasUserMod).ToArray(); // convert to array to avoid race conditions

        public IEnumerable<PluginInfo> GetCameraPluginInfos() =>
             m_Plugins.Where(p => p.isCameraScript);

        public int modCount => GetMods().Count();

        public void Load() => LoadPlugins().Wait();
        public async Task LoadPlugins() {
            Log.Info("Loading Plugins ...", true);
            Log.Debug(Environment.StackTrace);
            IsLoading = true;
            IsLoaded = false;


            try {
                IsLoading = true;
                IsLoaded = false;

                await Task.Run(LoadPluginsImpl);

                m_Plugins = m_Plugins.Where(p => p.HasUserMod).ToList();
                Log.Debug($"{m_Plugins.Count} plugins remained after purging non-mods.");

                Config.Mods = Config.Mods
                    .Union(m_Plugins.Select(item => item.ModInfo))
                    .ToArray();

                ConfigWrapper.Dirty = true;
            } catch (Exception ex) {
                Log.Exception(ex);
            } finally {
                IsLoaded = false;
                IsLoaded = true;
                //this.CreateReporters(modsPath);
                try { EventLoaded?.Invoke(); } catch (Exception ex) { ex.Log(); }
            }
        }

        void LoadPluginsImpl() {
            string builtinModsPath = Path.Combine(DataLocation.gameContentPath, "Mods");
            string addonsModsPath = DataLocation.modsPath;
            m_Plugins = new List<PluginInfo>();

            this.LoadPluginInfosAtPath(builtinModsPath, true);
            this.LoadPluginInfosAtPath(addonsModsPath, false);
            if (!PluginManager.noWorkshop) {
                this.LoadWorkshopPluginInfos();
            }
            //this.LoadAssemblies();
            Log.Debug($"{m_Plugins.Count} plugins loaded.");
            ModDataGrid.SetProgress(50);

            // purge plugins without a IUserMod Implementation. this also filters out non-cs mods.
            // all dependent assemblies must be loaded before doing this.
            for (int i = 0; i < m_Plugins.Count; ++i) {
                var p = m_Plugins[i];
                Log.Info($"hasUserMod:{p.HasUserMod} " + p, false);
                ModDataGrid.SetProgress(50 + (i * 40) / m_Plugins.Count);
            }
        }

        private void LoadPluginInfosAtPath(string path, bool builtin) {
            ContentUtil.EnsureLocalItemsAt(parentDir: path);
            string[] directories = Directory.GetDirectories(path);
            foreach (string dir in directories) {
                //if (!Path.GetFileName(dir).StartsWith("_"))
                m_Plugins.Add(new PluginInfo(dir, builtin, PublishedFileId.invalid));
                //else
                //    PluginManager.LogMessage(PluginManager.MessageType.Message, "Inactive mod path found: " + Path.GetFileName(dir));

            }
        }

        private void LoadWorkshopPluginInfos() {
            var subscribedItems = ContentUtil.GetSubscribedItems()?.ToArray();
            Log.Debug($"subscribed items are: " + string.Join(", ", subscribedItems));
            if (subscribedItems != null) {
                for (int i = 0; i < subscribedItems.Length; ++i) {
                    var id = subscribedItems[i];
                    string subscribedItemPath = ContentUtil.GetSubscribedItemPath(id);
                    if (subscribedItemPath != null && Directory.Exists(subscribedItemPath)) {
                        //Log.Debug("scanned: " + subscribedItemPath);
                        var files = new DirectoryInfo(subscribedItemPath).GetFiles("*.dll");
                        if (files.IsNullorEmpty()) {
                            //Log.Debug($"{subscribedItemPath} has no dlls");
                            continue;
                        }
                        m_Plugins.Add(new PluginInfo(subscribedItemPath, false, id));
                        ModDataGrid.SetProgress((i * 50) / subscribedItems.Length);
                    } else {
                        Log.Debug("directory does not exist: " + subscribedItemPath);
                    }
                }
            }
        }

        public void Save() { } // writing to game settings file or moving dirs is considered serialization not saving.

        public void ApplyPendingValues() {
            foreach(var p in GetMods())
                p.ApplyPendingValues();
        }

        public void LoadFromProfile(LoadOrderProfile profile, bool replace = true) {
            Log.Called("replace:" + replace);
            foreach (var pluginInfo in GetMods()) {
                var modProfile = profile.GetMod(pluginInfo.IncludedPath);
                if (modProfile != null) {
                    bool included0 = pluginInfo.IsIncludedPending;
                    bool enabled0 = pluginInfo.IsEnabledPending;
                    modProfile.WriteTo(pluginInfo);
                    if (!replace) {
                        pluginInfo.IsIncludedPending |= included0;
                        pluginInfo.IsEnabledPending |= enabled0;
                    }
                } else if (replace) {
                    pluginInfo.LoadOrder = LoadOrderConfig.DefaultLoadOrder;
                    pluginInfo.IsIncludedPending = false;
                    pluginInfo.IsEnabledPending = false;
                }
            }
        }

        public void SaveToProfile(LoadOrderProfile profile) {
            var list = new List<LoadOrderProfile.Mod>(this.m_Plugins.Count);
            foreach (var pluginInfo in m_Plugins) {
                var modProfile = new LoadOrderProfile.Mod(pluginInfo);
                if (modProfile.IsIncluded || modProfile.IsEnabled) {
                    list.Add(modProfile);
                }
            }
            profile.Mods = list.ToArray();
        }

        //private static int sUniqueCompilationID;
        //private static string[] m_AdditionalAssemblies;
        //private void LoadAssemblies()
        //{
        //    foreach (PluginInfo pluginInfo in m_Plugins) {
        //        string modPath = pluginInfo.ModPath;
        //        string[] files = Directory.GetFiles(modPath, "*.dll", SearchOption.AllDirectories);
        //        foreach (string dllpath in files) {
        //            try {
        //                Assembly asm = AssemblyUtil.LoadDLL(dllpath);
        //                pluginInfo.m_Assemblies.Add(asm);
        //                //this.m_AssemblyLocations[asm] = dllpath;
        //                Log.Info("Assembly loaded: " + asm);
        //            } catch (Exception ex) {
        //                Log.Info("Assembly at " + dllpath + " failed to load.\n" + ex.Message);
        //            }
        //        }
        //    }
        //}

        //public static void SetAdditionalAssemblies(params string[] assemblies)
        //{
        //    PluginManager.m_AdditionalAssemblies = assemblies;
        //}

        //public static void CompileScripts()
        //{
        //    PluginManager.CompileScripts(DataLocation.modsPath, PluginManager.m_AdditionalAssemblies);
        //}

        //public static void CompileScripts(string modPath, params string[] additionalAssemblies)
        //{
        //    try
        //    {
        //        string[] directories = Directory.GetDirectories(modPath);
        //        foreach (string text in directories)
        //        {
        //            if (!Path.GetFileName(text).StartsWith("_"))
        //            {
        //                string text2 = Path.Combine(text, "Source");
        //                if (Directory.Exists(text2))
        //                {
        //                    PluginManager.CompileSourceInFolder(text2, text, additionalAssemblies);
        //                }
        //                else
        //                {
        //                    PluginManager.LogMessage(PluginManager.MessageType.Message, "No source files found: " + Path.GetFileName(text));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PluginManager.LogMessage(PluginManager.MessageType.Error, ex.ToString());
        //    }
        //}

        //public static void CompileSourceInFolder(string sourcePath, string outputPath, params string[] additionalAssemblies)
        //{
        //    try
        //    {
        //        string[] files = Directory.GetFiles(sourcePath, "*.cs", SearchOption.AllDirectories);
        //        if (files.Length > 0)
        //        {
        //            PluginManager.CompileSource(files, Path.Combine(outputPath, Path.GetFileName(outputPath) + ".dll"), additionalAssemblies);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PluginManager.LogMessage(PluginManager.MessageType.Error, ex.ToString());
        //    }
        //}

        private static void LogMessage(PluginManager.MessageType type, string message)
        {
            if (PluginManager.eventLogMessage != null) {
                PluginManager.eventLogMessage(type, message);
            }
            if (type == PluginManager.MessageType.Error) {
                Log.Error(message);
                return;
            }
            if (type == PluginManager.MessageType.Warning) {
                Log.Error("WARNING: " + message);
                return;
            }
            if (type == PluginManager.MessageType.Message) {
                Log.Info(message);
            }
        }

        public void ResetCache() {
            foreach (var mod in GetMods()) {
                mod.ResetCache();
            }
        }

        public void ReloadConfig() {
            foreach(var mod in GetMods()) {
                mod.ReloadConfig();
            }
        }

        //private static bool CompileSource(string[] sourceFiles, string output, params string[] additionalAssemblies)
        //{
        //    Log.Info("Compiling " + string.Join(", ", sourceFiles));
        //    ColossalCSharpCodeProvider colossalCSharpCodeProvider = new ColossalCSharpCodeProvider();
        //    CompilerParameters compilerParameters = new CompilerParameters();
        //    foreach (string text in additionalAssemblies)
        //    {
        //        if (Path.IsPathRooted(text))
        //        {
        //            compilerParameters.ReferencedAssemblies.Add(text);
        //        }
        //        else
        //        {
        //            compilerParameters.ReferencedAssemblies.Add(DataLocation.ManagedDLL);
        //        }
        //    }
        //    string text2 = Path.Combine(DataLocation.addonsPath, "Temp");
        //    if (!Directory.Exists(text2))
        //    {
        //        Directory.CreateDirectory(text2);
        //    }
        //    string text3 = Path.Combine(text2, Path.GetFileNameWithoutExtension(output) + PluginManager.sUniqueCompilationID.ToString() + ".dll");
        //    PluginManager.sUniqueCompilationID++;
        //    compilerParameters.ReferencedAssemblies.Add(typeof(PluginManager).Assembly.Location);
        //    compilerParameters.OutputAssembly = text3;
        //    compilerParameters.GenerateInMemory = false;
        //    compilerParameters.TreatWarningsAsErrors = false;
        //    CompilerResults compilerResults = colossalCSharpCodeProvider.CompileAssemblyFromFile(compilerParameters, sourceFiles);
        //    if (compilerResults.Errors.HasErrors)
        //    {
        //        PluginManager.LogMessage(PluginManager.MessageType.Error, "Errors building assembly '" + output + "'");
        //        foreach (CompilerError compilerError in compilerResults.Errors)
        //        {
        //            if (!compilerError.IsWarning)
        //                PluginManager.LogMessage(PluginManager.MessageType.Error, compilerError.ToString());
        //        }
        //    }
        //    else
        //    {
        //        if (compilerResults.Errors.HasWarnings)
        //        {
        //            PluginManager.LogMessage(PluginManager.MessageType.Warning, "Warnings building assembly '" + output + "'");
        //        }
        //        foreach (object obj2 in compilerResults.Errors)
        //        {
        //            CompilerError compilerError2 = (CompilerError)obj2;
        //            if (compilerError2.IsWarning)
        //            {
        //                PluginManager.LogMessage(PluginManager.MessageType.Warning, compilerError2.ToString());
        //            }
        //        }
        //        string text4 = Path.Combine(DataLocation.modsPath, output);
        //        PluginManager.LogMessage(PluginManager.MessageType.Message, "Assembly '" + text4 + "' compiled successfully");
        //        if (File.Exists(text4))
        //        {
        //            File.Replace(text3, text4, null);
        //        }
        //        else
        //        {
        //            File.Move(text3, text4);
        //        }
        //    }
        //    DirectoryUtils.DeleteDirectory(text2);
        //    return !compilerResults.Errors.HasErrors;
        //}
    }
}
