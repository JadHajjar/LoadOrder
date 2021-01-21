namespace LoadOrderMod.Patches.Startup {
    using HarmonyLib;
    using static KianCommons.ReflectionHelpers;
    using UnityEngine;

    [HarmonyPatch(typeof(NewsFeedPanel), "Awake")]
    static class NewsFeedPanel_Awake {
        static bool Prefix(NewsFeedPanel __instance) {
            LogCalled();
            GameObject.DestroyImmediate(__instance.gameObject);
            return false;
        }
    }

}
