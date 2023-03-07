using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class PackageIcon : SlickImageControl
{
	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);

		e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentBackColor), new Rectangle(0, Height / 2, Width, Height / 2));

		if (Loading)
		{
			DrawLoader(e.Graphics, ClientRectangle.CenterR(UI.Scale(new Size(32, 32), UI.UIScale)));
			return;
		}

		e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

		if (Image == null)
		{
			using var image = new Bitmap(Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor));
			using var texture = new TextureBrush(image);
			var iconRect = ClientRectangle.CenterR(image.Size);

			e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.IconColor), ClientRectangle, (int)(10 * UI.FontScale));
			e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.BackColor), iconRect.Pad(1), (int)(10 * UI.FontScale));

			e.Graphics.TranslateTransform(iconRect.X, iconRect.Y);
			e.Graphics.FillRoundedRectangle(texture, new Rectangle(Point.Empty, iconRect.Size), (int)(10 * UI.FontScale));
		}
		else
		{
			using var image = new Bitmap(Image ?? Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor), Size);
			using var texture = new TextureBrush(image);

			e.Graphics.FillRoundedRectangle(texture, ClientRectangle, (int)(10 * UI.FontScale)); 
		}
	}
}
