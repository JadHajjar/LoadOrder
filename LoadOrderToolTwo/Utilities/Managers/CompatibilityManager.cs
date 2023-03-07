using CompatibilityReport.CatalogData;

using Extensions;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace LoadOrderToolTwo.Utilities.Managers;
#nullable disable
#pragma warning disable CS0649 // Never Used
#pragma warning disable IDE1006 // Naming Styles
internal class CompatibilityManager
{
	internal static Catalog? Catalog { get; private set; }
	internal static bool CatalogAvailable { get; private set; }

	internal static void LoadCompatibilityReport(Domain.Package compatibilityReport)
	{
		try
		{
			var file = Directory.GetFiles(compatibilityReport.Folder, "*.gz").FirstOrDefault();

			if (file == null)
			{
				return;
			}

			Catalog = ReadGzFile(file);

			if (Catalog != null)
			{
				Catalog.CreateIndexes();

				CatalogAvailable = true;
			}
		}
		catch { }
	}

	private static Catalog? ReadGzFile(string filePath)
	{
		using var fileStream = new FileStream(filePath, FileMode.Open);
		using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
		using var reader = new StreamReader(gzipStream);

		var ser = new XmlSerializer(typeof(Catalog));

		return ser.Deserialize(reader) as Catalog;
	}

	internal static ModInfo? GetCompatibilityReport(Domain.Package package)
	{
		if (Catalog is null)
		{
			return null;
		}

		var subscribedMod = Catalog.GetMod(package.SteamId);

		if (subscribedMod is null || package.Mod is null)
		{
			return null;
		}

		subscribedMod = subscribedMod.Clone();

		subscribedMod.UpdateSubscription(!package.Mod.IsEnabled, package.Mod.IsIncluded, false, package.Folder, package.LocalTime);

		var subscriptionAuthor = Catalog.GetAuthor(subscribedMod.AuthorID, subscribedMod.AuthorUrl);
		var authorName = subscriptionAuthor == null ? package.Author?.Name ?? "" : subscriptionAuthor.Name;

		var modInfo = new ModInfo
		{
			authorName = authorName,
			modName = subscribedMod.Name,
			isDisabled = subscribedMod.IsDisabled,
			isLocal = !package.Workshop,
			isCameraScript = subscribedMod.IsCameraScript
		};

		if (!modInfo.isLocal)
		{
			modInfo.instability = Instability(subscribedMod);
			modInfo.requiredDlc = RequiredDlc(subscribedMod);
			modInfo.unneededDependencyMod = UnneededDependencyMod2(subscribedMod);
			modInfo.disabled = Disabled(subscribedMod);
			modInfo.successors = Successors(subscribedMod);
			modInfo.stability = Stability(subscribedMod);
			modInfo.compatibilities = Compatibilities(subscribedMod);
			modInfo.requiredMods = RequiredMods(subscribedMod);
			modInfo.statuses = Statuses(subscribedMod, authorRetired: subscriptionAuthor != null && subscriptionAuthor.Retired);
			var note = ModNote(subscribedMod);
			modInfo.note = note.Value;
			modInfo.noteLocaleId = note.Id;
			modInfo.alternatives = Alternatives(subscribedMod);
			modInfo.reportSeverity = subscribedMod.ReportSeverity;
			modInfo.recommendations = subscribedMod.ReportSeverity <= Enums.ReportSeverity.MajorIssues ? Recommendations2(subscribedMod) : null;
			modInfo.anyIssues = subscribedMod.ReportSeverity == Enums.ReportSeverity.NothingToReport && subscribedMod.Stability > Enums.Stability.NotReviewed;
			modInfo.isCameraScript = subscribedMod.IsCameraScript;
			modInfo.steamUrl = package.SteamPage;
		}

		return modInfo;
	}

