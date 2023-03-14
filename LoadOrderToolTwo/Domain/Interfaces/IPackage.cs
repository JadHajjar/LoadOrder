﻿using LoadOrderToolTwo.Domain.Steam;

using System;
using System.Drawing;

namespace LoadOrderToolTwo.Domain.Interfaces;

public interface IPackage
{
	Package Package { get; }
	SteamUser? Author { get; set; }
	bool BuiltIn { get; }
	string? Class { get; set; }
	string Folder { get; }
	string? VirtualFolder { get; }
	Bitmap? IconImage { get; set; }
	Bitmap? AuthorIconImage { get; set; }
	string? IconUrl { get; set; }
	long LocalSize { get; }
	DateTime LocalTime { get; }
	string Name { get; set; }
	bool RemovedFromSteam { get; set; }
	long ServerSize { get; set; }
	DateTime ServerTime { get; set; }
	DownloadStatus Status { get; set; }
	string? StatusReason { get; set; }
	ulong SteamId { get; }
	bool SteamInfoLoaded { get; set; }
	string? SteamDescription { get; set; }
	string? SteamPage { get; }
	string[]? Tags { get; set; }
	bool Workshop { get; }
	bool IsIncluded { get; }
	bool IsRequired { get; set; }
}