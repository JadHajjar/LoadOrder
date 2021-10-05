namespace LoadOrderMod.Settings.Tabs {
    extern alias Injections;
    using ColossalFramework.PlatformServices;
    using ColossalFramework.UI;
    using KianCommons;
    using LoadOrderShared;
    using LoadOrderMod.Util;
    using SteamUtilities = Injections.LoadOrderInjections.SteamUtilities;
    using KianCommons.UI;
    using System.IO;
    using UnityEngine;
    using LoadOrderMod.Settings;
    using System.Diagnostics;
    using System;

    static class SubscriptionsTab {
        static LoadOrderConfig Config => ConfigUtil.Config;

        public static void Make(ExtUITabstrip tabStrip) {
            UIHelper panelHelper = tabStrip.AddTabPage("Subscriptions");

            UIButton button;
            UICheckBox checkBox;
            //g.AddButton("Perform All", OnPerformAllClicked);

            button = panelHelper.AddButton("Refresh workshop items (checks for bad items)", RequestItemDetails) as UIButton;
            button.tooltip = "checks for missing/partially downloaded/outdated items";

            button = panelHelper.AddButton("unsubscribe from depricated workshop items [EXPERIMENTAL] ", () => CheckSubsUtil.Instance.UnsubDepricated()) as UIButton;
            button.tooltip = "if steam does not return item path, i assume its depricated.";

            button = panelHelper.AddButton("Resubribe to all broken downloads (exits game) [EXPERIMENTAL]", CheckSubsUtil.ResubcribeExternally) as UIButton;
            button.tooltip = "less steam can hide problems. if you use less steam please click 'Refresh workshop items' to get all broken downloads";
            button.isVisible = false; //hide for now.

            checkBox = panelHelper.AddCheckbox(
                "Delete unsubscibed items on startup",
                Config.DeleteUnsubscribedItemsOnLoad,
                val => {
                    ConfigUtil.Config.DeleteUnsubscribedItemsOnLoad = val;
                    ConfigUtil.SaveConfig();
                }) as UICheckBox;

            button = panelHelper.AddButton("Delete Now", () => CheckSubsUtil.Instance.DeleteUnsubbed()) as UIButton;
            Settings.Pairup(checkBox, button);

            {
                var g = panelHelper.AddGroup("Broken downloads") as UIHelper;

                UITextField tfSteamPath = g.AddTextfield(
                    text: "Steam Path: ",
                    defaultContent: ConfigUtil.Config.SteamPath ?? "",
                    eventChangedCallback: _ => { },
                    eventSubmittedCallback: delegate (string text) {
                        if (CheckSteamPath(text)) {
                            ConfigUtil.Config.SteamPath = text;
                            ConfigUtil.SaveConfig();
                        }
                    }) as UITextField;
                tfSteamPath.width = 650;
                g.AddButton("Redownload broken downloads [EXPERIMENTAL]", delegate () {
                    try {
                        var path = tfSteamPath.text;
                        if (CheckSteamPath(path)) {
                            CheckSubsUtil.ReDownload(path);
                            Prompt.Warning("Exit", "Please exit to desktop, wait for steam download to finish, and then start Cities skylines again.");
                        }
                    }catch(Exception ex) {
                        ex.Log();
                    }
                });
            }

            //b = g.AddButton("delete duplicates", OnPerformAllClicked) as UIButton;
            //b.tooltip = "when excluded mod is updated, and included duplicate of it is created";
        }

        static bool IsSteamPathValid(string path) {
            return Path.GetFileNameWithoutExtension(path).ToLower() != "steam" && File.Exists(path);
        }

        static bool CheckSteamPath(string path) {
            if (!File.Exists(path)) {
                Prompt.Error("File not found", $"'{path}' not found.");
                return false;
            } else if (Path.GetFileNameWithoutExtension(path).ToLower() != "steam") {
                Prompt.Error("wrong file", $"'{path}' is not steam client.");
                return false;
            }
            return true;
        }

        static void OnPerformAllClicked() {
            Log.Debug("Perform all pressed");
            CheckSubsUtil.EnsureAll();
            SteamUtilities.DeleteUnsubbed();
        }
        static void RequestItemDetails() => CheckSubsUtil.Instance.RequestItemDetails();
    }
}
