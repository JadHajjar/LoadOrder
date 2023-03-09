using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class PackageCompatibilityReportControl : FlowLayoutPanel
{
	public PackageCompatibilityReportControl(Package package)
	{
		Package = package;
		Report = package.CompatibilityReport;
		AutoSize = true;
		AutoSizeMode = AutoSizeMode.GrowAndShrink;
	}

	public Package Package { get; }
	public CompatibilityManager.ModInfo? Report { get; }

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (DesignMode)
		{
			return;
		}

		Padding = UI.Scale(new Padding(10), UI.FontScale);

		if (Report == null)
		{
			GenerateSection(Locale.CompatibilityReport, Properties.Resources.I_CompatibilityReport, FormDesign.Design.ButtonColor, new CompatibilityMessageControl(new CompatibilityManager.Message { message = Locale.CR_NoAvailableReport }));
			return;
		}

		if (Report.instability != null)
		{
			GenerateSection(Locale.CompatibilityReport, Properties.Resources.I_CompatibilityReport, FormDesign.Design.RedColor.MergeColor(BackColor, 80), new CompatibilityMessageControl(Report.instability));
		}
	}

	private void GenerateSection(string title, Bitmap image, Color backColor, params Control[] controls)
	{
		if (controls.Length == 0)
		{
			return;
		}

		var tlp = new RoundedTableLayoutPanel
		{
			Padding = Padding,
			AutoSize = true,
			AutoSizeMode = AutoSizeMode.GrowAndShrink,
			BackColor = backColor,
			ForeColor = backColor.GetTextColor(),
		};

		var icon = new PictureBox
		{
			Image = image.Color(tlp.ForeColor),
			Enabled = false,
			Anchor = AnchorStyles.Left,
			Size = new Size(32, 32),
			SizeMode =	 PictureBoxSizeMode.CenterImage,
			Margin = new Padding(0,0,0,10),
		};

		var label = new Label
		{
			Text = title,
			AutoSize = true,
			Anchor = AnchorStyles.Left,
			Font = UI.Font(10.25F, FontStyle.Bold),
			Margin = new Padding(0,0,0,10),
		};

		tlp.ColumnStyles.Add(new ColumnStyle());
		tlp.ColumnStyles.Add(new ColumnStyle());
		tlp.RowStyles.Add(new RowStyle());
		tlp.Controls.Add(icon, 0, 0);
		tlp.Controls.Add(label, 1, 0);

		var i = 1;
		foreach (var item in controls)
		{
			tlp.RowStyles.Add(new RowStyle());
			tlp.Controls.Add(item, 0, i++);
			tlp.SetColumnSpan(item, 2);
		}

		tlp.RowCount = tlp.RowStyles.Count;
		tlp.ColumnCount = tlp.ColumnStyles.Count;

		Controls.Add(tlp);
	}
}
