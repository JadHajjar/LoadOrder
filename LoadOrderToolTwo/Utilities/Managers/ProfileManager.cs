using LoadOrderToolTwo.Domain;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadOrderToolTwo.Utilities.Managers;
public static class ProfileManager
{
	private static readonly List<Profile> _profiles;
	public static Profile CurrentProfile { get; private set; }

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
}
