namespace LoadOrderMod.UI {
    extern alias Injections;
    using KianCommons;
    using System;
    using static Injections.LoadOrderInjections.SteamUtilities.IsUGCUpToDateResult;
    using SteamUtilities = Injections.LoadOrderInjections.SteamUtilities;
    using UnityEngine;
    using UnityEngine.UI;
    using ColossalFramework;
    using ColossalFramework.UI;
    using KianCommons.UI;
    using static KianCommons.ReflectionHelpers;
    using System.Diagnostics;
    using HarmonyLib;

    public static class EntryStatusExtesions {
        public static void UpdateDownloadStatusSprite(this EntryData entryData) =>
            EntryStatusPanel.UpdateDownloadStatusSprite(entryData);
    }

    public class EntryStatusPanel : UIPanel{
        static readonly Vector2 POSITION = new Vector2(1600, 320);
        StatusButton StatusButton => GetComponentInChildren<StatusButton>();
        public override void Awake() {
            try {
                base.Awake();
                anchor = UIAnchorStyle.Top | UIAnchorStyle.Right;
                autoLayoutStart = LayoutStart.TopRight;
                autoLayout = true;
                autoLayoutPadding = new RectOffset(3, 3, 3, 3);
                autoLayoutDirection = LayoutDirection.Horizontal;
                relativePosition = POSITION - new Vector2(160,0);
                size = new Vector2(160, 80);
                var statusButton = AddUIComponent<StatusButton>();
                LogSucceeded();
            } catch(Exception ex) { ex.Log(); }
        }

        public static void UpdateDownloadStatusSprite(EntryData entryData) {
            try {
                var ugc = entryData.workshopDetails;
                var status = SteamUtilities.IsUGCUpToDate(ugc, out string reason);
                if (status != OK) {
                    string m = "$subscribed item not installed properly:" +
                        $"{ugc.publishedFileId} {ugc.title}\n" +
                        $"reason={reason}. " +
                        $"try reinstalling the item.";
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, m);

                }

                SetStatus(entryData, status, reason);
            } catch (Exception ex) { ex.Log(); }
        }

        public static void SetStatus(EntryData entryData, SteamUtilities.IsUGCUpToDateResult status, string reason) {
            Log.Called(status, "entryData.attachedEntry="+ entryData.attachedEntry.ToSTR());

            //Assertion.Assert(entryData.attachedEntry is not null, "x");
            if (!entryData.attachedEntry) {
                Log.Debug("entryData.attachedEntry == null", false);
                return;
            }

            if (status == SteamUtilities.IsUGCUpToDateResult.OK) {
                GetStatusPanel(entryData)?.StatusButton?.SetStatus(status, reason);
            } else {
                GetorCreateStatusPanel(entryData).StatusButton.SetStatus(status, reason);
            }
            Log.Succeeded();
        }

        public static EntryStatusPanel GetorCreateStatusPanel(EntryData entryData) {
            var packageEntry = entryData.attachedEntry;
            if (packageEntry is null) return null;
            return packageEntry.GetComponent<EntryStatusPanel>() ?? Create(packageEntry)
                ?? throw new Exception("failed to create panel");
        }

        public static EntryStatusPanel GetStatusPanel(EntryData entryData) {
            var packageEntry = entryData.attachedEntry;
            if (packageEntry is null) return null;
            return packageEntry.GetComponent<EntryStatusPanel>();
        }

        static EntryStatusPanel Create(PackageEntry packageEntry) {
            Assertion.Assert(packageEntry, "packageEntry");
            var topPanel = packageEntry.GetComponent<UIPanel>();
            Assertion.Assert(topPanel, "topPanel");
            var ret = topPanel.AddUIComponent<EntryStatusPanel>();
            ret.StatusButton.EntryData = m_EntryDataRef(packageEntry);
            return ret;
        }

        private static AccessTools.FieldRef<PackageEntry, EntryData> m_EntryDataRef =
            AccessTools.FieldRefAccess<PackageEntry, EntryData>("m_EntryData");

    }
}
