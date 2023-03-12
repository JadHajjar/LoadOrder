namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_Profiles
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_Profiles));
			this.TLP_ProfileName = new SlickControls.RoundedTableLayoutPanel();
			this.I_ProfileIcon = new SlickControls.SlickIcon();
			this.L_CurrentProfile = new System.Windows.Forms.Label();
			this.B_EditName = new SlickControls.SlickIcon();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.I_TempProfile = new SlickControls.SlickIcon();
			this.L_TempProfile = new System.Windows.Forms.Label();
			this.B_LoadProfiles = new SlickControls.SlickButton();
			this.B_NewProfile = new SlickControls.SlickButton();
			this.P_ScrollPanel = new System.Windows.Forms.Panel();
			this.FLP_Options = new System.Windows.Forms.FlowLayoutPanel();
			this.TLP_GeneralSettings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_AutoSave = new SlickControls.SlickCheckbox();
			this.TLP_LaunchSettings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_NoMods = new SlickControls.SlickCheckbox();
			this.CB_LoadSave = new SlickControls.SlickCheckbox();
			this.CB_LHT = new SlickControls.SlickCheckbox();
			this.TB_SavePath = new SlickControls.SlickPathTextBox();
			this.CB_NoWorkshop = new SlickControls.SlickCheckbox();
			this.CB_NoAssets = new SlickControls.SlickCheckbox();
			this.slickScroll = new SlickControls.SlickScroll();
			this.P_Profiles = new System.Windows.Forms.Panel();
			this.P_Profiles2 = new System.Windows.Forms.Panel();
			this.FLP_Profiles = new System.Windows.Forms.FlowLayoutPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_ProfileName.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			this.P_ScrollPanel.SuspendLayout();
			this.FLP_Options.SuspendLayout();
			this.TLP_GeneralSettings.SuspendLayout();
			this.TLP_LaunchSettings.SuspendLayout();
			this.P_Profiles.SuspendLayout();
			this.P_Profiles2.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			// 
			// TLP_ProfileName
			// 
			this.TLP_ProfileName.AutoSize = true;
			this.TLP_ProfileName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ProfileName.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.TLP_ProfileName, 2);
			this.TLP_ProfileName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_ProfileName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_ProfileName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_ProfileName.Controls.Add(this.I_ProfileIcon, 0, 0);
			this.TLP_ProfileName.Controls.Add(this.L_CurrentProfile, 1, 0);
			this.TLP_ProfileName.Controls.Add(this.B_EditName, 2, 0);
			this.TLP_ProfileName.Location = new System.Drawing.Point(10, 10);
			this.TLP_ProfileName.Margin = new System.Windows.Forms.Padding(10);
			this.TLP_ProfileName.Name = "TLP_ProfileName";
			this.TLP_ProfileName.Padding = new System.Windows.Forms.Padding(5);
			this.TLP_ProfileName.RowCount = 1;
			this.TLP_ProfileName.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ProfileName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_ProfileName.Size = new System.Drawing.Size(147, 48);
			this.TLP_ProfileName.TabIndex = 13;
			// 
			// I_ProfileIcon
			// 
			this.I_ProfileIcon.ActiveColor = null;
			this.I_ProfileIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.I_ProfileIcon.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_ProfileIcon.Enabled = false;
			this.I_ProfileIcon.Image = global::LoadOrderToolTwo.Properties.Resources.I_ProfileSettings;
			this.I_ProfileIcon.Location = new System.Drawing.Point(8, 8);
			this.I_ProfileIcon.Name = "I_ProfileIcon";
			this.I_ProfileIcon.Size = new System.Drawing.Size(32, 32);
			this.I_ProfileIcon.TabIndex = 2;
			this.I_ProfileIcon.TabStop = false;
			// 
			// L_CurrentProfile
			// 
			this.L_CurrentProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_CurrentProfile.AutoSize = true;
			this.L_CurrentProfile.Location = new System.Drawing.Point(46, 12);
			this.L_CurrentProfile.Name = "L_CurrentProfile";
			this.L_CurrentProfile.Size = new System.Drawing.Size(55, 23);
			this.L_CurrentProfile.TabIndex = 0;
			this.L_CurrentProfile.Text = "label1";
			// 
			// B_EditName
			// 
			this.B_EditName.ActiveColor = null;
			this.B_EditName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.B_EditName.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_EditName.Image = ((System.Drawing.Image)(resources.GetObject("B_EditName.Image")));
			this.B_EditName.Location = new System.Drawing.Point(107, 8);
			this.B_EditName.Name = "B_EditName";
			this.B_EditName.Size = new System.Drawing.Size(32, 32);
			this.B_EditName.TabIndex = 1;
			this.B_EditName.TabStop = false;
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 4;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.I_TempProfile, 0, 1);
			this.TLP_Main.Controls.Add(this.L_TempProfile, 1, 1);
			this.TLP_Main.Controls.Add(this.TLP_ProfileName, 0, 0);
			this.TLP_Main.Controls.Add(this.B_LoadProfiles, 2, 0);
			this.TLP_Main.Controls.Add(this.B_NewProfile, 3, 0);
			this.TLP_Main.Controls.Add(this.P_ScrollPanel, 0, 2);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(807, 456);
			this.TLP_Main.TabIndex = 14;
			// 
			// I_TempProfile
			// 
			this.I_TempProfile.ActiveColor = null;
			this.I_TempProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.I_TempProfile.ColorStyle = Extensions.ColorStyle.Yellow;
			this.I_TempProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_TempProfile.Enabled = false;
			this.I_TempProfile.Image = global::LoadOrderToolTwo.Properties.Resources.I_Warning;
			this.I_TempProfile.Location = new System.Drawing.Point(18, 73);
			this.I_TempProfile.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
			this.I_TempProfile.Name = "I_TempProfile";
			this.I_TempProfile.Selected = true;
			this.I_TempProfile.Size = new System.Drawing.Size(32, 32);
			this.I_TempProfile.TabIndex = 2;
			this.I_TempProfile.TabStop = false;
			// 
			// L_TempProfile
			// 
			this.L_TempProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_TempProfile.AutoSize = true;
			this.L_TempProfile.Location = new System.Drawing.Point(56, 78);
			this.L_TempProfile.Margin = new System.Windows.Forms.Padding(3, 10, 10, 10);
			this.L_TempProfile.Name = "L_TempProfile";
			this.L_TempProfile.Size = new System.Drawing.Size(55, 23);
			this.L_TempProfile.TabIndex = 15;
			this.L_TempProfile.Text = "label1";
			// 
			// B_LoadProfiles
			// 
			this.B_LoadProfiles.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_LoadProfiles.ColorShade = null;
			this.B_LoadProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_LoadProfiles.Image = global::LoadOrderToolTwo.Properties.Resources.I_Import;
			this.B_LoadProfiles.Location = new System.Drawing.Point(577, 19);
			this.B_LoadProfiles.Margin = new System.Windows.Forms.Padding(10);
			this.B_LoadProfiles.Name = "B_LoadProfiles";
			this.B_LoadProfiles.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
			this.B_LoadProfiles.Size = new System.Drawing.Size(100, 30);
			this.B_LoadProfiles.SpaceTriggersClick = true;
			this.B_LoadProfiles.TabIndex = 14;
			this.B_LoadProfiles.Text = "LoadProfile";
			this.B_LoadProfiles.Click += new System.EventHandler(this.B_LoadProfiles_Click);
			// 
			// B_NewProfile
			// 
			this.B_NewProfile.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_NewProfile.ColorShade = null;
			this.B_NewProfile.ColorStyle = Extensions.ColorStyle.Green;
			this.B_NewProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_NewProfile.Image = global::LoadOrderToolTwo.Properties.Resources.I_Add;
			this.B_NewProfile.Location = new System.Drawing.Point(697, 19);
			this.B_NewProfile.Margin = new System.Windows.Forms.Padding(10);
			this.B_NewProfile.Name = "B_NewProfile";
			this.B_NewProfile.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
			this.B_NewProfile.Size = new System.Drawing.Size(100, 30);
			this.B_NewProfile.SpaceTriggersClick = true;
			this.B_NewProfile.TabIndex = 14;
			this.B_NewProfile.Text = "AddProfile";
			// 
			// P_ScrollPanel
			// 
			this.TLP_Main.SetColumnSpan(this.P_ScrollPanel, 4);
			this.P_ScrollPanel.Controls.Add(this.FLP_Options);
			this.P_ScrollPanel.Controls.Add(this.slickScroll);
			this.P_ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_ScrollPanel.Location = new System.Drawing.Point(0, 111);
			this.P_ScrollPanel.Margin = new System.Windows.Forms.Padding(0);
			this.P_ScrollPanel.Name = "P_ScrollPanel";
			this.P_ScrollPanel.Size = new System.Drawing.Size(807, 345);
			this.P_ScrollPanel.TabIndex = 16;
			// 
			// FLP_Options
			// 
			this.FLP_Options.AutoSize = true;
			this.FLP_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Options.Controls.Add(this.TLP_GeneralSettings);
			this.FLP_Options.Controls.Add(this.TLP_LaunchSettings);
			this.FLP_Options.Location = new System.Drawing.Point(0, 0);
			this.FLP_Options.Name = "FLP_Options";
			this.FLP_Options.Size = new System.Drawing.Size(756, 233);
			this.FLP_Options.TabIndex = 18;
			// 
			// TLP_GeneralSettings
			// 
			this.TLP_GeneralSettings.AutoSize = true;
			this.TLP_GeneralSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_GeneralSettings.ColumnCount = 2;
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_GeneralSettings.Controls.Add(this.CB_AutoSave, 0, 0);
			this.TLP_GeneralSettings.Image = ((System.Drawing.Image)(resources.GetObject("TLP_GeneralSettings.Image")));
			this.TLP_GeneralSettings.Location = new System.Drawing.Point(3, 3);
			this.TLP_GeneralSettings.Name = "TLP_GeneralSettings";
			this.TLP_GeneralSettings.Padding = new System.Windows.Forms.Padding(7, 38, 7, 7);
			this.TLP_GeneralSettings.RowCount = 1;
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_GeneralSettings.Size = new System.Drawing.Size(185, 92);
			this.TLP_GeneralSettings.TabIndex = 17;
			// 
			// CB_AutoSave
			// 
			this.CB_AutoSave.ActiveColor = null;
			this.CB_AutoSave.AutoSize = true;
			this.CB_AutoSave.Center = false;
			this.CB_AutoSave.Checked = false;
			this.CB_AutoSave.CheckedText = null;
			this.TLP_GeneralSettings.SetColumnSpan(this.CB_AutoSave, 2);
			this.CB_AutoSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_AutoSave.DefaultValue = false;
			this.CB_AutoSave.EnterTriggersClick = false;
			this.CB_AutoSave.HideText = false;
			this.CB_AutoSave.IconSize = 16;
			this.CB_AutoSave.Image = ((System.Drawing.Image)(resources.GetObject("CB_AutoSave.Image")));
			this.CB_AutoSave.Location = new System.Drawing.Point(10, 48);
			this.CB_AutoSave.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_AutoSave.Name = "CB_AutoSave";
			this.CB_AutoSave.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_AutoSave.Selected = false;
			this.CB_AutoSave.Size = new System.Drawing.Size(165, 34);
			this.CB_AutoSave.SpaceTriggersClick = true;
			this.CB_AutoSave.TabIndex = 0;
			this.CB_AutoSave.Text = "AutoSave";
			this.CB_AutoSave.UncheckedText = null;
			this.CB_AutoSave.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// TLP_LaunchSettings
			// 
			this.TLP_LaunchSettings.AutoSize = true;
			this.TLP_LaunchSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_LaunchSettings.ColumnCount = 4;
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_LaunchSettings.Controls.Add(this.CB_NoMods, 3, 1);
			this.TLP_LaunchSettings.Controls.Add(this.CB_LoadSave, 0, 2);
			this.TLP_LaunchSettings.Controls.Add(this.CB_LHT, 0, 0);
			this.TLP_LaunchSettings.Controls.Add(this.TB_SavePath, 0, 3);
			this.TLP_LaunchSettings.Controls.Add(this.CB_NoWorkshop, 0, 1);
			this.TLP_LaunchSettings.Controls.Add(this.CB_NoAssets, 2, 1);
			this.TLP_LaunchSettings.Image = global::LoadOrderToolTwo.Properties.Resources.I_Launch;
			this.TLP_LaunchSettings.Location = new System.Drawing.Point(194, 3);
			this.TLP_LaunchSettings.Name = "TLP_LaunchSettings";
			this.TLP_LaunchSettings.Padding = new System.Windows.Forms.Padding(7, 38, 7, 7);
			this.TLP_LaunchSettings.RowCount = 3;
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_LaunchSettings.Size = new System.Drawing.Size(559, 227);
			this.TLP_LaunchSettings.TabIndex = 1;
			// 
			// CB_NoMods
			// 
			this.CB_NoMods.ActiveColor = null;
			this.CB_NoMods.AutoSize = true;
			this.CB_NoMods.Center = false;
			this.CB_NoMods.Checked = false;
			this.CB_NoMods.CheckedText = null;
			this.CB_NoMods.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoMods.DefaultValue = false;
			this.CB_NoMods.EnterTriggersClick = false;
			this.CB_NoMods.HideText = false;
			this.CB_NoMods.IconSize = 16;
			this.CB_NoMods.Image = ((System.Drawing.Image)(resources.GetObject("CB_NoMods.Image")));
			this.CB_NoMods.Location = new System.Drawing.Point(437, 95);
			this.CB_NoMods.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoMods.Name = "CB_NoMods";
			this.CB_NoMods.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_NoMods.Selected = false;
			this.CB_NoMods.Size = new System.Drawing.Size(112, 34);
			this.CB_NoMods.SpaceTriggersClick = true;
			this.CB_NoMods.TabIndex = 6;
			this.CB_NoMods.Text = "NoMods";
			this.CB_NoMods.UncheckedText = null;
			this.CB_NoMods.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_LoadSave
			// 
			this.CB_LoadSave.ActiveColor = null;
			this.CB_LoadSave.AutoSize = true;
			this.CB_LoadSave.Center = false;
			this.CB_LoadSave.Checked = false;
			this.CB_LoadSave.CheckedText = null;
			this.TLP_LaunchSettings.SetColumnSpan(this.CB_LoadSave, 4);
			this.CB_LoadSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LoadSave.DefaultValue = false;
			this.CB_LoadSave.EnterTriggersClick = false;
			this.CB_LoadSave.HideText = false;
			this.CB_LoadSave.IconSize = 16;
			this.CB_LoadSave.Image = ((System.Drawing.Image)(resources.GetObject("CB_LoadSave.Image")));
			this.CB_LoadSave.Location = new System.Drawing.Point(10, 142);
			this.CB_LoadSave.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_LoadSave.Name = "CB_LoadSave";
			this.CB_LoadSave.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_LoadSave.Selected = false;
			this.CB_LoadSave.Size = new System.Drawing.Size(119, 34);
			this.CB_LoadSave.SpaceTriggersClick = true;
			this.CB_LoadSave.TabIndex = 0;
			this.CB_LoadSave.Text = "LoadSave";
			this.CB_LoadSave.UncheckedText = null;
			this.CB_LoadSave.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_LHT
			// 
			this.CB_LHT.ActiveColor = null;
			this.CB_LHT.AutoSize = true;
			this.CB_LHT.Center = false;
			this.CB_LHT.Checked = false;
			this.CB_LHT.CheckedText = null;
			this.TLP_LaunchSettings.SetColumnSpan(this.CB_LHT, 2);
			this.CB_LHT.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LHT.DefaultValue = false;
			this.CB_LHT.EnterTriggersClick = false;
			this.CB_LHT.HideText = false;
			this.CB_LHT.IconSize = 16;
			this.CB_LHT.Image = ((System.Drawing.Image)(resources.GetObject("CB_LHT.Image")));
			this.CB_LHT.Location = new System.Drawing.Point(10, 48);
			this.CB_LHT.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_LHT.Name = "CB_LHT";
			this.CB_LHT.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_LHT.Selected = false;
			this.CB_LHT.Size = new System.Drawing.Size(175, 34);
			this.CB_LHT.SpaceTriggersClick = true;
			this.CB_LHT.TabIndex = 0;
			this.CB_LHT.Text = "LHT";
			this.CB_LHT.UncheckedText = null;
			this.CB_LHT.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// TB_SavePath
			// 
			this.TB_SavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_LaunchSettings.SetColumnSpan(this.TB_SavePath, 4);
			this.TB_SavePath.FileExtensions = new string[0];
			this.TB_SavePath.Folder = true;
			this.TB_SavePath.Image = ((System.Drawing.Image)(resources.GetObject("TB_SavePath.Image")));
			this.TB_SavePath.LabelText = "SaveFile";
			this.TB_SavePath.Location = new System.Drawing.Point(10, 182);
			this.TB_SavePath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_SavePath.Name = "TB_SavePath";
			this.TB_SavePath.Placeholder = "PathToSaveFile";
			this.TB_SavePath.SelectedText = "";
			this.TB_SavePath.SelectionLength = 0;
			this.TB_SavePath.SelectionStart = 0;
			this.TB_SavePath.Size = new System.Drawing.Size(539, 35);
			this.TB_SavePath.TabIndex = 3;
			this.TB_SavePath.TextChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_NoWorkshop
			// 
			this.CB_NoWorkshop.ActiveColor = null;
			this.CB_NoWorkshop.AutoSize = true;
			this.CB_NoWorkshop.Center = false;
			this.CB_NoWorkshop.Checked = false;
			this.CB_NoWorkshop.CheckedText = null;
			this.TLP_LaunchSettings.SetColumnSpan(this.CB_NoWorkshop, 2);
			this.CB_NoWorkshop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoWorkshop.DefaultValue = false;
			this.CB_NoWorkshop.EnterTriggersClick = false;
			this.CB_NoWorkshop.HideText = false;
			this.CB_NoWorkshop.IconSize = 16;
			this.CB_NoWorkshop.Image = ((System.Drawing.Image)(resources.GetObject("CB_NoWorkshop.Image")));
			this.CB_NoWorkshop.Location = new System.Drawing.Point(10, 95);
			this.CB_NoWorkshop.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoWorkshop.Name = "CB_NoWorkshop";
			this.CB_NoWorkshop.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_NoWorkshop.Selected = false;
			this.CB_NoWorkshop.Size = new System.Drawing.Size(229, 34);
			this.CB_NoWorkshop.SpaceTriggersClick = true;
			this.CB_NoWorkshop.TabIndex = 4;
			this.CB_NoWorkshop.Text = "NoWorkshop";
			this.CB_NoWorkshop.UncheckedText = null;
			this.CB_NoWorkshop.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_NoAssets
			// 
			this.CB_NoAssets.ActiveColor = null;
			this.CB_NoAssets.AutoSize = true;
			this.CB_NoAssets.Center = false;
			this.CB_NoAssets.Checked = false;
			this.CB_NoAssets.CheckedText = null;
			this.CB_NoAssets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoAssets.DefaultValue = false;
			this.CB_NoAssets.EnterTriggersClick = false;
			this.CB_NoAssets.HideText = false;
			this.CB_NoAssets.IconSize = 16;
			this.CB_NoAssets.Image = ((System.Drawing.Image)(resources.GetObject("CB_NoAssets.Image")));
			this.CB_NoAssets.Location = new System.Drawing.Point(245, 95);
			this.CB_NoAssets.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoAssets.Name = "CB_NoAssets";
			this.CB_NoAssets.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_NoAssets.Selected = false;
			this.CB_NoAssets.Size = new System.Drawing.Size(186, 34);
			this.CB_NoAssets.SpaceTriggersClick = true;
			this.CB_NoAssets.TabIndex = 5;
			this.CB_NoAssets.Text = "NoAssets";
			this.CB_NoAssets.UncheckedText = null;
			this.CB_NoAssets.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// slickScroll
			// 
			this.slickScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll.LinkedControl = this.FLP_Options;
			this.slickScroll.Location = new System.Drawing.Point(800, 0);
			this.slickScroll.Name = "slickScroll";
			this.slickScroll.Size = new System.Drawing.Size(7, 345);
			this.slickScroll.Style = SlickControls.StyleType.Vertical;
			this.slickScroll.TabIndex = 16;
			this.slickScroll.TabStop = false;
			this.slickScroll.Text = "slickScroll1";
			// 
			// P_Profiles
			// 
			this.P_Profiles.Controls.Add(this.P_Profiles2);
			this.P_Profiles.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_Profiles.Location = new System.Drawing.Point(807, 30);
			this.P_Profiles.Name = "P_Profiles";
			this.P_Profiles.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
			this.P_Profiles.Size = new System.Drawing.Size(0, 456);
			this.P_Profiles.TabIndex = 15;
			// 
			// P_Profiles2
			// 
			this.P_Profiles2.Controls.Add(this.FLP_Profiles);
			this.P_Profiles2.Controls.Add(this.slickScroll1);
			this.P_Profiles2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Profiles2.Location = new System.Drawing.Point(1, 1);
			this.P_Profiles2.Name = "P_Profiles2";
			this.P_Profiles2.Size = new System.Drawing.Size(0, 455);
			this.P_Profiles2.TabIndex = 2;
			// 
			// FLP_Profiles
			// 
			this.FLP_Profiles.AutoSize = true;
			this.FLP_Profiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Profiles.Location = new System.Drawing.Point(0, 0);
			this.FLP_Profiles.Name = "FLP_Profiles";
			this.FLP_Profiles.Size = new System.Drawing.Size(0, 0);
			this.FLP_Profiles.TabIndex = 2;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.FLP_Profiles;
			this.slickScroll1.Location = new System.Drawing.Point(-7, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(7, 455);
			this.slickScroll1.SmallHandle = true;
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 1;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// PC_Profiles
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Profiles);
			this.Controls.Add(this.TLP_Main);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_Profiles";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(807, 486);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.Controls.SetChildIndex(this.P_Profiles, 0);
			this.TLP_ProfileName.ResumeLayout(false);
			this.TLP_ProfileName.PerformLayout();
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.P_ScrollPanel.ResumeLayout(false);
			this.P_ScrollPanel.PerformLayout();
			this.FLP_Options.ResumeLayout(false);
			this.FLP_Options.PerformLayout();
			this.TLP_GeneralSettings.ResumeLayout(false);
			this.TLP_GeneralSettings.PerformLayout();
			this.TLP_LaunchSettings.ResumeLayout(false);
			this.TLP_LaunchSettings.PerformLayout();
			this.P_Profiles.ResumeLayout(false);
			this.P_Profiles2.ResumeLayout(false);
			this.P_Profiles2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickControls.RoundedTableLayoutPanel TLP_ProfileName;
	private System.Windows.Forms.Label L_CurrentProfile;
	private SlickControls.SlickIcon B_EditName;
	private SlickControls.SlickIcon I_ProfileIcon;
	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private SlickControls.SlickButton B_LoadProfiles;
	private SlickControls.SlickButton B_NewProfile;
	private System.Windows.Forms.Label L_TempProfile;
	private System.Windows.Forms.Panel P_ScrollPanel;
	private SlickControls.SlickScroll slickScroll;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_LaunchSettings;
	private SlickControls.SlickCheckbox CB_LoadSave;
	private SlickControls.SlickPathTextBox TB_SavePath;
	private SlickControls.SlickCheckbox CB_LHT;
	private SlickControls.SlickCheckbox CB_NoWorkshop;
	private SlickControls.SlickCheckbox CB_NoAssets;
	private SlickControls.SlickCheckbox CB_NoMods;
	private SlickControls.SlickIcon I_TempProfile;
	private System.Windows.Forms.Panel P_Profiles;
	private System.Windows.Forms.Panel P_Profiles2;
	private System.Windows.Forms.FlowLayoutPanel FLP_Profiles;
	private SlickControls.SlickScroll slickScroll1;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_GeneralSettings;
	private SlickControls.SlickCheckbox CB_AutoSave;
	private System.Windows.Forms.FlowLayoutPanel FLP_Options;
}
