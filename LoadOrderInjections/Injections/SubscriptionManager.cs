using ColossalFramework.PlatformServices;
using KianCommons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static KianCommons.ReflectionHelpers;
using ColossalFramework;
using LoadOrderShared;
using ColossalFramework.Threading;
using System.Threading;
using ColossalFramework.IO;
using System.Diagnostics;

namespace LoadOrderInjections {
    public enum DownloadStatus {
        DownloadOK,
        OutOfDate,
        CatalogOutOfDate,
        NotDownloaded,
        PartiallyDownloaded,
        Gone,
    }

    public class DoNothingComponent : MonoBehaviour {
        void Awake() => UnityEngine.Debug.Log("TestComponent.Awake() was called");
        void Start() => UnityEngine.Debug.Log("TestComponent.Start() was called");
    }


    public class TestComponent : MonoBehaviour {
        void Awake() => Log.Debug("TestComponent.Awake() was called");
        void Start() => Log.Debug("TestComponent.Start() was called");

        void OnGUI() {
            //if (GUILayout.Button("Check Installed"))
            //    SteamUtilities.EnsureAll();
            if (GUILayout.Button("Delete UnInstalled"))
                SteamUtilities.DeleteUnsubbed();
        }
    }

#if !NO_CO_STEAM_API
    public class MassSubscribe : MonoBehaviour
    {
        public enum StateT
        {
            None = 0,
            SubSent = 1,
            Subbed = 2,
            Failed = 3,
        }

        public class ItemT
        {
            public ItemT(ulong id)
            {
                PublishedFileId = new PublishedFileId(id);
                State = StateT.None;
                LastEventTime = default;
                RequestCount = 0;
            }

            public PublishedFileId PublishedFileId;
            public StateT State;
            public DateTime LastEventTime;
            public int RequestCount;

            public void Subscribe()
            {
                RequestCount++;
                LastEventTime = DateTime.Now;
                if (PublishedFileId == default || PublishedFileId == PublishedFileId.invalid)
                {
                    State = StateT.Failed;
                }
                PlatformService.workshop.Subscribe(PublishedFileId);
                State = StateT.SubSent;
            }
        }

        DateTime LastEventTime = default;

        public List<ItemT> Items = new List<ItemT>();
        public IEnumerable<ItemT> RemainingItems => Items.Where(item => item.State <= StateT.SubSent);

        void Awake() => LogCalled();
        
        void Start()
        {
            try
            {
                LogCalled();
                SteamUtilities.GetMassSub(out string filePath);
                List<ulong> ids;
                if(filePath.IsNullorEmpty()) {
                    string path = Path.Combine(DataLocation.localApplicationData, "LoadOrder");
                    ids = UGCListTransfer.GetList(out _);
                } else {
                    ids = UGCListTransfer.GetListFromFile(filePath, out _);
                }

                var subscriedItems = PlatformService.workshop.GetSubscribedItems();
                foreach (var id in ids)
                {
                    if(!subscriedItems.Any(item => item.AsUInt64 == id))
                        Items.Add(new ItemT(id));
                }
                RemainingCount = Items.Count();
                StartSubToAll();
                StartUpdateUI();
            }
            catch(Exception ex) { ex.Log(); }
        }

