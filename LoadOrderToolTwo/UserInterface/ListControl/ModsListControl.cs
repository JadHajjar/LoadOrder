using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.UserInterface.Panels;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using static CompatibilityReport.CatalogData.Enums;

namespace LoadOrderToolTwo.UserInterface.ListControl;
internal class ModsListControl : SlickStackedListControl<Mod>
{
	public ModsListControl()
	{
		HighlightOnHover = true;
		SeparateWithLines = true;

		CentralManager.ModInformationUpdated += (_) => Invalidate();
	}

	protected override void UIChanged()
	{
		ItemHeight = 36;

		base.UIChanged();

		Padding = UI.Scale(new Padding(3, 2, 3, 2), UI.FontScale);
	}

	protected override IEnumerable<DrawableItem<Mod>> OrderItems(IEnumerable<DrawableItem<Mod>> items)
	{
		return items.OrderBy(x => x.Item.Name);
	}

	protected override bool IsItemActionHovered(DrawableItem<Mod> item, Point location)
	{
		return GetActionRectangles(item.Bounds, item.Item).Contain(location);
	}

	protected override void OnItemMouseClick(DrawableItem<Mod> item, MouseEventArgs e)
	{
		base.OnItemMouseClick(item, e);

		var rects = GetActionRectangles(item.Bounds, item.Item);

		if (rects.IncludedRect.Contains(e.Location))
		{
			item.Item.IsIncluded = !item.Item.IsIncluded;
			return;
		}

		if (rects.EnabledRect.Contains(e.Location))
		{
			item.Item.IsEnabled = !item.Item.IsEnabled;
			return;
		}

		if (rects.TextRect.Contains(e.Location) || rects.IconRect.Contains(e.Location))
		{
			(FindForm() as BasePanelForm)?.PushPanel(null, new PC_PackagePage(item.Item.Package));
			return;
		}

		if (rects.FolderRect.Contains(e.Location))
		{
			OpenFolder(item.Item);
			return;
		}

		if (item.Item.Workshop)
		{
			if (rects.SteamRect.Contains(e.Location))
			{
				OpenSteamLink(item.Item);
				return;
			}

			if (rects.RedownloadRect.Contains(e.Location))
			{
				Redownload(item.Item);
				return;
			}

			if (rects.SteamIdRect.Contains(e.Location))
			{
				Clipboard.SetText(item.Item.SteamId.ToString());
				return;
			}
		}
	}

	private void Redownload(Mod item)
	{
		SteamUtil.ReDownload(item.SteamId);
	}

	private void OpenSteamLink(Mod item)
	{
		try
		{ Process.Start(item.SteamPage); }
		catch { }
	}

	private void OpenFolder(Mod item)
	{
		try
		{ Process.Start(item.Folder); }
		catch { }
	}

	protected override void OnPaintItem(ItemPaintEventArgs<Mod> e)
	{
		var rects = GetActionRectangles(e.ClipRectangle, e.Item);
		var isIncluded = e.Item.IsIncluded;
		var inclEnableRect = new Rectangle(rects.IncludedRect.Location, new Size(rects.EnabledRect.Right - rects.IncludedRect.Left, rects.IncludedRect.Height)).Pad(-Padding.Left, -Padding.Top, -Padding.Right, -Padding.Bottom);

		if (isIncluded)
		{
			e.Graphics.FillRoundedRectangle(inclEnableRect.Gradient(e.Item.IsEnabled ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor), inclEnableRect, 4);
		}

		if (isIncluded && !e.HoverState.HasFlag(HoverState.Hovered))
		{
			e.Graphics.DrawImage((e.Item.IsEnabled ? Properties.Resources.I_Ok : Properties.Resources.I_Cancel).Color(FormDesign.Design.ActiveForeColor), inclEnableRect.CenterR(24, 24));
		}
		else
		{
			e.Graphics.DrawImage((isIncluded ? Properties.Resources.I_Check : Properties.Resources.I_X).Color(rects.IncludedRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : ForeColor), rects.IncludedRect);
			e.Graphics.DrawImage((e.Item.IsEnabled ? Properties.Resources.I_Enabled : Properties.Resources.I_Disabled).Color(rects.EnabledRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : base.ForeColor), rects.EnabledRect);
		}

		var iconRectangle = rects.IconRect;
		var textRect = rects.TextRect;

		using var image = new Bitmap(e.Item.IconImage ?? Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor), iconRectangle.Size);
		using var texture = new TextureBrush(image);

		e.Graphics.TranslateTransform(iconRectangle.X, iconRectangle.Y);
		e.Graphics.FillRoundedRectangle(texture, new Rectangle(Point.Empty, iconRectangle.Size), (int)(4 * UI.FontScale));
		e.Graphics.TranslateTransform(-iconRectangle.X, -iconRectangle.Y);

