﻿using Extensions;

using LoadOrderShared;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.Managers;
internal static class CentralManager
{
	private static readonly object _packageLock = new();
	private static List<Package>? packages;
	private const ulong CR_STEAM_ID = 2881031511;

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
			lock (_packageLock)
			{
				if (Packages == null)
				{
					yield break;
				}

				foreach (var package in Packages)
				{
					if (package.Mod != null)
					{
						yield return package.Mod;
					}
				}
			}
		}
	}
	public static IEnumerable<Asset> Assets
	{
		get
		{
			lock (_packageLock)
			{
				if (Packages == null)
				{
					yield break;
				}

				foreach (var package in Packages)
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
	}

	public static void Start()
	{
		var content = ContentUtil.LoadContents();

		var compatibilityReport = content.FirstOrDefault(x => x.SteamId == CR_STEAM_ID);

		if (compatibilityReport != null)
		{
			CompatibilityManager.LoadCompatibilityReport(compatibilityReport);
		}

		lock (_packageLock)
		{
			packages = content;
		}

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

			Parallel.ForEach(Packages, (package, state) =>
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

			Parallel.ForEach(Packages, (package, state) =>
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
}
