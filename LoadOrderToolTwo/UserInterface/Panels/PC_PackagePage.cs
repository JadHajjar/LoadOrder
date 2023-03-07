using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static CompatibilityReport.CatalogData.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_PackagePage : PanelContent
{
	public PC_PackagePage(Package package)
	{
		InitializeComponent();

		Package = package;

		Text = Locale.Back;
		L_Title.Text = package.Name;
	}

	public Package Package { get; }

	protected override void UIChanged()
	{
		base.UIChanged();

		L_Title.Font = UI.Font(11.25F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(7), UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Back.BackColor = design.AccentBackColor;
	}

	private void P_Info_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.Clear(FormDesign.Design.AccentBackColor);

		//var versionRect = DrawLabel(e, Package.BuiltIn ? Locale.Vanilla : "v" + Package.Mod?.Version.GetString(), null, FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(textRect.X, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		//GetStatusDescriptors(Package, out var text, out var icon, out var color);
		//var statusRect = string.IsNullOrEmpty(text) ? versionRect : DrawLabel(e, text, icon, color.MergeColor(FormDesign.Design.BackColor, 60), new Rectangle(versionRect.X + versionRect.Width + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		//DrawLabel(e, Package.LocalTime.ToLocalTime().ToString("g"), null, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.BackColor, 75), new Rectangle(statusRect.X + statusRect.Width + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		//if (Package.Workshop)
		//{
		//	DrawLabel(e, Package.Author?.Name, Properties.Resources.I_Developer_16, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.TopLeft);
		//	DrawLabel(e, Package.SteamId.ToString(), Properties.Resources.I_Steam_16, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		//}

		//if (Package.CompatibilityReport is not null)
		//{
		//	DrawLabel(e, Package.CompatibilityReport.reportSeverity.ToString().FormatWords(), Properties.Resources.I_CompatibilityReport_16, Package.CompatibilityReport.reportSeverity switch
		//	{
		//		ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
		//		ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
		//		ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
		//		ReportSeverity.Remarks => FormDesign.Design.ForeColor,
		//		_ => FormDesign.Design.GreenColor
		//	}, rects.CompatibilityRect, ContentAlignment.MiddleRight);
		//}
	}

	private Rectangle DrawLabel(PaintEventArgs e, string? text, Bitmap? icon, Color color, Rectangle rectangle, ContentAlignment alignment)
	{
		if (text == null)
		{
			return Rectangle.Empty;
		}

		var size = e.Graphics.Measure(text, UI.Font(7.5F)).ToSize();

		if (icon is not null)
		{
			size.Width += icon.Width + Padding.Left;
		}

		size.Width += Padding.Left;

		rectangle = rectangle.Pad(Padding).Align(size, alignment);

		using var backBrush = rectangle.Gradient(color);
		using var foreBrush = new SolidBrush(color.GetTextColor());

		e.Graphics.FillRoundedRectangle(backBrush, rectangle, (int)(3 * UI.FontScale));
		e.Graphics.DrawString(text, UI.Font(7.5F), foreBrush, icon is null ? rectangle : rectangle.Pad(icon.Width + (Padding.Left * 2) - 2, 0, 0, 0), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

		if (icon is not null)
		{
			e.Graphics.DrawImage(icon.Color(color.GetTextColor()), rectangle.Pad(Padding.Left, 0, 0, 0).Align(icon.Size, ContentAlignment.MiddleLeft));
		}

		return rectangle;
	}

	private void GetStatusDescriptors(Package package, out string text, out Bitmap? icon, out Color color)
	{
		switch (package.Status)
		{
			case DownloadStatus.OK:
				text = Locale.UpToDate;
				icon = Properties.Resources.I_Ok_16;
				color = FormDesign.Design.GreenColor;
				return;
			case DownloadStatus.Unknown:
				text = Locale.StatusUnknown;
				icon = Properties.Resources.I_Question_16;
				color = FormDesign.Design.YellowColor;
				return;
			case DownloadStatus.OutOfDate:
				text = Locale.OutOfDate;
				icon = Properties.Resources.I_OutOfDate_16;
				color = FormDesign.Design.YellowColor;
				return;
			case DownloadStatus.NotDownloaded:
				text = Locale.ModIsNotDownloaded;
				icon = Properties.Resources.I_Question_16;
				color = FormDesign.Design.RedColor;
				return;
			case DownloadStatus.PartiallyDownloaded:
				text = Locale.PartiallyDownloaded;
				icon = Properties.Resources.I_Broken_16;
				color = FormDesign.Design.RedColor;
				return;
			case DownloadStatus.Removed:
				text = Locale.ModIsRemoved;
				icon = Properties.Resources.I_ContentRemoved_16;
				color = FormDesign.Design.RedColor;
				return;
		}

		text = string.Empty;
		icon = null;
		color = Color.White;
	}
}
