using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;

internal class CompatibilityMessageControl : Label
{
	public CompatibilityMessageControl(CompatibilityManager.Message message)
	{
		AutoSize = true;
		Text = message.message;
	}
}