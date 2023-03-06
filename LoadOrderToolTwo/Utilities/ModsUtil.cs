using Extensions;

using LoadOrderToolTwo.ColossalOrder;
using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Utilities.IO;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
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

	public static Mod? GetMod(Package package)
	{
		if (IsValidModFolder(package.Folder, out var dllPath, out var version))
		{
			return new Mod(package, dllPath!, version!);
		}

		return null;
	}

	private static IEnumerable<Mod> LoadModsInPath(string path, bool builtIn)
	{
		if (!Directory.Exists(path))
		{
			yield break;
		}

		foreach (var dir in Directory.EnumerateDirectories(path))
		{
			if (IsValidModFolder(dir, out var dllPath, out var version))
			{
				//yield return new Mod(dir, builtIn, false, dllPath!, version!);
			}
		}
	}

	private static IEnumerable<Mod> LoadWorkshopMods()
	{
		var potentialMods = ContentUtil.GetSubscribedItemPaths();
		var lockObj = new object();
		var mods = new List<Mod>();

		Parallel.ForEach(potentialMods, (path, state) =>
		{
			if (IsValidModFolder(path, out var dllPath, out var version))
			{
				//var mod = new Mod(path, false, true, dllPath!, version!);

				//lock (lockObj)
				//{
				//	mods.Add(mod);
				//}
			}
		});

		return mods;
	}

	private static bool IsValidModFolder(string dir, out string? dllPath, out Version? version)
	{
		var files = Directory.GetFiles(dir, "*.dll");

		if (files != null && files.Length > 0)
		{
			return AssemblyUtil.FindImplementation(files, "ICities.IUserMod", out dllPath, out version);
		}

		dllPath = null;
		version = null;
		return false;
	}

	internal static bool IsIncluded(Mod mod)
	{
		return !File.Exists(Path.Combine(mod.Folder, ContentUtil.EXCLUDED_FILE_NAME));
	}

	internal static void SetIncluded(Mod mod, bool value)
	{
		if (value)
		{
			File.Delete(Path.Combine(mod.Folder, ContentUtil.EXCLUDED_FILE_NAME));
		}
		else
		{
			File.WriteAllBytes(Path.Combine(mod.Folder, ContentUtil.EXCLUDED_FILE_NAME), new byte[0]);
		}
	}

	internal static SavedBool GetEnabledSetting(Mod mod)
	{
		var savedEnabledKey_ = $"{Path.GetFileNameWithoutExtension(mod.Folder)}{GetLegacyHashCode(mod.VirtualFolder ?? mod.Folder)}.enabled";

		return new SavedBool(savedEnabledKey_, "userGameState", def: false, autoUpdate: true);
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	public static unsafe int GetLegacyHashCode(string str)
	{
		fixed (char* ptr = str + (RuntimeHelpers.OffsetToStringData / 2))
		{
			var ptr2 = ptr;
			var ptr3 = ptr2 + str.Length - 1;
			var num = 0;
			while (ptr2 < ptr3)
			{
				num = (num << 5) - num + *ptr2;
				num = (num << 5) - num + ptr2[1];
				ptr2 += 2;
			}
			ptr3++;
			if (ptr2 < ptr3)
			{
				num = (num << 5) - num + *ptr2;
			}
			return num;
		}
	}

	public static DownloadStatus GetStatus(IPackage mod, out string reason)
	{
		if (!mod.Workshop)
		{
			reason = Locale.ModIsLocal;
			return DownloadStatus.OK;
		}

		if (mod.RemovedFromSteam)
		{
			reason = Locale.ModIsRemoved;
			return DownloadStatus.Removed;
		}

		if (!mod.SteamInfoLoaded)
		{
			reason = Locale.ModIsUnknown;
			return DownloadStatus.Unknown;
		}

		if (!Directory.Exists(mod.Folder))
		{
			reason = Locale.ModIsNotDownloaded;
			return DownloadStatus.NotDownloaded;
		}

		var updatedServer = mod.ServerTime;
		var updatedLocal = mod.LocalTime;
		var sizeServer = mod.ServerSize;
		var localSize = ContentUtil.GetTotalSize(mod.Folder);

		if (updatedLocal < updatedServer)
		{
			var certain =
				localSize < sizeServer ||
				updatedLocal < updatedServer.AddHours(-24);

			reason = certain ? Locale.ModIsOutOfDate : Locale.ModIsMaybeOutOfDate;
			reason += $"\r\n{Locale.Server}: {updatedServer:g} | {Locale.Local}: {updatedLocal:g}";
			return DownloadStatus.OutOfDate;
		}

		if (localSize < sizeServer)
		{
			reason = Locale.ModIsIncomplete;
			reason += $"\r\n{Locale.Server}: {sizeServer.SizeString()} | {Locale.Local}: {localSize.SizeString()}";
			return DownloadStatus.PartiallyDownloaded;
		}

		reason = Locale.ModIsUpToDate;
		return DownloadStatus.OK;
	}

	public static IEnumerable<IGrouping<string, Mod>> GetDuplicateMods()
	{
		return CentralManager.Mods
			.Where(x => x.IsIncluded)
			.GroupBy(x => Path.GetFileName(x.FileName))
			.Where(x => x.Count() > 1);
	}
}
