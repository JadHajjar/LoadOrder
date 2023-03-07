using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Domain;
public class Profile
{
	public static readonly Profile TransitoryProfile = new("Temporary Profile") { Temporary = true };
	
	[JsonIgnore] public bool Temporary { get; private set; }

	public Profile(string name)
	{
		Name = name;
		LaunchSettings = new();
		Assets = new();
		Mods= new();
	}

	public void Save() => ProfileManager.Save(this);

	public string Name { get; set; }
	public List<Asset> Assets { get; set; }
	public List<Mod> Mods { get; set; }
	public LaunchSettings LaunchSettings { get; set; }
	public string? LsmSkipFile { get; set; }

	public class Asset
	{
		public string? Name { get; set; }
		public string? RelativePath { get;  set; }
	}

	public class Mod
	{
		public string? Name { get; set; }
		public string? RelativePath { get; set; }
		public ulong SteamId { get; set; }
		public bool Enabled { get; set; }
	}
}
