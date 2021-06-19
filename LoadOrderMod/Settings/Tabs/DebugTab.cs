namespace LoadOrderMod.Settings.Tabs {
    extern alias Injections;
    using ColossalFramework.PlatformServices;
    using ColossalFramework.UI;
    using KianCommons;
    using LoadOrderShared;
    using System;
    using UnityEngine;
    using SteamUtilities = Injections.LoadOrderInjections.SteamUtilities;
    using LoadOrderMod.UI;
    static class DebugTab {
        public static void Make(ExtUITabstrip tabStrip) {
            UIHelper panelHelper = tabStrip.AddTabPage("Developer");
            panelHelper.AddCheckbox(
                "Soft assembly dependancy",
                ConfigUtil.Config.SoftDLLDependancy,
                val => {
                    ConfigUtil.Config.SoftDLLDependancy = val;
                    ConfigUtil.SaveConfig();
                });
            panelHelper.AddButton("Ensure All", CheckSubsUtil.EnsureAll);
            //g.AddButton("RequestItemDetails", OnRequestItemDetailsClicked);
            //g.AddButton("QueryItems", OnQueryItemsClicked);
            panelHelper.AddButton("RunCallbacks", OnRunCallbacksClicked);

            var bufferedToggle = panelHelper.AddCheckbox("Bufferred Log", Log.Buffered, (val) => Log.Buffered = val) as UICheckBox;
            bufferedToggle.eventVisibilityChanged += new PropertyChangedEventHandler<bool>( (_,___) => bufferedToggle.isChecked = Log.Buffered);
        }

        static void OnRunCallbacksClicked() {
            Log.Debug("RunCallbacks pressed");
            try {
                PlatformService.RunCallbacks();
            } catch (Exception ex) {
                Log.Exception(ex);
            }
        }
        static void OnQueryItemsClicked() {
            Log.Debug("QueryItems pressed");
            PlatformService.workshop.QueryItems().LogRet($"QueryItems()"); ;
        }

        static void OnUGCQueryCompleted(UGCDetails result, bool ioError) {
            Log.Debug($"OnUGCQueryCompleted(result:{result.result} {result.publishedFileId}, ioError:{ioError})");
        }
        static void OnUGCRequestUGCDetailsCompleted(UGCDetails result, bool ioError) {
            Log.Debug($"OnUGCRequestUGCDetailsCompleted(result:{result.result} {result.publishedFileId}, ioError:{ioError})");
        }
    }
}
