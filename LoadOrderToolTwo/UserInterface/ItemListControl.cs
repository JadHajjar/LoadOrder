using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Interfaces;
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

namespace LoadOrderToolTwo.UserInterface;
internal class ItemListControl<T> : SlickStackedListControl<T> where T : IPackage
{
	public ItemListControl()
	{
		DoubleSizeOnHover = true;
		HighlightOnHover = true;
		SeparateWithLines = true;

		if (!CentralManager.IsContentLoaded)
		{
			Loading = true;

			CentralManager.ContentLoaded += () => Loading = false;
		}

		CentralManager.ModInformationUpdated += (_) => Invalidate();
	}

	protected override void UIChanged()
	{
		ItemHeight = 36;

		base.UIChanged();

		Padding = UI.Scale(new Padding(3, 2, 3, 2), UI.FontScale);
	}

	protected override IEnumerable<DrawableItem<T>> OrderItems(IEnumerable<DrawableItem<T>> items)
	{
		return items
			.OrderByDescending(x => x.Item.IsIncluded)
			.ThenByDescending(x => x.Item.Workshop)
			.ThenBy(x => x.Item.Name);
	}

	protected override bool IsItemActionHovered(DrawableItem<T> item, Point location)
	{
		return GetActionRectangles(item.Bounds, item.Item, true).Contain(location);
	}

