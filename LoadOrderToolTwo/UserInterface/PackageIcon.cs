using Extensions;

using LoadOrderToolTwo.Domain;

using SlickControls;

using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class PackageIcon : SlickImageControl
{
	public Package? Package { get; set; }

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(FormDesign.Design.BackColor);

		e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentBackColor), new Rectangle(0, 0, Width, Height / 2));

		if (Loading)
		{
			DrawLoader(e.Graphics, ClientRectangle.CenterR(UI.Scale(new Size(32, 32), UI.UIScale)));
			return;
		}

		e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

		if (Image == null)
		{
			using var image = new Bitmap((Package?.Mod is not null ? Properties.Resources.I_ModIcon : Properties.Resources.I_AssetIcon).Color(FormDesign.Design.IconColor));
			using var texture = new TextureBrush(image);
			var iconRect = ClientRectangle.CenterR(image.Size);

			e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.IconColor), ClientRectangle, (int)(10 * UI.FontScale));
			e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.BackColor), iconRect.Pad(1), (int)(10 * UI.FontScale));

			e.Graphics.TranslateTransform(iconRect.X, iconRect.Y);
			e.Graphics.FillRoundedRectangle(texture, new Rectangle(Point.Empty, iconRect.Size), (int)(10 * UI.FontScale));
		}
		else
		{
			using var image = new Bitmap(Image, Size);
			using var texture = new TextureBrush(image);

			e.Graphics.FillRoundedRectangle(texture, ClientRectangle, (int)(10 * UI.FontScale));
		}
	}
}
