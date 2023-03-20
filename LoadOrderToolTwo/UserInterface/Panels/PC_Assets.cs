﻿using Extensions;
using LoadOrderToolTwo.Utilities.Managers;
using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using LoadOrderToolTwo.Domain;
using System.Windows.Forms;
using System.Drawing;
using LoadOrderToolTwo.Domain.Utilities;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Assets : PanelContent
{
	private DelayedAction _delayedSearch = new(350);
	private ItemListControl<Asset> LC_Assets;

	public PC_Assets()
	{
		InitializeComponent();

		LC_Assets = new() { Dock = DockStyle.Fill, Margin = new() };

		TLP_Main.Controls.Add(LC_Assets, 0, 2);
		TLP_Main.SetColumnSpan(LC_Assets, 2);

		OT_Workshop.Visible = !CentralManager.CurrentProfile.LaunchSettings.NoWorkshop;

		LC_Assets.CanDrawItem += LC_Assets_CanDrawItem;

		Text = $"{Locale.Assets} - {ProfileManager.CurrentProfile.Name}";
		DD_PackageStatus.Text = Locale.AssetStatus;
		P_Filters.Text = Locale.Filters;

		if (!CentralManager.IsContentLoaded)
		{
			LC_Assets.Loading = true;
		}
		else
		{
			LC_Assets.SetItems(CentralManager.Assets);
		}

		CentralManager.ContentLoaded += CentralManager_ContentLoaded;
		CentralManager.WorkshopInfoUpdated += LC_Assets.Invalidate;
	}

	private void LC_Assets_CanDrawItem(object sender, CanDrawItemEventArgs<Domain.Asset> e)
	{
		if (CentralManager.CurrentProfile.LaunchSettings.NoWorkshop)
		{
			e.DoNotDraw = e.Item.Workshop;
		}

		if (!e.DoNotDraw && CentralManager.CurrentProfile.ForAssetEditor)
		{
			e.DoNotDraw = e.Item.Package.ForNormalGame == true;
		}

		if (!e.DoNotDraw && CentralManager.CurrentProfile.ForGameplay)
		{
			e.DoNotDraw = e.Item.Package.ForAssetEditor == true;
		}

		if (!e.DoNotDraw && CentralManager.SessionSettings.LinkModAssets)
		{
			e.DoNotDraw = e.Item.Package.Mod is not null;
		}

		if (!e.DoNotDraw && OT_Workshop.SelectedValue != ThreeOptionToggle.Value.None)
		{
			e.DoNotDraw = OT_Workshop.SelectedValue == ThreeOptionToggle.Value.Option1 == e.Item.Workshop;
		}

		if (!e.DoNotDraw && OT_Included.SelectedValue != ThreeOptionToggle.Value.None)
		{
			e.DoNotDraw = OT_Included.SelectedValue == ThreeOptionToggle.Value.Option1 == e.Item.IsIncluded;
		}

		if (!e.DoNotDraw && (int)DD_PackageStatus.SelectedItem != -1)
		{
			if (DD_PackageStatus.SelectedItem == DownloadStatus.None)
			{
				e.DoNotDraw = e.Item.Workshop;
			}
			else
			{
				e.DoNotDraw = DD_PackageStatus.SelectedItem != e.Item.Status;
			}
		}

		if (!e.DoNotDraw && !string.IsNullOrWhiteSpace(TB_Search.Text))
		{
			e.DoNotDraw = !(
				TB_Search.Text.SearchCheck(e.Item.Name) ||
				TB_Search.Text.SearchCheck(e.Item.Author?.Name) ||
				TB_Search.Text.SearchCheck(e.Item.SteamId.ToString()));
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Search.Margin = UI.Scale(new Padding(5), UI.FontScale);
		TB_Search.Height = 1;
		P_Filters.Margin = UI.Scale(new Padding(10, 0, 10, 10), UI.FontScale);
		P_Filters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_Filter));
		I_ClearFilters.Image = ImageManager.GetIcon(nameof(Properties.Resources.I_ClearFilter));
		I_ClearFilters.Size = UI.FontScale >= 1.25 ? new(32, 32) : new(24, 24);
		I_ClearFilters.Location = new(P_Filters.Width - P_Filters.Padding.Right - I_ClearFilters.Width, P_Filters.Padding.Bottom);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Filters.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 1, -1));
		BackColor = design.AccentBackColor;
		LC_Assets.BackColor = design.BackColor;
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.AccentBackColor;
	}

	private void CentralManager_ContentLoaded()
	{
		if (LC_Assets.Loading)
		{
			LC_Assets.Loading = false;
		}

		LC_Assets.SetItems(CentralManager.Assets);
	}

	private void FilterChanged(object sender, EventArgs e)
	{
		LC_Assets.FilterOrSortingChanged();
	}

	private void I_ClearFilters_Click(object sender, EventArgs e)
	{
		this.ClearForm();
	}

	private void TB_Search_TextChanged(object sender, EventArgs e)
	{
		TB_Search.Loading = true;
		_delayedSearch.Run(DelayedFilterChanged);
	}

	private void DelayedFilterChanged()
	{
		LC_Assets.FilterOrSortingChanged();
		TB_Search.Loading = false;
	}
}