	protected override void OnItemMouseClick(DrawableItem<T> item, MouseEventArgs e)
	{
		base.OnItemMouseClick(item, e);

		if (e.Button != MouseButtons.Left)
		{
			return;
		}

		var rects = GetActionRectangles(item.Bounds, item.Item, true);

		if (item.Item is Mod mod)
		{
			if (rects.IncludedRect.Contains(e.Location))
			{
				mod.IsIncluded = !mod.IsIncluded;
				return;
			}

			if (rects.EnabledRect.Contains(e.Location))
			{
				mod.IsEnabled = !mod.IsEnabled;
				return;
			}
		}

		if (item.Item is Asset asset)
		{
			if (rects.IncludedRect.Contains(e.Location))
			{
				asset.IsIncluded = !asset.IsIncluded;
				return;
			}
		}

		if (rects.CenterRect.Contains(e.Location))
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

	private void Redownload(T item)
	{
		SteamUtil.ReDownload(item.SteamId);
	}

	private void OpenSteamLink(T item)
	{
		try
		{ Process.Start(item.SteamPage); }
		catch { }
	}

	private void OpenFolder(T item)
	{
		try
		{ Process.Start(item.Folder); }
		catch { }
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (Loading)
		{
			base.OnPaint(e);
		}
		else if (!Items.Any())
		{
			e.Graphics.DrawString(Locale.NoLocalPackagesFound, UI.Font(9.75F, FontStyle.Italic), new SolidBrush(ForeColor), ClientRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
		else if (!SafeGetItems().Any())
		{
			e.Graphics.DrawString(Locale.NoPackagesMatchFilters, UI.Font(9.75F, FontStyle.Italic), new SolidBrush(ForeColor), ClientRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
		else
		{
			base.OnPaint(e);
		}
	}

	protected override void OnPaintItem(ItemPaintEventArgs<T> e)
	{
		var large = e.HoverState.HasFlag(HoverState.Hovered) || e.HoverState.HasFlag(HoverState.Pressed);
		var rects = GetActionRectangles(e.ClipRectangle, e.Item, large);
		var inclEnableRect = new Rectangle(rects.IncludedRect.Location, new Size(rects.EnabledRect.Right - rects.IncludedRect.Left, rects.IncludedRect.Height)).Pad(-Padding.Left, -Padding.Top, -Padding.Right, -Padding.Bottom);
		var isPressed = e.HoverState.HasFlag(HoverState.Pressed);
		var isIncluded = false;

		if (isPressed && !rects.CenterRect.Contains(CursorLocation))
		{
			e.HoverState &= ~HoverState.Pressed;
		}

		base.OnPaintItem(e);

		if (e.Item is Mod mod)
		{
			isIncluded = mod.IsIncluded;

			if (isIncluded)
			{
				e.Graphics.FillRoundedRectangle(inclEnableRect.Gradient(mod.IsEnabled ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor), inclEnableRect, 4);
			}

			if (isIncluded && !e.HoverState.HasFlag(HoverState.Hovered))
			{
				e.Graphics.DrawImage((mod.IsEnabled ? Properties.Resources.I_Ok : Properties.Resources.I_Cancel).Color(FormDesign.Design.ActiveForeColor), inclEnableRect.CenterR(24, 24));
			}
			else
			{
				e.Graphics.DrawImage((isIncluded ? Properties.Resources.I_Check : Properties.Resources.I_X).Color(rects.IncludedRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : ForeColor), rects.IncludedRect);
				e.Graphics.DrawImage((mod.IsEnabled ? Properties.Resources.I_Enabled : Properties.Resources.I_Disabled).Color(rects.EnabledRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : base.ForeColor), rects.EnabledRect);
			}
		}

		if (e.Item is Asset asset)
		{
			isIncluded = asset.IsIncluded;

			e.Graphics.DrawImage((isIncluded ? Properties.Resources.I_Check : Properties.Resources.I_X).Color(rects.IncludedRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : ForeColor), rects.IncludedRect);
		}

		var iconRectangle = rects.IconRect;
		var textRect = rects.TextRect;

		e.Graphics.DrawRoundedImage(e.Item.IconImage ?? Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor), iconRectangle, (int)(4 * UI.FontScale));

		e.Graphics.DrawString(e.Item.Name.RegexRemove(@"v?\d+\.\d+(\.\d+)?(\.\d+)?").RemoveDoubleSpaces(), UI.Font(large ? 11.25F : 9F, FontStyle.Bold), new SolidBrush(e.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : rects.CenterRect.Contains(CursorLocation) && e.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : ForeColor), textRect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

		var versionRect = DrawLabel(e, e.Item is Mod
			? (e.Item.BuiltIn ? Locale.Vanilla : "v" + e.Item.Package.Mod!.Version.GetString())
			: (e.Item as Asset)!.FileSize.SizeString(), null, FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(textRect.X, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		var timeRect = DrawLabel(e, e.Item.LocalTime.ToLocalTime().ToString("g"), Properties.Resources.I_UpdateTime, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.BackColor, 75), new Rectangle(versionRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		GetStatusDescriptors(e.Item, out var text, out var icon, out var color);
		var statusRect = string.IsNullOrEmpty(text) ? timeRect : DrawLabel(e, text, icon, color.MergeColor(FormDesign.Design.BackColor, 60), new Rectangle(timeRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		if (e.Item.Workshop)
		{
			if (large && e.Item.Author is not null)
			{
				var size = e.Graphics.Measure("by " + e.Item.Author.Name, UI.Font(9.75F)).ToSize();
				var authorRect = rects.AuthorRect.Align(new Size(size.Width + Padding.Horizontal + Padding.Right + size.Height, size.Height + Padding.Vertical*2), ContentAlignment.MiddleRight);
				authorRect.X -= Padding.Left;
				authorRect.Y += Padding.Top;
				var avatarRect = authorRect.Pad(Padding).Align(new(size.Height, size.Height), ContentAlignment.MiddleLeft);

				e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.BackColor), authorRect, (int)(6 * UI.FontScale));

				e.Graphics.DrawString("by " + e.Item.Author.Name, UI.Font(9.75F), new SolidBrush(FormDesign.Design.ForeColor), authorRect.Pad(avatarRect.Width + Padding.Horizontal, 0, 0, 0), new StringFormat { LineAlignment = StringAlignment.Center });

				if (e.Item.AuthorIconImage is not null)
				{
					e.Graphics.DrawRoundImage(e.Item.AuthorIconImage ?? Properties.Resources.I_AuthorIcon.Color(FormDesign.Design.IconColor), avatarRect, (int)(4 * UI.FontScale));
				}
			}
			else
			{
				DrawLabel(e, e.Item.Author?.Name, Properties.Resources.I_Developer_16, rects.AuthorRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), rects.AuthorRect, ContentAlignment.TopLeft);
			}

			DrawLabel(e, e.Item.SteamId.ToString(), Properties.Resources.I_Steam_16, rects.SteamIdRect.Contains(CursorLocation) ? FormDesign.Design.ActiveColor : FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), rects.SteamIdRect, ContentAlignment.BottomLeft);
		}

		var report = e.Item.Package.CompatibilityReport;
		if (report is not null)
		{
			DrawLabel(e, LocaleHelper.GetGlobalText($"CR_{report.Severity}"), Properties.Resources.I_CompatibilityReport_16, (report.Severity switch
			{
				ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
				ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
				ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
				ReportSeverity.Remarks => FormDesign.Design.ButtonColor,
				_ => FormDesign.Design.GreenColor
			}).MergeColor(FormDesign.Design.BackColor, 60), new Rectangle(statusRect.Right + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		}

		SlickButton.DrawButton(e, rects.FolderRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Folder)), null, rects.FolderRect.Contains(CursorLocation) ? e.HoverState | (isPressed ? HoverState.Pressed : 0) : HoverState.Normal);

		if (e.Item.Workshop)
		{
			SlickButton.DrawButton(e, rects.SteamRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Steam)), null, rects.SteamRect.Contains(CursorLocation) ? e.HoverState | (isPressed ? HoverState.Pressed : 0) : HoverState.Normal);
			SlickButton.DrawButton(e, rects.RedownloadRect, string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_ReDownload)), null, rects.RedownloadRect.Contains(CursorLocation) ? e.HoverState | (isPressed ? HoverState.Pressed : 0) : HoverState.Normal);
		}

		if (!isIncluded)
		{
			var filledRect = e.ClipRectangle.Pad(0, -Padding.Top, 0, -Padding.Bottom);
			e.Graphics.SetClip(filledRect);
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(e.HoverState.HasFlag(HoverState.Hovered) ? 50 : 135, BackColor)), filledRect);
		}
	}

