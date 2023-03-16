using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using static CompatibilityReport.CatalogData.Enums;

namespace LoadOrderToolTwo.UserInterface;
internal class SteamPackageViewControl : SlickImageControl
{
	public SteamPackageViewControl(SteamWorkshopItem item)
	{
		Item = item;
		Anchor = AnchorStyles.Left | AnchorStyles.Right;

		var steamId = ulong.Parse(item.PublishedFileID);
		_compatibilityReport = CompatibilityManager.GetCompatibilityReport(steamId);
		LocalPackage = CentralManager.Packages.FirstOrDefault(x => x.SteamId == steamId);

		LoadImage(item.PreviewURL, ImageManager.GetImage);
	}

	public SteamWorkshopItem Item { get; }

	private readonly CompatibilityManager.ReportInfo? _compatibilityReport;
	private Rectangle buttonRect;

	public Package? LocalPackage { get; }

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(5), UI.FontScale);
		Margin = UI.Scale(new Padding(3), UI.FontScale);
		Height = (int)(75 * UI.FontScale);
		Font = UI.Font(9F, FontStyle.Bold);
	}

	private State GetState()
	{
		if (LocalPackage == null)
		{
			return State.Unsubscribed;
		}

		if (LocalPackage.IsIncluded)
		{
			return (LocalPackage.Mod?.IsEnabled ?? true) ? State.Enabled : State.Disabled;
		}

		return State.Excluded;
	}

	private enum State
	{
		Unsubscribed,
		Disabled,
		Enabled,
		Excluded
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		var iconRect = new Rectangle(Padding.Left, Padding.Top, Height - Padding.Vertical, Height - Padding.Vertical);

		if (iconRect.Contains(e.Location))
		{
			try
			{ Process.Start($"https://steamcommunity.com/workshop/filedetails/?id={Item.PublishedFileID}"); }
			catch { }
		}

		if (buttonRect.Contains(e.Location))
		{

		}

		base.OnMouseClick(e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.BackColor), ClientRectangle.Pad(Padding), Padding.Left);
		e.Graphics.DrawRoundedRectangle(new Pen(FormDesign.Design.AccentColor, (float)(1.5 * UI.FontScale)), ClientRectangle.Pad(Padding), Padding.Left);

		if (HoverState.HasFlag(HoverState.Hovered))
		{
			using var brush = new LinearGradientBrush(ClientRectangle, Color.FromArgb(FormDesign.Design.Type == FormDesignType.Light ? 150 : 220, FormDesign.Design.AccentColor), Color.Empty, LinearGradientMode.Horizontal);
			e.Graphics.FillRoundedRectangle(brush, ClientRectangle.Pad(Padding), Padding.Left);
		}

		var iconRect = new Rectangle(Padding.Left, Padding.Top, Height - Padding.Vertical, Height - Padding.Vertical);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRect.CenterR(UI.Scale(new Size(32, 32), UI.FontScale)));
		}
		else
		{
			e.Graphics.DrawRoundedImage(Image, iconRect, Padding.Left, FormDesign.Design.IconColor, topRight: false, botRight: false);
		}

		e.Graphics.DrawString(Item.Title.RemoveVersionText(), Font, new SolidBrush(ForeColor), ClientRectangle.Pad(Padding.Horizontal + iconRect.Width, Padding.Top, 0, 0));

		var x = Padding.Left + iconRect.Width;
		var y = (int)e.Graphics.Measure(Item.Title, Font).Height + Padding.Top;
		var secondY = y;

		if (Item.Tags is not null)
		{
			foreach (var tag in Item.Tags)
			{
				var tagRect = DrawLabel(e, tag, null, FormDesign.Design.ButtonColor, ClientRectangle.Pad(x, y, 0, 0), ContentAlignment.TopLeft);
				secondY = Math.Max(secondY, tagRect.Bottom);
				x = tagRect.Right;
			}
		}

		x = Padding.Left + iconRect.Width;

		if (Item.Author is not null)
		{
			var authorRect = DrawLabel(e, Item.Author.Name, Properties.Resources.I_Developer_16, FormDesign.Design.ButtonColor.MergeColor(FormDesign.Design.ActiveColor, 75), ClientRectangle.Pad(x, secondY, 0, 0), ContentAlignment.TopLeft);

			x = authorRect.Right;
		}

		if (_compatibilityReport is not null)
		{
			DrawLabel(e, LocaleHelper.GetGlobalText($"CR_{_compatibilityReport.Severity}"), Properties.Resources.I_CompatibilityReport_16, (_compatibilityReport.Severity switch
			{
				ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
				ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
				ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
				ReportSeverity.Remarks => FormDesign.Design.ButtonColor,
				_ => FormDesign.Design.GreenColor
			}).MergeColor(FormDesign.Design.BackColor, 60), ClientRectangle.Pad(x, secondY, 0, 0), ContentAlignment.TopLeft);
		}

		//var buttonIcon = GetState() switch { State.Excluded => Locale.Include, State.Enabled  State.Unsubscribed => Locale.Subscribe}
		var buttonSize = SlickButton.GetSize(e.Graphics, Properties.Resources.I_Add, "Subscribe", new Font(Font, FontStyle.Regular));
		buttonRect = ClientRectangle.Pad(Padding).Pad(Padding).Align(buttonSize, ContentAlignment.BottomRight);
		var hovered = buttonRect.Contains(PointToClient(Cursor.Position));

		SlickButton.DrawButton(e, buttonRect, "Subscribe", new Font(Font, FontStyle.Regular), Properties.Resources.I_Add, null, hovered ? HoverState & ~HoverState.Focused : HoverState.Normal);

		Cursor = hovered || iconRect.Contains(PointToClient(Cursor.Position)) ? Cursors.Hand : Cursors.Default;
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
}
