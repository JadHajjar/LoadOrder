using Extensions;

using LoadOrderToolTwo.UserInterface.Panels;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Reflection;
using System.Windows.Forms;

namespace LoadOrderToolTwo;

public partial class MainForm : BasePanelForm
{
	private bool startBoundsSet;

	public MainForm()
	{
		InitializeComponent();

#if DEBUG
		L_Version.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
#else
		L_Version.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
#endif
		try
		{ FormDesign.Initialize(this, DesignChanged); }
		catch { }

		try
		{ SetPanel<PC_MainPage>(PI_Dashboard); }
		catch (Exception ex)
		{ MessagePrompt.Show(ex, "Failed to load the dashboard"); }

		new BackgroundAction("Loading content", CentralManager.Start).Run();
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (!startBoundsSet)
		{
			if (CentralManager.SessionSettings.WindowBounds != null)
			{
				DefaultBounds = Bounds = CentralManager.SessionSettings.WindowBounds.Value;
			}

			if (CentralManager.SessionSettings.WindowIsMaximized)
			{
				WindowState = FormWindowState.Maximized;
			}

			startBoundsSet = true;
		}
	}

	protected override void OnResizeEnd(EventArgs e)
	{
		base.OnResizeEnd(e);

		if (!TopMost && startBoundsSet)
		{
			if (!(CentralManager.SessionSettings.WindowIsMaximized = WindowState == FormWindowState.Maximized))
			{
				CentralManager.SessionSettings.WindowBounds = Bounds;
			}

			CentralManager.SessionSettings.Save();
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);

		if (!TopMost && startBoundsSet)
		{
			CentralManager.SessionSettings.WindowIsMaximized = WindowState == FormWindowState.Maximized;
			CentralManager.SessionSettings.Save();
		}
	}

	private void PI_Dashboard_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_MainPage>(PI_Dashboard);
	}

	private void PI_Mods_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Mods>(PI_Mods);
	}

	private void PI_Assets_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Assets>(PI_Assets);
	}

	private void PI_Profiles_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Profiles>(PI_Profiles);
	}

	private void PI_ModReview_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_ModUtilities>(PI_ModReview);
	}
}
