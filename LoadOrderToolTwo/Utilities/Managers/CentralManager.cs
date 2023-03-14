using Extensions;

using LoadOrderShared;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Utilities;

using Steamworks;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.Managers;
internal static class CentralManager
{
	private const ulong CompatibilityReport_STEAM_ID = 2881031511;
	private const ulong HARMONY_ALPHA_STEAM_ID = 2399204842;
	private const ulong HARMONY_STEAM_ID = 2040656402;
	private const ulong PATCH_LOADER_MOD_STEAM_ID = 2041457644;
	private const ulong LOM_ALPHA_STEAM_ID = 2620852727;
	private const ulong LOM_STEAM_ID = 2448824112;

	private static List<Package>? packages;

	public static event Action? ContentLoaded;
	public static event Action? WorkshopInfoUpdated;
	public static event Action<Package>? PackageInformationUpdated;
	public static event Action<Mod>? ModInformationUpdated;
	public static event Action<Asset>? AssetInformationUpdated;

	public static Profile CurrentProfile => ProfileManager.CurrentProfile;
	public static bool IsContentLoaded { get; private set; }
	public static SessionSettings SessionSettings { get; set; } = ISave.Load<SessionSettings>(nameof(SessionSettings) + ".tf");
	public static IEnumerable<Package> Packages => packages ?? new();

	public static IEnumerable<Mod> Mods
	{
		get
		{
			var currentPackages = packages ?? new();

			foreach (var package in currentPackages)
			{
				if (package.Mod != null)
				{
					yield return package.Mod;
				}
			}
		}
	}

	public static IEnumerable<Asset> Assets
	{
		get
		{
			var currentPackages = packages ?? new();

			foreach (var package in currentPackages)
			{
				if (package.Assets == null)
				{
					continue;
				}

				foreach (var asset in package.Assets)
				{
					yield return asset;
				}
			}
		}
	}

	public static void Start()
	{
		var content = ContentUtil.LoadContents();

		AnalyzePackages(content);

		packages = content;

		IsContentLoaded = true;

		ContentLoaded?.Invoke();

		var cachedSteamInfo = SteamUtil.GetCachedInfo();

		if (cachedSteamInfo != null)
		{
			foreach (var package in Packages)
			{
				if (cachedSteamInfo.ContainsKey(package.SteamId))
				{
					package.SetSteamInformation(cachedSteamInfo[package.SteamId], true);
				}
			}

			WorkshopInfoUpdated?.Invoke();

			Parallel.ForEach(Packages.OrderBy(x => x.Mod == null), (package, state) =>
			{
				package.Status = ModsUtil.GetStatus(package, out var reason);
				package.StatusReason = reason;

				InformationUpdate(package);

				if (!string.IsNullOrWhiteSpace(package.IconUrl))
				{
					package.IconImage = ImageManager.GetImage(package.IconUrl!, true);

					InformationUpdate(package);
				}

				if (!string.IsNullOrWhiteSpace(package.Author?.AvatarUrl))
				{
					package.AuthorIconImage = ImageManager.GetImage(package.Author!.AvatarUrl!, true);

					InformationUpdate(package);
				}
			});
		}

		ConnectionHandler.WhenConnected(async () =>
		{
			var result = await SteamUtil.LoadDataAsync(Packages.Where(x => x.Workshop).Select(x => x.SteamId).ToArray());

			foreach (var package in Packages)
			{
				if (result.ContainsKey(package.SteamId))
				{
					package.SetSteamInformation(result[package.SteamId], false);
				}
			}

			WorkshopInfoUpdated?.Invoke();

			Parallel.ForEach(Packages.OrderBy(x => x.Mod == null).ThenBy(x => x.Name), (package, state) =>
			{
				if (!string.IsNullOrWhiteSpace(package.IconUrl) && package.IconImage is null)
				{
					package.IconImage = ImageManager.GetImage(package.IconUrl!);

					InformationUpdate(package);
				}

				if (!string.IsNullOrWhiteSpace(package.Author?.AvatarUrl) && package.AuthorIconImage is null)
				{
					package.AuthorIconImage = ImageManager.GetImage(package.Author!.AvatarUrl!);

					InformationUpdate(package);
				}
			});

			WorkshopInfoUpdated?.Invoke();
		});
	}

