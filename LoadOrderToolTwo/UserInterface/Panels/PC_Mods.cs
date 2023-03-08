using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Mods : PanelContent
{
	private ItemListControl<Mod> LC_Mods;

	public PC_Mods()
	{
		InitializeComponent();

		LC_Mods = new() { Dock = DockStyle.Fill };

		TLP_Main.Controls.Add(LC_Mods, 0, 2);
		TLP_Main.SetColumnSpan(LC_Mods, 2);

		OT_Workshop.Visible = !CentralManager.CurrentProfile.LaunchSettings.NoWorkshop;

		LC_Mods.CanDrawItem += LC_Mods_CanDrawItem;

		Text = $"{Locale.Mods} - {ProfileManager.CurrentProfile.Name}";

		if (!CentralManager.IsContentLoaded)
		{
			LC_Mods.Loading = true;

			CentralManager.ContentLoaded += CentralManager_ContentLoaded;
		}
		else
		{
			LC_Mods.SetItems(CentralManager.Mods);
		}

		CentralManager.WorkshopInfoUpdated += LC_Mods.Invalidate;
	}

	private void LC_Mods_CanDrawItem(object sender, CanDrawItemEventArgs<Domain.Mod> e)
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

		if (!e.DoNotDraw && OT_Enabled.SelectedValue != ThreeOptionToggle.Value.None)
		{
			e.DoNotDraw |= OT_Enabled.SelectedValue == ThreeOptionToggle.Value.Option1 == e.Item.IsEnabled;
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
}
