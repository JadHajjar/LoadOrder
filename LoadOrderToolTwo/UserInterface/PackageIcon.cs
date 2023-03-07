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
internal class PackageIcon : SlickPictureBox
{
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (!DesignMode)
		{
			Width = Height;
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);

		if (Loading || Image == null)
		{
			DrawLoader(e.Graphics, ClientRectangle.CenterR(UI.Scale(new Size(32, 32), UI.UIScale)));
			return;
		}

		e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentBackColor), new Rectangle(0, Height / 2, Width, Height / 2));

		using var image = new Bitmap(Image ?? Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor), Size);
		using var texture = new TextureBrush(image);

		e.Graphics.FillRoundedRectangle(texture, ClientRectangle, (int)(10 * UI.FontScale));
	}
}
