using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities.Assembly;
using LoadOrderToolTwo.Utilities.Managers;

using Mono.Cecil;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;
internal class ModsUtil
{
	public static IEnumerable<Mod> GetMods()
	{
		var gameModsPath = Path.Combine(LocationManager.GameContentPath, "Mods");
		var addonsModsPath = LocationManager.ModsPath;

		foreach (var mod in LoadModsInPath(gameModsPath, true))
		{
			yield return mod;
		}

		foreach (var mod in LoadModsInPath(addonsModsPath, false))
		{
			yield return mod;
		}

		foreach (var mod in LoadWorkshopMods())
		{
			yield return mod;
		}
	}

	private static IEnumerable<Mod> LoadModsInPath(string path, bool builtIn)
	{
		if (!Directory.Exists(path))
		{
			yield break;
		}

		foreach (var dir in Directory.EnumerateDirectories	(path))
		{
			if (IsValidModFolder(dir,  out var dllPath, out var version))
			{
				yield return new Mod(dir, builtIn, false, dllPath!, version!);
			}
		}
	}

	private static IEnumerable<Mod> LoadWorkshopMods()
	{
		var potentialMods = ContentUtil.GetSubscribedItemPaths();
		var mods = new List<Mod>();

		Parallel.ForEach(potentialMods, (path, state) =>
		{
			if (IsValidModFolder(path, out var dllPath, out var version))
			{
				mods.Add(new Mod(path, false, true, dllPath!, version!));
			}
		});

		return mods;
	}

	private static bool IsValidModFolder(string dir, out string? dllPath, out Version? version)
	{
		var files = Directory.GetFiles(dir, "*.dll");

		if (files != null && files.Length > 0)
			return AssemblyUtil.FindImplementation(files, "ICities.IUserMod", out dllPath, out version);

		dllPath = null;
		version = null;
		return false;
	}
}
