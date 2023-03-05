using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class LocationManager
{
	// Base Folders
	public static string GamePath { get; set; } = ConfigurationManager.AppSettings[nameof(GamePath)];
	public static string AppDataPath { get; set; } = ConfigurationManager.AppSettings[nameof(AppDataPath)];
	public static string SteamPath { get; set; } = ConfigurationManager.AppSettings[nameof(SteamPath)];
	public static string VirtualGamePath { get; set; } = ConfigurationManager.AppSettings[nameof(VirtualGamePath)];
	public static string VirtualAppDataPath { get; set; } = ConfigurationManager.AppSettings[nameof(VirtualAppDataPath)];

	public static string DataPath => Path.Combine(GamePath, "Cities_Data");
	public static string ManagedDLL => Path.Combine(DataPath, "Managed");
	public static string MonoPath => Path.Combine(DataPath, "Mono");
	public static string AddonsPath => Path.Combine(AppDataPath, "Addons");
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

	public static string GameContentPath
	{
		get
		{
			//if (isMacOSX)
			//{
			//	return Path.Combine(Path.Combine(GamePath, "Resources"), "Files");
			//}

			return Path.Combine(GamePath, "Files");
		}
	}

	public static string CitiesExe
	{
		get
		{
			//if (isWindows)
				return "Cities.exe";
			//else if (isLinux)
			//	return "Cities.x64";
			//else if (isMacOSX)
			//	return "Cities";
			//else
			//	return "Cities"; // unknown platform.
		}
	}

	public static string SteamExe
	{
		get
		{
			//if (isWindows)
				return "Steam.exe";
			//else if (isLinux)
			//	return "Steam";
			//else if (isMacOSX)
			//	return "Steam";
			//else
			//	return "Steam"; // unknown platform.
		}
	}

	public static string CurrentDirectory { get; } = Directory.GetParent(Application.ExecutablePath).FullName;
}
