using Extensions;

using LoadOrderToolTwo.Domain.Utilities;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Options : PanelContent
{
	private bool folderPathsChanged;

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

		TB_VirtualAppDataPath.Visible = TB_VirtualGamePath.Visible = LocationManager.Platform is not Platform.Windows;

		TB_GamePath.Text = LocationManager.GamePath;
		TB_AppDataPath.Text = LocationManager.AppDataPath;
		TB_SteamPath.Text = LocationManager.SteamPath;
		TB_VirtualAppDataPath.Text = LocationManager.VirtualAppDataPath;
		TB_VirtualGamePath.Text = LocationManager.VirtualGamePath;

		folderPathsChanged = false;
	}

	protected override void LocaleChanged()
	{
		Text = Locale.Options;
		TLP_GeneralSettings.Text = Locale.Preferences;
		TLP_Folders.Text = Locale.FolderSettings;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TLP_GeneralSettings.Margin = TLP_Folders.Margin = UI.Scale(new Padding(10), UI.UIScale);
	}

	public override bool CanExit(bool toBeDisposed)
	{
		if (folderPathsChanged)
		{
			if (ShowPrompt(Locale.ChangingFoldersRequiresRestart, PromptButtons.OKCancel, PromptIcons.Hand) == System.Windows.Forms.DialogResult.OK)
			{
				LocationManager.SetPaths(TB_GamePath.Text, TB_AppDataPath.Text, TB_SteamPath.Text, TB_VirtualAppDataPath.Text, TB_VirtualGamePath.Text);

				Process.Start(Application.ExecutablePath);

				folderPathsChanged = false;

				Application.Exit();

				return false;
			}
		}

		return true;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		foreach (var tb in TLP_Folders.GetControls<SlickPathTextBox>())
		{
			tb.Image = Properties.Resources.I_FolderSearch;
			tb.Width = (int)(450 * UI.UIScale);
		}
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_GeneralSettings.BackColor = TLP_Folders.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 1, -1));
	}

	private void CB_CheckChanged(object sender, EventArgs e)
	{
		var cb = (sender as SlickCheckbox)!;

		typeof(SessionSettings)
			.GetProperty(cb.Tag!.ToString(), BindingFlags.Instance | BindingFlags.Public)
			.SetValue(CentralManager.SessionSettings, cb.Checked);

		CentralManager.SessionSettings.Save();
	}

	private void TB_FolderPath_TextChanged(object sender, EventArgs e)
	{
		folderPathsChanged = true;
	}
}
