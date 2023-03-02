using Extensions;

using LoadOrderToolTwo.UserInterface.Panels;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.IO;
using System.Windows.Forms;

namespace LoadOrderToolTwo
{
	public partial class MainForm : BasePanelForm
	{
		public MainForm()
		{
			InitializeComponent();

			try
			{ FormDesign.Initialize(this, DesignChanged); }
			catch { }

			try
			{ SetPanel<PC_MainPage>(PI_Dashboard); }
			catch (Exception ex)
			{ MessagePrompt.Show(ex.ToString(), "Error"); }

			new Action(CentralManager.Start).RunInBackground();
		}

		protected override void UIChanged()
		{
			base.UIChanged();
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
	}
}
