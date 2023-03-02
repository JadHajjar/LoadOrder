using System.IO;
using System.Windows.Forms;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class LocationManager
{
	// Base Folders
	public static string GamePath { get; set; } = @"C:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines";
	public static string AppDataPath { get; set; } = @"C:\Users\DotCa\AppData\Local\Colossal Order\Cities_Skylines";
	public static string SteamPath { get; set; } = @"C:\Program Files (x86)\Steam";

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
			if (/*isMacOSX*/false)
			{
				return Path.Combine(Path.Combine(GamePath, "Resources"), "Files");
			}

			return Path.Combine(GamePath, "Files");
		}
	}

	public static string CurrentDirectory { get; } = Directory.GetParent(Application.ExecutablePath).FullName;
}
