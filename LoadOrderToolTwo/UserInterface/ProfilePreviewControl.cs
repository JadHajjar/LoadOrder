using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.UserInterface.StatusBubbles;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class ProfilePreviewControl : StatusBubbleBase
{
    public ProfilePreviewControl(Profile profile)
    {
		Profile = profile;
		Image = Properties.Resources.I_ProfileSettings;
		Text = profile.Name;
	}

	public Profile Profile { get; }

	protected override void UIChanged()
	{
		base.UIChanged();

		Padding = UI.Scale(new Padding(7), UI.FontScale);
		Margin = UI.Scale(new Padding(10), UI.FontScale);
		Size = UI.Scale(new Size(300, 50), UI.FontScale);
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		var y = targetHeight;
		DrawValue(e, ref targetHeight, Profile.Mods.Count.ToString(), Profile.Mods.Count == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural);
		DrawValue(e, ref y, Profile.Assets.Count.ToString(), Profile.Assets.Count == 1 ? Locale.AssetIncluded : Locale.AssetIncludedPlural, x: Width / 2);

		if (Profile.IsMissingItems)
		{
			DrawText(e, ref targetHeight, Locale.IncludesItemsYouDoNotHave, FormDesign.Design.RedColor);
		}
	}
}
