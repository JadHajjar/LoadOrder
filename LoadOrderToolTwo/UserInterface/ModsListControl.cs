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

namespace LoadOrderToolTwo.UserInterface;
internal class ModsListControl : SlickStackedListControl<Mod>
{
	public ModsListControl()
	{
		HighlightOnHover = true;
		SeparateWithLines = true;

		CentralManager.ModInformationUpdated += Invalidate;
	}

	protected override void UIChanged()
	{
		ItemHeight = 36;

		base.UIChanged();

		Padding = UI.Scale(new Padding(3, 2, 3, 2), UI.FontScale);
	}

	protected override bool IsItemActionHovered(DrawableItem<Mod> item, Point location)
	{
		return GetActionRectangles(item.Bounds, item.Item).Any(x => x.Contains(location));
	}

	protected override void OnItemMouseClick(DrawableItem<Mod> item, MouseEventArgs e)
	{
		base.OnItemMouseClick(item, e);

		var actionRectangles = GetActionRectangles(item.Bounds, item.Item);

		if (actionRectangles[0].Contains(e.Location))
		{
			item.Item.IsIncluded = !item.Item.IsIncluded;
			return;
		}

		if (actionRectangles[1].Contains(e.Location))
		{
			item.Item.IsEnabled = !item.Item.IsEnabled;
			return;
		}

		if (actionRectangles[2].Contains(e.Location) || actionRectangles[3].Contains(e.Location))
		{
			(FindForm() as BasePanelForm)?.PushPanel(null, new PC_ModPage(item.Item));
			return;
		}

		if (actionRectangles[4].Contains(e.Location))
		{
			OpenFolder(item.Item);
			return;
		}

		if (item.Item.Workshop)
		{
			if (actionRectangles[5].Contains(e.Location))
			{
				OpenSteamLink(item.Item);
				return;
			}

			if (actionRectangles[6].Contains(e.Location))
			{
				Redownload(item.Item);
				return;
			}
		}
	}

	private Rectangle[] GetActionRectangles(Rectangle rectangle, Mod item)
	{
		return internalGet().ToArray();

		IEnumerable<Rectangle> internalGet()
		{
			yield return rectangle.Pad(3 * Padding.Left, 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft);

			var rect1 = rectangle.Pad(24 + (5 * Padding.Left), 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft);

			yield return rect1;

			var buttonRectangle = rectangle.Pad(0, 0, Padding.Right, 0).Align(new Size(rectangle.Height - Padding.Vertical, rectangle.Height - Padding.Vertical), ContentAlignment.MiddleRight);
			var iconSize = rectangle.Height - Padding.Vertical;
			var iconRectangle = rectangle.Pad(rect1.Right + (4 * Padding.Left)).Align(new Size(iconSize, iconSize), ContentAlignment.MiddleLeft);
			var textRect = rectangle.Pad(iconRectangle.X + iconRectangle.Width + Padding.Left, 0, (item.Workshop ? ((2 * Padding.Left) + (2 * buttonRectangle.Width) + (int)(100 * UI.FontScale)) : 0) + rectangle.Width - buttonRectangle.X, rectangle.Height / 2);

			yield return iconRectangle;
			yield return textRect;

			yield return buttonRectangle;

			if (item.Workshop)
			{
				buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
				yield return buttonRectangle;

				buttonRectangle.X -= Padding.Left + buttonRectangle.Width;
				yield return buttonRectangle;
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
		var actionRectangles = GetActionRectangles(e.ClipRectangle, e.Item);
		var isIncluded = e.Item.IsIncluded;
		var inclEnableRect = new Rectangle(actionRectangles[0].Location, new Size(actionRectangles[1].Right - actionRectangles[0].Left, actionRectangles[0].Height)).Pad(-Padding.Left, -Padding.Top, -Padding.Right, -Padding.Bottom);

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
			e.Graphics.DrawImage((isIncluded ? Properties.Resources.I_Check : Properties.Resources.I_X).Color(actionRectangles[0].Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : ForeColor), actionRectangles[0]);
			e.Graphics.DrawImage((e.Item.IsEnabled ? Properties.Resources.I_Enabled : Properties.Resources.I_Disabled).Color(actionRectangles[1].Contains(CursorLocation) ? FormDesign.Design.ActiveColor : isIncluded ? FormDesign.Design.ActiveForeColor : base.ForeColor), actionRectangles[1]);
		}

		var iconRectangle = actionRectangles[2];
		var textRect = actionRectangles[3];

		using var image = new Bitmap(e.Item.IconImage ?? Properties.Resources.I_ModIcon.Color(FormDesign.Design.IconColor), iconRectangle.Size);
		using var texture = new TextureBrush(image);

		e.Graphics.TranslateTransform(iconRectangle.X, iconRectangle.Y);
		e.Graphics.FillRoundedRectangle(texture, new Rectangle(Point.Empty, iconRectangle.Size), (int)(4 * UI.FontScale));
		e.Graphics.TranslateTransform(-iconRectangle.X, -iconRectangle.Y);

		var textHovered = e.HoverState.HasFlag(HoverState.Hovered) && (actionRectangles[2].Contains(CursorLocation) || actionRectangles[3].Contains(CursorLocation));
		e.Graphics.DrawString(e.Item.Name.RegexRemove(@"v?\d+\.\d+(\.\d+)?(\.\d+)?"), UI.Font(9F, FontStyle.Bold | (textHovered ? FontStyle.Underline : FontStyle.Regular)), new SolidBrush(textHovered ? FormDesign.Design.ActiveColor : ForeColor), textRect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

		var versionRect = DrawLabel(e, e.Item.BuiltIn ? Locale.Vanilla : ("v" + e.Item.Version.GetString()), null, FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(textRect.X, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		GetStatusDescriptors(e.Item, out var text, out var icon, out var color);
		var statusRect = string.IsNullOrEmpty(text) ? versionRect : DrawLabel(e, text, icon, color.MergeColor(FormDesign.Design.BackColor, 60), new Rectangle(versionRect.X + versionRect.Width + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);

		DrawLabel(e, e.Item.LocalTime.ToLocalTime().ToString("g"), null, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.BackColor, 75), new Rectangle(statusRect.X + statusRect.Width + Padding.Left, e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		if (e.Item.Workshop)
		{
			DrawLabel(e, e.Item.Author?.Name, Properties.Resources.I_Developer_16, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(actionRectangles.Last().X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.TopLeft);
			DrawLabel(e, e.Item.SteamId.ToString(), Properties.Resources.I_Steam_16, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ActiveColor, 75).MergeColor(FormDesign.Design.BackColor, 40), new Rectangle(actionRectangles.Last().X - (int)(100 * UI.FontScale), e.ClipRectangle.Y, (int)(100 * UI.FontScale), e.ClipRectangle.Height), ContentAlignment.BottomLeft);
		}

		if (!isIncluded)
		{
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, BackColor)), e.ClipRectangle);
		}

		SlickButton.DrawButton(e, actionRectangles[4], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Folder)), null, actionRectangles[4].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);

		if (e.Item.Workshop)
		{
			SlickButton.DrawButton(e, actionRectangles[5], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Steam)), null, actionRectangles[5].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
			SlickButton.DrawButton(e, actionRectangles[6], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_ReDownload)), null, actionRectangles[6].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
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
}
