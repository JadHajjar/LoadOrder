using Extensions;

using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LoadOrderToolTwo.Domain;

public class Package : IPackage
{
	public Package(string folder, bool builtIn, bool workshop)
	{
		Name = string.Empty;
		Folder = folder;
		BuiltIn = builtIn;
		Workshop = workshop;

		if (workshop)
		{
			SteamId = ulong.Parse(Path.GetFileName(folder));
			SteamPage = $"https://steamcommunity.com/workshop/filedetails/?id={SteamId}";
		}

		if (!string.IsNullOrWhiteSpace(LocationManager.VirtualGamePath) && Folder.Contains(LocationManager.GamePath))
		{
			VirtualFolder = Folder.Replace(LocationManager.GamePath, LocationManager.VirtualGamePath).Replace("\\", "/");
		}

		if (!string.IsNullOrWhiteSpace(LocationManager.VirtualWorkshopContentPath) && Folder.Contains(LocationManager.WorkshopContentPath))
		{
			VirtualFolder = Folder.Replace(LocationManager.WorkshopContentPath, LocationManager.VirtualWorkshopContentPath).Replace("\\", "/");
		}

		if (!string.IsNullOrWhiteSpace(LocationManager.VirtualAppDataPath) && Folder.Contains(LocationManager.AppDataPath))
		{
			VirtualFolder = Folder.Replace(LocationManager.AppDataPath, LocationManager.VirtualAppDataPath).Replace("\\", "/");
		}
	}

	public Asset[]? Assets { get; set; }
	public Mod? Mod { get; set; }

	public long LocalSize => ContentUtil.GetTotalSize(Folder);
	public DateTime LocalTime => ContentUtil.GetLocalUpdatedTime(Folder).ToUniversalTime();

	public string? VirtualFolder { get; set; }
	public string Folder { get; set; }
	public ulong SteamId { get; set; }
	public bool BuiltIn { get; set; }
	public bool Workshop { get; set; }
	public string? SteamPage { get; set; }
	public SteamUser? Author { get; set; }
	public string? Class { get; set; }
	public Bitmap? IconImage { get; set; }
	public Bitmap? AuthorIconImage { get; set; }
	public string? IconUrl { get; set; }
	public string Name { get; set; }
	public bool RemovedFromSteam { get; set; }
	public long ServerSize { get; set; }
	public DateTime ServerTime { get; set; }
	public DownloadStatus Status { get; set; }
	public string? StatusReason { get; set; }
	public bool SteamInfoLoaded { get; set; }
	public string[]? Tags { get; set; }
	public string? SteamDescription { get; set; }

	internal CompatibilityManager.ReportInfo? CompatibilityReport => CompatibilityManager.GetCompatibilityReport(this);
	Package IPackage.Package => this;
	public bool IsIncluded => (Mod?.IsIncluded ?? false) && (Assets?.All(x => x.IsIncluded) ?? false);

	public bool IsRequired { get; set; }

	public override string ToString()
	{
		if (!string.IsNullOrEmpty(Name))
		{ return Name; }

		if (!string.IsNullOrEmpty(Mod?.Name))
		{ return Mod!.Name; }

		return Path.GetFileNameWithoutExtension(Folder).FormatWords();
	}
}