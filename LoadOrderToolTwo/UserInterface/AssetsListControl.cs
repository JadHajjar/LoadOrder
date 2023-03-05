using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class AssetsListControl : SlickStackedListControl<Asset>
{
    public AssetsListControl()
    {
		ItemHeight = 28;
		SeparateWithLines = true;
    }

	protected override void UIChanged()
	{
		base.UIChanged();

		Padding = UI.Scale(new Padding(3, 2, 3, 2), UI.FontScale);
	}

	protected override bool IsItemActionHovered(DrawableItem<Asset> item, Point location)
	{
		return GetActionRectangles(item.Bounds, item.Item).Any(x => x.Contains(location));
	}

	protected override void OnItemMouseClick(DrawableItem<Asset> item, MouseEventArgs e)
	{
		base.OnItemMouseClick(item, e);

		var actionRectangles = GetActionRectangles(item.Bounds, item.Item);

		if (actionRectangles[0].Contains(e.Location))
		{
			OpenFolder(item.Item);
			return;
		}

		if (item.Item.Workshop)
		{
			if (actionRectangles[1].Contains(e.Location))
			{
				OpenSteamLink(item.Item);
				return;
			}

			if (actionRectangles[2].Contains(e.Location))
			{
				Redownload(item.Item);
				return;
			}
		}
	}

	private Rectangle[] GetActionRectangles(Rectangle rectangle, Asset item)
	{
		return internalGet().ToArray();

		IEnumerable<Rectangle> internalGet()
		{
			var buttonRectangle = rectangle.Pad(0, 0, Padding.Left, 0).Align(new Size(rectangle.Height - Padding.Vertical, rectangle.Height - Padding.Vertical), ContentAlignment.MiddleRight);

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

	private void Redownload(Asset item)
	{
		SteamUtil.ReDownload(item.SteamId);
	}

	private void OpenSteamLink(Asset item)
	{
		try { Process.Start(item.SteamPage); }
		catch { }
	}

	private void OpenFolder(Asset item)
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "explorer",
				Arguments = $"/e, /select, \"{item.FileName}\""
			});
		}
		catch
		{
			try
			{ Process.Start(item.Folder); }
			catch { }
		}
	}

	protected override void OnPaintItem(ItemPaintEventArgs<Asset> e)
	{
		if (e.Item.IconImage != null)
		{
			var iconSize = e.ClipRectangle.Height - Padding.Vertical;
			iconSize += iconSize % 2;
			var rectangle = e.ClipRectangle.Align(new Size(iconSize, iconSize), ContentAlignment.MiddleLeft);

			using var image = new Bitmap(e.Item.IconImage, rectangle.Size);
			using var texture = new TextureBrush(image);

			e.Graphics.TranslateTransform(rectangle.X, rectangle.Y);
			e.Graphics.FillRoundedRectangle(texture, new Rectangle(Point.Empty, rectangle.Size), (int)(4 * UI.FontScale));
			e.Graphics.TranslateTransform(-rectangle.X, -rectangle.Y);
		}

		e.Graphics.DrawImage((e.Item.IsIncluded ? Properties.Resources.I_Check : Properties.Resources.I_X).Color(ForeColor), e.ClipRectangle.Pad(32, 0, 0, 0).Align(new Size(24, 24), ContentAlignment.MiddleLeft));
		e.Graphics.DrawString(e.Item.Name, Font, new SolidBrush(ForeColor), e.ClipRectangle.Pad(64, 0, 0, 0), new StringFormat { LineAlignment = StringAlignment.Center });

		if (!e.Item.IsIncluded)
		{
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, BackColor)), e.ClipRectangle);
		}

		var actionRectangles = GetActionRectangles(e.ClipRectangle, e.Item);

		SlickButton.DrawButton(e, actionRectangles[0], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Folder)), null, actionRectangles[0].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);

		if (e.Item.Workshop)
		{
			SlickButton.DrawButton(e, actionRectangles[1], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_Steam)), null, actionRectangles[1].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
			SlickButton.DrawButton(e, actionRectangles[2], string.Empty, Font, ImageManager.GetIcon(nameof(Properties.Resources.I_ReDownload)), null, actionRectangles[2].Contains(CursorLocation) ? e.HoverState : HoverState.Normal);
		}
	}
}
