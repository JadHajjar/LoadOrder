using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Mods : PanelContent
{
	public PC_Mods()
	{
		InitializeComponent();

		Text = $"{Locale.Mods} - {ProfileManager.CurrentProfile.Name}";

		if (CentralManager.Mods == null)
		{
			modsListControl1.Loading = true;

			CentralManager.ModsLoaded += CentralManager_ModsLoaded;
		}
		else
			modsListControl1.SetItems(CentralManager.Mods);

		CentralManager.ModsUpdated += CentralManager_ModsLoaded;
	}

	private void CentralManager_ModsLoaded()
	{
		if (modsListControl1.Loading)
			modsListControl1.Loading = true;

		modsListControl1.SetItems(CentralManager.Mods);
	}
}
