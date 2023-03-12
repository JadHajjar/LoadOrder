using Extensions;

using LoadOrderToolTwo.Domain.Steam;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class SteamPackageViewControl : SlickImageControl
{
	public SteamPackageViewControl(SteamWorkshopItem item)
	{
		Item = item;
		Anchor = AnchorStyles.Left | AnchorStyles.Right;

		LoadImage(item.PreviewURL);
	}

	public SteamWorkshopItem Item { get; }

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(5), UI.FontScale);
		Height = (int)(64 * UI.FontScale);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);

		var iconRect = new Rectangle(Padding.Left, Padding.Top, Height - Padding.Vertical, Height - Padding.Vertical);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRect.CenterR(UI.Scale(new Size(32, 32), UI.FontScale)));
		}
		else
		{
			e.Graphics.DrawRoundedImage(Image, iconRect, Padding.Left);
		}

		e.Graphics.DrawString(Item.Title, Font, new SolidBrush(ForeColor), ClientRectangle.Pad(Padding.Horizontal + iconRect.Width, 0, 0, 0));
	}
}
