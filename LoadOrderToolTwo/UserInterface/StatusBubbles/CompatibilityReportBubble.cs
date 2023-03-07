using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ReportSeverity = CompatibilityReport.CatalogData.Enums.ReportSeverity;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;
internal class CompatibilityReportBubble : StatusBubbleBase
{
	public CompatibilityReportBubble()
	{ }

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (!Live)
		{
			return;
		}

		Image = Properties.Resources.I_CompatibilityReport;
		Text = Locale.CompatibilityReport;

		if (!CentralManager.IsContentLoaded)
		{
			Loading = true;

			CentralManager.ContentLoaded += CentralManager_InfoUpdated;
		}
	}

	private void CentralManager_InfoUpdated()
	{
		if (Loading)
		{
			Loading = false;
		}
		else
		{
			Invalidate();
		}
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (CentralManager.IsContentLoaded && !CompatibilityManager.CatalogAvailable)
		{
			try
			{ Process.Start("https://steamcommunity.com/sharedfiles/filedetails/?id=2881031511"); }
			catch { }
		}
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		if (!CentralManager.IsContentLoaded)
		{
			DrawText(e, ref targetHeight, Locale.Loading, FormDesign.Design.InfoColor);
			return;
		}

		if (!CompatibilityManager.CatalogAvailable)
		{
			DrawText(e, ref targetHeight, "Compatibility report is not available, click to subscribe to the mod", FormDesign.Design.RedColor);
			return;
		}

		var groups = CentralManager.Mods.GroupBy(x => x.Package.CompatibilityReport?.reportSeverity);

		DrawValue(e, ref targetHeight, groups.Sum(x => !(x.Key > ReportSeverity.Remarks) ? x.Count() : 0).ToString(), "mods with no issues");

		foreach ( var group in groups.OrderBy(x => x.Key))
		{
			if (!(group.Key > ReportSeverity.Remarks))
			{
				continue;
			}

			DrawValue(e, ref targetHeight, group.Count().ToString(), group.Key switch
			{
				ReportSeverity.MinorIssues => "mods with minor issues",
				ReportSeverity.MajorIssues => "mods with major issues",
				ReportSeverity.Unsubscribe => "mods that should be unsubscribed from",
				_ => ""
			}, group.Key switch
			{
				ReportSeverity.MinorIssues => FormDesign.Design.YellowColor,
				ReportSeverity.MajorIssues => FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.RedColor),
				ReportSeverity.Unsubscribe => FormDesign.Design.RedColor,
				_ => Color.Empty
			});
		}
	}
}
