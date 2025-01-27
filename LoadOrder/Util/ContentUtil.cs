namespace LoadOrderTool.Util {
    using CO.IO;
    using CO.PlatformServices;
    using LoadOrderShared;
    using LoadOrderTool.Data;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using static LoadOrderTool.Data.SteamCache;

    public static class ContentUtil {
        public const string WS_URL_PREFIX = @"https://steamcommunity.com/sharedfiles/filedetails/?id=";
        public const string DLC_URL_PREFIX = @"https://store.steampowered.com/app/";
        public const string EXCLUDED_FILE_NAME = ".excluded";
        public static string GetItemURL(PublishedFileId id) {
            if (id == PublishedFileId.invalid || id.AsUInt64 == 0)
                return null;
            return WS_URL_PREFIX + id.AsUInt64;
        }
        public static string GetItemURL(string id) {
            if (string.IsNullOrEmpty(id))
                return null;
            return WS_URL_PREFIX + id;
        }

        public static string GetDLCURL(PublishedFileId id) {
            if (id == PublishedFileId.invalid || id.AsUInt64 == 0)
                return null;
            return DLC_URL_PREFIX + id.AsUInt64;
        }

        public static Process OpenURL(string url) {
            try {
                var ps = new ProcessStartInfo(url) {
                    UseShellExecute = true,
                    Verb = "open"
                };
                return Process.Start(ps);
            } catch (Exception ex2) {
                Log.Exception(
                    new Exception("could not open url: " + url, ex2),
                    "could not open url");
                return null;
            }
        }

        /// <summary>
        /// opens folder or file location in explorer.
        /// </summary>
        public static Process OpenPath(string path) {
            Log.Called(path);
            try {
                if (File.Exists(path)) {
                    string cmd = "explorer.exe";
                    string arg = "/select, " + path;
                    return Process.Start(cmd, arg);
                } else {
                    string cmd = "explorer.exe";
                    string arg = path;
                    return Process.Start(cmd, arg);
                }

            } catch (Exception ex) {
                Log.Exception(new Exception("could not open path: " + path, ex));
                return null;
            }
        }

        public static Process Execute(string dir, string exeFile, string args) {
            try {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    WorkingDirectory = dir,
                    FileName = exeFile,
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                };
                Log.Info($"Executing ...\n" +
                    $"\tWorkingDirectory={dir}\n" +
                    $"\tFileName={exeFile}\n" +
                    $"\tArguments={args}");
                Process process = new Process { StartInfo = startInfo };
                process.Start();
                process.OutputDataReceived += (_, e) => Log.Info(e.Data);
                process.ErrorDataReceived += (_, e) => Log.Warning(e.Data);
                process.Exited += (_, e) => Log.Info("process exited with code " + process.ExitCode);
                return process;
            } catch (Exception ex) {
                Log.Exception(ex);
                return null;
            }
        }

        public static Process Subscribe(IEnumerable<PublishedFileId> ids, bool unsub=false) => Subscribe(ids.Select(id => id.AsUInt64),unsub);
        public static Process Subscribe(IEnumerable<string> ids, bool unsub = false) => Subscribe(UGCListTransfer.ToNumber(ids), unsub);
        public static Process Subscribe(IEnumerable<ulong> ids, bool unsub = false) {
            if (ids.IsNullorEmpty()) return null;
            UGCListTransfer.SendList(ids, false);
            string command = unsub ?
                $"-applaunch 255710 -unsubscribe" :
                $"-applaunch 255710 -subscribe";
            return Execute(DataLocation.SteamPath, DataLocation.SteamExe, command);
        }

        public static bool IsPathIncluded(string fullPath) {
            return Path.GetFileName(fullPath).StartsWith("_");
        }
        public static string ToIncludedPath(string fullPath) {
            string parent = Path.GetDirectoryName(fullPath);
            string file = Path.GetFileName(fullPath);
            if (file.StartsWith("_"))
                file = file.Substring(1); //drop _
            return Path.Combine(parent, file);
        }

        public static string ToExcludedPath(string fullPath) {
            string parent = Path.GetDirectoryName(fullPath);
            string file = Path.GetFileName(fullPath);
            if (!file.StartsWith("_"))
                file = "_" + file;
            return Path.Combine(parent, file);
        }

        public static string ToIncludedPathFull(string path) {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path");


            var dirs = SplitPath(path);
            for (int i = 0; i < dirs.Length; ++i) {
                var dir = dirs[i];
                if (dir[0] == '_')
                    dirs[i] = dir.Substring(1);
            }
            return Path.Combine(dirs);
        }

        public static string[] SplitPath(string path) {
            return path.Split(
               new[] { Path.DirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool TryGetAssetID(string path, out ulong id) {
            if (!path.Contains(DataLocation.WorkshopContentPath)) {
                id = 0;
                return false;
            }
            path = GetRelativePath(DataLocation.WorkshopContentPath, path);
            int i = path.IndexOf('\\');
            var dirname = i < 0 ? path : path.Substring(0, i);
            Log.Debug($"path={path} dirname={dirname}");
            return TryGetID(dirname, out id);
        }

        public static string GetRelativePath(string relativeTo, string path) {
            var relativeToUri = new Uri(relativeTo.EndsWith(Path.DirectorySeparatorChar.ToString()) ? relativeTo : relativeTo + Path.DirectorySeparatorChar);
            var pathUri = new Uri(path);

            if (relativeToUri.Scheme != pathUri.Scheme) {
                return path;
            }

            var relativeUri = relativeToUri.MakeRelativeUri(pathUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        public static bool TryGetModID(string dir, out ulong id) {
            string dirname = new DirectoryInfo(dir).Name;
            return TryGetID(dirname, out id);
        }

        private static bool TryGetID(string dirName, out ulong id) {
            if (dirName.StartsWith("_"))
                dirName = dirName.Substring(1);
            return ulong.TryParse(dirName, out id);
        }


        static ulong Path2ID(string path) {
            TryGetID(Path.GetFileName(path), out ulong ret);
            return ret;
        }

        public static void Touch(string path) {
            if (!File.Exists(path)) {
                File.CreateText(path).Dispose();
            }
        }

    public static void TryDelete(string path) {
        if (File.Exists(path)) {
            File.Delete(path);
        }
    }

    public static bool Exclude(string path) {
        try {
            string excludedPath = ToExcludedPath(path);
            string includedPath = ToIncludedPath(path);
            if (Directory.Exists(excludedPath)) {
                if (Directory.Exists(includedPath)) {
                    Directory.Delete(excludedPath, true);
                } else {
                    Directory.Move(excludedPath, includedPath);
                } 
            } else if(!Directory.Exists(includedPath)) {
                Log.Error(message: $"'{path}' does not exist.");
                return false;
            }
            Touch(Path.Combine(includedPath, EXCLUDED_FILE_NAME));
            return true;
        } catch(Exception ex) { ex.Log(); }
        return false;
    }

        public static bool Include(string path) {
            try {
                string excludedPath = ToExcludedPath(path);
                string includedPath = ToIncludedPath(path);
                if (Directory.Exists(excludedPath)) {
                    if (Directory.Exists(includedPath)) {
                        Directory.Delete(excludedPath, true);
                    } else {
                        Directory.Move(excludedPath, includedPath);
                    }
                } else if (!Directory.Exists(includedPath)) {
                    Log.Error(message: $"'{path}' does not exist.");
                    return false;
                }
                TryDelete(Path.Combine(includedPath, EXCLUDED_FILE_NAME));
                return true;
            } catch (Exception ex) { ex.Log(); }
            return false;
        }

        static object ensureLock_ = new object();
        public static void EnsureSubscribedItems() {
            Log.Called();
            lock (ensureLock_) {
                foreach (var path in Directory.GetDirectories(DataLocation.WorkshopContentPath)) {
                    var dirName = Path.GetFileName(path);
                    if (!TryGetID(dirName, out ulong id)) continue;
                    if (dirName.StartsWith("_")) {
                        Exclude(path);
                    }
                }
            }
        }

        public static void EnsureLocalItemsAt(string parentDir) {
            Log.Called(parentDir);
            lock (ensureLock_) {
                foreach (var path in Directory.GetDirectories(parentDir)) {
                    var dirName = Path.GetFileName(path);
                    if (dirName.StartsWith("_")) {
                        Exclude(path);
                    }
                }
            }
        }

        public static PublishedFileId[] GetSubscribedItems() {
            var dirs = Directory.GetDirectories(DataLocation.WorkshopContentPath);
            var ret = new List<PublishedFileId>(dirs.Length);
            foreach (var path in Directory.GetDirectories(DataLocation.WorkshopContentPath)) {
                var dirName = Path.GetFileName(path);
                if (!TryGetID(dirName, out ulong id)) continue;
                ret.Add(new PublishedFileId(id));
            }
            return subscribedItemsCached_ = ret.ToArray();
        }


        static PublishedFileId[] subscribedItemsCached_;
        /// <summary>
        /// Get Items that are missing root dir
        /// </summary>
        public static ulong[] GetMissingDirItems() {
            subscribedItemsCached_ ??= GetSubscribedItems();
            var missing = ConfigWrapper.instance.CSCache.MissingDir;
            var subs = subscribedItemsCached_.Select(item => item.AsUInt64);
            return missing.Except(subs).ToArray();
        }

        public static string GetSubscribedItemPath(PublishedFileId id) => GetSubscribedItemPath(id.AsUInt64);
        public static string GetSubscribedItemPath(ulong id) {
            var ret = Path.Combine(DataLocation.WorkshopContentPath, id.ToString());
            if (Directory.Exists(ret))
                return ret;
            ret = Path.Combine(DataLocation.WorkshopContentPath, "_" + id.ToString());
            if (Directory.Exists(ret))
                return ret;
            return null;
        }

        public static DownloadStatus IsUGCUpToDate(SteamUtil.PublishedFileDTO det, out string reason) {
            Assertion.Neq(det.PublishedFileID, 0ul, "id");
            if (det.Result != SteamUtil.EResult.k_EResultOK) {
                reason = "could not get steam details. result:" + det.Result;
                if (det.Result == SteamUtil.EResult.k_EResultBanned ||
                   det.Result == SteamUtil.EResult.k_EResultItemDeleted) {
                    return DownloadStatus.Removed;
                } else {
                    return DownloadStatus.Unknown;
                }
            }

            string localPath = GetSubscribedItemPath(det.PublishedFileID);

            if (localPath == null) {
                reason = det.Class + " is not downloaded. path does not exits: " + localPath;
                return DownloadStatus.NotDownloaded;
            }

            var updatedServer = det.UpdatedUTC;
            var updatedLocal = GetLocalTimeUpdated(localPath).ToUniversalTime();
            var sizeServer = det.Size;
            var localSize = GetTotalSize(localPath);
            if (updatedLocal < updatedServer) {
                bool sure =
                    localSize < sizeServer ||
                    updatedLocal < updatedServer.AddHours(-24);
                string be = sure ? "is" : "may be";
                const ulong CR = 2881031511; // compatibility report
                if (det.PublishedFileID == CR) {
                    reason = $"Compatibility report Catalog {be} out of date.\n\t" +
                        $"server-time={STR(updatedServer)} |  local-time={STR(updatedLocal)}";
                    return DownloadStatus.CatalogOutOfDate;
                } else {
                    reason = $"{det.Class} {be} out of date.\n\t" +
                        $"server-time={STR(updatedServer)} |  local-time={STR(updatedLocal)}";
                    return DownloadStatus.OutOfDate;
                }
            }

            if (localSize < sizeServer) // could be smaller if user has its own files in there.
            {
                reason = $"{det.Class} download is incomplete. server-size={sizeServer}) local-size={localSize})";
                return DownloadStatus.PartiallyDownloaded;
            }

            reason = null;
            return DownloadStatus.OK;
        }

        public static DateTime GetLocalTimeUpdated(string path) {
            DateTime dateTime = DateTime.MinValue;
            if (Directory.Exists(path)) {
                foreach (string filePAth in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {
                    string ext = Path.GetExtension(filePAth);
                    if (Path.GetFileName(path) != EXCLUDED_FILE_NAME) {
                        DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePAth);
                        if (lastWriteTimeUtc > dateTime) {
                            dateTime = lastWriteTimeUtc;
                        }
                    }
                }
            }
            return dateTime;
        }

        public static ulong GetTotalSize(string path) {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            return (ulong)files.Sum(_f => new FileInfo(_f).Length);
        }

        static string STR(DateTime time) {
            var local = time.ToLocalTime().ToString();
            var utc = time.ToUniversalTime().ToShortTimeString();
            return $"{local} (UTC {utc})";
        }
    }
}
