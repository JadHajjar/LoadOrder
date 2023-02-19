using Extensions;

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
			{ SetPanel<PC_MainPage>(null); }
			catch (Exception ex)
			{ MessagePrompt.Show(ex.ToString(), "Error"); }
		}

		protected override void UIChanged()
		{
			base.UIChanged();
		}
	}
}
