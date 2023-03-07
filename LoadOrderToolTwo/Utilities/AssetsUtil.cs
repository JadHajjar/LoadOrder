﻿using Extensions;

using LoadOrderShared;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Utilities;
using LoadOrderToolTwo.Utilities.IO;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadOrderToolTwo.Utilities;
internal class AssetsUtil
{
	private static readonly LoadOrderConfig _config;

	public static HashSet<string> ExcludedHashSet { get; }
	public static Dictionary<string, CSCache.Asset> AssetInfoCache { get; }

	static AssetsUtil()
	{
		_config = LoadOrderConfig.Deserialize() ?? new();
		var cache = CSCache.Deserialize()?.Assets.ToDictionary(x => x.IncludedPath, x => x, StringComparer.InvariantCultureIgnoreCase);

		AssetInfoCache = cache ?? new();
		ExcludedHashSet = new HashSet<string>(_config.Assets.Where(x => x.Excluded).Select(x => x.Path.ToLower()));
	}

	public static IEnumerable<Asset> GetAssets(Package package)
	{
		foreach (var file in Directory.EnumerateFiles(package.Folder, $"*.crp"))
		{
			yield return new Asset(package, file);
		}
	}

	internal static bool IsIncluded(Asset asset)
	{
		return !ExcludedHashSet.Contains(asset.FileName.ToLower());
	}

	internal static void SetIncluded(Asset asset, bool value)
	{
		if (value)
		{
			ExcludedHashSet.Remove(asset.FileName.ToLower());
		}
		else
		{
			ExcludedHashSet.Add(asset.FileName.ToLower());
		}

		CentralManager.InformationUpdate(asset);

		SaveChanges();
	}

	private static void SaveChanges()
	{
		_config.Assets = CentralManager.Assets
				.Where(x => !x.IsIncluded)
				.Select(x => new AssetInfo { Excluded = true, Path = x.FileName })
				.ToArray();

		_config.Serialize();
	}
}