	private static void AnalyzePackages(List<Package> content)
	{
		Package? compatibilityReport = null, harmony = null, harmonyAlpha = null, lom = null, lomAlpha = null, plm = null;
		
		foreach (var package in content)
		{
			switch (package.SteamId)
			{
				case LOM_STEAM_ID:
					lom = package;
					break;

				case LOM_ALPHA_STEAM_ID:
					lomAlpha = package;
					break;

				case PATCH_LOADER_MOD_STEAM_ID:
					plm = package;
					break;

				case HARMONY_STEAM_ID:
					harmony = package;
					break;

				case HARMONY_ALPHA_STEAM_ID:
					harmonyAlpha = package;
					break;

				case CompatibilityReport_STEAM_ID:
					compatibilityReport = package;
					break;
			}
		}

		if (compatibilityReport != null)
		{
			CompatibilityManager.LoadCompatibilityReport(compatibilityReport);
		}

		if (plm != null)
		{
			plm.IsRequired = true;
			plm.Mod!.IsIncluded = true;
			plm.Mod!.IsEnabled = true;
		}

		if (lomAlpha != null)
		{
			lomAlpha.IsRequired = true;
			lomAlpha.Mod!.IsIncluded = true;
			lomAlpha.Mod!.IsEnabled = true;
		}
		else if (lom != null)
		{
			lom.IsRequired = true;
			lom.Mod!.IsIncluded = true;
			lom.Mod!.IsEnabled = true;
		}

		if (harmonyAlpha != null)
		{
			harmonyAlpha.IsRequired = true;
			harmonyAlpha.Mod!.IsIncluded = true;
			harmonyAlpha.Mod!.IsEnabled = true;
		}
		else if (harmony != null)
		{
			harmony.IsRequired = true;
			harmony.Mod!.IsIncluded = true;
			harmony.Mod!.IsEnabled = true;
		}
	}

	public static void InformationUpdate(IPackage iPackage)
	{
		if (iPackage is Package package)
		{
			PackageInformationUpdated?.Invoke(package);

			if (package.Mod != null)
			{
				ModInformationUpdated?.Invoke(package.Mod);
			}

			if (package.Assets != null)
			{
				foreach (var asset in package.Assets)
				{
					AssetInformationUpdated?.Invoke(asset);
				}
			}
		}
		else if (iPackage is Mod mod)
		{
			ModInformationUpdated?.Invoke(mod);
		}
		else if (iPackage is Asset asset)
		{
			AssetInformationUpdated?.Invoke(asset);
		}
	}

	internal static void AddPackage(Package package)
	{
		if (package.SteamId == CompatibilityReport_STEAM_ID)
		{
			CompatibilityManager.LoadCompatibilityReport(package);
		}
		
		var cachedSteamInfo = SteamUtil.GetCachedInfo();

		if (cachedSteamInfo != null && cachedSteamInfo.ContainsKey(package.SteamId))
		{
			package.SetSteamInformation(cachedSteamInfo[package.SteamId], true);
		}

		var newPackages = new List<Package>(packages)
		{
			package
		};

		packages = newPackages;

		RefreshSteamInfo(package);
		ContentLoaded?.Invoke();
	}

	internal static void RefreshSteamInfo(Package package)
	{
		if (!package.Workshop)
		{
			return;
		}

		ConnectionHandler.WhenConnected(async () =>
		{
			var result = await SteamUtil.LoadDataAsync(new ulong[] { package.SteamId });

			if (result.ContainsKey(package.SteamId))
			{
				package.SetSteamInformation(result[package.SteamId], false);
			}

			WorkshopInfoUpdated?.Invoke();

			if (!string.IsNullOrWhiteSpace(package.IconUrl) && package.IconImage is null)
			{
				package.IconImage = ImageManager.GetImage(package.IconUrl!);

				InformationUpdate(package);
			}

			if (!string.IsNullOrWhiteSpace(package.Author?.AvatarUrl) && package.AuthorIconImage is null)
			{
				package.AuthorIconImage = ImageManager.GetImage(package.Author!.AvatarUrl!);

				InformationUpdate(package);
			}

			WorkshopInfoUpdated?.Invoke();
		});
	}

	internal static void RemovePackage(Package package)
	{
		var newPackages = new List<Package>(packages);

		newPackages.Remove(package);

		packages = newPackages;

		package.Status = DownloadStatus.NotDownloaded;
		ContentLoaded?.Invoke();
		WorkshopInfoUpdated?.Invoke();
	}
}
