using Extensions;

using LoadOrderShared;

using LoadOrderToolTwo.ColossalOrder;
using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities.Assembly;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace LoadOrderToolTwo.Utilities;
internal class AssetsUtil
{
	public static HashSet<string> ExcludedHashSet { get; }
	public static Dictionary<string, CSCache.Asset> AssetInfoCache { get; }

	static AssetsUtil()
	{
		var config = LoadOrderConfig.Deserialize();
		var cache = CSCache.Deserialize()?.Assets.ToDictionary(x => x.IncludedPath, x => x, StringComparer.InvariantCultureIgnoreCase);

		AssetInfoCache = cache ?? new();

		if (config != null)
		{
			ExcludedHashSet = new HashSet<string>(config.Assets.Where(x => x.Excluded).Select(x => x.Path.ToLower()));
		}
		else
		{
			ExcludedHashSet = new HashSet<string>();
		}
	}

	public static IEnumerable<Asset> GetAssets(Package package)
	{
		foreach (var file in Directory.EnumerateFiles(package.Folder, $"*.crp"))
		{
			yield return new Asset(package, file);
		}
	}

	private static bool IsValidAssetFolder(string dir, out string? dllPath, out Version? version)
	{
		var files = Directory.GetFiles(dir, "*.dll");

		if (files != null && files.Length > 0)
		{
			return AssemblyUtil.FindImplementation(files, "ICities.IUserAsset", out dllPath, out version);
		}

		dllPath = null;
		version = null;
		return false;
	}

	internal static bool IsIncluded(Asset asset)
	{
		return !ExcludedHashSet.Contains(asset.FileName.ToLower());
	}

	internal static void SetIncluded(Asset asset, bool value)
	{
		if (value)
		{
			ExcludedHashSet.Add(asset.FileName);
		}
		else
		{
			ExcludedHashSet.Remove(asset.FileName);
		}
	}

	internal static SavedBool GetEnabledSetting(Asset mod)
	{
		var savedEnabledKey_ = $"{Path.GetFileNameWithoutExtension(mod.Folder)}{GetLegacyHashCode(mod.Folder)}.enabled";

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

	public static DownloadStatus GetStatus(Asset mod, out string reason)
	{
		if (!mod.Workshop)
		{
			reason = Locale.AssetIsLocal;
			return DownloadStatus.OK;
		}

		if (mod.RemovedFromSteam)
		{
			reason = Locale.AssetIsRemoved;
			return DownloadStatus.Removed;
		}

		if (!mod.SteamInfoLoaded)
		{
			reason = Locale.AssetIsUnknown;
			return DownloadStatus.Unknown;
		}

		if (!Directory.Exists(mod.Folder))
		{
			reason = Locale.AssetIsNotDownloaded;
			return DownloadStatus.NotDownloaded;
		}

		var updatedServer = mod.ServerTime;
		var updatedLocal = ContentUtil.GetLocalUpdatedTime(mod.Folder).ToUniversalTime();
		var sizeServer = mod.ServerSize;
		var localSize = ContentUtil.GetTotalSize(mod.Folder);

		if (updatedLocal < updatedServer)
		{
			var certain =
				localSize < sizeServer ||
				updatedLocal < updatedServer.AddHours(-24);

			reason = certain ? Locale.AssetIsOutOfDate : Locale.AssetIsMaybeOutOfDate;
			reason += $"\r\n{Locale.Server}: {updatedServer:g} | {Locale.Local}: {updatedLocal:g}";
			return DownloadStatus.OutOfDate;
		}

		if (localSize < sizeServer)
		{
			reason = Locale.AssetIsIncomplete;
			reason += $"\r\n{Locale.Server}: {sizeServer.SizeString()} | {Locale.Local}: {localSize.SizeString()}";
			return DownloadStatus.PartiallyDownloaded;
		}

		reason = Locale.AssetIsUpToDate;
		return DownloadStatus.OK;
	}

	public static IEnumerable<IGrouping<string, Asset>> GetDuplicateAssets()
	{
		return CentralManager.Assets
			.Where(x => x.IsIncluded)
			.GroupBy(x => Path.GetFileName(x.FileName))
			.Where(x => x.Count() > 1);
	}
}
