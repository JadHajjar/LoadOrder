using Extensions;

using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;
internal class ProfileBubble : StatusBubbleBase
{
	public ProfileBubble()
	{
		Image = Properties.Resources.I_ProfileSettings;
		Text = Locale.ProfileBubble;
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		SlickButton.GetColors(out var fore, out _, HoverState);
		
		e.Graphics.DrawString("Asset Editor", UI.Font(9.75F), new SolidBrush(fore),Padding.Left, targetHeight);

		targetHeight += (int)e.Graphics.Measure("Asset Editor", UI.Font(9.75F)).Height + Padding.Bottom;
	}
}
