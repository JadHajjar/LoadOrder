using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_MainPage : PanelContent
{
	private readonly System.Timers.Timer _citiesMonitorTimer = new(1000);
	private bool buttonStateRunning;
	public PC_MainPage()
	{
		InitializeComponent();

		Text = Locale.Dashboard;
		B_StartStop.Enabled = CentralManager.IsContentLoaded;

		if (!CentralManager.IsContentLoaded)
		{
			CentralManager.ContentLoaded += () => this.TryInvoke(() => B_StartStop.Enabled = true);
		}

		_citiesMonitorTimer.Elapsed += (s, e) => RefreshButtonState();
		_citiesMonitorTimer.Start();

		RefreshButtonState();
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

	private void B_StartStop_Click(object sender, System.EventArgs e)
	{
		if (CitiesManager.IsRunning())
		{
			B_StartStop.Loading = true;
			new Action(CitiesManager.Kill).RunInBackground();
		}
		else
		{
			B_StartStop.Loading = true;
			new Action(CitiesManager.Launch).RunInBackground();
		}
	}

	private void RefreshButtonState(bool firstTime = false)
	{
		var running = CitiesManager.IsRunning();

		if (!running)
		{
			if (buttonStateRunning || firstTime)
			{
				this.TryInvoke(() =>
				{
					B_StartStop.Text = Locale.StartCities;
					B_StartStop.Image = Properties.Resources.I_Launch;
					buttonStateRunning = false;
				});
			}

			return;
		}
		
		if (!buttonStateRunning || firstTime)
		{
			this.TryInvoke(() =>
			{
				B_StartStop.Text = Locale.StopCities;
				B_StartStop.Image = Properties.Resources.I_Stop;
				buttonStateRunning = true;
			});
		}
	}
}
