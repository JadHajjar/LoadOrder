﻿using Extensions;

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

using static CompatibilityReport.CatalogData.Enums;

namespace LoadOrderToolTwo.UserInterface;
internal class ReportSeverityDropDown : SlickSelectionDropDown<ReportSeverity>
{
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (Live)
		{
			Items = new[] { (ReportSeverity)(-1) }.Concat(Enum.GetValues(typeof(ReportSeverity)).Cast<ReportSeverity>()).ToArray();
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

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, ReportSeverity item)
	{
		var text = (int)item == -1 ? Locale.AnyReportStatus : LocaleHelper.GetGlobalText($"CR_{item}");
		var color = (item switch
		{
			ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
			ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
			ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
			ReportSeverity.Remarks or (ReportSeverity)(-1) => FormDesign.Design.ForeColor,
			_ => FormDesign.Design.GreenColor
		});

		using var icon = (int)item == -1 ? ImageManager.GetIcon("I_Slash") : item.GetSeverityIcon(true);

		e.Graphics.DrawImage(icon.Color(color), rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));

		var textRect = new Rectangle(rectangle.X + icon.Width + Padding.Left, rectangle.Y + (rectangle.Height - Font.Height)/2,0,Font.Height);

		textRect.Width = rectangle.Width - textRect.X;

		e.Graphics.DrawString(text, Font, new SolidBrush(foreColor), textRect, new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
	}
}
