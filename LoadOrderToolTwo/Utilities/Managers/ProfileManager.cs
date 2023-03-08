using Extensions;

using LoadOrderToolTwo.Domain;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadOrderToolTwo.Utilities.Managers;
public static class ProfileManager
{
	private const string LOCAL_APP_DATA_PATH = "%LOCALAPPDATA%";
	private const string CITIES_PATH = "%CITIES%";
	private const string WS_CONTENT_PATH = "%WORKSHOP%";
	private static readonly List<Profile> _profiles;
	public static Profile CurrentProfile { get; private set; }
	public static IEnumerable<Profile> Profiles
	{
		get
		{
			foreach (var profile in _profiles)
			{
				yield return profile;
			}
		}
	}

	public static event Action<Profile>? ProfileChanged;

	static ProfileManager()
	{
		try
		{ _profiles = LoadProfiles(); }
		catch
		{ _profiles = new List<Profile>(); }

		var currentProfile = _profiles.FirstOrDefault(x => x.Name == CentralManager.SessionSettings.CurrentProfile);

		if (currentProfile != null)
		{
			CurrentProfile = currentProfile;
		}
		else
		{
			CurrentProfile = Profile.TransitoryProfile;
		}

		CentralManager.ContentLoaded += CentralManager_ContentLoaded;
	}

	private static void CentralManager_ContentLoaded() => new BackgroundAction(() =>
	{
		foreach (var profile in _profiles)
		{
			profile.IsMissingItems = profile.Mods.Any(x => GetMod(x) is null) || profile.Assets.Any(x => GetAsset(x) is null);
		}
	}).Run();

	internal static void SetProfile(Profile profile)
	{
		CurrentProfile = profile;
		ProfileChanged?.Invoke(CurrentProfile);
	}

	private static List<Profile> LoadProfiles()
	{
		var profiles = new List<Profile>();

		try
		{
			// Load Legacy Profiles
			foreach (var profile in Directory.EnumerateFiles(LocationManager.LotProfilesAppDataPath, "*.xml"))
			{
				var legacyProfile = LoadOrderTool.Legacy.LoadOrderProfile.Deserialize(profile);
				var newProfile = legacyProfile.ToLot2Profile(Path.GetFileNameWithoutExtension(profile));

				profiles.Add(newProfile);

				newProfile.Save();

				File.Delete(profile);
			}
		}
		catch { }

		try
		{
			foreach (var profile in Directory.EnumerateFiles(LocationManager.LotProfilesAppDataPath, "*.json"))
			{
				var newProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<Profile>(File.ReadAllText(profile));

				if (newProfile != null)
				{
					profiles.Add(newProfile);
				}
				else
				{
					Log.Error($"Could not load the profile: '{profile}'");
				}
			}
		}
		catch { }

		return profiles;
	}

	public static bool Save(Profile? profile)
	{
		if (profile == null || profile.Temporary)
		{
			return false;
		}

		try
		{
			File.WriteAllText(
				Path.Combine(LocationManager.LotProfilesAppDataPath, $"{profile.Name}.json"),
				Newtonsoft.Json.JsonConvert.SerializeObject(profile, Newtonsoft.Json.Formatting.Indented));

			return true;
		}
		catch (Exception ex)
		{ Log.Exception(ex, $"Failed to save profile ({profile.Name}) to {Path.Combine(LocationManager.LotProfilesAppDataPath, $"{profile.Name}.json")}"); }

		return false;
	}

	internal static Mod GetMod(Profile.Mod mod)
	{
		return ModsUtil.GetMod(ToLocalPath(mod.RelativePath));
	}

	internal static Asset GetAsset(Profile.Asset asset)
	{
		return AssetsUtil.GetAsset(ToLocalPath(asset.RelativePath));
	}

	internal static string? ToRelativePath(string? localPath)
	{
		return localPath?
			.Replace(LocationManager.AppDataPath, LOCAL_APP_DATA_PATH)
			.Replace(LocationManager.GamePath, CITIES_PATH)
			.Replace(LocationManager.WorkshopContentPath, WS_CONTENT_PATH);
	}

	internal static string? ToLocalPath(string? relativePath)
	{
		return relativePath?
			.Replace(LOCAL_APP_DATA_PATH, LocationManager.AppDataPath)
			.Replace(CITIES_PATH, LocationManager.GamePath)
			.Replace(WS_CONTENT_PATH, LocationManager.WorkshopContentPath);
	}
}
