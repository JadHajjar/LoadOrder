using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Mods : PanelContent
{
	private readonly ItemListControl<Mod> LC_Mods;

	public PC_Mods()
	{
		InitializeComponent();

		LC_Mods = new() { Dock = DockStyle.Fill, Margin = new() };

		TLP_Main.Controls.Add(LC_Mods, 0, 3);

		OT_Workshop.Visible = !CentralManager.CurrentProfile.LaunchSettings.NoWorkshop;

		LC_Mods.CanDrawItem += LC_Mods_CanDrawItem;

		if (!CentralManager.IsContentLoaded)
		{
			LC_Mods.Loading = true;
		}
		else
		{
			LC_Mods.SetItems(CentralManager.Mods);
		}

		CentralManager.ContentLoaded += CentralManager_ContentLoaded;
		CentralManager.WorkshopInfoUpdated += LC_Mods.Invalidate;
	}

	protected override void LocaleChanged()
	{
		Text = $"{Locale.Mods} - {ProfileManager.CurrentProfile.Name}";
		DD_PackageStatus.Text = Locale.ModStatus;
		DD_ReportSeverity.Text = Locale.ReportSeverity;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Search.Margin = UI.Scale(new Padding(5), UI.FontScale);
		TB_Search.Height = 1;
		P_Filters.Margin = P_Actions.Margin = UI.Scale(new Padding(10, 0, 10, 10), UI.FontScale);
		P_Filters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_Filter));
		//P_Actions.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_Wrench));
		B_ReDownload.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_ReDownload));
		B_ReDownload.Margin = UI.Scale(new Padding(5), UI.FontScale);
		I_ClearFilters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_ClearFilter));
		I_ClearFilters.Size = UI.FontScale >= 1.25 ? new(32, 32) : new(24, 24);
		I_ClearFilters.Location = new(P_Filters.Width - P_Filters.Padding.Right - I_ClearFilters.Width, P_Filters.Padding.Bottom);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Filters.BackColor = P_Actions.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 1, -1));
		BackColor = design.AccentBackColor;
		LC_Mods.BackColor = design.BackColor;
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.AccentBackColor;
	}

	private void LC_Mods_CanDrawItem(object sender, CanDrawItemEventArgs<Domain.Mod> e)
	{
		e.DoNotDraw = IsFilteredOut(e.Item);
	}

	private bool IsFilteredOut(Mod mod)
	{
		var doNotDraw = false;

		if (CentralManager.CurrentProfile.LaunchSettings.NoWorkshop)
		{
			doNotDraw = mod.Workshop;
		}

		if (!doNotDraw && CentralManager.CurrentProfile.ForAssetEditor)
		{
			doNotDraw = mod.Package.ForNormalGame == true;
		}

		if (!doNotDraw && CentralManager.CurrentProfile.ForGameplay)
		{
			doNotDraw = mod.Package.ForAssetEditor == true;
		}

		if (!doNotDraw && OT_Workshop.SelectedValue != ThreeOptionToggle.Value.None)
		{
			doNotDraw = OT_Workshop.SelectedValue == ThreeOptionToggle.Value.Option1 == mod.Workshop;
		}

		if (!doNotDraw && OT_Included.SelectedValue != ThreeOptionToggle.Value.None)
		{
			doNotDraw = OT_Included.SelectedValue == ThreeOptionToggle.Value.Option1 == mod.IsIncluded;
		}

		if (!doNotDraw && OT_Enabled.SelectedValue != ThreeOptionToggle.Value.None)
		{
			doNotDraw = OT_Enabled.SelectedValue == ThreeOptionToggle.Value.Option1 == mod.IsEnabled;
		}

		if (!doNotDraw && (int)DD_PackageStatus.SelectedItem != -1)
		{
			if (DD_PackageStatus.SelectedItem == DownloadStatus.None)
			{
				doNotDraw = mod.Workshop;
			}
			else
			{
				doNotDraw = DD_PackageStatus.SelectedItem != mod.Status;
			}
		}

		if (!doNotDraw && (int)DD_ReportSeverity.SelectedItem != -1)
		{
			doNotDraw = DD_ReportSeverity.SelectedItem != mod.Package.CompatibilityReport?.Severity;
		}

		if (!doNotDraw && !string.IsNullOrWhiteSpace(TB_Search.Text))
		{
			doNotDraw = !(
				TB_Search.Text.SearchCheck(mod.Name) ||
				TB_Search.Text.SearchCheck(mod.Author?.Name) ||
				TB_Search.Text.SearchCheck(mod.SteamId.ToString()));
		}

		return doNotDraw;
	}

	private void CentralManager_ContentLoaded()
	{
		if (LC_Mods.Loading)
		{
			LC_Mods.Loading = false;
		}

		LC_Mods.SetItems(CentralManager.Mods);
	}

	private void FilterChanged(object sender, EventArgs e)
	{
		LC_Mods.FilterChanged();
	}

	private void I_ClearFilters_Click(object sender, EventArgs e)
	{
		this.ClearForm();
	}

	private void B_ExInclude_LeftClicked(object sender, EventArgs e)
	{
		ModsUtil.SetIncluded(CentralManager.Mods.Where(x => !IsFilteredOut(x)), false);
	}

	private void B_ExInclude_RightClicked(object sender, EventArgs e)
	{
		ModsUtil.SetIncluded(CentralManager.Mods.Where(x => !IsFilteredOut(x)), true);
	}

	private void B_DisEnable_LeftClicked(object sender, EventArgs e)
	{
		ModsUtil.SetEnabled(CentralManager.Mods.Where(x => !IsFilteredOut(x)), false);
	}

	private void B_DisEnable_RightClicked(object sender, EventArgs e)
	{
		ModsUtil.SetEnabled(CentralManager.Mods.Where(x => !IsFilteredOut(x)), true);
	}

	private void B_ReDownload_Click(object sender, EventArgs e)
	{
		SteamUtil.ReDownload(CentralManager.Mods.Where(x => x.Status is DownloadStatus.OutOfDate or DownloadStatus.PartiallyDownloaded).Select(x => x.SteamId).ToArray());
	}
}
