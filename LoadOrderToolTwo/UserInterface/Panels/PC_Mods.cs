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
		
		RefreshCounts();

		CentralManager.ContentLoaded += CentralManager_ContentLoaded;
		CentralManager.WorkshopInfoUpdated += CentralManager_WorkshopInfoUpdated;
		CentralManager.ModInformationUpdated += _ => this.TryInvoke(RefreshCounts);
	}

	private void CentralManager_WorkshopInfoUpdated()
	{
		LC_Mods.Invalidate();

		this.TryInvoke(RefreshCounts);
	}

	private void RefreshCounts()
	{
		var modsIncluded = CentralManager.Mods.Count(x => x.IsIncluded);
		var modsEnabled = CentralManager.Mods.Count(x => x.IsEnabled && x.IsIncluded);

		if (!CentralManager.SessionSettings.AdvancedIncludeEnable)
		{
			L_Counts.Text = $"{modsIncluded} {(modsIncluded == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural)}, {CentralManager.Mods.Count()} {Locale.Total.ToLower()}";
		}
		if (modsIncluded == modsEnabled)
		{
			L_Counts.Text = $"{modsIncluded} {(modsIncluded == 1 ? Locale.ModIncludedAndEnabled : Locale.ModIncludedAndEnabledPlural)}, {CentralManager.Mods.Count()} {Locale.Total.ToLower()}";
		}
		else
		{
			L_Counts.Text = $"{modsIncluded} {(modsIncluded == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural)} && {modsEnabled} {(modsEnabled == 1 ? Locale.ModEnabled : Locale.ModEnabledPlural)}, {CentralManager.Mods.Count()} {Locale.Total.ToLower()}";
		}

		L_Counts.Location = new(I_ClearFilters.Left - P_Filters.Padding.Right - L_Counts.Width, P_Filters.Padding.Bottom + (I_ClearFilters.Height - L_Counts.Height) / 2);
	}

	protected override void LocaleChanged()
	{
		Text = $"{Locale.Mods} - {ProfileManager.CurrentProfile.Name}";
		DD_PackageStatus.Text = Locale.ModStatus;
		DD_ReportSeverity.Text = Locale.ReportSeverity;
		DD_Sorting.Text = Locale.Sorting;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Search.Margin = UI.Scale(new Padding(5), UI.FontScale);
		TB_Search.Height = 1;
		P_Filters.Margin = P_Actions.Margin = UI.Scale(new Padding(10, 0, 10, 10), UI.FontScale);
		P_Filters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_Filter));
		I_ClearFilters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_ClearFilter));
		I_ClearFilters.Size = UI.FontScale >= 1.25 ? new(32, 32) : new(24, 24);
		I_ClearFilters.Location = new(P_Filters.Width - P_Filters.Padding.Right - I_ClearFilters.Width, P_Filters.Padding.Bottom);
		L_Counts.Font = UI.Font(7.5F, FontStyle.Bold);
		L_Counts.Location = new(I_ClearFilters.Left - P_Filters.Padding.Right - L_Counts.Width, P_Filters.Padding.Bottom + (I_ClearFilters.Height - L_Counts.Height) / 2);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Filters.BackColor = P_Actions.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 1, -1));
		BackColor = design.AccentBackColor;
		LC_Mods.BackColor = design.BackColor;
		L_Counts.ForeColor = design.InfoColor;
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

		this.TryInvoke(RefreshCounts);
	}

	private void FilterChanged(object sender, EventArgs e)
	{
		LC_Mods.FilterOrSortingChanged();
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

	private void DD_Sorting_SelectedItemChanged(object sender, EventArgs e)
	{
		LC_Mods.SetSorting(DD_Sorting.SelectedItem);
	}
}
