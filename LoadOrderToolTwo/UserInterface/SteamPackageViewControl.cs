using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Domain.Steam.Markdown;
using LoadOrderToolTwo.Utilities.Managers;

using Newtonsoft.Json.Serialization;

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

		var bb=BBCode.Parse(item.Description);

		LoadImage(item.PreviewURL, ImageManager.GetImage);
	}

	public SteamWorkshopItem Item { get; }

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(5), UI.FontScale);
		Height = (int)(64 * UI.FontScale);
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);

		e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
		e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

		var iconRect = new Rectangle(Padding.Left, Padding.Top, Height - Padding.Vertical, Height - Padding.Vertical);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRect.CenterR(UI.Scale(new Size(32, 32), UI.FontScale)));
		}
		else
		{
			e.Graphics.DrawRoundedImage(Image, iconRect, Padding.Left, FormDesign.Design.IconColor);
		}

		e.Graphics.DrawString(Item.Title, Font, new SolidBrush(ForeColor), ClientRectangle.Pad(Padding.Horizontal + iconRect.Width, 0, 0, 0));
	}
}
