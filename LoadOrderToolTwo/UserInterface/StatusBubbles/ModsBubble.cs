﻿using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;

internal class ModsBubble : StatusBubbleBase
{
	public ModsBubble()
	{ }

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (!Live)
		{
			return;
		}

		Image = Properties.Resources.I_Mods;
		Text = Locale.ModsBubble;

		if (!CentralManager.IsContentLoaded)
		{
			Loading = true;

			CentralManager.ContentLoaded += Invalidate;
		}

		CentralManager.WorkshopInfoUpdated += CentralManager_WorkshopInfoUpdated;
		CentralManager.ModInformationUpdated += _ => Invalidate();
	}

	private void CentralManager_WorkshopInfoUpdated()
	{
		if (Loading)
		{
			Loading = false;
		}
		else
		{
			Invalidate();
		}
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		if (!CentralManager.IsContentLoaded)
		{
			DrawText(e, ref targetHeight, Locale.Loading, FormDesign.Design.InfoColor);
			return;
		}

		var allIncludedModsAreAlsoEnabled = CentralManager.Mods.All(x => x.IsIncluded == x.IsEnabled);
		var modsIncluded = CentralManager.Mods.Count(x => x.IsIncluded);
		var modsEnabled = CentralManager.Mods.Count(x => x.IsEnabled);
		var modsOutOfDate = CentralManager.Mods.Count(x => x.IsIncluded && x.Status == Domain.DownloadStatus.OutOfDate);
		var modsIncomplete = CentralManager.Mods.Count(x => x.IsIncluded && x.Status == Domain.DownloadStatus.PartiallyDownloaded);
		var multipleModsIncluded = ModsUtil.GetDuplicateMods().Any();

		if (allIncludedModsAreAlsoEnabled)
		{
			DrawValue(e, ref targetHeight, modsIncluded.ToString(), modsIncluded == 1 ? Locale.ModIncludedAndEnabled : Locale.ModIncludedAndEnabledPlural);
		}
		else
		{
			DrawValue(e, ref targetHeight, modsIncluded.ToString(), modsIncluded == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural);
			DrawValue(e, ref targetHeight, modsEnabled.ToString(), modsEnabled == 1 ? Locale.ModEnabled : Locale.ModEnabledPlural);
		}

		if (modsOutOfDate > 0)
		{
			DrawValue(e, ref targetHeight, modsOutOfDate.ToString(), modsOutOfDate == 1 ? Locale.ModOutOfDate : Locale.ModOutOfDatePlural, FormDesign.Design.YellowColor);
		}

		if (modsIncomplete > 0)
		{
			DrawValue(e, ref targetHeight, modsIncomplete.ToString(), modsIncomplete == 1 ? Locale.ModIncomplete : Locale.ModIncompletePlural, FormDesign.Design.YellowColor);
		}

		if (multipleModsIncluded)
		{
			DrawText(e, ref targetHeight, Locale.MultipleModsIncluded, FormDesign.Design.RedColor);
		}
	}
}
