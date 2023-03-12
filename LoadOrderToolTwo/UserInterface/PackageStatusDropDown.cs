using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;
using SlickControls.Controls.Form;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class PackageStatusDropDown : SlickSelectionDropDown<DownloadStatus>
{
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (Live)
		{
			Items = new[] { (DownloadStatus)(-1) }.Concat(Enum.GetValues(typeof(DownloadStatus)).Cast<DownloadStatus>()).ToArray();
		}
	}

	protected override void UIChanged()
	{
		Font = UI.Font(9.75F);
		Margin = UI.Scale(new Padding(5), UI.FontScale);
		Padding = UI.Scale(new Padding(5), UI.FontScale);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);

		Height = (int)(42 * UI.UIScale);
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, DownloadStatus item)
	{
		GetStatusDescriptors(item, out var text, out var icon, out var color);

		using (icon.Color(hoverState.HasFlag(HoverState.Pressed) ? foreColor : color))
		{
			e.Graphics.DrawImage(icon, rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));
			
			var textRect = new Rectangle(rectangle.X + icon.Width + Padding.Left, rectangle.Y + (rectangle.Height - Font.Height) / 2, 0, Font.Height);

			textRect.Width = rectangle.Width - textRect.X;

			e.Graphics.DrawString(text, Font, new SolidBrush(foreColor), textRect, new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
		}
	}

	private void GetStatusDescriptors(DownloadStatus status, out string text, out Bitmap icon, out Color color)
	{
		if ((int)status == -1)
		{
			text = Locale.AnyStatus;
			icon = ImageManager.GetIcon("I_Slash");
			color = FormDesign.Design.ForeColor;
			return;
		}

		switch (status)
		{
			case DownloadStatus.OK:
				text = Locale.UpToDate;
				icon = ImageManager.GetIcon("I_Ok");
				color = FormDesign.Design.GreenColor;
				return;
			case DownloadStatus.Unknown:
				text = Locale.StatusUnknown;
				icon = ImageManager.GetIcon("I_Question");
				color = FormDesign.Design.YellowColor;
				return;
			case DownloadStatus.OutOfDate:
				text = Locale.OutOfDate;
				icon = ImageManager.GetIcon("I_OutOfDate");
				color = FormDesign.Design.YellowColor;
				return;
			case DownloadStatus.NotDownloaded:
				text = Locale.ModIsNotDownloaded;
				icon = ImageManager.GetIcon("I_Question");
				color = FormDesign.Design.RedColor;
				return;
			case DownloadStatus.PartiallyDownloaded:
				text = Locale.PartiallyDownloaded;
				icon = ImageManager.GetIcon("I_Broken");
				color = FormDesign.Design.RedColor;
				return;
			case DownloadStatus.Removed:
				text = Locale.ModIsRemoved;
				icon = ImageManager.GetIcon("I_ContentRemoved");
				color = FormDesign.Design.RedColor;
				return;
		}

		text = Locale.Local;
		icon = ImageManager.GetIcon("I_Local");
		color = FormDesign.Design.YellowColor;
	}
}
