using Extensions;

using LoadOrderToolTwo.Domain.Steam;

using Mono.Cecil;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Domain;
internal class Mod
{
	public Mod(string folder, bool builtIn, bool workshop,  string dllPath, Version version)
	{
		FileName = dllPath;
		Folder = folder;
		BuiltIn = builtIn;
		Workshop = workshop;
		Version = version;
		Name = Path.GetFileNameWithoutExtension(dllPath).FormatWords();

		if (workshop)
		{
			SteamId = ulong.Parse(Path.GetFileName(folder));
		}
	}

	public Version Version { get; }
	public string Folder { get; }
	public bool BuiltIn { get; }
	public bool Workshop { get; }
	public string FileName { get; }
	public ulong SteamId { get; }

	public string Name { get; set; }
	public SteamUser? Author { get; set; }
	public DateTime LocalTime { get; set; }
    public DateTime ServerTime { get; set; }
	public long LocalSize { get; set; }
    public long ServerSize { get; set; }
    public string? IconUrl { get; set; }
	public string[]? Tags { get; set; }
	public string? Class { get; set; }
	public bool Included { get; internal set; }

	internal void SetSteamInformation(SteamWorkshopItem steamWorkshopItem)
	{
		Name = steamWorkshopItem.Title;
		Author = steamWorkshopItem.Author;
		ServerTime = steamWorkshopItem.UpdatedUTC;
		Tags = steamWorkshopItem.Tags;
		Class = steamWorkshopItem.Class;
		IconUrl = steamWorkshopItem.PreviewURL;
	}
}
