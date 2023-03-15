﻿using Extensions;

using LoadOrderToolTwo.ColossalOrder;
using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Drawing;
using System.IO;

namespace LoadOrderToolTwo.Domain;
public class Mod : IPackage
{
	private readonly string _dllName;
	public Mod(Package package, string dllPath, Version version)
	{
		Package = package;
		FileName = dllPath;
		Version = version;
		_dllName = Path.GetFileNameWithoutExtension(dllPath).FormatWords();
	}

	public string FileName { get; }
	public Version Version { get; }
	public Package Package { get; }
	public string Name { get => Package.Name.IfEmpty(_dllName); set => Package.Name = value; }

	public bool IsIncluded { get => ModsUtil.IsIncluded(this); set => ModsUtil.SetIncluded(this, value); }
	public bool IsEnabled { get => ModsUtil.IsEnabled(this); set => ModsUtil.SetEnabled(this, value); }

	public string Folder => ((IPackage)Package).Folder;
	public string? VirtualFolder => ((IPackage)Package).VirtualFolder;
	public bool BuiltIn => ((IPackage)Package).BuiltIn;
	public ulong SteamId => ((IPackage)Package).SteamId;
	public string? SteamPage => ((IPackage)Package).SteamPage;
	public bool Workshop => ((IPackage)Package).Workshop;
	public SteamUser? Author { get => ((IPackage)Package).Author; set => ((IPackage)Package).Author = value; }
	public string? Class { get => ((IPackage)Package).Class; set => ((IPackage)Package).Class = value; }
	public Bitmap? IconImage { get => ((IPackage)Package).IconImage; }
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
	public Bitmap? AuthorIconImage { get => ((IPackage)Package).AuthorIconImage; }
	public bool IsRequired { get => ((IPackage)Package).IsRequired; set => ((IPackage)Package).IsRequired = value; }

	public override string ToString() => Name;
}
