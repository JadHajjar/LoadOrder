using KianCommons;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PluginInfo = ColossalFramework.Plugins.PluginManager.PluginInfo;

namespace LoadOrderInjections.Injections {
    public static class ReplaceAssembies {
        public static Dictionary<string, AssemblyDefinition> asms_ = new();
        public static string[] shared_ = new string[] {
            "UnifiedUILib",
        };

        public static void Init(PluginInfo[] plugins) {
            try {
                Log.Called();

                foreach (var pluginInfo in plugins) {
                    string modPath = pluginInfo.modPath;
                    string[] dllPaths = Directory.GetFiles(modPath, "*.dll", SearchOption.AllDirectories);
                    foreach (string dllPath in dllPaths) {
                        var asm = ReadAssemblyDefinition(dllPath);
                        if (asm != null) {
                            asms_[dllPath] = asm;
                        }
                    }
                }
            } catch(Exception ex) { ex.Log(); }
        }

        internal static AssemblyDefinition ReadAssemblyDefinition(string dllpath) {
            try {
                Log.Called(dllpath);
                var r = new MyAssemblyResolver();
                var readerParameters = new ReaderParameters {
                    ReadWrite = false,
                    InMemory = true,
                    AssemblyResolver = r,
                };
                r.ReaderParameters = readerParameters;
                var asm = AssemblyDefinition.ReadAssembly(dllpath, readerParameters);

                if (asm != null)
                    Log.Info("Assembly Definition loaded: " + asm);
                else
                    Log.Info("Assembly Definition at " + dllpath + " failed to load.");

                return asm;
            } catch (Exception ex) {
                Log.Info("Assembly Definition at " + dllpath + " failed to load.\n" + ex.Message);
                return null;
            }
        }
        internal static Version Take(this Version version, int fieldCount) =>
            new Version(version.ToString(fieldCount));

        public static string ReplaceAssemblyPath(string dllPath) {
            try {
                if (asms_.TryGetValue(dllPath, out var asm) && shared_.Contains(asm.Name.Name)) {
                    var varients = asms_.Where(item => item.Value.Name.Name == asm.Name.Name).ToArray();
                    var latest = varients.MaxBy(item => item.Value.Name.Version);

                    // if only the revision is different then return the current assembly (good for hot-reload).
                    var version0 = asm.Name.Version.Take(3);
                    var version = latest.Value.Name.Version.Take(3);
                    if (version > version0) {
                        Log.Info($"Replacing {asm.Name.Name} V{asm.Name.Version} with V{latest.Value.Name.Version}");
                        return latest.Key;
                    }
                }
            } catch (Exception ex) { ex.Log(); }
            return dllPath;
        }
    }
}
