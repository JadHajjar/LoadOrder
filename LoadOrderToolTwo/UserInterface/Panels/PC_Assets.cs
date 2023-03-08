using Extensions;
using LoadOrderToolTwo.Utilities.Managers;
using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using LoadOrderToolTwo.Domain;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Assets : PanelContent
{
	private ItemListControl<Asset> LC_Assets;

	public PC_Assets()
	{
		InitializeComponent();

		LC_Assets = new() { Dock = DockStyle.Fill };

		TLP_Main.Controls.Add(LC_Assets, 0, 2);
		TLP_Main.SetColumnSpan(LC_Assets, 2);

		OT_Workshop.Visible = !CentralManager.CurrentProfile.LaunchSettings.NoWorkshop;

		LC_Assets.CanDrawItem += LC_Assets_CanDrawItem;

		Text = $"{Locale.Assets} - {ProfileManager.CurrentProfile.Name}";

		if (!CentralManager.IsContentLoaded)
		{
			LC_Assets.Loading = true;

			CentralManager.ContentLoaded += CentralManager_ContentLoaded;
		}
		else
		{
			LC_Assets.SetItems(CentralManager.Assets);
		}

		CentralManager.WorkshopInfoUpdated += LC_Assets.Invalidate;
	}

	private void LC_Assets_CanDrawItem(object sender, CanDrawItemEventArgs<Domain.Asset> e)
	{
		if (CentralManager.CurrentProfile.LaunchSettings.NoWorkshop)
		{
			e.DoNotDraw = e.Item.Workshop;
		}

		if (!e.DoNotDraw && OT_Workshop.SelectedValue != ThreeOptionToggle.Value.None)
		{
			e.DoNotDraw |= OT_Workshop.SelectedValue == ThreeOptionToggle.Value.Option1 == e.Item.Workshop;
		}

		if (!e.DoNotDraw && OT_Included.SelectedValue != ThreeOptionToggle.Value.None)
		{
			e.DoNotDraw |= OT_Included.SelectedValue == ThreeOptionToggle.Value.Option1 == e.Item.IsIncluded;
		}

		if (!e.DoNotDraw && !string.IsNullOrWhiteSpace(TB_Search.Text))
		{
			e.DoNotDraw |= !(e.Item.Name.SearchCheck(TB_Search.Text)
				|| (e.Item.Author?.Name.SearchCheck(TB_Search.Text) ?? false)
				|| e.Item.SteamId.ToString().SearchCheck(TB_Search.Text));
		}
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
		LC_Assets.FilterChanged();
	}
}
