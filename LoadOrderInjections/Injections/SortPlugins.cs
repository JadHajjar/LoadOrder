using ColossalFramework.PlatformServices;
using KianCommons;
using LoadOrderInjections.Util;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PluginInfo = ColossalFramework.Plugins.PluginManager.PluginInfo;

namespace LoadOrderInjections.Injections {
    public static class SortPlugins {
        public static int Comparison(PluginInfo p1, PluginInfo p2) {
            var savedOrder1 = p1.SavedLoadOrder();
            var savedOrder2 = p2.SavedLoadOrder();

            // orderless harmony comes first
            if (!savedOrder1.exists && p1.IsHarmonyMod())
                return -1;
            if (!savedOrder2.exists && p2.IsHarmonyMod())
                return +1;

            // if neither have order, use string comparison
            // then builin first, workshop second, local last
            // otherwise use string comparison
            if (!savedOrder1.exists && !savedOrder2.exists) {
                int order(PluginInfo _p) =>
                    _p.isBuiltin ? 0 :
                    (_p.publishedFileID != PublishedFileId.invalid ? 1 : 2);
                if (order(p1) != order(p2))
                    return order(p1) - order(p2);
                return p1.name.CompareTo(p2.name);
            }

            // use assigned or default values
            return savedOrder1 - savedOrder2;
        }

        public static void Sort(Dictionary<string, PluginInfo> plugins) {
            var list = plugins.ToList();
            list.Sort((p1, p2) => Comparison(p1.Value, p2.Value));

            plugins.Clear();
            foreach (var pair in list)
                plugins.Add(pair.Key, pair.Value);

            Log.Debug("\n=========================== plugins.Values: =======================");
            foreach (var p in plugins.Values) {
                string dlls = string.Join( ", ",
                    Directory.GetFiles(p.modPath, "*.dll", SearchOption.AllDirectories));
                Log.Debug(
                    $"loadOrder={p.GetLoadOrder()} path={p.modPath} dlls={{{dlls}}}"
                    , false);
            }
            Log.Debug("\n=========================== END plugins.Values =====================\n");

        }
    }
}