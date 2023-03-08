using LoadOrderToolTwo.Utilities.Managers;

using Newtonsoft.Json;

using System.Collections.Generic;

namespace LoadOrderToolTwo.Domain;
public class Profile
{
	public static readonly Profile TransitoryProfile = new("Temporary Profile") { Temporary = true, AutoSave = false };

	[JsonIgnore] public bool Temporary { get; private set; }

	public Profile(string name) : this()
	{
		Name = name;
	}

	public Profile()
	{
		LaunchSettings = new();
		Assets = new();
		Mods = new();
		AutoSave = true;
	}

	public void Save()
	{
		ProfileManager.Save(this);
	}

	public string? Name { get; set; }
	public List<Asset> Assets { get; set; }
	public List<Mod> Mods { get; set; }
	public LaunchSettings LaunchSettings { get; set; }
	public string? LsmSkipFile { get; set; }
	public bool AutoSave { get; set; }

	public class Asset
	{
		public string? Name { get; set; }
		public string? RelativePath { get; set; }
	}

	public class Mod
	{
		public string? Name { get; set; }
		public string? RelativePath { get; set; }
		public ulong SteamId { get; set; }
		public bool Enabled { get; set; }
	}
}
