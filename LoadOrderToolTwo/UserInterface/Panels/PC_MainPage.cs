using LoadOrderToolTwo.Utilities;

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
public partial class PC_MainPage : PanelContent
{
	public PC_MainPage()
	{
		InitializeComponent();

		Text = Locale.Dashboard;
		B_StartStop.Text = Locale.StartCities;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		B_StartStop.Font = UI.Font(9.75F, FontStyle.Bold);
	}

	private void ProfileBubble_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			Form.PushPanel<PC_Profiles>(null);
		}
	}

	private void ModsBubble_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			Form.PushPanel<PC_Mods>((Form as MainForm)?.PI_Mods);
		}
	}

	private void AssetsBubble_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			Form.PushPanel<PC_Assets>((Form as MainForm)?.PI_Assets);
		}
	}
}
