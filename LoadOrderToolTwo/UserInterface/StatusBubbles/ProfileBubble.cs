using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System.Drawing;
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
		DrawText(e, ref targetHeight, CentralManager.CurrentProfile.Name);
	}
}