	private Rectangle DrawLabel(ItemPaintEventArgs<T> e, string? text, Bitmap? icon, Color color, Rectangle rectangle, ContentAlignment alignment)
	{
		if (text == null)
		{
			return Rectangle.Empty;
		}

		var large = e.HoverState.HasFlag(HoverState.Hovered) || e.HoverState.HasFlag(HoverState.Pressed);
		var size = e.Graphics.Measure(text, UI.Font(large ? 9F : 7.5F)).ToSize();

		if (icon is not null)
		{
			size.Width += icon.Width + Padding.Left;
		}

		size.Width += Padding.Left;

		rectangle = rectangle.Pad(Padding).Align(size, alignment);

		using var backBrush = rectangle.Gradient(color);
		using var foreBrush = new SolidBrush(color.GetTextColor());

		e.Graphics.FillRoundedRectangle(backBrush, rectangle, (int)(3 * UI.FontScale));
		e.Graphics.DrawString(text, UI.Font(large ? 9F : 7.5F), foreBrush, icon is null ? rectangle : rectangle.Pad(icon.Width + (Padding.Left * 2) - 2, 0, 0, 0), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

		if (icon is not null)
		{
			e.Graphics.DrawImage(icon.Color(color.GetTextColor()), rectangle.Pad(Padding.Left, 0, 0, 0).Align(icon.Size, ContentAlignment.MiddleLeft));
		}

		return rectangle;
	}

	private void GetStatusDescriptors(T mod, out string text, out Bitmap? icon, out Color color)
	{
		if (!mod.Workshop)
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

	private Rectangles GetActionRectangles(Rectangle rectangle, T item, bool doubleSize)
	{
		var rects = new Rectangles
		{
			IncludedRect = rectangle.Pad(3 * Padding.Left, 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft),
		};

		if (item is Mod)
		{
			rects.EnabledRect = rectangle.Pad(24 + (5 * Padding.Left), 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft);
		};

		var buttonRectangle = rectangle.Pad(0, 0, Padding.Right, 0).Align(new Size(ItemHeight, ItemHeight), ContentAlignment.TopRight);
		var iconSize = rectangle.Height - Padding.Vertical;

		rects.FolderRect = buttonRectangle;
		rects.IconRect = rectangle.Pad(Math.Max(rects.IncludedRect.Right, rects.EnabledRect.Right) + (4 * Padding.Left)).Align(new Size(iconSize, iconSize), ContentAlignment.MiddleLeft);
		rects.TextRect = rectangle.Pad(rects.IconRect.X + rects.IconRect.Width + Padding.Left, 0, (item.Workshop ? (2 * Padding.Left) + (2 * buttonRectangle.Width) + (int)(100 * UI.FontScale) : 0) + rectangle.Width - buttonRectangle.X, rectangle.Height / 2);

		if (item.Workshop)
		{
			buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
			rects.SteamRect = buttonRectangle;

			buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
			rects.RedownloadRect = buttonRectangle;

			if (doubleSize)
			{
				rects.SteamIdRect = new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), rectangle.Y + Padding.Top, (int)(100 * UI.FontScale), rectangle.Height / 4);
				rects.AuthorRect = new Rectangle(rectangle.X, rectangle.Y + (rectangle.Height / 2), rectangle.Width, rectangle.Height / 2);
			}
			else
			{
				rects.AuthorRect = new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), rectangle.Y + (rectangle.Height / 2), (int)(100 * UI.FontScale), rectangle.Height / 2);
				rects.SteamIdRect = new Rectangle(rects.RedownloadRect.X - (int)(100 * UI.FontScale), rectangle.Y, (int)(100 * UI.FontScale), rectangle.Height / 2);
			}

			rects.CenterRect = new Rectangle(rects.IconRect.X, rectangle.Y, rects.SteamIdRect.X - rects.IconRect.X, rectangle.Height);
		}
		else
		{
			rects.CenterRect = new Rectangle(rects.IconRect.X, rectangle.Y, buttonRectangle.X - rects.IconRect.X, rectangle.Height);
		}

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
		internal Rectangle SteamIdRect;
		internal Rectangle CenterRect;
		internal Rectangle AuthorRect;

		internal bool Contain(Point location)
		{
			return
				IncludedRect.Contains(location) ||
				EnabledRect.Contains(location) ||
				FolderRect.Contains(location) ||
				CenterRect.Contains(location) ||
				SteamRect.Contains(location) ||
				RedownloadRect.Contains(location) ||
				AuthorRect.Contains(location) ||
				SteamIdRect.Contains(location);
		}
	}
}
