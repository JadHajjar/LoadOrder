using Extensions;

using LoadOrderToolTwo.Utilities;

using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;

internal class ModsBubble : StatusBubbleBase
{
	public ModsBubble()
	{
		Image = Properties.Resources.I_Mods;
		Text = Locale.ModsBubble;
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		var modsIncluded = 24;
		var modsOutOfDate = 1;

		DrawValue(e, ref targetHeight, modsIncluded.ToString(), modsIncluded == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural);
		DrawValue(e, ref targetHeight, modsOutOfDate.ToString(), modsOutOfDate == 1 ? Locale.ModOutOfDate : Locale.ModOutOfDatePlural, FormDesign.Design.YellowColor);
		DrawText(e, ref targetHeight, Locale.MultipleModsIncluded, FormDesign.Design.RedColor);
	}
}
