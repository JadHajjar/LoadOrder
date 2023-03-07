using LoadOrderToolTwo.Utilities.IO;

using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class LocationManager
{
	// Base Folders
	public static string GamePath { get; set; } = ConfigurationManager.AppSettings[nameof(GamePath)].TrimEnd('/', '\\');
	public static string AppDataPath { get; set; } = ConfigurationManager.AppSettings[nameof(AppDataPath)].TrimEnd('/', '\\');
	public static string SteamPath { get; set; } = ConfigurationManager.AppSettings[nameof(SteamPath)].TrimEnd('/', '\\');
	public static string VirtualGamePath { get; set; } = ConfigurationManager.AppSettings[nameof(VirtualGamePath)].TrimEnd('/', '\\');
	public static string VirtualAppDataPath { get; set; } = ConfigurationManager.AppSettings[nameof(VirtualAppDataPath)].TrimEnd('/', '\\');

	public static string DataPath => Path.Combine(GamePath, "Cities_Data");
	public static string ManagedDLL => Path.Combine(DataPath, "Managed");
	public static string MonoPath => Path.Combine(DataPath, "Mono");
	public static string AddonsPath => Path.Combine(AppDataPath, "Addons");
	public static string LotAppDataPath => Path.Combine(AppDataPath, "LoadOrder");
	public static string LotProfilesAppDataPath => Path.Combine(LotAppDataPath, "LOMProfiles");
	public static string ModsPath => Path.Combine(AddonsPath, "Mods");
	public static string AssetsPath => Path.Combine(AddonsPath, "Assets");
	public static string MapThemesPath => Path.Combine(AddonsPath, "MapThemes");
	public static string StylesPath => Path.Combine(AddonsPath, "Styles");

	public static string WorkshopContentPath
	{
		get
		{
			if (string.IsNullOrWhiteSpace(GamePath))
			{
				return string.Empty;
			}

			return Path.Combine(Directory.GetParent(GamePath).Parent.FullName, "workshop", "content", "255710");
		}
	}

	public static string VirtualWorkshopContentPath
	{
		get
		{
			if (string.IsNullOrWhiteSpace(VirtualGamePath))
			{
				return string.Empty;
			}

			var parent = VirtualGamePath.Substring(0, VirtualGamePath.LastIndexOfAny(new[] { '/', '\\' }));
			parent = parent.Substring(0, parent.LastIndexOfAny(new[] { '/', '\\' }));

			return Path.Combine(parent, "workshop", "content", "255710");
		}
	}

	public static string GameContentPath
	{
		get
		{
			if (PlatformUtil.CurrentPlatform == PlatformUtil.Platform.MacOSX)
			{
				return Path.Combine(Path.Combine(GamePath, "Resources"), "Files");
			}

			return Path.Combine(GamePath, "Files");
		}
	}

	public static string CitiesExe => PlatformUtil.CurrentPlatform switch
	{
		PlatformUtil.Platform.Windows => "Cities.exe",
		PlatformUtil.Platform.MacOSX => "Cities",
		PlatformUtil.Platform.WineOnLinux or PlatformUtil.Platform.Linux => "Cities.x64",
		_ => "Cities",
	};

	public static string SteamExe => PlatformUtil.CurrentPlatform switch
	{
		PlatformUtil.Platform.Windows => "Steam.exe",
		PlatformUtil.Platform.MacOSX => "Steam",
		PlatformUtil.Platform.WineOnLinux or PlatformUtil.Platform.Linux => "Steam",
		_ => "Steam",
	};

	public static string CurrentDirectory { get; } = Directory.GetParent(Application.ExecutablePath).FullName;
}
