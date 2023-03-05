using Extensions;

using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Utilities;

using System;
using System.Drawing;
using System.IO;

namespace LoadOrderToolTwo.Domain;
public class Asset : IPackage
{
	public Asset(Package package, string crpPath)
	{
		FileName = crpPath;
		Package = package;
		FileSize = new FileInfo(crpPath).Length;

		if (AssetsUtil.AssetInfoCache.TryGetValue(FileName, out var asset))
		{
			Name = asset.Name;
			Description = asset.Description;
		}
		else
		{
			Name = Path.GetFileNameWithoutExtension(crpPath).FormatWords();
		}
	}

	public string FileName { get; }
	public Package Package { get; }
	public long FileSize { get; }
	public string Name { get; set; }
	public string? Description { get; }
	public bool IsIncluded { get => AssetsUtil.IsIncluded(this); set => AssetsUtil.SetIncluded(this, value); }

	public string Folder => ((IPackage)Package).Folder;
	public bool BuiltIn => ((IPackage)Package).BuiltIn;
	public ulong SteamId => ((IPackage)Package).SteamId;
	public string? SteamPage => ((IPackage)Package).SteamPage;
	public bool Workshop => ((IPackage)Package).Workshop;
	public SteamUser? Author { get => ((IPackage)Package).Author; set => ((IPackage)Package).Author = value; }
	public string? Class { get => ((IPackage)Package).Class; set => ((IPackage)Package).Class = value; }
	public Bitmap? IconImage { get => ((IPackage)Package).IconImage; set => ((IPackage)Package).IconImage = value; }
	public string? IconUrl { get => ((IPackage)Package).IconUrl; set => ((IPackage)Package).IconUrl = value; }
	public long LocalSize { get => ((IPackage)Package).LocalSize; }
	public DateTime LocalTime { get => ((IPackage)Package).LocalTime; }
	public bool RemovedFromSteam { get => ((IPackage)Package).RemovedFromSteam; set => ((IPackage)Package).RemovedFromSteam = value; }
	public long ServerSize { get => ((IPackage)Package).ServerSize; set => ((IPackage)Package).ServerSize = value; }
	public DateTime ServerTime { get => ((IPackage)Package).ServerTime; set => ((IPackage)Package).ServerTime = value; }
	public DownloadStatus Status { get => ((IPackage)Package).Status; set => ((IPackage)Package).Status = value; }
	public string? StatusReason { get => ((IPackage)Package).StatusReason; set => ((IPackage)Package).StatusReason = value; }
	public bool SteamInfoLoaded { get => ((IPackage)Package).SteamInfoLoaded; set => ((IPackage)Package).SteamInfoLoaded = value; }
	public string[]? Tags { get => ((IPackage)Package).Tags; set => ((IPackage)Package).Tags = value; }
	public string? SteamDescription { get => ((IPackage)Package).SteamDescription; set => ((IPackage)Package).SteamDescription = value; }

	public string? VirtualFolder => ((IPackage)Package).VirtualFolder;

	public override string ToString()
	{
		return Name;
	}
}
