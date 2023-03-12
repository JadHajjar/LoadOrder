﻿namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_Mods
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_Mods));
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.P_Actions = new SlickControls.RoundedGroupPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.B_ReDownload = new SlickControls.SlickButton();
			this.P_Filters = new SlickControls.RoundedGroupPanel();
			this.I_ClearFilters = new SlickControls.SlickIcon();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.B_ExInclude = new LoadOrderToolTwo.UserInterface.DoubleButton();
			this.B_DisEnable = new LoadOrderToolTwo.UserInterface.DoubleButton();
			this.OT_Included = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Workshop = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.DD_PackageStatus = new LoadOrderToolTwo.UserInterface.PackageStatusDropDown();
			this.DD_ReportSeverity = new LoadOrderToolTwo.UserInterface.ReportSeverityDropDown();
			this.OT_Enabled = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.TLP_Main.SuspendLayout();
			this.P_Actions.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.P_Filters.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(49, 26);
			this.base_Text.Text = "Mods";
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.P_Actions, 0, 1);
			this.TLP_Main.Controls.Add(this.P_Filters, 0, 0);
			this.TLP_Main.Controls.Add(this.slickSpacer1, 0, 2);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 4;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(895, 486);
			this.TLP_Main.TabIndex = 0;
			// 
			// P_Actions
			// 
			this.P_Actions.AddOutline = true;
			this.P_Actions.AutoSize = true;
			this.P_Actions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Actions.Controls.Add(this.tableLayoutPanel2);
			this.P_Actions.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Actions.Location = new System.Drawing.Point(3, 179);
			this.P_Actions.Name = "P_Actions";
			this.P_Actions.Padding = new System.Windows.Forms.Padding(7);
			this.P_Actions.Size = new System.Drawing.Size(889, 60);
			this.P_Actions.TabIndex = 4;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.Controls.Add(this.B_ExInclude, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_DisEnable, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_ReDownload, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 7);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(875, 46);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// B_ReDownload
			// 
			this.B_ReDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ReDownload.ColorShade = null;
			this.B_ReDownload.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ReDownload.Location = new System.Drawing.Point(585, 3);
			this.B_ReDownload.Name = "B_ReDownload";
			this.B_ReDownload.Size = new System.Drawing.Size(287, 40);
			this.B_ReDownload.SpaceTriggersClick = true;
			this.B_ReDownload.TabIndex = 1;
			this.B_ReDownload.Text = "RedownloadMods";
			this.B_ReDownload.Click += new System.EventHandler(this.B_ReDownload_Click);
			// 
			// P_Filters
			// 
			this.P_Filters.AddOutline = true;
			this.P_Filters.AutoSize = true;
			this.P_Filters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Filters.Controls.Add(this.I_ClearFilters);
			this.P_Filters.Controls.Add(this.tableLayoutPanel1);
			this.P_Filters.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Filters.Image = ((System.Drawing.Image)(resources.GetObject("P_Filters.Image")));
			this.P_Filters.Location = new System.Drawing.Point(3, 3);
			this.P_Filters.Name = "P_Filters";
			this.P_Filters.Padding = new System.Windows.Forms.Padding(7, 43, 7, 7);
			this.P_Filters.Size = new System.Drawing.Size(889, 170);
			this.P_Filters.TabIndex = 3;
			this.P_Filters.Text = "Filters";
			// 
			// I_ClearFilters
			// 
			this.I_ClearFilters.ActiveColor = null;
			this.I_ClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_ClearFilters.ColorStyle = Extensions.ColorStyle.Red;
			this.I_ClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_ClearFilters.Location = new System.Drawing.Point(849, 8);
			this.I_ClearFilters.Name = "I_ClearFilters";
			this.I_ClearFilters.Size = new System.Drawing.Size(32, 32);
			this.I_ClearFilters.TabIndex = 1;
			this.I_ClearFilters.Click += new System.EventHandler(this.I_ClearFilters_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.OT_Included, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.OT_Workshop, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.DD_PackageStatus, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.DD_ReportSeverity, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.OT_Enabled, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 43);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(875, 120);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TB_Search.Image = ((System.Drawing.Image)(resources.GetObject("TB_Search.Image")));
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Placeholder = "SearchMods";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.Size = new System.Drawing.Size(285, 68);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextChanged += new System.EventHandler(this.FilterChanged);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(0, 242);
			this.slickSpacer1.Margin = new System.Windows.Forms.Padding(0);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(895, 2);
			this.slickSpacer1.TabIndex = 5;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// B_ExInclude
			// 
			this.B_ExInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ExInclude.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ExInclude.Image1 = "I_X";
			this.B_ExInclude.Image2 = "I_Check";
			this.B_ExInclude.Location = new System.Drawing.Point(3, 3);
			this.B_ExInclude.Name = "B_ExInclude";
			this.B_ExInclude.Option1 = "ExcludeAll";
			this.B_ExInclude.Option2 = "IncludeAll";
			this.B_ExInclude.Size = new System.Drawing.Size(285, 40);
			this.B_ExInclude.TabIndex = 0;
			this.B_ExInclude.LeftClicked += new System.EventHandler(this.B_ExInclude_LeftClicked);
			this.B_ExInclude.RightClicked += new System.EventHandler(this.B_ExInclude_RightClicked);
			// 
			// B_DisEnable
			// 
			this.B_DisEnable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.B_DisEnable.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_DisEnable.Image1 = "I_Disabled";
			this.B_DisEnable.Image2 = "I_Enabled";
			this.B_DisEnable.Location = new System.Drawing.Point(294, 3);
			this.B_DisEnable.Name = "B_DisEnable";
			this.B_DisEnable.Option1 = "DisableAll";
			this.B_DisEnable.Option2 = "EnableAll";
			this.B_DisEnable.Size = new System.Drawing.Size(285, 40);
			this.B_DisEnable.TabIndex = 0;
			this.B_DisEnable.LeftClicked += new System.EventHandler(this.B_DisEnable_LeftClicked);
			this.B_DisEnable.RightClicked += new System.EventHandler(this.B_DisEnable_RightClicked);
			// 
			// OT_Included
			// 
			this.OT_Included.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Included.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Included.Image1 = "I_X";
			this.OT_Included.Image2 = "I_Check";
			this.OT_Included.Location = new System.Drawing.Point(3, 77);
			this.OT_Included.Name = "OT_Included";
			this.OT_Included.Option1 = "Excluded";
			this.OT_Included.Option2 = "Included";
			this.OT_Included.Size = new System.Drawing.Size(285, 40);
			this.OT_Included.TabIndex = 1;
			this.OT_Included.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Workshop
			// 
			this.OT_Workshop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Workshop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Workshop.Image1 = "I_Local";
			this.OT_Workshop.Image2 = "I_Steam";
			this.OT_Workshop.Location = new System.Drawing.Point(585, 77);
			this.OT_Workshop.Name = "OT_Workshop";
			this.OT_Workshop.Option1 = "Local";
			this.OT_Workshop.Option2 = "Workshop";
			this.OT_Workshop.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_Workshop.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_Workshop.Size = new System.Drawing.Size(287, 40);
			this.OT_Workshop.TabIndex = 1;
			this.OT_Workshop.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_PackageStatus
			// 
			this.DD_PackageStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_PackageStatus.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageStatus.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_PackageStatus.Location = new System.Drawing.Point(298, 7);
			this.DD_PackageStatus.Margin = new System.Windows.Forms.Padding(7);
			this.DD_PackageStatus.Name = "DD_PackageStatus";
			this.DD_PackageStatus.Padding = new System.Windows.Forms.Padding(7);
			this.DD_PackageStatus.Size = new System.Drawing.Size(277, 60);
			this.DD_PackageStatus.TabIndex = 6;
			this.DD_PackageStatus.Text = "packageStatusDropDown1";
			this.DD_PackageStatus.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_ReportSeverity
			// 
			this.DD_ReportSeverity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_ReportSeverity.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_ReportSeverity.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_ReportSeverity.Location = new System.Drawing.Point(589, 7);
			this.DD_ReportSeverity.Margin = new System.Windows.Forms.Padding(7);
			this.DD_ReportSeverity.Name = "DD_ReportSeverity";
			this.DD_ReportSeverity.Padding = new System.Windows.Forms.Padding(7);
			this.DD_ReportSeverity.Size = new System.Drawing.Size(279, 60);
			this.DD_ReportSeverity.TabIndex = 4;
			this.DD_ReportSeverity.Text = "reportSeverityDropDown1";
			this.DD_ReportSeverity.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Enabled
			// 
			this.OT_Enabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Enabled.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Enabled.Image1 = "I_Disabled";
			this.OT_Enabled.Image2 = "I_Enabled";
			this.OT_Enabled.Location = new System.Drawing.Point(294, 77);
			this.OT_Enabled.Name = "OT_Enabled";
			this.OT_Enabled.Option1 = "Disabled";
			this.OT_Enabled.Option2 = "Enabled";
			this.OT_Enabled.Size = new System.Drawing.Size(285, 40);
			this.OT_Enabled.TabIndex = 1;
			this.OT_Enabled.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// PC_Mods
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_Main);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_Mods";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(895, 516);
			this.Text = "Mods";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.P_Actions.ResumeLayout(false);
			this.P_Actions.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.P_Filters.ResumeLayout(false);
			this.P_Filters.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private SlickControls.SlickTextBox TB_Search;
	private ThreeOptionToggle OT_Workshop;
	private ThreeOptionToggle OT_Enabled;
	private ThreeOptionToggle OT_Included;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControls.RoundedGroupPanel P_Filters;
	private ReportSeverityDropDown DD_ReportSeverity;
	private PackageStatusDropDown DD_PackageStatus;
	private SlickControls.SlickIcon I_ClearFilters;
	private SlickControls.RoundedGroupPanel P_Actions;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private SlickControls.SlickSpacer slickSpacer1;
	private DoubleButton B_ExInclude;
	private DoubleButton B_DisEnable;
	private SlickControls.SlickButton B_ReDownload;
}
