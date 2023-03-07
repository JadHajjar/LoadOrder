﻿using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;
internal abstract class StatusBubbleBase : SlickImageControl
{
	public StatusBubbleBase()
	{
		Cursor = Cursors.Hand;
	}

	protected override void UIChanged()
	{
		Margin = UI.Scale(new Padding(10, 10, 5, 10), UI.FontScale);
		Padding = UI.Scale(new Padding(7), UI.FontScale);
		Width = (int)(180 * UI.FontScale);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{ }

	protected sealed override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);
		e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

		SlickButton.GetColors(out var fore, out var back, HoverState);

		e.Graphics.FillRoundedRectangle(ClientRectangle.Gradient(back, 0.8F), ClientRectangle, Padding.Left);

		var titleHeight = Math.Max(24, (int)e.Graphics.Measure(Text, UI.Font(9.75F, FontStyle.Bold), Width - Padding.Horizontal).Height);
		var iconRectangle = new Rectangle(Padding.Left, Padding.Top + ((titleHeight - 24) / 2), 24, 24);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRectangle);
		}
		else if (Image != null)
		{
			using var icon = new Bitmap(Image);

			e.Graphics.DrawImage(icon.Color(fore), iconRectangle);
		}

		e.Graphics.DrawString(Text, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(fore), new Rectangle(24 + (Padding.Left * 2), Padding.Top, Width - Padding.Horizontal, titleHeight), new StringFormat { LineAlignment = StringAlignment.Center });

		var targetHeight = titleHeight + Padding.Vertical;

		CustomDraw(e, ref targetHeight);

		if (Height != targetHeight)
		{
			Height = targetHeight;
		}
	}

	protected abstract void CustomDraw(PaintEventArgs e, ref int targetHeight);

	protected void DrawValue(PaintEventArgs e, ref int targetHeight, string value, string descriptor, Color? foreColor = null)
	{
		SlickButton.GetColors(out var fore, out _, HoverState);

		if (foreColor != null && !HoverState.HasFlag(HoverState.Pressed))
		{
			fore = fore.MergeColor(foreColor.Value, 10);
		}

		var valueSize = e.Graphics.Measure(value, UI.Font(8.25F, FontStyle.Bold), Width - Padding.Horizontal).ToSize();
		var descriptorSize = e.Graphics.Measure(descriptor, UI.Font(8.25F), Width - valueSize.Width - Padding.Right + 3).ToSize();

		e.Graphics.DrawString(value, UI.Font(8.25F, FontStyle.Bold), new SolidBrush(fore), new Rectangle(Padding.Left, targetHeight, valueSize.Width + 3, valueSize.Height));

		e.Graphics.DrawString(descriptor, UI.Font(8.25F), new SolidBrush(fore), new Rectangle(valueSize.Width + Padding.Left, targetHeight, Width - valueSize.Width - Padding.Right + 3, Height));

		targetHeight += Math.Max(valueSize.Height, descriptorSize.Height) + Padding.Bottom;
	}

	protected void DrawText(PaintEventArgs e, ref int targetHeight, string text, Color? foreColor = null)
	{
		SlickButton.GetColors(out var fore, out _, HoverState);

		if (foreColor != null && !HoverState.HasFlag(HoverState.Pressed))
		{
			fore = fore.MergeColor(foreColor.Value, 10);
		}

		var valueSize = e.Graphics.Measure(text, UI.Font(8.25F), Width - Padding.Horizontal).ToSize();

		e.Graphics.DrawString(text, UI.Font(8.25F), new SolidBrush(fore), new Rectangle(Padding.Left, targetHeight, valueSize.Width + 3, valueSize.Height));

		targetHeight += valueSize.Height + Padding.Bottom;
	}
}