	private static string SeverityToText(Enums.ReportSeverity severity)
	{
		return severity switch
		{
			Enums.ReportSeverity.NothingToReport => "Nothing to report",
			Enums.ReportSeverity.Remarks => "Remarks",
			Enums.ReportSeverity.MinorIssues => "Minor issues",
			Enums.ReportSeverity.MajorIssues => "Major issues",
			Enums.ReportSeverity.Unsubscribe => "Unsubscribe",
			_ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null),
		};
	}

	private static string SeverityToLocaleId(Enums.ReportSeverity severity)
	{
		return severity switch
		{
			Enums.ReportSeverity.NothingToReport => "HRT_LIL_NTR",
			Enums.ReportSeverity.Remarks => "HRT_LIL_R",
			Enums.ReportSeverity.MinorIssues => "HRT_LIL_MI",
			Enums.ReportSeverity.MajorIssues => "HRT_LIL_MAI",
			Enums.ReportSeverity.Unsubscribe => "HRT_LIL_U",
			_ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null),
		};
	}

	/// <summary>Creates report message for stability issues of a mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>Only reports major issues and worse.</remarks>
	/// <returns>Text wrapped in Message object.</returns>
	private static Message Instability(Mod subscribedMod)
	{
		switch (subscribedMod.Stability)
		{
			case Enums.Stability.IncompatibleAccordingToWorkshop:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
				return new Message() { message = "UNSUBSCRIBE! This mod is totally incompatible with the current game version.", messageLocaleId = "HRTC_I_IATW", details = subscribedMod.StabilityNote.Value, detailsLocaleId = subscribedMod.StabilityNote.Id };

			case Enums.Stability.RequiresIncompatibleMod:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
				return new Message() { message = "UNSUBSCRIBE! This requires a mod that is totally incompatible with the current game version.", messageLocaleId = "HRTC_I_RIM", details = subscribedMod.StabilityNote.Value, detailsLocaleId = subscribedMod.StabilityNote.Id };

			case Enums.Stability.GameBreaking:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
				return new Message() { message = "UNSUBSCRIBE! This mod breaks the game.", messageLocaleId = "HRTC_I_GB", details = subscribedMod.StabilityNote.Value, detailsLocaleId = subscribedMod.StabilityNote.Id };

			case Enums.Stability.Broken:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
				return new Message() { message = "Unsubscribe! This mod is broken.", messageLocaleId = "HRTC_I_B", details = subscribedMod.StabilityNote.Value, detailsLocaleId = subscribedMod.StabilityNote.Id };

			case Enums.Stability.MajorIssues:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);
				return new Message() { message = $"Unsubscribe would be wise. This has major issues{(string.IsNullOrEmpty(subscribedMod.StabilityNote.Value) ? ". Check its Workshop page for details." : ":")}", messageLocaleId = "HRTC_I_MAI", localeIdVariables = $"StabilityNote█{subscribedMod.StabilityNote}", details = subscribedMod.StabilityNote.Value, detailsLocaleId = subscribedMod.StabilityNote.Id };

			default:
				return null;
		}
	}

	/// <summary>Creates report message for the stability of a mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>Only reports minor issues and better.</remarks>
	/// <returns>Text wrapped in Message object.</returns>
	private static Message Stability(Mod subscribedMod)
	{
		switch (subscribedMod.Stability)
		{
			case Enums.Stability.MinorIssues:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);
				var hasNote = !string.IsNullOrEmpty(subscribedMod.StabilityNote.Value) || !string.IsNullOrEmpty(subscribedMod.StabilityNote.Id);
				return new Message()
				{
					message = $"This has minor issues{(!hasNote ? ". Check its Workshop page for details." : ":")}",
					messageLocaleId = $"{(!hasNote ? "HRTC_S_MI" : "HRTC_S_MIS")}",
					localeIdVariables = "StabilityNote█",
					details = subscribedMod.StabilityNote.Value,
					detailsLocaleId = $"{(hasNote ? $"{subscribedMod.StabilityNote.Id}" : "HRTC_I_MAIC")}"
				};

			case Enums.Stability.UsersReportIssues:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);
				var hasNote2 = !string.IsNullOrEmpty(subscribedMod.StabilityNote.Value) || !string.IsNullOrEmpty(subscribedMod.StabilityNote.Id);
				return new Message()
				{
					message = $"Users are reporting issues{(!hasNote2 ? ". Check its Workshop page for details." : ": ")}",
					messageLocaleId = $"{(!hasNote2 ? "HRTC_S_URI" : "HRTC_S_URS")}",
					localeIdVariables = $"StabilityNote█",
					details = subscribedMod.StabilityNote.Value,
					detailsLocaleId = $"{(hasNote2 ? $"{subscribedMod.StabilityNote.Id}" : "HRTC_I_MAIC")}"
				};
			case Enums.Stability.NotEnoughInformation:
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
				var updatedText = subscribedMod.GameVersion() < CurrentMajorGameVersion()
					? "."
					: subscribedMod.GameVersion() == CurrentGameVersion()
						? ", but it was updated for the current game version."
						: $", but it was updated for game version {subscribedMod.GameVersion().ToString(2)}.";
				return new Message()
				{
					message = $"There is not enough information about this mod to know if it works well{updatedText}",
					messageLocaleId = "HRTC_S_NEI",
					localeIdVariables = $"█{(subscribedMod.GameVersion() == CurrentGameVersion() ? "HRTC_S_NRC" : "")}",
					details = subscribedMod.StabilityNote.Value,
					detailsLocaleId = subscribedMod.StabilityNote.Id
				};
			case Enums.Stability.Stable:
				subscribedMod.IncreaseReportSeverity((string.IsNullOrEmpty(subscribedMod.StabilityNote.Value) && string.IsNullOrEmpty(subscribedMod.StabilityNote.Id)) ? Enums.ReportSeverity.NothingToReport : Enums.ReportSeverity.Remarks);
				return new Message()
				{
					message = $"This is compatible with the current game version.",
					messageLocaleId = "HRTC_S_S",
					details = subscribedMod.StabilityNote.Value,
					detailsLocaleId = subscribedMod.StabilityNote.Id
				};
			case Enums.Stability.NotReviewed:
			case Enums.Stability.Undefined:
				var updatedText2 = subscribedMod.GameVersion() < CurrentMajorGameVersion()
					? "."
					: subscribedMod.GameVersion() == CurrentGameVersion()
						? ", but it was updated for the current game version."
						: $", but it was updated for game version {subscribedMod.GameVersion().ToString(2)}.";
				return new Message() { message = $"This mod has not been reviewed yet{updatedText2}", messageLocaleId = "HRTC_S_NR", localeIdVariables = "" };
			default:
				return null;
		}
	}

	private static Version CurrentGameVersion()
	{
		return new Version(1, 16, 0, 3);
	}

	private static Version CurrentMajorGameVersion()
	{
		return new Version(1, 16);
	}

	/// <summary>Creates report MessageList object for the statuses of a mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>Also reported: retired author. DependencyMod has its own method. ModNamesDiffer is reported in the mod note (at Catalog.ScanSubscriptions()).
	///          Not reported: UnlistedInWorkshop, SourceObfuscated.</remarks>
	/// <returns>Message list, or an empty string if no reported status found.</returns>
	private static MessageList Statuses(Mod subscribedMod, bool authorRetired)
	{
		var nestedItem = new MessageList() { messages = new List<Message>() };

		if (subscribedMod.Statuses.Contains(Enums.Status.Obsolete))
		{
			subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
			nestedItem.title = "Unsubscribe this. It is no longer needed.";
			nestedItem.titleLocaleId = "HRTC_S_O";
		}
		else if (subscribedMod.Statuses.Contains(Enums.Status.RemovedFromWorkshop))
		{
			subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);
			nestedItem.title = "Unsubscribe would be wise. This is no longer available on the Steam Workshop.";
			nestedItem.titleLocaleId = "HRTC_S_RFW";
		}
		else if (subscribedMod.Statuses.Contains(Enums.Status.Deprecated))
		{
			subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);
			nestedItem.title = "This is deprecated, no longer supported by the author and a successor is available. Therefore it would be wise to unsubscribe this mod and subscribe the successor instead.";
			nestedItem.titleLocaleId = "HRTC_S_D";
		}
		else if (subscribedMod.Statuses.Contains(Enums.Status.Abandoned))
		{
			nestedItem.title = authorRetired
				? "This seems to be abandoned and the author seems retired. Future updates are unlikely."
				: "This seems to be abandoned. Future updates are unlikely.";
			nestedItem.titleLocaleId = authorRetired ? "HRTC_S_A" : "HRTC_S_NIAR";
		}
		else if (authorRetired)
		{
			nestedItem.title = "The author seems to be retired. Future updates are unlikely.";
			nestedItem.titleLocaleId = "HRTC_S_AR";
		}

		if (subscribedMod.ReportSeverity < Enums.ReportSeverity.Unsubscribe)
		{
			// Several statuses only listed if there are no breaking issues.
			if (subscribedMod.Statuses.Contains(Enums.Status.Reupload))
			{
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
				nestedItem.messages.Add(new Message() { message = "Unsubscribe this. It is a re-upload of another mod, use that one instead (or its successor).", messageLocaleId = "HRTC_RU_U" });
			}

			if (subscribedMod.Statuses.Contains(Enums.Status.NoDescription))
			{
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);
				nestedItem.messages.Add(new Message() { message = "This has no description on the Steam Workshop. Support from the author is unlikely.", messageLocaleId = "HRTC_ND_MI" });
			}

			if (subscribedMod.Statuses.Contains(Enums.Status.NoCommentSection))
			{
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);
				nestedItem.messages.Add(new Message()
				{
					message = "This mod has the comment section disabled on the Steam Workshop, making it hard to see if other users are experiencing issues. " +
					"Use with caution.",
					messageLocaleId = "HRTC_NCS_MI"
				});
			}

			if (subscribedMod.Statuses.Contains(Enums.Status.BreaksEditors))
			{
				subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
				nestedItem.messages.Add(new Message() { message = "If you use the asset editor and/or map editor, this may give serious issues.", messageLocaleId = "HRTC_BE_R" });
			}

			if (subscribedMod.Statuses.Contains(Enums.Status.ModForModders))
			{
				nestedItem.messages.Add(new Message() { message = "This is only needed for modders or asset creators. Regular users don't need this one. Such mods should not be activated if you're not in the Editor. It's even better to exclude such mods with Loading Order Tool or to unsubscribe from them if you want to play the normal game.", messageLocaleId = "HRTC_S_MFM" });
			}

			if (subscribedMod.Statuses.Contains(Enums.Status.TestVersion))
			{
				nestedItem.messages.Add(new Message()
				{
					message = "This is a test version" +
					(subscribedMod.Alternatives.Any() ? ". If you don't have a specific reason to use it, you'd better use the stable version instead." :
					subscribedMod.Stability == Enums.Stability.Stable ? ", but is considered quite stable." : "."),
					messageLocaleId = subscribedMod.Alternatives.Any() ? "HRTC_S_TVA" : subscribedMod.Stability == Enums.Stability.Stable ? "HRTC_SS_TV" : string.Empty
				});
			}
			// Changed to have only one Music Pack Copyright status
			if (subscribedMod.Statuses.Contains(Enums.Status.MusicCopyright))
			{
				nestedItem.messages.Add(new Message() { message = "As the included music might be copyright protected, it is better not to use Music Packs for streaming to avoid legal issues.", messageLocaleId = "HRTC_S_MC" });
			}
			//if (subscribedMod.Statuses.Contains(Enums.Status.MusicCopyrightFree))
			//{
			//    nestedItem.messages.Add(new Message(){message = "The included music is said to be copyright-free and safe for streaming. Some restrictions might still apply though.", messageLocaleId = "HRTC_S_MCF"});
			//}
			//else if (subscribedMod.Statuses.Contains(Enums.Status.MusicCopyrighted))
			//
			//{
			//    nestedItem.messages.Add(new Message(){message = "As the included music might be copyright protected, it is better not to use Music Packs for streaming to avoid legal issues.", messageLocaleId = "HRTC_S_MC"});
			//}
			//else if (subscribedMod.Statuses.Contains(Enums.Status.MusicCopyrightUnknown))
			//{
			//    nestedItem.messages.Add(new Message(){message = "This includes music with unknown copyright status. Safer not to use it for streaming.", messageLocaleId = "HRTC_S_MCU"});
			//}
		}

		if (subscribedMod.Statuses.Contains(Enums.Status.SavesCantLoadWithout))
		{
			nestedItem.messages.Add(new Message() { message = "NOTE: After using this mod, savegames won't (easily) load without it anymore.", messageLocaleId = "HRTC_S_SCLW" });
		}

		//added to inform about non public source code
		if (subscribedMod.Statuses.Contains(Enums.Status.SourceNotPublic))
		{
			nestedItem.messages.Add(new Message() { message = "No source code published. Without source code it is harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_S_SCNP" });
		}

		// added to inform about mods that have no comment section active, no workshop description and no source code available
		if (subscribedMod.Statuses.Contains(Enums.Status.NoDescription) && !subscribedMod.Statuses.Contains(Enums.Status.NoCommentSection) && !subscribedMod.Statuses.Contains(Enums.Status.SourceNotPublic))
		{
			nestedItem.messages.Add(new Message() { message = "Mods without source codes, without comment section and with an unclear description should better not be subscribed. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SDCP" });
		}
		else if (subscribedMod.Statuses.Contains(Enums.Status.NoCommentSection) && !subscribedMod.Statuses.Contains(Enums.Status.SourceNotPublic))
		{
			nestedItem.messages.Add(new Message() { message = "Mods without source codes and without comment section should better not be subscribed. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SCP" });
		}
		else if (subscribedMod.Statuses.Contains(Enums.Status.NoDescription) && !subscribedMod.Statuses.Contains(Enums.Status.SourceNotPublic))
		{
			nestedItem.messages.Add(new Message() { message = "Mods without source codes and with an unclear description should better not be subscribed. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SDP" });
		}

		var abandoned = subscribedMod.Statuses.Contains(Enums.Status.Obsolete) || subscribedMod.Statuses.Contains(Enums.Status.Deprecated) ||
			subscribedMod.Statuses.Contains(Enums.Status.RemovedFromWorkshop) || subscribedMod.Statuses.Contains(Enums.Status.Abandoned) ||
			(subscribedMod.Stability == Enums.Stability.IncompatibleAccordingToWorkshop) || authorRetired;

		if (abandoned && string.IsNullOrEmpty(subscribedMod.SourceUrl) && subscribedMod.Statuses.Contains(Enums.Status.SourceBundled))
		{
			nestedItem.messages.Add(new Message() { message = "Author activated that source code should be bundled with the mod but is not published. This make it harder to continue by another modder. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SURL" });
		}
		else if (abandoned && subscribedMod.Statuses.Contains(Enums.Status.SourceNotPublic))
		{
			nestedItem.messages.Add(new Message() { message = "No source code published, making it hard to continue by another modder. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SNU" });
		}
		else if (abandoned && subscribedMod.Statuses.Contains(Enums.Status.SourceObfuscated))
		{
			nestedItem.messages.Add(new Message() { message = "The author has deliberately hidden the mod code from other modders, which is somewhat suspicious. Without source code it is also harder to ensure a mod not include malicious code.", messageLocaleId = "HRTC_A_SOB" });
		}

		if (!string.IsNullOrEmpty(nestedItem.title) || nestedItem.messages.Count > 0)
		{
			subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
		}

		return nestedItem;
	}

	/// <summary>Creates report text for an unneeded dependency mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>If this mod is a member of a group, all group members are considered for this check.</remarks>
	/// <returns>Text wrapped in Message object or null if this is not a dependency mod or if another subscription has this mod as required.</returns>
	private static Message UnneededDependencyMod2(Mod subscribedMod)
	{
		if (!subscribedMod.Statuses.Contains(Enums.Status.DependencyMod))
		{
			return null;
		}

		// Check if any of the mods that need this is actually subscribed, enabled or not. If this is in a group, check all group members. Exit if any is needed.
		if (Catalog.IsGroupMember(subscribedMod.SteamID))
		{
			foreach (var groupMemberID in Catalog.GetThisModsGroup(subscribedMod.SteamID).GroupMembers)
			{
				if (IsModNeeded(groupMemberID))
				{
					// Group member is needed. No need to check other group members.
					return null;
				}
			}
		}
		else if (IsModNeeded(subscribedMod.SteamID))
		{
			return null;
		}
		return null;

		//if (Catalog.IsValidID(ModSettings.LowestLocalModID))
		//{
		//	subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);

		//	return new Message()
		//	{
		//		message = "Unsubscribe this unless it's needed for one of your local mods. " +
		//			"None of your Steam Workshop mods need this, and it doesn't provide any functionality on its own.",
		//		messageLocaleId = "HRTC_IMN_RL|HRTC_IMN_R"
		//	};
		//}
		//else
		//{
		//	subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
		//	return new Message() { message = "Unsubscribe this. It is only needed for mods you don't have, and it doesn't provide any functionality on its own.", messageLocaleId = "HRTC_IMN_U" };
		//}
	}


	/// <summary>Checks if any of the mods that need this is actually subscribed, enabled or not.</summary>
	/// <returns>True if a mod needs this, otherwise false.</returns>
	private static bool IsModNeeded(ulong SteamID)
	{
		// Check if any of the mods that need this is actually subscribed, enabled or not.
		var ModsRequiringThis = Catalog.Mods.FindAll(x => x.RequiredMods.Contains(SteamID));

		foreach (var mod in ModsRequiringThis)
		{
			if (Catalog.GetSubscription(mod.SteamID) != null)
			{
				// Found a subscribed mod that needs this.
				return true;
			}
		}

		return false;
	}

	/// <summary>Creates report text for a disabled mod and increases the report severity for the mod if appropriate.</summary>
	/// <returns>Text wrapped in Message object, or an empty string if not disabled or if this mod works while disabled.</returns>
	private static Message Disabled(Mod subscribedMod)
	{
		if (!subscribedMod.IsDisabled || subscribedMod.Statuses.Contains(Enums.Status.WorksWhenDisabled))
		{
			return null;
		}

		if (!subscribedMod.IsIncluded)
		{
			return null;
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);

		return new Message() { message = "Enable this if you want to use it, or unsubscribe it. Disabled mods can cause issues.", messageLocaleId = "HRTC_ID_U" };
	}


	/// <summary>Creates report text for a mod not and increases the report severity for the mod if appropriate.</summary>
	/// <returns>Formatted text, or an empty string if no mod note exists.</returns>
	private static ElementWithId ModNote(Mod subscribedMod)
	{
		if (subscribedMod.Note == null || string.IsNullOrEmpty(subscribedMod.Note.Value))
		{
			return new ElementWithId();
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);

		return subscribedMod.Note;
	}


	/// <summary>Creates report text for missing DLCs for a mod and increases the report severity for the mod if appropriate.</summary>
	/// <returns>Text wrapped in Message object, or null if no DLC is required or if all required DLCs are installed.</returns>
	private static MessageList RequiredDlc(Mod subscribedMod)
	{
		var dlcs = new MessageList()
		{
			title = "Unsubscribe this. It requires DLC you don't have:",
			titleLocaleId = "HRTC_RDLC_U",
			messages = new List<Message>()
		};

		//foreach (Enums.Dlc dlc in subscribedMod.RequiredDlcs)
		//{
		//	if (!PlatformService.IsDlcInstalled((uint)dlc))
		//	{
		//		// Add the missing DLC.
		//		dlcs.messages.Add(new Message() { message = ConvertDlcToString(dlc) });
		//	}
		//}

		if (dlcs.messages.Count == 0)
		{
			return null;
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);

		return dlcs;
	}

	/// <summary>Creates report text for missing 'required mods' for a mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>If a required mod is not subscribed but in a group, the other group members are checked. 
	///          Required mods that are disabled are mentioned as such.</remarks>
	/// <returns>MessageList object filled with messages, or null if this requires no other mods or all required mods are subscribed and enabled.</returns>
	private static MessageList RequiredMods(Mod subscribedMod)
	{
		var item = new MessageList()
		{
			title = "This mod requires other mods you don't have, or which are not enabled:",
			titleLocaleId = "HRTC_RM_SM",
			messages = new List<Message>()
		};

		foreach (var steamID in subscribedMod.RequiredMods)
		{
			if (Catalog.IsValidID(steamID))
			{
				var mod = ModAndGroupItem(steamID);
				if (mod != null)
				{
					item.messages.Add(mod);
				}
			}
			else
			{
				// Mod not found in the catalog, which should not happen unless bugs or a manual edit of the Catalog.
				//Logger.Log($"Required mod {steamID} not found in Catalog.", //Logger.Debug);
				item.messages.Add(new Message() { message = $"[Steam ID {steamID,10}]" });
			}
		}

		if (item.messages.Count == 0)
		{
			return null;
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);

		return item;
	}

	/// <summary>Creates report text for recommended mods for a mod and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>If a recommended mod is not subscribed but in a group, the other group members are checked. 
	///          Recommended mods that are disabled are mentioned as such.</remarks>
	/// <returns>MessageList object filled with messages, or null if this mod has no recommendations or all recommended mods are subscribed and enabled.</returns>
	private static MessageList Recommendations2(Mod subscribedMod)
	{
		var list = new MessageList
		{
			title = "The author or the users of this mod recommend using the following as well:",
			titleLocaleId = "HRTC_R2_SM",
			messages = new List<Message>()
		};

		foreach (var steamID in subscribedMod.Recommendations)
		{
			if (Catalog.IsValidID(steamID))
			{
				var item = ModAndGroupItem(steamID);
				if (item != null)
				{
					list.messages.Add(item);
				}
			}
			else
			{
				// Mod not found in the catalog, which should not happen unless bugs or a manual edit of the Catalog.
				//Logger.Log($"Recommended mod {steamID} not found in Catalog.", //Logger.Debug);

				list.messages.Add(new Message() { message = $"[Steam ID {steamID,10}]" });
			}
		}

		if (list.messages.Count == 0)
		{
			return null;
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);

		return list;
	}

	/// <summary>Checks if a mod or any member of its group is subscribed and enabled.</summary>
	/// <returns>A Message with text for the report, or null if the mod or another group member is subscribed and enabled.</returns>
	private static Message ModAndGroupItem(ulong steamID)
	{
		var catalogMod = Catalog.GetSubscription(steamID);

		if (catalogMod != null && (!catalogMod.IsDisabled || catalogMod.Statuses.Contains(Enums.Status.WorksWhenDisabled)))
		{
			// Mod is subscribed and enabled (or works when disabled). Don't report.
			return null;
		}
		catalogMod = Catalog.GetMod(steamID);

		if (catalogMod.IsDisabled)
		{
			// Mod is subscribed and disabled. Report as "missing", without Workshop page.
			return new Message() { message = Catalog.GetMod(steamID).ToString(hideFakeID: true, nameFirst: true, html: true) };
		}

		if (!Catalog.IsGroupMember(steamID))
		{
			// Mod is not subscribed and not in a group. Report as missing with Workshop page.
			return new Message() { message = catalogMod.ToString() };
		}

		// Mod is not subscribed but in a group. Check if another group member is subscribed.
		foreach (var groupMemberID in Catalog.GetThisModsGroup(steamID).GroupMembers)
		{
			var groupMember = Catalog.GetSubscription(groupMemberID);

			if (groupMember != null)
			{
				// Group member is subscribed. No need to check other group members, but report as "missing" if disabled (unless it works when disabled).
				if (!groupMember.IsDisabled || groupMember.Statuses.Contains(Enums.Status.WorksWhenDisabled))
				{
					return null;
				}
				return new Message() { message = groupMember.ToString(hideFakeID: true, nameFirst: true, html: true) };
			}
		}

		// No group member is subscribed. Report original mod as missing.
		return new Message() { message = catalogMod.ToString() };
	}

	/// <summary>Creates report text for successors of a mod and increases the report severity for the mod if appropriate.</summary>
	/// <returns>Text wrapped in Message object, or null if this mod has no successors.</returns>
	private static MessageList Successors(Mod subscribedMod)
	{
		if (!subscribedMod.Successors.Any())
		{
			return null;
		}

		var successors = new MessageList
		{
			title = (subscribedMod.Successors.Count == 1)
			? "The successor of this mod is:"
			: "This is succeeded by any of the following (pick one, not all):",
			titleLocaleId = (subscribedMod.Successors.Count == 1) ? "HRTC_S_SMC" : "HRTC_SS_SM"
		};

		var successorsCollection = new List<Message>();
		successors.messages = successorsCollection;

		foreach (var steamID in subscribedMod.Successors)
		{
			var s = new Message();
			if (Catalog.IsValidID(steamID))
			{
				if (Catalog.GetSubscription(steamID) != null)
				{
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);

					s.message = "Unsubscribe this. It is succeeded by a mod you already have:";
					s.messageLocaleId = "HRTC_S_U";
					s.details = Catalog.GetMod(steamID).ToString();
					s.detailsLocaleId = " ";
					s.detailsLocalized = Catalog.GetMod(steamID).ToString();
					s.detailsValue = Catalog.GetMod(steamID).ToString();
					successorsCollection.Add(s);
					return successors;
				}

				s.message = Catalog.GetMod(steamID).ToString();
				s.localeIdVariables = $"https://steamcommunity.com/sharedfiles/filedetails/?id={steamID}";
				successorsCollection.Add(s);
			}
			else
			{
				// Mod not found in the catalog, which should not happen unless bugs or a manual edit of the Catalog.
				//Logger.Log($"Successor mod {steamID} not found in Catalog.", //Logger.Debug);

				s.message = $"[Steam ID {steamID,10}]";
				successorsCollection.Add(s);
			}
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);

		return successors;
	}

	/// <summary>Creates report text for alternatives for a mod and increases the report severity for the mod if appropriate.</summary>
	/// <returns>Text wrapped in Message object, or null if this mod has no alternatives.</returns>
	private static MessageList Alternatives(Mod subscribedMod)
	{
		if (!subscribedMod.Alternatives.Any())
		{
			return null;
		}

		var result = new MessageList()
		{
			title = subscribedMod.Alternatives.Count == 1
				? "An alternative you could use:"
				: "Some alternatives for this are (pick one, not all):",
			titleLocaleId = subscribedMod.Alternatives.Count == 1 ? "HRTC_A_SMC" : "HRTC_A_SM",
			messages = new List<Message>()
		};

		foreach (var steamID in subscribedMod.Alternatives)
		{
			var alternativeMod = Catalog.GetSubscription(steamID);

			if (alternativeMod != null && (!alternativeMod.IsDisabled || alternativeMod.Statuses.Contains(Enums.Status.WorksWhenDisabled)))
			{
				// Already subscribed, don't report any.
				return null;
			}

			if (Catalog.IsValidID(steamID))
			{
				result.messages.Add(new Message() { message = Catalog.GetMod(steamID).ToString() });
			}
			else
			{
				// Mod not found in the catalog, which should not happen unless bugs or a manual edit of the Catalog.
				//Logger.Log($"Alternative mod {steamID} not found in Catalog.", //Logger.Debug);

				result.messages.Add(new Message() { message = $"[Steam ID {steamID,10}]" });
			}
		}

		subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);

		return result;
	}

	/// <summary>Creates report text for compatibility issues with other subscribed mods, and increases the report severity for the mod if appropriate.</summary>
	/// <remarks>Result could be multiple mods with multiple statuses. Not reported: CompatibleAccordingToAuthor.</remarks>
	/// <returns>Text wrapped in Message object, or null if there are no known compatibility issues.</returns>
	private static List<CompatibilityList> Compatibilities(Mod subscribedMod)
	{

		var result = new Dictionary<Enums.CompatibilityStatus, CompatibilityList>();

		foreach (var compatibility in Catalog.GetSubscriptionCompatibilities(subscribedMod.SteamID))
		{
			var otherModID = (subscribedMod.SteamID == compatibility.FirstModID) ? compatibility.SecondModID : compatibility.FirstModID;

			var otherMod = Catalog.GetMod(otherModID);
			if (subscribedMod.Successors.Contains(otherModID) || otherMod == null || otherMod.Successors.Contains(subscribedMod.SteamID))
			{
				// Don't mention the incompatibility if either mod is the others successor. The succeeded mod will already be mentioned in 'Unsubscribe' severity.
				continue;
			}

			var otherModString = Catalog.GetMod(otherModID).ToString();

			if (!result.TryGetValue(compatibility.Status, out var item))
			{
				item = new CompatibilityList();
				result.Add(compatibility.Status, item);
			}
			switch (compatibility.Status)
			{
				case Enums.CompatibilityStatus.SameModDifferentReleaseType:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "Unsubscribe either this or the other edition of the same mod:";
						item.messageLocaleId = "HRTC_IRS_SMDRT";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.SameFunctionality:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "Unsubscribe either this or the following incompatible mod with similar functionality:";
						item.messageLocaleId = "HRTC_IRS_SF";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.IncompatibleAccordingToAuthor:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Unsubscribe);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "Unsubscribe either this one or the following mod it's incompatible with:";
						item.messageLocaleId = "HRTC_IRS_IATA";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.IncompatibleAccordingToUsers:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "Users report an incompatibility with:";
						item.messageLocaleId = "HRTC_IRS_IATU";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = $"{compatibility.Note.Id}",
					});
					break;

				case Enums.CompatibilityStatus.MajorIssues:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MajorIssues);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "This has major issues with:";
						item.messageLocaleId = "HRTC_IRS_MAI";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.MinorIssues:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.MinorIssues);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "This has minor issues with:";
						item.messageLocaleId = "HRTC_IRS_MI";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.RequiresSpecificSettings:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "This requires specific configuration to work together with:";
						item.messageLocaleId = "HRTC_IRS_RSS";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.SameFunctionalityCompatible:
					subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
					if (string.IsNullOrEmpty(item.message))
					{
						item.message = "This has very similar functionality, but is still compatible with (do you need both?):";
						item.messageLocaleId = "HRTC_IRS_SFC";
						item.details = new List<Details>();
					}
					item.details.Add(new Details()
					{
						details = $"{otherModString} {compatibility.Note}",
						detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
						detailsLocaleId = compatibility.Note.Id,
					});
					break;

				case Enums.CompatibilityStatus.CompatibleAccordingToAuthor:
					if (compatibility.Note != null && !string.IsNullOrEmpty(compatibility.Note.Value))
					{
						subscribedMod.IncreaseReportSeverity(Enums.ReportSeverity.Remarks);
						if (string.IsNullOrEmpty(item.message))
						{
							item.message = "This is compatible with:";
							item.messageLocaleId = "HRTC_IRS_CATA";
							item.details = new List<Details>();
						}
						item.details.Add(new Details()
						{
							details = $"{otherModString} {compatibility.Note}",
							detailsLocalized = $"{otherModString} {(string.IsNullOrEmpty(compatibility.Note.Id) ? compatibility.Note.Value : string.Empty)}",
							detailsLocaleId = $"{compatibility.Note.Id}",
						});
					}
					break;

				default:
					break;
			}
		}

		return result.Values.ToList();
	}

	public static Version ConvertToVersion(string versionString)
	{
		try
		{
			var elements = versionString.Split(new char[] { '.', '-', 'f' }, StringSplitOptions.RemoveEmptyEntries);

			if (elements.Length >= 4)
			{
				return new Version(Convert.ToInt32(elements[0]), Convert.ToInt32(elements[1]), Convert.ToInt32(elements[2]), Convert.ToInt32(elements[3]));
			}
		}
		catch { }
		
		return new();
	}

	internal class ModInfo
	{
		public bool isLocal;
		public string authorName;
		public string modName;
		public string idString;
		public bool isDisabled;
		public bool isCameraScript;
		public Enums.ReportSeverity reportSeverity;
		public Message instability;
		public MessageList requiredDlc;
		public Message unneededDependencyMod;
		public Message disabled;
		public MessageList successors;
		public Message stability;
		public List<CompatibilityList> compatibilities;
		public MessageList requiredMods;
		public MessageList statuses;
		public string note;
		public string noteLocaleId;
		public MessageList alternatives;
		public MessageList recommendations;
		public bool anyIssues;
		public string steamUrl;
	}

	private class InstalledModInfo
	{
		public string disabled;
		public bool isSteam;

		public string subscriptionName;
		public string type;
		public string typeLocaleID;
		public string status;
		public string statusLocaleID;
		public string url;

		public InstalledModInfo(string subscriptionName, string disabled, string type, string typeLocaleID, string status, string statusLocaleID, bool isSteam, string url)
		{
			this.subscriptionName = subscriptionName;
			this.disabled = disabled;
			this.type = type;
			this.typeLocaleID = typeLocaleID;
			this.status = status;
			this.statusLocaleID = statusLocaleID;
			this.isSteam = isSteam;
			this.url = url;
		}
	}

	internal class MessageList
	{
		public string title;
		public string titleLocaleId;
		public List<Message> messages;
		public Dictionary<int, List<Message>> messageDictionary;
	}

	internal class CompatibilityList
	{
		public string message;
		public string messageLocaleId;
		public string messageLocalized;
		public List<Details> details;
	}

	internal class Message
	{
		public string message;
		public string messageLocaleId;
		public string messageLocalized;
		public string localeIdVariables;
		public string details;
		public string detailsLocaleId;
		public string detailsLocalized;
		public string detailsValue;
	}

	internal class Details
	{
		public string details;
		public string detailsLocaleId;
		public string detailsLocalized;
		public string detailsValue;
	}
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS0649 // Never Used
#nullable enable