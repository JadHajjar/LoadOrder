using LoadOrderToolTwo.Domain.Utilities;

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class LocationManager
{
	// Base Folders
	public static string GamePath { get; set; }
	public static string AppDataPath { get; set; }
	public static string SteamPath { get; set; }
	public static string VirtualGamePath { get; set; }
	public static string VirtualAppDataPath { get; set; }
	public static string CurrentDirectory { get; }
	public static Platform Platform { get; }

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
			if (Platform == Platform.MacOSX)
			{
				return Path.Combine(Path.Combine(GamePath, "Resources"), "Files");
			}

			return Path.Combine(GamePath, "Files");
		}
	}

	public static string CitiesExe => Platform switch
	{
		Platform.Windows => "Cities.exe",
		Platform.MacOSX => "Cities",
		Platform.Linux => "Cities.x64",
		_ => "Cities",
	};

	public static string SteamExe => Platform switch
	{
		Platform.Windows => "Steam.exe",
		Platform.MacOSX => "Steam",
		Platform.Linux => "Steam",
		_ => "Steam",
	};

	static LocationManager()
	{
		GamePath = ConfigurationManager.AppSettings[nameof(GamePath)].TrimEnd('/', '\\');
		AppDataPath = ConfigurationManager.AppSettings[nameof(AppDataPath)].TrimEnd('/', '\\');
		SteamPath = ConfigurationManager.AppSettings[nameof(SteamPath)].TrimEnd('/', '\\');
		VirtualGamePath = ConfigurationManager.AppSettings[nameof(VirtualGamePath)].TrimEnd('/', '\\');
		VirtualAppDataPath = ConfigurationManager.AppSettings[nameof(VirtualAppDataPath)].TrimEnd('/', '\\');
		CurrentDirectory = Directory.GetParent(Application.ExecutablePath).FullName;

		Platform = Enum.TryParse(ConfigurationManager.AppSettings[nameof(Platform)], out Platform platform) ? platform : Platform.Windows;
	}

	internal static void RunFirstTimeSetup()
	{
		if (Platform == Platform.Windows)
		{
			return;
		}

		Log.Info("Checking Virtual Paths");

		if (GamePath.StartsWith("/"))
		{
			Log.Info($"GamePath: {GamePath}");
			foreach (var item in DriveInfo.GetDrives().Reverse())
			{
				var virtualPath = item.Name + GamePath.Substring(1);

				if (Directory.Exists(virtualPath))
				{
					Log.Info($"GamePath Matched: {virtualPath}");
					VirtualGamePath = GamePath;
					GamePath = virtualPath;
					break;
				}
				Log.Info($"GamePath Try Failed for: {virtualPath}");
			}
		}

		if (AppDataPath.StartsWith("/"))
		{
			Log.Info($"AppDataPath: {AppDataPath}");

			foreach (var item in DriveInfo.GetDrives().Reverse())
			{
				var virtualPath = item.Name + AppDataPath.Substring(1);

				if (Directory.Exists(virtualPath))
				{
					Log.Info($"AppDataPath Matched: {virtualPath}");
					VirtualAppDataPath = AppDataPath;
					AppDataPath = virtualPath;
					break;
				}
				Log.Info($"AppDataPath Try Failed for: {virtualPath}");
			}
		}
	}

	internal static void SetPaths(string gamePath, string appDataPath, string steamPath, string virtualGamePath, string virtualAppDataPath)
	{
		var externalConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		var appSettings = externalConfig.AppSettings;

		appSettings.Settings[nameof(GamePath)].Value = gamePath;
		appSettings.Settings[nameof(AppDataPath)].Value = appDataPath;
		appSettings.Settings[nameof(SteamPath)].Value = steamPath;
		appSettings.Settings[nameof(VirtualGamePath)].Value = virtualGamePath;
		appSettings.Settings[nameof(VirtualAppDataPath)].Value = virtualAppDataPath;

		externalConfig.Save();
	}
}
