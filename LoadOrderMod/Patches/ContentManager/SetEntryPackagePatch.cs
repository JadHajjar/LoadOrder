namespace LoadOrderMod.Patches.ContentManager {
    using ColossalFramework.PlatformServices;
    using HarmonyLib;
    using KianCommons;
    using LoadOrderMod.Settings;
    using LoadOrderMod.Util;
    using System;
    using LoadOrderMod.UI;

    [HarmonyPatch(typeof(PackageEntry))]
    static class SetEntryPackagePatch {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(PackageEntry.SetEntry))]
        static void SetEntry_Postfix(PackageEntry __instance, EntryData data) {
            Log.Called($"entry: {data.publishedFileId} {data.entryName}");
            EntryStatusPanel.UpdateDownloadStatusSprite(__instance);
        }
    }

}
