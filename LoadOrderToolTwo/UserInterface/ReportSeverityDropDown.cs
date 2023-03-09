using Extensions;

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
    public ReportSeverityDropDown()
    {
		Items = Enum.GetValues(typeof(ReportSeverity)).Cast<ReportSeverity>().ToArray();
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

		Height = (int)(28 * UI.UIScale);
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, ReportSeverity item)
	{
		var text = LocaleHelper.GetGlobalText($"CR_{item}");
		var color = (item switch
		{
			ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
			ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
			ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
			ReportSeverity.Remarks => FormDesign.Design.ForeColor,
			_ => FormDesign.Design.GreenColor
		});

		using var icon = ImageManager.GetIcon("I_CompatibilityReport").Color(hoverState.HasFlag(HoverState.Pressed) ? foreColor : color);

		e.Graphics.DrawImage(icon, rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));

		e.Graphics.DrawString(text, Font, new SolidBrush(foreColor), rectangle.Pad(icon.Width + Padding.Left, 0, 0, 1), new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
	}
}
