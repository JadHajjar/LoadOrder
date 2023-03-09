using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class PackageCompatibilityReportControl : FlowLayoutPanel
{
    public PackageCompatibilityReportControl(Package package)
    {
        Package = package;
        Report = package.CompatibilityReport;
    }

	public Package Package { get; }
	public CompatibilityManager.ModInfo? Report { get; }

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (DesignMode)
			return;

		Padding = UI.Scale(new Padding(10), UI.FontScale);

		GenerateSection("", Properties.Resources.I_Add, Color.Empty);
	}

	private void GenerateSection(string title, Bitmap image, Color backColor, params Control[] controls)
	{
		if (controls.Length == 0)
			return;

		var flp = new RoundedFlowLayoutPanel
		{
			Padding = Padding,
			AutoSize = true,
			BackColor = backColor
		};

		var icon = new SlickIcon
		{
			Image = image,
			Enabled = false,
			Anchor = AnchorStyles.Left,
			Size = new Size(32, 32)
		};

		var label = new Label
		{
			Text = title,
			Anchor = AnchorStyles.Left,
			Font = UI.Font(11.25F, FontStyle.Bold)
		};

		flp.Controls.Add(icon);
		flp.Controls.Add(label);
		flp.SetFlowBreak(label, true);

		foreach (var item in controls)
		{
			flp.Controls.Add(item);
			flp.SetFlowBreak(item, true);
		}
	}
}
