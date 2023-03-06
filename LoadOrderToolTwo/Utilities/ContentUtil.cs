using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;
internal class ContentUtil
{
	public const string EXCLUDED_FILE_NAME = ".excluded";

	public static IEnumerable<ulong> GetSubscribedItems()
	{
		foreach (var path in GetSubscribedItemPaths())
		{
			yield return ulong.Parse(Path.GetFileName(path));
		}
	}

	public static IEnumerable<string> GetSubscribedItemPaths()
	{
		if (!Directory.Exists(LocationManager.WorkshopContentPath))
		{
			yield break;
		}

		foreach (var path in Directory.EnumerateDirectories(LocationManager.WorkshopContentPath))
		{
			if (ulong.TryParse(Path.GetFileName(path), out _))
			{
				yield return path;
			}
		}
	}

	public static string GetSubscribedItemPath(ulong id)
	{
		return Path.Combine(LocationManager.WorkshopContentPath, id.ToString());
	}

	public static DateTime GetLocalUpdatedTime(string path)
	{
		var dateTime = DateTime.MinValue;

		if (Directory.Exists(path))
		{
			foreach (var filePAth in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
			{
				if (Path.GetFileName(path) != EXCLUDED_FILE_NAME)
				{
					var lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePAth);
					if (lastWriteTimeUtc > dateTime)
					{
						dateTime = lastWriteTimeUtc;
					}
				}
			}
		}

		return dateTime;
	}

	public static long GetTotalSize(string path)
	{
		var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
		return files.Sum(f => new FileInfo(f).Length);
	}

	internal static List<Package> LoadContents()
	{
		var packages = new List<Package>();
		var gameModsPath = Path.Combine(LocationManager.GameContentPath, "Mods");
		var addonsModsPath = LocationManager.ModsPath;
		var addonsAssetsPath = new[]
		{
			LocationManager.AssetsPath,
			LocationManager.StylesPath,
			LocationManager.MapThemesPath
		};

		foreach (var folder in addonsAssetsPath)
		{
			getPackage(folder, false, false);
		}

		if(Directory.Exists(gameModsPath))
		foreach (var folder in Directory.GetDirectories(gameModsPath))
		{
			getPackage(folder, true, false);
		}

		if(Directory.Exists(addonsModsPath))
		foreach (var folder in Directory.GetDirectories(addonsModsPath))
		{
			getPackage(folder, false, false);
		}

		var subscribedItems = GetSubscribedItemPaths().ToList();

		Parallel.ForEach(subscribedItems, (folder, state) =>
		{
			getPackage(folder, false, true);
		});

		return packages;

		void getPackage(string folder, bool builtIn, bool workshop)
		{
			if (!Directory.Exists(folder))
			{
				return;
			}

			var package = new Package(folder, builtIn, workshop);

			package.Assets = AssetsUtil.GetAssets(package).ToArray();
			package.Mod = ModsUtil.GetMod(package);

			if (package.Assets.Length != 0 || package.Mod != null)
			{
				lock (packages)
				{
					packages.Add(package);
				}
			}
		}
	}
}