		var textHovered = e.HoverState.HasFlag(HoverState.Hovered) && (rects.TextRect.Contains(CursorLocation) || rects.IconRect.Contains(CursorLocation));
		e.Graphics.DrawString(e.Item.Name.RegexRemove(@"v?\d+\.\d+(\.\d+)?(\.\d+)?"), UI.Font(9F, FontStyle.Bold | (textHovered ? FontStyle.Underline : FontStyle.Regular)), new SolidBrush(textHovered ? FormDesign.Design.ActiveColor : ForeColor), textRect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

		var versionRect = DrawLabel(e, e.Item.BuiltIn ? Locale.Vanilla : "v" + e.Item.Version.GetString(), null, FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(textRect.X, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		var timeRect = DrawLabel(e, e.Item.LocalTime.ToLocalTime().ToString("g"), Properties.Resources.I_UpdateTime, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.BackColor, 75), new Rectangle(versionRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		
		GetStatusDescriptors(e.Item, out var text, out var icon, out var color);
		var statusRect = string.IsNullOrEmpty(text) ? timeRect : DrawLabel(e, text, icon, color.MergeColor(FormDesign.Design.BackColor, 60), new Rectangle(timeRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		if (e.Item.Workshop)
		{
			DrawLabel(e, e.Item.Author?.Name, Properties.Resources.I_Developer_16, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.TopLeft);
			DrawLabel(e, e.Item.SteamId.ToString(), Properties.Resources.I_Steam_16, rects.SteamIdRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		}

		if (e.Item.Package.CompatibilityReport is not null)
		{
			DrawLabel(e, e.Item.Package.CompatibilityReport.reportSeverity.ToString().FormatWords(), Properties.Resources.I_CompatibilityReport_16, e.Item.Package.CompatibilityReport.reportSeverity switch
			{
				ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
				ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
				ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
				ReportSeverity.Remarks => FormDesign.Design.ButtonColor,
				_ => FormDesign.Design.GreenColor
			}, new Rectangle(statusRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		}

		if (!isIncluded)
		{
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, BackColor)), e.ClipRectangle);
		}

		SlickButton.DrawButton(e, rects.FolderRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Folder)), null, rects.FolderRect.Contains(CursorLocation) ? e.HoverState : HoverState.Normal);

		if (e.Item.Workshop)
		{
			SlickButton.DrawButton(e, rects.SteamRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Steam)), null, rects.SteamRect.Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
			SlickButton.DrawButton(e, rects.RedownloadRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_ReDownload)), null, rects.RedownloadRect.Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
		}
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

	private void GetStatusDescriptors(Mod mod, out string text, out Bitmap? icon, out Color color)
	{
		if (!mod.Workshop && !mod.BuiltIn)
		{
			text = Locale.Local;
			icon = Properties.Resources.I_Local_16;
			color = FormDesign.Design.YellowColor;
			return;
		}

		switch (mod.Status)
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

	private Rectangles GetActionRectangles(Rectangle rectangle, Mod item)
	{
		var rects = new Rectangles
		{
			IncludedRect = rectangle.Pad(3 * Padding.Left, 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft),

			EnabledRect = rectangle.Pad(24 + (5 * Padding.Left), 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft)
		};

		var buttonRectangle = rectangle.Pad(0, 0, Padding.Right, 0).Align(new Size(rectangle.Height - Padding.Vertical, rectangle.Height - Padding.Vertical), ContentAlignment.MiddleRight);
		var iconSize = rectangle.Height - Padding.Vertical;

		rects.FolderRect = buttonRectangle;
		rects.IconRect = rectangle.Pad(rects.EnabledRect.Right + (4 * Padding.Left)).Align(new Size(iconSize, iconSize), ContentAlignment.MiddleLeft);
		rects.TextRect = rectangle.Pad(rects.IconRect.X + rects.IconRect.Width + Padding.Left, 0, (item.Workshop ? (2 * Padding.Left) + (2 * buttonRectangle.Width) + (int)(100 * UI.FontScale) : 0) + rectangle.Width - buttonRectangle.X, rectangle.Height / 2);

		if (item.Package.CompatibilityReport != null)
		{
			rects.TextRect.Width -= (int)(100 * UI.FontScale);
		}

		if (item.Workshop)
		{
			buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
			rects.SteamRect = buttonRectangle;

			buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
			rects.RedownloadRect = buttonRectangle;

			rects.SteamIdRect = new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), rectangle.Y + rectangle.Height / 2, (int)(100 * UI.FontScale), rectangle.Height / 2);
		}

		rects.CompatibilityRect = new Rectangle(buttonRectangle.X - (int)(200 * UI.FontScale), rectangle.Y, (int)(100 * UI.FontScale), rectangle.Height);

		return rects;
	}

	struct Rectangles
	{
		internal Rectangle IncludedRect;
		internal Rectangle EnabledRect;
		internal Rectangle FolderRect;
		internal Rectangle IconRect;
		internal Rectangle TextRect;
		internal Rectangle SteamRect;
		internal Rectangle RedownloadRect;
		internal Rectangle CompatibilityRect;
		internal Rectangle SteamIdRect;

		internal bool Contain(Point location)
		{
			return
				IncludedRect.Contains(location) ||
				EnabledRect.Contains(location) ||
				FolderRect.Contains(location) ||
				IconRect.Contains(location) ||
				TextRect.Contains(location) ||
				SteamRect.Contains(location) ||
				RedownloadRect.Contains(location) ||
				CompatibilityRect.Contains(location) ||
				SteamIdRect.Contains(location);
		}
	}
}
