using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
public class ThreeOptionToggle : SlickControl
{
    public enum Value
    {
        None = 0,
        Option1 = 1,
        Option2 = 2
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Value SelectedValue { get; set; }
    [Category("Design"), DefaultValue("")]
    public string Option1 { get; set; } = string.Empty;
	[Category("Design"), DefaultValue("")]
	public string Option2 { get; set; } = string.Empty;
    [Category("Appearance"), DefaultValue(ColorStyle.Red)]
    public ColorStyle OptionStyle1 { get; set; } = ColorStyle.Red;
    [Category("Appearance"), DefaultValue(ColorStyle.Green)]
    public ColorStyle OptionStyle2 { get; set; } = ColorStyle.Green;

	protected override void UIChanged()
	{
		Margin = UI.Scale(new Padding(5), UI.FontScale);
		Padding = UI.Scale(new Padding(4), UI.FontScale);
	}

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);

        var cursorLocation = PointToClient(Cursor.Position);
        var brush = !HoverState.HasFlag(HoverState.Hovered) ? ClientRectangle.Gradient(FormDesign.Design.ButtonColor) :
           null;

        e.Graphics.FillRoundedRectangle(brush, ClientRectangle, Padding.Left);
    }
}
