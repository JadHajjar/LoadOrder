using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;

internal class ModsBubble : StatusBubbleBase
{
	public ModsBubble()
	{
		Image = Properties.Resources.I_Mods;
		Text = Locale.ModsBubble;
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (CentralManager.Mods == null)
		{
			Loading = true;

			CentralManager.ModsLoaded += CentralManager_ModsLoaded;
		}

		CentralManager.ModsUpdated += CentralManager_ModsLoaded;
	}

	private void CentralManager_ModsLoaded()
	{
		if (Loading)
			Loading = false;
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		if (CentralManager.Mods == null)
		{
			DrawText(e, ref targetHeight, Locale.Loading, FormDesign.Design.InfoColor);
			return;
		}

		var modsIncluded = CentralManager.Mods.Count(x => !x.Included);
		var modsOutOfDate = 1;

		DrawValue(e, ref targetHeight, modsIncluded.ToString(), modsIncluded == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural);
		DrawValue(e, ref targetHeight, modsOutOfDate.ToString(), modsOutOfDate == 1 ? Locale.ModOutOfDate : Locale.ModOutOfDatePlural, FormDesign.Design.YellowColor);
		DrawText(e, ref targetHeight, Locale.MultipleModsIncluded, FormDesign.Design.RedColor);
	}
}
