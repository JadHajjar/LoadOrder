using Extensions;

using LoadOrderToolTwo.Domain.Utilities;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Reflection;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Options : PanelContent
{
	public PC_Options()
	{
		InitializeComponent();

		foreach (var cb in this.GetControls<SlickCheckbox>())
		{
			if (!string.IsNullOrWhiteSpace(cb.Tag?.ToString()))
			{
				cb.Checked = (bool)typeof(SessionSettings)
					.GetProperty(cb.Tag!.ToString(), BindingFlags.Instance | BindingFlags.Public)
					.GetValue(CentralManager.SessionSettings);
			}
		}
	}

	protected override void LocaleChanged()
	{
		Text = Locale.Options;
		TLP_GeneralSettings.Text = Locale.Preferences;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_GeneralSettings.BackColor = design.ButtonColor;
	}

	private void CB_CheckChanged(object sender, EventArgs e)
	{
		var cb = (sender as SlickCheckbox)!;

		typeof(SessionSettings)
			.GetProperty(cb.Tag!.ToString(), BindingFlags.Instance | BindingFlags.Public)
			.SetValue(CentralManager.SessionSettings, cb.Checked);

		CentralManager.SessionSettings.Save();
	}
}
