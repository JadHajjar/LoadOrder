﻿namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_Options
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_Options));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Folders = new SlickControls.RoundedGroupTableLayoutPanel();
			this.TB_VirtualAppDataPath = new SlickControls.SlickPathTextBox();
			this.TB_VirtualGamePath = new SlickControls.SlickPathTextBox();
			this.TB_SteamPath = new SlickControls.SlickPathTextBox();
			this.TB_AppDataPath = new SlickControls.SlickPathTextBox();
			this.TB_GamePath = new SlickControls.SlickPathTextBox();
			this.TLP_GeneralSettings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickCheckbox2 = new SlickControls.SlickCheckbox();
			this.slickCheckbox1 = new SlickControls.SlickCheckbox();
			this.CB_LinkModAssets = new SlickControls.SlickCheckbox();
			this.slickCheckbox3 = new SlickControls.SlickCheckbox();
			this.slickCheckbox4 = new SlickControls.SlickCheckbox();
			this.slickCheckbox5 = new SlickControls.SlickCheckbox();
			this.slickCheckbox6 = new SlickControls.SlickCheckbox();
			this.DD_Language = new LoadOrderToolTwo.UserInterface.LanguageDropDown();
			this.tableLayoutPanel1.SuspendLayout();
			this.TLP_Folders.SuspendLayout();
			this.TLP_GeneralSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(77, 26);
			this.base_Text.Text = "Language";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.TLP_Folders, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.TLP_GeneralSettings, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 544);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// TLP_Folders
			// 
			this.TLP_Folders.AddOutline = true;
			this.TLP_Folders.AutoSize = true;
			this.TLP_Folders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Folders.ColumnCount = 1;
			this.TLP_Folders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Folders.Controls.Add(this.TB_VirtualAppDataPath, 0, 4);
			this.TLP_Folders.Controls.Add(this.TB_VirtualGamePath, 0, 3);
			this.TLP_Folders.Controls.Add(this.TB_SteamPath, 0, 2);
			this.TLP_Folders.Controls.Add(this.TB_AppDataPath, 0, 1);
			this.TLP_Folders.Controls.Add(this.TB_GamePath, 0, 0);
			this.TLP_Folders.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Folders.Image = ((System.Drawing.Image)(resources.GetObject("TLP_Folders.Image")));
			this.TLP_Folders.Location = new System.Drawing.Point(3, 341);
			this.TLP_Folders.Name = "TLP_Folders";
			this.TLP_Folders.Padding = new System.Windows.Forms.Padding(7, 38, 7, 7);
			this.TLP_Folders.RowCount = 5;
			this.TLP_Folders.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Folders.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Folders.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Folders.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Folders.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Folders.Size = new System.Drawing.Size(767, 200);
			this.TLP_Folders.TabIndex = 19;
			// 
			// TB_VirtualAppDataPath
			// 
			this.TB_VirtualAppDataPath.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_VirtualAppDataPath.FileExtensions = new string[0];
			this.TB_VirtualAppDataPath.Folder = true;
			this.TB_VirtualAppDataPath.Image = ((System.Drawing.Image)(resources.GetObject("TB_VirtualAppDataPath.Image")));
			this.TB_VirtualAppDataPath.LabelText = "VirtualAppDataPath";
			this.TB_VirtualAppDataPath.Location = new System.Drawing.Point(12, 228);
			this.TB_VirtualAppDataPath.Margin = new System.Windows.Forms.Padding(5);
			this.TB_VirtualAppDataPath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_VirtualAppDataPath.Name = "TB_VirtualAppDataPath";
			this.TB_VirtualAppDataPath.Placeholder = "/home/user/.local/share/Colossal Order/Cities_Skylines";
			this.TB_VirtualAppDataPath.SelectedText = "";
			this.TB_VirtualAppDataPath.SelectionLength = 0;
			this.TB_VirtualAppDataPath.SelectionStart = 0;
			this.TB_VirtualAppDataPath.Size = new System.Drawing.Size(743, 35);
			this.TB_VirtualAppDataPath.TabIndex = 4;
			this.TB_VirtualAppDataPath.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// TB_VirtualGamePath
			// 
			this.TB_VirtualGamePath.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_VirtualGamePath.FileExtensions = new string[0];
			this.TB_VirtualGamePath.Folder = true;
			this.TB_VirtualGamePath.Image = ((System.Drawing.Image)(resources.GetObject("TB_VirtualGamePath.Image")));
			this.TB_VirtualGamePath.LabelText = "VirtualGamePath";
			this.TB_VirtualGamePath.Location = new System.Drawing.Point(12, 183);
			this.TB_VirtualGamePath.Margin = new System.Windows.Forms.Padding(5);
			this.TB_VirtualGamePath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_VirtualGamePath.Name = "TB_VirtualGamePath";
			this.TB_VirtualGamePath.Placeholder = "/home/user/.steam/steam/steamapps/common/Cities_Skylines";
			this.TB_VirtualGamePath.SelectedText = "";
			this.TB_VirtualGamePath.SelectionLength = 0;
			this.TB_VirtualGamePath.SelectionStart = 0;
			this.TB_VirtualGamePath.Size = new System.Drawing.Size(743, 35);
			this.TB_VirtualGamePath.TabIndex = 3;
			this.TB_VirtualGamePath.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// TB_SteamPath
			// 
			this.TB_SteamPath.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_SteamPath.FileExtensions = new string[0];
			this.TB_SteamPath.Folder = true;
			this.TB_SteamPath.Image = ((System.Drawing.Image)(resources.GetObject("TB_SteamPath.Image")));
			this.TB_SteamPath.LabelText = "SteamPath";
			this.TB_SteamPath.Location = new System.Drawing.Point(12, 138);
			this.TB_SteamPath.Margin = new System.Windows.Forms.Padding(5);
			this.TB_SteamPath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_SteamPath.Name = "TB_SteamPath";
			this.TB_SteamPath.Placeholder = "C:\\Program Files (x86)\\Steam";
			this.TB_SteamPath.SelectedText = "";
			this.TB_SteamPath.SelectionLength = 0;
			this.TB_SteamPath.SelectionStart = 0;
			this.TB_SteamPath.Size = new System.Drawing.Size(743, 35);
			this.TB_SteamPath.TabIndex = 2;
			this.TB_SteamPath.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// TB_AppDataPath
			// 
			this.TB_AppDataPath.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_AppDataPath.FileExtensions = new string[0];
			this.TB_AppDataPath.Folder = true;
			this.TB_AppDataPath.Image = ((System.Drawing.Image)(resources.GetObject("TB_AppDataPath.Image")));
			this.TB_AppDataPath.LabelText = "AppDataPath";
			this.TB_AppDataPath.Location = new System.Drawing.Point(12, 93);
			this.TB_AppDataPath.Margin = new System.Windows.Forms.Padding(5);
			this.TB_AppDataPath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_AppDataPath.Name = "TB_AppDataPath";
			this.TB_AppDataPath.Placeholder = "%LocalAppData%\\Colossal Order\\Cities_Skylines";
			this.TB_AppDataPath.SelectedText = "";
			this.TB_AppDataPath.SelectionLength = 0;
			this.TB_AppDataPath.SelectionStart = 0;
			this.TB_AppDataPath.Size = new System.Drawing.Size(743, 35);
			this.TB_AppDataPath.TabIndex = 1;
			this.TB_AppDataPath.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// TB_GamePath
			// 
			this.TB_GamePath.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_GamePath.FileExtensions = new string[0];
			this.TB_GamePath.Folder = true;
			this.TB_GamePath.Image = ((System.Drawing.Image)(resources.GetObject("TB_GamePath.Image")));
			this.TB_GamePath.LabelText = "GamePath";
			this.TB_GamePath.Location = new System.Drawing.Point(12, 48);
			this.TB_GamePath.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
			this.TB_GamePath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_GamePath.Name = "TB_GamePath";
			this.TB_GamePath.Placeholder = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Cities_Skylines";
			this.TB_GamePath.SelectedText = "";
			this.TB_GamePath.SelectionLength = 0;
			this.TB_GamePath.SelectionStart = 0;
			this.TB_GamePath.Size = new System.Drawing.Size(743, 35);
			this.TB_GamePath.TabIndex = 0;
			this.TB_GamePath.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// TLP_GeneralSettings
			// 
			this.TLP_GeneralSettings.AddOutline = true;
			this.TLP_GeneralSettings.AutoSize = true;
			this.TLP_GeneralSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_GeneralSettings.ColumnCount = 2;
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox6, 0, 6);
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox5, 0, 4);
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox3, 0, 3);
			this.TLP_GeneralSettings.Controls.Add(this.DD_Language, 1, 0);
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox2, 0, 2);
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox1, 0, 1);
			this.TLP_GeneralSettings.Controls.Add(this.CB_LinkModAssets, 0, 0);
			this.TLP_GeneralSettings.Controls.Add(this.slickCheckbox4, 0, 5);
			this.TLP_GeneralSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_GeneralSettings.Image = ((System.Drawing.Image)(resources.GetObject("TLP_GeneralSettings.Image")));
			this.TLP_GeneralSettings.Location = new System.Drawing.Point(3, 3);
			this.TLP_GeneralSettings.Name = "TLP_GeneralSettings";
			this.TLP_GeneralSettings.Padding = new System.Windows.Forms.Padding(7, 38, 7, 7);
			this.TLP_GeneralSettings.RowCount = 6;
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.Size = new System.Drawing.Size(767, 332);
			this.TLP_GeneralSettings.TabIndex = 18;
			// 
			// slickCheckbox2
			// 
			this.slickCheckbox2.ActiveColor = null;
			this.slickCheckbox2.AutoSize = true;
			this.slickCheckbox2.Center = false;
			this.slickCheckbox2.Checked = false;
			this.slickCheckbox2.CheckedText = null;
			this.slickCheckbox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox2.DefaultValue = false;
			this.slickCheckbox2.EnterTriggersClick = false;
			this.slickCheckbox2.HideText = false;
			this.slickCheckbox2.IconSize = 16;
			this.slickCheckbox2.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox2.Image")));
			this.slickCheckbox2.Location = new System.Drawing.Point(10, 128);
			this.slickCheckbox2.Name = "slickCheckbox2";
			this.slickCheckbox2.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox2.Selected = false;
			this.slickCheckbox2.Size = new System.Drawing.Size(204, 34);
			this.slickCheckbox2.SpaceTriggersClick = true;
			this.slickCheckbox2.TabIndex = 2;
			this.slickCheckbox2.Tag = "ShowDatesRelatively";
			this.slickCheckbox2.Text = "ShowDatesRelatively";
			this.slickCheckbox2.UncheckedText = null;
			this.slickCheckbox2.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// slickCheckbox1
			// 
			this.slickCheckbox1.ActiveColor = null;
			this.slickCheckbox1.AutoSize = true;
			this.slickCheckbox1.Center = false;
			this.slickCheckbox1.Checked = false;
			this.slickCheckbox1.CheckedText = null;
			this.slickCheckbox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox1.DefaultValue = false;
			this.slickCheckbox1.EnterTriggersClick = false;
			this.slickCheckbox1.HideText = false;
			this.slickCheckbox1.IconSize = 16;
			this.slickCheckbox1.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox1.Image")));
			this.slickCheckbox1.Location = new System.Drawing.Point(10, 88);
			this.slickCheckbox1.Name = "slickCheckbox1";
			this.slickCheckbox1.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox1.Selected = false;
			this.slickCheckbox1.Size = new System.Drawing.Size(243, 34);
			this.slickCheckbox1.SpaceTriggersClick = true;
			this.slickCheckbox1.TabIndex = 1;
			this.slickCheckbox1.Tag = "LargeItemOnHover";
			this.slickCheckbox1.Text = "IncreaseItemSizeOnHover";
			this.slickCheckbox1.UncheckedText = null;
			this.slickCheckbox1.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// CB_LinkModAssets
			// 
			this.CB_LinkModAssets.ActiveColor = null;
			this.CB_LinkModAssets.AutoSize = true;
			this.CB_LinkModAssets.Center = false;
			this.CB_LinkModAssets.Checked = false;
			this.CB_LinkModAssets.CheckedText = null;
			this.CB_LinkModAssets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LinkModAssets.DefaultValue = false;
			this.CB_LinkModAssets.EnterTriggersClick = false;
			this.CB_LinkModAssets.HideText = false;
			this.CB_LinkModAssets.IconSize = 16;
			this.CB_LinkModAssets.Image = ((System.Drawing.Image)(resources.GetObject("CB_LinkModAssets.Image")));
			this.CB_LinkModAssets.Location = new System.Drawing.Point(10, 48);
			this.CB_LinkModAssets.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_LinkModAssets.Name = "CB_LinkModAssets";
			this.CB_LinkModAssets.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_LinkModAssets.Selected = false;
			this.CB_LinkModAssets.Size = new System.Drawing.Size(161, 34);
			this.CB_LinkModAssets.SpaceTriggersClick = true;
			this.CB_LinkModAssets.TabIndex = 0;
			this.CB_LinkModAssets.Tag = "LinkModAssets";
			this.CB_LinkModAssets.Text = "LinkModAssets";
			this.CB_LinkModAssets.UncheckedText = null;
			this.CB_LinkModAssets.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// slickCheckbox3
			// 
			this.slickCheckbox3.ActiveColor = null;
			this.slickCheckbox3.AutoSize = true;
			this.slickCheckbox3.Center = false;
			this.slickCheckbox3.Checked = false;
			this.slickCheckbox3.CheckedText = null;
			this.slickCheckbox3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox3.DefaultValue = false;
			this.slickCheckbox3.EnterTriggersClick = false;
			this.slickCheckbox3.HideText = false;
			this.slickCheckbox3.IconSize = 16;
			this.slickCheckbox3.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox3.Image")));
			this.slickCheckbox3.Location = new System.Drawing.Point(10, 168);
			this.slickCheckbox3.Name = "slickCheckbox3";
			this.slickCheckbox3.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox3.Selected = false;
			this.slickCheckbox3.Size = new System.Drawing.Size(229, 34);
			this.slickCheckbox3.SpaceTriggersClick = true;
			this.slickCheckbox3.TabIndex = 15;
			this.slickCheckbox3.Tag = "AdvancedIncludeEnable";
			this.slickCheckbox3.Text = "AdvancedIncludeEnable";
			this.slickCheckbox3.UncheckedText = null;
			this.slickCheckbox3.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// slickCheckbox4
			// 
			this.slickCheckbox4.ActiveColor = null;
			this.slickCheckbox4.AutoSize = true;
			this.slickCheckbox4.Center = false;
			this.slickCheckbox4.Checked = false;
			this.slickCheckbox4.CheckedText = null;
			this.slickCheckbox4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox4.DefaultValue = false;
			this.slickCheckbox4.EnterTriggersClick = false;
			this.slickCheckbox4.HideText = false;
			this.slickCheckbox4.IconSize = 16;
			this.slickCheckbox4.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox4.Image")));
			this.slickCheckbox4.Location = new System.Drawing.Point(10, 248);
			this.slickCheckbox4.Name = "slickCheckbox4";
			this.slickCheckbox4.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox4.Selected = false;
			this.slickCheckbox4.Size = new System.Drawing.Size(260, 34);
			this.slickCheckbox4.SpaceTriggersClick = true;
			this.slickCheckbox4.TabIndex = 16;
			this.slickCheckbox4.Tag = "DisableNewAssetsByDefault";
			this.slickCheckbox4.Text = "DisableNewAssetsByDefault";
			this.slickCheckbox4.UncheckedText = null;
			this.slickCheckbox4.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// slickCheckbox5
			// 
			this.slickCheckbox5.ActiveColor = null;
			this.slickCheckbox5.AutoSize = true;
			this.slickCheckbox5.Center = false;
			this.slickCheckbox5.Checked = false;
			this.slickCheckbox5.CheckedText = null;
			this.slickCheckbox5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox5.DefaultValue = false;
			this.slickCheckbox5.EnterTriggersClick = false;
			this.slickCheckbox5.HideText = false;
			this.slickCheckbox5.IconSize = 16;
			this.slickCheckbox5.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox5.Image")));
			this.slickCheckbox5.Location = new System.Drawing.Point(10, 208);
			this.slickCheckbox5.Name = "slickCheckbox5";
			this.slickCheckbox5.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox5.Selected = false;
			this.slickCheckbox5.Size = new System.Drawing.Size(255, 34);
			this.slickCheckbox5.SpaceTriggersClick = true;
			this.slickCheckbox5.TabIndex = 17;
			this.slickCheckbox5.Tag = "DisableNewModsByDefault";
			this.slickCheckbox5.Text = "DisableNewModsByDefault";
			this.slickCheckbox5.UncheckedText = null;
			this.slickCheckbox5.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// slickCheckbox6
			// 
			this.slickCheckbox6.ActiveColor = null;
			this.slickCheckbox6.AutoSize = true;
			this.slickCheckbox6.Center = false;
			this.slickCheckbox6.Checked = false;
			this.slickCheckbox6.CheckedText = null;
			this.slickCheckbox6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox6.DefaultValue = false;
			this.slickCheckbox6.EnterTriggersClick = false;
			this.slickCheckbox6.HideText = false;
			this.slickCheckbox6.IconSize = 16;
			this.slickCheckbox6.Image = ((System.Drawing.Image)(resources.GetObject("slickCheckbox6.Image")));
			this.slickCheckbox6.Location = new System.Drawing.Point(10, 288);
			this.slickCheckbox6.Name = "slickCheckbox6";
			this.slickCheckbox6.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox6.Selected = false;
			this.slickCheckbox6.Size = new System.Drawing.Size(223, 34);
			this.slickCheckbox6.SpaceTriggersClick = true;
			this.slickCheckbox6.TabIndex = 18;
			this.slickCheckbox6.Tag = "OverrideGameChanges";
			this.slickCheckbox6.Text = "OverrideGameChanges";
			this.slickCheckbox6.UncheckedText = null;
			this.slickCheckbox6.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// DD_Language
			// 
			this.DD_Language.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Language.Font = new System.Drawing.Font("Segoe UI", 15F);
			this.DD_Language.Location = new System.Drawing.Point(603, 45);
			this.DD_Language.Margin = new System.Windows.Forms.Padding(7);
			this.DD_Language.Name = "DD_Language";
			this.DD_Language.Padding = new System.Windows.Forms.Padding(7);
			this.TLP_GeneralSettings.SetRowSpan(this.DD_Language, 3);
			this.DD_Language.Size = new System.Drawing.Size(150, 60);
			this.DD_Language.TabIndex = 14;
			this.DD_Language.Text = "Language";
			// 
			// PC_Options
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Options";
			this.Size = new System.Drawing.Size(783, 579);
			this.Text = "Language";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.TLP_Folders.ResumeLayout(false);
			this.TLP_GeneralSettings.ResumeLayout(false);
			this.TLP_GeneralSettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_GeneralSettings;
	private SlickControls.SlickCheckbox CB_LinkModAssets;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Folders;
	private SlickControls.SlickPathTextBox TB_VirtualAppDataPath;
	private SlickControls.SlickPathTextBox TB_VirtualGamePath;
	private SlickControls.SlickPathTextBox TB_SteamPath;
	private SlickControls.SlickPathTextBox TB_AppDataPath;
	private SlickControls.SlickPathTextBox TB_GamePath;
	private SlickControls.SlickCheckbox slickCheckbox1;
	private SlickControls.SlickCheckbox slickCheckbox2;
	private LanguageDropDown DD_Language;
	private SlickControls.SlickCheckbox slickCheckbox3;
	private SlickControls.SlickCheckbox slickCheckbox5;
	private SlickControls.SlickCheckbox slickCheckbox4;
	private SlickControls.SlickCheckbox slickCheckbox6;
}