        public Coroutine StartSubToAll() => StartCoroutine(SubToAllCoroutine());
        private IEnumerator SubToAllCoroutine()
        {
            LogCalled();
            while (!SteamUtilities.SteamInitialized)
                yield return new WaitForSeconds(0.1f);

            PlatformService.workshop.eventWorkshopSubscriptionChanged += Workshop_eventWorkshopSubscriptionChanged;

            Log.Info("entering loop");
            for(; ; )
            {
                int counter = 0;
                foreach (var item in Items)
                {
                    yield return new WaitForSeconds(0.05f);
                    item.Subscribe();
                    counter++;
                    if (counter % 100 == 0)
                        yield return 0;
                }

                RemainingCount = RemainingItems.Count();
                if (RemainingCount == 0) break;
                yield return new WaitForSeconds(1);
                yield return new WaitForSeconds(0.01f * RemainingCount);
            }
            Log.Info("all items subscribed!");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Workshop_eventWorkshopSubscriptionChanged(PublishedFileId fileID, bool subscribed)
        {
            try
            {
                LastEventTime = DateTime.Now;
                var item = Items.Find(item => item.PublishedFileId == fileID);
                if (item == null)
                    return;
                else if (subscribed)
                    item.State = StateT.Subbed;
                else
                    item.Subscribe();
            }
            catch (Exception ex) { ex.Log(); }
        }

        public int RemainingCount;
        public Coroutine StartUpdateUI() => StartCoroutine(UpdateUICoroutine());
        private IEnumerator UpdateUICoroutine()
        {
            while(true) {
                RemainingCount = RemainingItems.Count();
                if(RemainingCount == 0) {
                    Log.Info("all items subscribed!");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        void OnGUI()
        {
            GUILayout.Label($"{Items.Count - RemainingCount}/{Items.Count} of assets are subscribed");
            if (GUILayout.Button("Terminate Now"))
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }

    public class MassUnSubscribe : MonoBehaviour {
        public enum StateT {
            None = 0,
            UnSubSent = 1,
            UnSubbed = 2,
            Failed = 3,
        }

        public class ItemT {
            public ItemT(ulong id) {
                PublishedFileId = new PublishedFileId(id);
                State = StateT.None;
                LastEventTime = default;
                RequestCount = 0;
            }

            public PublishedFileId PublishedFileId;
            public StateT State;
            public DateTime LastEventTime;
            public int RequestCount;

            public void UnSubscribe() {
                RequestCount++;
                LastEventTime = DateTime.Now;
                if(PublishedFileId == default || PublishedFileId == PublishedFileId.invalid) {
                    State = StateT.Failed;
                }
                PlatformService.workshop.Unsubscribe(PublishedFileId);
                State = StateT.UnSubSent;
                Log.Called("unsubscribe request sent for: " + PublishedFileId);
            }
        }

        DateTime LastEventTime = default;

        public List<ItemT> Items = new List<ItemT>();
        public IEnumerable<ItemT> RemainingItems => Items.Where(item => item.State <= StateT.UnSubSent);

        void Awake() => LogCalled();

        void Start() {
            try {
                LogCalled();
                SteamUtilities.GetMassUnSub(out string filePath);
                List<ulong> ids;
                bool missing;
                if(filePath.IsNullorEmpty()) {
                    string path = Path.Combine(DataLocation.localApplicationData, "LoadOrder");
                    ids = UGCListTransfer.GetList(out missing);
                    if(missing) {
                        ids.AddRange(SteamUtilities.GetMissingItems().Select(item => item.AsUInt64));
                        UGCListTransfer.SendList(ids, false); // replace missing with actual ids.
                    }
                } else {
                    ids = UGCListTransfer.GetListFromFile(filePath, out missing);
                    if(missing) {
                        ids.AddRange(SteamUtilities.GetMissingItems().Select(item => item.AsUInt64));
                        UGCListTransfer.SendList(ids, false); // replace missing with actual ids.
                    }
                }

                var subscriedItems = PlatformService.workshop.GetSubscribedItems();
                foreach(var id in ids) {
                    if(subscriedItems.Any(item => item.AsUInt64 == id))
                        Items.Add(new ItemT(id));
                }
                RemainingCount = Items.Count();
                StartUnSubToAll();
                StartUpdateUI();
            } catch(Exception ex) { ex.Log(); }
        }

        public Coroutine StartUnSubToAll() => StartCoroutine(UnSubToAllCoroutine());
        private IEnumerator UnSubToAllCoroutine() {
            LogCalled();
            while(!SteamUtilities.SteamInitialized)
                yield return new WaitForSeconds(0.1f);

            PlatformService.workshop.eventWorkshopSubscriptionChanged += Workshop_eventWorkshopSubscriptionChanged;
            Log.Info("entering loop");
            for(; ; )
            {
                int counter = 0;
                foreach(var item in Items) {
                    item.UnSubscribe();
                    counter++;
                    if(counter % 100 == 0)
                        yield return 0;
                }

                RemainingCount = RemainingItems.Count();
                if(RemainingCount == 0) break;
                yield return new WaitForSeconds(1);
                yield return new WaitForSeconds(0.01f * RemainingCount);
            }
            Log.Info("all items unsubscribed!");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Workshop_eventWorkshopSubscriptionChanged(PublishedFileId fileID, bool subscribed) {
            try {
                Log.Called(fileID, subscribed);
                LastEventTime = DateTime.Now;
                var item = Items.Find(item => item.PublishedFileId == fileID);
                if(item == null)
                    return;
                else if(!subscribed)
                    item.State = StateT.UnSubbed;
                else
                    item.UnSubscribe();
            } catch(Exception ex) { ex.Log(); }
        }

        public int RemainingCount;
        public Coroutine StartUpdateUI() => StartCoroutine(UpdateUICoroutine());
        private IEnumerator UpdateUICoroutine() {
            while(true) {
                RemainingCount = RemainingItems.Count();
                if(RemainingCount == 0) {
                    Log.Info("all items unsubscribed!");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }


        void OnGUI() {
            GUILayout.Label($"{Items.Count - RemainingCount}/{Items.Count} of assets are unsubscribed");
            if(GUILayout.Button("Terminate Now"))
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
#endif


    public class Example : MonoBehaviour {
        private enum UpDown { Down = -1, Start = 0, Up = 1 };
        private UpDown textChanged = UpDown.Start;
        private Text TextComponent;

        // Load the Arial font from the Unity Resources folder.
        static Font Arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        void Awake() {
            Log.Debug("Example.Awake() was called");

            var canvas = AddCanvas();
            TextComponent = AddText(canvas.gameObject, "press any key");
            //var btn1 = AddButton(
            //    canvas.gameObject, 
            //    "RequestItemDetails", 
            //    Vector2.zero,
            //    SteamUtilities.OnRequestItemDetailsClicked);
            //var btn2 = AddButton(
            //    canvas.gameObject, 
            //    "QueryItems", 
            //    new Vector2(0, 250),
            //    SteamUtilities.OnQueryItemsClicked);
        }

        static Canvas AddCanvas() {
            // Create Canvas GameObject.
            GameObject canvasGO = new GameObject();
            canvasGO.name = "Canvas";
            canvasGO.AddComponent<Canvas>();
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            // Get canvas from the GameObject.
            Canvas canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            return canvas;
        }

        static Text AddText(GameObject parent, string label) {
            // Create the Text GameObject.
            GameObject textGO = new GameObject();
            textGO.transform.parent = parent.transform;
            textGO.AddComponent<Text>();

            // Set Text component properties.
            Text text = textGO.GetComponent<Text>();
            text.font = Arial;
            text.text = label;
            text.fontSize = 48;
            text.alignment = TextAnchor.MiddleCenter;

            // Provide Text position and size using RectTransform.
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.sizeDelta = new Vector2(600, 200);

            return text;
        }

        //static Button AddButton(GameObject canvasGO, string label, Vector2 position, UnityAction action)
        //{
        //    // Create the Text GameObject.
        //    GameObject buttonGO = new GameObject();
        //    buttonGO.transform.parent = canvasGO.transform;
        //    var button = buttonGO.AddComponent<Button>();

        //    var image = buttonGO.AddComponent<Image>();
        //    image.sprite = new Sprite()

        //    var text = AddText(buttonGO, label);

        //    RectTransform rectTransform = button.GetComponent<RectTransform>();
        //    rectTransform.sizeDelta = new Vector2(600, 200);
        //    rectTransform.localPosition = rectTransform.sizeDelta * 0.5f + new Vector2(50, 50) + position;
        //    button.onClick.AddListener(action);
        //    return button;
        //}

        void Update() {
            // Press the space key to change the Text message.
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (textChanged != UpDown.Down) {
                    TextComponent.text = "Text changed";
                    textChanged = UpDown.Down;
                } else {
                    TextComponent.text = "Text changed back";
                    textChanged = UpDown.Up;
                }
            }
        }
    }

    public static class SubscriptionManager {
        /// <returns>true, to avoid loading intro</returns>
        public static bool PostBootAction() {
            Log.Called();
            if (SteamUtilities.sman) {
                new GameObject().AddComponent<Camera>();
#if !NO_CO_STEAM_API
                new GameObject("base").AddComponent<Example>();
                new GameObject("test go").AddComponent<TestComponent>();
#endif
                return true;
            } else if (SteamUtilities.GetMassSub(out _)) {
                new GameObject().AddComponent<Camera>();
                //new GameObject("base").AddComponent<Example>();
#if !NO_CO_STEAM_API
                new GameObject("mass subscribe go").AddComponent<MassSubscribe>();
#endif
                return true;
            } else if(SteamUtilities.GetMassUnSub(out _)) {
                new GameObject().AddComponent<Camera>();
                //new GameObject("base").AddComponent<Example>();
#if !NO_CO_STEAM_API
                new GameObject("mass unsubscribe go").AddComponent<MassUnSubscribe>();
#endif
                return true;
            } else {
                return false.LogRet(ThisMethod);
            }
        }

        public static void DoNothing() {
            new GameObject().AddComponent<Camera>();
            new GameObject("nop go").AddComponent<DoNothingComponent>();
        }
    }

    public static class CMPatchHelpers {
        public static bool IsDirectoryExcluded(string path) {
            return
                path.IsNullOrWhiteSpace() ||
                path[0] == '_' ||
                Directory.Exists("_" + path) ||
                File.Exists(Path.Combine(path, SteamUtilities.EXCLUDED_FILE_NAME));
        }

        public static bool IsIDExcluded(PublishedFileId id) {
            string path = PlatformService.workshop.GetSubscribedItemPath(id);
            return IsDirectoryExcluded(path);
        }

        public static string CheckFiles(string path) {
            EnsureIncludedOrExcludedFiles(path);
            return path;
        }

        public static void EnsureIncludedOrExcludedFiles(string path) {
            try {
                foreach (string file in Directory.GetFiles(path, "*", searchOption: SearchOption.AllDirectories))
                    EnsureFile(file);
            } catch (Exception ex) {
                Log.Exception(ex);
            }
        }

        public static void EnsureFile(string fullFilePath) {
            if (string.IsNullOrEmpty(fullFilePath)) return;
            string included = SteamUtilities.ToIncludedPath(fullFilePath);
            string excluded = SteamUtilities.ToExcludedPath2(fullFilePath);
            if (File.Exists(included) && File.Exists(excluded)) {
                File.Delete(excluded);
                File.Move(included, excluded);
                fullFilePath = excluded;
            }
        }
    }

    public static class SteamUtilities {
        public static bool Initialized { get; private set; } = false;
        public static bool sman = Environment.GetCommandLineArgs().Any(_arg => _arg == "-sman");
        public static bool GetMassSub(out string filePath) => ParseCommandLine("subscribe", out filePath);
        public static bool GetMassUnSub(out string filePath) => ParseCommandLine("unsubscribe", out filePath);


        static LoadOrderShared.LoadOrderConfig Config =>
            LoadOrderInjections.Util.LoadOrderUtil.Config;
        // steam manager is already initialized at this point.

        /// <summary>
        /// if no match was found, value=null and returns false.
        /// if a match is found, value="" or string after --prototype= and returns true.
        /// </summary>
        public static bool ParseCommandLine(string prototypes, out string value) {
            foreach(string arg in Environment.GetCommandLineArgs()) {
                foreach(string prototype in prototypes.Split("|")) {
                    if(MatchCommandLineArg(arg, prototype, out string val)) {
                        value = val;
                        return true;
                    }
                }
            }
            value = null;
            return false;
        }

        /// <summary>
        /// matches one arg with one prototype
        /// </summary>
        public static bool MatchCommandLineArg(string arg, string prototype, out string value) {
            if(arg == "-" + prototype) {
                value = "";
                return true;
            } else if(arg.StartsWith($"--{prototype}=")) {
                int i = prototype.Length + 3;
                if(arg.Length > i)
                    value = arg.Substring(i);
                else
                    value = "";
                return true;
            } else {
                value = null;
                return false;
            }
        }

        public static void RegisterEvents() {
            if (Initialized) return;
            Initialized = true; // used to check if patch loader is effective.
            Log.Debug(Environment.StackTrace);
            PlatformService.eventSteamControllerInit += OnInitSteamController;

            PlatformService.eventGameOverlayActivated += OnGameOverlayActivated;
            PlatformService.workshop.eventSubmitItemUpdate += OnSubmitItemUpdate;
            PlatformService.workshop.eventWorkshopItemInstalled += OnWorkshopItemInstalled;
            PlatformService.workshop.eventWorkshopSubscriptionChanged -= OnWorkshopSubscriptionChanged;
            PlatformService.workshop.eventWorkshopSubscriptionChanged += OnWorkshopSubscriptionChanged;
            PlatformService.workshop.eventUGCQueryCompleted += OnUGCQueryCompleted;
            PlatformService.workshop.eventUGCRequestUGCDetailsCompleted += OnUGCRequestUGCDetailsCompleted;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args) {
            Exception ex = (Exception)args.ExceptionObject;
            ex.Log(showInPannel:false);
        }

        public static bool firstTime = true;
        public static bool SteamInitialized { get; private set; } = false;
        public static void OnInitSteamController() {
            if (!firstTime)
                return;
            firstTime = false;

            Log.Debug(Environment.StackTrace);
            var items = PlatformService.workshop.GetSubscribedItems();
            Log.Info("Subscribed Items are: " + items.ToSTR());

            EnsureIncludedOrExcludedAllFast();
            if (Config.DeleteUnsubscribedItemsOnLoad)
                DeleteUnsubbed();
            SteamInitialized = true;
        }

        public static bool IsCloudEnabled() {
            var ret = PlatformService.cloud?.enabled ?? false;
            Log.Info("Cloud.enabled=" + ret);
            if (!ret) {
                Log.Info("Skipping cloud packages.");
            }
            return ret;
        }

        /// <summary>
        /// returns a list of items that are subscribed but not downloaded (excluding deleted items).
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PublishedFileId> GetMissingItems() {
            foreach(var id in PlatformService.workshop.GetSubscribedItems()) {
                var path = PlatformService.workshop.GetSubscribedItemPath(id);
                if(!path.IsNullOrWhiteSpace() && !Directory.Exists(path) && !Directory.Exists(ToExcludedPath2(path)))
                    yield return id;
            }
        }

        static DirectoryInfo GetWSPath() {
            foreach(var id in PlatformService.workshop.GetSubscribedItems()) {
                var path = PlatformService.workshop.GetSubscribedItemPath(id);
                if (!path.IsNullOrWhiteSpace()) {
                    return new DirectoryInfo(path).Parent;
                }
            }
            return default;
        }

        //public static void OnRequestItemDetailsClicked() {
        //    Log.Debug("RequestItemDetails pressed");
        //    foreach (var id in PlatformService.workshop.GetSubscribedItems())
        //        PlatformService.workshop.RequestItemDetails(id).LogRet($"RequestItemDetails({id.AsUInt64})");
        //    //var id = new PublishedFileId(2040656402ul);
        //    //PlatformService.workshop.RequestItemDetails(id).LogRet($"RequestItemDetails({id.AsUInt64})");
        //}
        //public static void OnQueryItemsClicked() {
        //    Log.Debug("QueryItems pressed");
        //    PlatformService.workshop.QueryItems().LogRet($"QueryItems()"); ;
        //}

        private static void OnSubmitItemUpdate(SubmitItemUpdateResult result, bool ioError) {
            Log.Debug($"PlatformService.workshop.eventSubmitItemUpdate(result:{result.result}, {ioError})");
        }


        private static void OnGameOverlayActivated(bool active) {
            Log.Debug($"PlatformService.workshop.eventGameOverlayActivated({active})");
        }

        private static void OnWorkshopItemInstalled(PublishedFileId id) {
            Log.Debug($"PlatformService.workshop.eventWorkshopItemInstalled({id.AsUInt64})");
        }

        private static void OnWorkshopSubscriptionChanged(PublishedFileId id, bool subscribed) {
            Log.Debug($"PlatformService.workshop.eventWorkshopSubscriptionChanged({id.AsUInt64}, {subscribed})");
        }

        private static void OnUGCQueryCompleted(UGCDetails result, bool ioError) {
            // called after QueryItems
            Log.Debug($"OnUGCQueryCompleted(" +
                $"result:{result.ToSTR()}, " +
                $"ioError:{ioError})");
        }

        public static string Class(ref this UGCDetails ugc) {
            var tags = ugc.tags.Split(",").Select(tag => tag.Trim().ToLower()).ToArray();
            if (tags.Contains("mod"))
                return "Mod";
            else if (tags.Length > 0)
                return "Asset";
            else
                return "subscribed item";
        }


        public static void OnUGCRequestUGCDetailsCompleted(UGCDetails result, bool ioError) {
            ThreadPool.QueueUserWorkItem((_) => {
               bool good = IsUGCUpToDate(result, out string reason) == DownloadStatus.DownloadOK;
                if (!good && !IsExcludedMod(result.publishedFileId)) {
                    Log.Warning($"{result.Class()} not installed properly:{result.publishedFileId} {result.title} " +
                        $"reason={reason}. " +
                        $"try reinstalling the item.",
                        true);
                } else {
                    Log.Debug($"{result.Class()} is good:{result.publishedFileId} {result.title}");
                }
            });
        }

        public static string ToSTR(this ref UGCDetails result)
            => $"UGCDetails({result.result} {result.publishedFileId.AsUInt64})";

        public static string ToSTR2(this ref UGCDetails result) {
#pragma warning disable
            string m =
                $"UGCDetails:\n" +
                $"    publishedFileId:{result.publishedFileId}\n" +
                $"    title:{result.title}\n" +
                $"    creator: {result.creatorID} : {result.creatorID.ToAuthorName()}\n" +
                $"    result:{result.result}\n" +
                $"    timeUpdated:{result.timeUpdated} : {result.timeUpdated.ToLocalTime()}\n" +
                $"    timeCreated:{result.timeCreated} : {result.timeCreated.ToLocalTime()}\n" +
                $"    fileSize:{result.fileSize}\n" +
                $"    tags:{result.tags}\n";

#pragma warning enable
            return m;
        }

        //code copied from package entry
        public static DateTime GetLocalTimeCreated(string modPath) {
            DateTime dateTime = DateTime.MinValue;
            foreach(string path in Directory.GetFiles(modPath, "*", searchOption: SearchOption.AllDirectories)) {
                DateTime creationTimeUtc = File.GetCreationTimeUtc(path);
                if(creationTimeUtc > dateTime) {
                    dateTime = creationTimeUtc;
                }
            }
            return dateTime;
        }

        //code copied from package entry
        public static DateTime GetLocalTimeUpdated(string modPath) {
            DateTime dateTime = DateTime.MinValue;
            if(Directory.Exists(modPath)) {
                foreach(string path in Directory.GetFiles(modPath, "*", searchOption: SearchOption.AllDirectories)) {
                    DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(path);
                    if(lastWriteTimeUtc > dateTime) {
                        dateTime = lastWriteTimeUtc;
                    }
                }
            }
            return dateTime;
        }

        public static DateTime kEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToUTCTime(this uint time) => kEpoch.AddSeconds(time);
        public static DateTime ToLocalTime(this uint time) => kEpoch.AddSeconds(time).ToLocalTime();

        public static string ToAuthorName(this UserID userID) => new Friend(userID).personaName;

        static string STR(DateTime time) {
            var local = time.ToLocalTime().ToString();
            var utc = time.ToUniversalTime().ToShortTimeString();
            return $"{local} (UTC {utc})";
        }

        public static DownloadStatus IsUGCUpToDate(UGCDetails det, out string reason) {
            Assertion.Assert(det.publishedFileId != PublishedFileId.invalid,"invalid id");
            Assertion.Assert(det.publishedFileId.AsUInt64 != 0, "id 0");
            if (det.title.IsNullOrWhiteSpace()) {
                reason = "could not get steam details (removed from workshop?)";
                return DownloadStatus.Gone;
            }
            string path = PlatformService.workshop.GetSubscribedItemPath(det.publishedFileId);
            if (path.IsNullOrWhiteSpace()) {
                reason = "could not get item path (removed from workshop?)";
                return DownloadStatus.Gone;
            }

            string localPath = GetFinalPath(path);
            if (localPath == null) {
                reason = $"{det.Class()} is not downloaded. path does not exits: " +
                    PlatformService.workshop.GetSubscribedItemPath(det.publishedFileId);
                return DownloadStatus.NotDownloaded;
            }

            var updatedServer = det.timeUpdated.ToUTCTime();
            var updatedLocal = GetLocalTimeUpdated(localPath).ToUniversalTime();
            var sizeServer = det.fileSize;
            var localSize = GetTotalSize(localPath);

            if(localSize == 0) {
                reason = $"{det.Class()} is not downloaded (empty folder '{localPath}'): " +
                    PlatformService.workshop.GetSubscribedItemPath(det.publishedFileId);
                return DownloadStatus.NotDownloaded;
            }

            if (updatedLocal == DateTime.MinValue) {
                reason = $"Error getting local time at {localPath}";
                return DownloadStatus.NotDownloaded;
            } else if (updatedLocal < updatedServer) {
                bool sure =
                    localSize < sizeServer ||
                    updatedLocal < updatedServer.AddHours(-24);
                string be = sure ? "is" : "may be";

                PublishedFileId CR = new(2881031511);
                if (det.publishedFileId == CR) {
                    reason = $"Compatibility report Catalog {be} out of date.\n\t" +
                    $"server-time={STR(updatedServer)} |  local-time={STR(updatedLocal)}";
                    return DownloadStatus.CatalogOutOfDate;
                } else {
                    reason = $"{det.Class()} {be} out of date.\n\t" +
                        $"server-time={STR(updatedServer)} |  local-time={STR(updatedLocal)}";
                    return DownloadStatus.OutOfDate;
                }
            }

            if (localSize < sizeServer) // could be bigger if user has its own files in there.
            {
                reason = $"{det.Class()} download is incomplete. server-size={sizeServer}) local-size={localSize})";
                return DownloadStatus.PartiallyDownloaded;
            }

            reason = null;
            return DownloadStatus.DownloadOK;
        }

        public static long GetTotalSize(string path) {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            return files.Sum(_f => new FileInfo(_f).Length);
        }


        #region included excluded
        public static string GetFinalPath(string includedPath) {
            if (includedPath.IsNullorEmpty()) return null;
            if (Directory.Exists(includedPath))
                return includedPath;

            string excludedPath = ToExcludedPath(includedPath);
            if (Directory.Exists(excludedPath))
                return excludedPath;

            return null;
        }

        /// <summary>
        /// puts _ behind directory 
        /// prints error if it already has _ or if path is empty.
        /// </summary>
        public static string ToExcludedPath(string includedPath) {
            string p1 = Path.GetDirectoryName(includedPath);
            string p2 = Path.GetFileName(includedPath);
            if (string.IsNullOrEmpty(p1) || string.IsNullOrEmpty(p2)) {
                Log.Error(ThisMethod +
                    $"includedPath={includedPath}\n" +
                    $"p1={p1}\n" +
                    $"p2={p2}\n");
            }
            if (p2.StartsWith("_")) {
                Log.Error($"includedPath={includedPath} should not start with _");
                return includedPath;
            }
            p2 = "_" + p2;
            return Path.Combine(p1, p2);
        }

        /// <summary>
        /// puts _ behind directory if it does not have _ already
        /// </summary>
        public static string ToExcludedPath2(string fullPath) {
            Assertion.Assert(!fullPath.IsNullorEmpty(), "fullPath=" + fullPath);
            string parent = Path.GetDirectoryName(fullPath);
            string file = Path.GetFileName(fullPath);
            if (!file.StartsWith("_"))
                file = "_" + file;
            return Path.Combine(parent, file);
        }

        public static bool IsPathIncluded(string fullPath) {
            Assertion.Assert(!fullPath.IsNullorEmpty(), "fullPath=" + fullPath);
            return Path.GetFileName(fullPath).StartsWith("_");
        }
        public static string ToIncludedPath(string fullPath) {
            Assertion.Assert(!fullPath.IsNullorEmpty(), "fullPath=" + fullPath);
            string parent = Path.GetDirectoryName(fullPath);
            string file = Path.GetFileName(fullPath);
            if (file.StartsWith("_"))
                file = file.Substring(1); //drop _
            return Path.Combine(parent, file);
        }


        public static bool IsExcludedMod(PublishedFileId publishedFileId) {
            try {
                var path = PlatformService.workshop.GetSubscribedItemPath(publishedFileId);
                return CMPatchHelpers.IsDirectoryExcluded(path);
            } catch (Exception ex) {
                ex.Log("publishedFileId=" + publishedFileId);
                throw;
            }
        }

        public static void EnsureIncludedOrExcluded(PublishedFileId id) {
            try {
                string path1 = PlatformService.workshop.GetSubscribedItemPath(id);
                if (path1.IsNullorEmpty()) {
                    Log.Warning($"item {id} does not have path");
                    return;
                }
                
                string path2 = ToExcludedPath(path1);
                if (Directory.Exists(path1) && Directory.Exists(path2)) {
                    Directory.Delete(path2, true);
                    Directory.Move(path1, path2);
                }
            } catch (Exception ex) {
                Log.Exception(ex, $"EnsureIncludedOrExcluded({id})", showInPanel: false);
            }
        }


        //public static void EnsureAll() {
        //    Log.Called();
        //    var items = PlatformService.workshop.GetSubscribedItems();
        //    foreach (var id in items) {
        //        EnsureIncludedOrExcluded(id);
        //        PlatformService.workshop.RequestItemDetails(id)
        //            .LogRet($"RequestItemDetails({id.AsUInt64})");
        //    }
        //}

        public static void EnsureIncludedOrExcludedAllFast() {
            var dir = GetWSPath();
            if (dir != default)
                EnsureIncludedOrExcludedAllFast(dir);
        }

        /// <summary>
        /// I think GetFiles/GetDirectories is cached and therefore the normal EnsureIncludedOrExcluded should work fast
        /// but just in case it doesn't for some OS then I should use this method
        /// </summary>
        public static void EnsureIncludedOrExcludedAllFast(DirectoryInfo RootDir) {
            try {
                Log.Called();
                foreach (var path in Directory.GetDirectories(RootDir.FullName)) {
                    var dirName = Path.GetFileName(path);
                    if (dirName.StartsWith("_")) {
                        Exclude(dirName);
                    }
                }
            } catch (Exception ex) {
                Log.Exception(ex, $"EnsureIncludedOrExcluded({RootDir})", showInPanel: false);
            }
            Log.Succeeded();
        }

        public static bool Exclude(string path) {
            try {
                string excludedPath = ToExcludedPath2(path);
                string includedPath = ToIncludedPath(path);
                if (Directory.Exists(excludedPath)) {
                    if (Directory.Exists(includedPath)) {
                        Directory.Delete(excludedPath);
                    } else {
                        Directory.Move(excludedPath, includedPath);
                    }
                } else if (!Directory.Exists(includedPath)) {
                    return false;
                }
                Touch(Path.Combine(includedPath, EXCLUDED_FILE_NAME));
                return true;
            } catch (Exception ex) { ex.Log(); }
            return false;
        }


        public const string EXCLUDED_FILE_NAME = ".excluded";
        public static void Touch(string path) {
            if (!File.Exists(path)) {
                File.CreateText(path).Dispose();
            }
        }

        #endregion

        public static void DeleteUnsubbed() {
            Log.Info("DeleteUnsubbed called ...");
            var items = PlatformService.workshop.GetSubscribedItems();
            if (items == null || items.Length == 0)
                return;

            var path = PlatformService.workshop.GetSubscribedItemPath(items[0]);
            path = Path.GetDirectoryName(path);

            foreach (var dir in Directory.GetDirectories(path)) {
                ulong id;
                string strID = Path.GetFileName(dir);
                if (strID.StartsWith("_"))
                    strID = strID.Substring(1);
                if (!ulong.TryParse(strID, out id))
                    continue;
                bool deleted = !items.Any(item => item.AsUInt64 == id);
                if (deleted) {
                    Log.Warning("unsubbed mod will be deleted: " + dir);
                    Directory.Delete(dir, true);
                }
            }
            Log.Succeeded();
        }

        public static DirectoryInfo FindWSDir() {
            foreach (var id in PlatformService.workshop.GetSubscribedItems()) {
                var path = PlatformService.workshop.GetSubscribedItemPath(id);
                if (path != null) {
                    return new DirectoryInfo(path).Parent;
                }
            }
            return null;
        }
    }
}
