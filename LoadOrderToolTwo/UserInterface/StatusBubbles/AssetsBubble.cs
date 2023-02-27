using LoadOrderToolTwo.Utilities;

using System;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;

internal class AssetsBubble : StatusBubbleBase
{
	public AssetsBubble()
	{
		Image = Properties.Resources.I_Assets;
		Text = Locale.AssetsBubble;
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		DrawValue(e, ref targetHeight, "7152", "assets included");
		DrawValue(e, ref targetHeight, "16.41 GB", "total size");
	}
}
