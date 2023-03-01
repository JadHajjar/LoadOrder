﻿using Extensions;

using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
public class ThreeOptionToggle : SlickControl
{
	private Value _selectedValue;

	public enum Value
	{
		None = 0,
		Option1 = 1,
		Option2 = 2
	}

	public event EventHandler? SelectedValueChanged;

	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Value SelectedValue { get => _selectedValue; set { _selectedValue = value; Invalidate(); SelectedValueChanged?.Invoke(this, EventArgs.Empty); } }
	[Category("Design"), DefaultValue("")]
	public string Option1 { get; set; } = string.Empty;
	[Category("Design"), DefaultValue("")]
	public string Option2 { get; set; } = string.Empty;
	[Category("Appearance"), DefaultValue(ColorStyle.Red)]
	public ColorStyle OptionStyle1 { get; set; } = ColorStyle.Red;
	[Category("Appearance"), DefaultValue(ColorStyle.Green)]
	public ColorStyle OptionStyle2 { get; set; } = ColorStyle.Green;
	[Category("Appearance"), DefaultValue(null)]
	public Image? Image1 { get; set; }
	[Category("Appearance"), DefaultValue(null)]
	public Image? Image2 { get; set; }

	public ThreeOptionToggle()
	{
		Cursor = Cursors.Hand;
	}

	protected override void UIChanged()
	{
		Margin = UI.Scale(new Padding(5), UI.FontScale);
		Padding = UI.Scale(new Padding(5), UI.FontScale);
		Height = (int)(28 * UI.FontScale);
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		var centerWidth = 50;
		var option1Hovered = e.Location.X < (Width - centerWidth) / 2;
		var option2Hovered = e.Location.X > (Width + centerWidth) / 2;
		var noneHovered = e.Location.X.IsWithin(Width / 2 - centerWidth / 2 - 1, Width / 2 + centerWidth / 2 + 1);

		if (option1Hovered)
		{
			SelectedValue = Value.Option1;
		}
		else if (option2Hovered)
		{
			SelectedValue = Value.Option2;
		}
		else if (noneHovered)
		{
			SelectedValue = Value.None;
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.Clear(BackColor);
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

		var iconSize = UI.FontScale >= 1.5 ? 24 : 16;
		var centerWidth = (int)(40 * UI.FontScale);
		var cursorLocation = PointToClient(Cursor.Position);
		var rectangle1 = new Rectangle(0, 0, (Width - centerWidth) / 2, Height);
		var rectangle2 = new Rectangle((Width + centerWidth) / 2, 0, (Width - centerWidth) / 2, Height);
		var rectangleNone = new Rectangle((Width - centerWidth) / 2, 0, centerWidth, Height);
		var option1Hovered = rectangle1.Contains(cursorLocation) && HoverState.HasFlag(HoverState.Hovered);
		var option2Hovered = rectangle2.Contains(cursorLocation) && HoverState.HasFlag(HoverState.Hovered);
		var noneHovered = rectangleNone.Contains(cursorLocation) && HoverState.HasFlag(HoverState.Hovered);
		var textColor1 = SelectedValue == Value.Option1 || (option1Hovered && HoverState.HasFlag(HoverState.Pressed)) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor;
		var textColor2 = SelectedValue == Value.Option2 || (option2Hovered && HoverState.HasFlag(HoverState.Pressed)) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor;
		var textColorNone = SelectedValue == Value.None || (noneHovered && HoverState.HasFlag(HoverState.Pressed)) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor;

		e.Graphics.FillRoundedRectangle(ClientRectangle.Gradient(FormDesign.Design.ButtonColor), ClientRectangle, Padding.Left);

		// Option 1
		if (option1Hovered || SelectedValue == Value.Option1)
		{
			e.Graphics.FillRoundedRectangle(rectangle1.Gradient(Color.FromArgb(HoverState.HasFlag(HoverState.Pressed) || SelectedValue == Value.Option1 ? 255 : 100, OptionStyle1.GetColor())), rectangle1, Padding.Left, topRight: false, botRight: false);
		}

		if (Image1 != null)
		{
			using var img1 = new Bitmap(Image1).Color(textColor1);
			e.Graphics.DrawImage(img1, new Rectangle(Padding.Left, (Height - iconSize) / 2, iconSize, iconSize));
		}

		e.Graphics.DrawString(LocaleHelper.GetGlobalText(Option1), Font, new SolidBrush(textColor1), rectangle1.Pad(Padding).Pad(Image1 != null ? (Image1.Width) : 0, 0, 0, 0), new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

		// Option 2
		if (option2Hovered || SelectedValue == Value.Option2)
		{
			e.Graphics.FillRoundedRectangle(rectangle2.Gradient(Color.FromArgb(HoverState.HasFlag(HoverState.Pressed) || SelectedValue == Value.Option2 ? 255 : 100, OptionStyle2.GetColor())), rectangle2, Padding.Left, topLeft: false, botLeft: false);
		}

		if (Image2 != null)
		{
			using var img2 = new Bitmap(Image2).Color(textColor2);
			e.Graphics.DrawImage(img2, new Rectangle(Width - iconSize - Padding.Right, (Height - iconSize) / 2, iconSize, iconSize));
		}

		e.Graphics.DrawString(LocaleHelper.GetGlobalText(Option2), Font, new SolidBrush(textColor2), rectangle2.Pad(Padding).Pad(0, 0, Image2 != null ? (Image2.Width) : 0, 0), new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

		// Center
		if (noneHovered || SelectedValue == Value.None)
		{
			e.Graphics.FillRectangle(rectangleNone.Gradient(Color.FromArgb(HoverState.HasFlag(HoverState.Pressed) || SelectedValue == Value.None ? 255 : 100, FormDesign.Design.ActiveColor)), rectangleNone);
		}

		using var slash = Properties.Resources.I_Slash.Color(textColorNone);
		e.Graphics.DrawImage(slash, ClientRectangle.CenterR(iconSize, iconSize));
	}
}
