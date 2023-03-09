namespace LoadOrderToolTwo.UserInterface.Panels;

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
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.roundedGroupBox1 = new SlickControls.RoundedGroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.OT_Workshop = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Enabled = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Included = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.reportSeverityDropDown1 = new LoadOrderToolTwo.UserInterface.ReportSeverityDropDown();
			this.packageStatusDropDown1 = new LoadOrderToolTwo.UserInterface.PackageStatusDropDown();
			this.TLP_Main.SuspendLayout();
			this.roundedGroupBox1.SuspendLayout();
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
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Controls.Add(this.roundedGroupBox1, 1, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(895, 486);
			this.TLP_Main.TabIndex = 0;
			// 
			// roundedGroupBox1
			// 
			this.roundedGroupBox1.AutoSize = true;
			this.roundedGroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupBox1.Controls.Add(this.tableLayoutPanel1);
			this.roundedGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.roundedGroupBox1.Image = global::LoadOrderToolTwo.Properties.Resources.I_Filter;
			this.roundedGroupBox1.Location = new System.Drawing.Point(450, 3);
			this.roundedGroupBox1.Name = "roundedGroupBox1";
			this.roundedGroupBox1.Padding = new System.Windows.Forms.Padding(5, 34, 5, 5);
			this.roundedGroupBox1.Size = new System.Drawing.Size(442, 286);
			this.roundedGroupBox1.TabIndex = 3;
			this.roundedGroupBox1.Text = "Filter";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.packageStatusDropDown1, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.reportSeverityDropDown1, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.OT_Workshop, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.OT_Enabled, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.OT_Included, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 34);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 247);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TB_Search.Image = global::LoadOrderToolTwo.Properties.Resources.I_Search;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Placeholder = "SearchMods";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.Size = new System.Drawing.Size(426, 39);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Workshop
			// 
			this.OT_Workshop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Workshop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Workshop.Image1 = "I_Local";
			this.OT_Workshop.Image2 = "I_Steam";
			this.OT_Workshop.Location = new System.Drawing.Point(3, 116);
			this.OT_Workshop.Name = "OT_Workshop";
			this.OT_Workshop.Option1 = "Local";
			this.OT_Workshop.Option2 = "Workshop";
			this.OT_Workshop.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_Workshop.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_Workshop.Size = new System.Drawing.Size(426, 28);
			this.OT_Workshop.TabIndex = 1;
			this.OT_Workshop.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Enabled
			// 
			this.OT_Enabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Enabled.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Enabled.Image1 = "I_Disabled";
			this.OT_Enabled.Image2 = "I_Enabled";
			this.OT_Enabled.Location = new System.Drawing.Point(3, 82);
			this.OT_Enabled.Name = "OT_Enabled";
			this.OT_Enabled.Option1 = "Disabled";
			this.OT_Enabled.Option2 = "Enabled";
			this.OT_Enabled.Size = new System.Drawing.Size(426, 28);
			this.OT_Enabled.TabIndex = 1;
			this.OT_Enabled.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Included
			// 
			this.OT_Included.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Included.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Included.Image1 = "I_X";
			this.OT_Included.Image2 = "I_Check";
			this.OT_Included.Location = new System.Drawing.Point(3, 48);
			this.OT_Included.Name = "OT_Included";
			this.OT_Included.Option1 = "Excluded";
			this.OT_Included.Option2 = "Included";
			this.OT_Included.Size = new System.Drawing.Size(426, 28);
			this.OT_Included.TabIndex = 1;
			this.OT_Included.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// reportSeverityDropDown1
			// 
			this.reportSeverityDropDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.reportSeverityDropDown1.Items = new CompatibilityReport.CatalogData.Enums.ReportSeverity[] {
        CompatibilityReport.CatalogData.Enums.ReportSeverity.NothingToReport,
        CompatibilityReport.CatalogData.Enums.ReportSeverity.Remarks,
        CompatibilityReport.CatalogData.Enums.ReportSeverity.MinorIssues,
        CompatibilityReport.CatalogData.Enums.ReportSeverity.MajorIssues,
        CompatibilityReport.CatalogData.Enums.ReportSeverity.Unsubscribe};
			this.reportSeverityDropDown1.Location = new System.Drawing.Point(3, 150);
			this.reportSeverityDropDown1.Name = "reportSeverityDropDown1";
			this.reportSeverityDropDown1.Padding = new System.Windows.Forms.Padding(5);
			this.reportSeverityDropDown1.SelectedItem = CompatibilityReport.CatalogData.Enums.ReportSeverity.NothingToReport;
			this.reportSeverityDropDown1.Size = new System.Drawing.Size(426, 40);
			this.reportSeverityDropDown1.TabIndex = 4;
			this.reportSeverityDropDown1.Text = "reportSeverityDropDown1";
			// 
			// packageStatusDropDown1
			// 
			this.packageStatusDropDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.packageStatusDropDown1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.packageStatusDropDown1.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.packageStatusDropDown1.Items = new LoadOrderToolTwo.Domain.DownloadStatus[] {
        LoadOrderToolTwo.Domain.DownloadStatus.None,
        LoadOrderToolTwo.Domain.DownloadStatus.OK,
        LoadOrderToolTwo.Domain.DownloadStatus.Unknown,
        LoadOrderToolTwo.Domain.DownloadStatus.OutOfDate,
        LoadOrderToolTwo.Domain.DownloadStatus.NotDownloaded,
        LoadOrderToolTwo.Domain.DownloadStatus.PartiallyDownloaded,
        LoadOrderToolTwo.Domain.DownloadStatus.Removed};
			this.packageStatusDropDown1.Location = new System.Drawing.Point(7, 200);
			this.packageStatusDropDown1.Margin = new System.Windows.Forms.Padding(7);
			this.packageStatusDropDown1.Name = "packageStatusDropDown1";
			this.packageStatusDropDown1.Padding = new System.Windows.Forms.Padding(7);
			this.packageStatusDropDown1.SelectedItem = LoadOrderToolTwo.Domain.DownloadStatus.None;
			this.packageStatusDropDown1.Size = new System.Drawing.Size(418, 40);
			this.packageStatusDropDown1.TabIndex = 6;
			this.packageStatusDropDown1.Text = "packageStatusDropDown1";
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
			this.roundedGroupBox1.ResumeLayout(false);
			this.roundedGroupBox1.PerformLayout();
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
	private SlickControls.RoundedGroupBox roundedGroupBox1;
	private ReportSeverityDropDown reportSeverityDropDown1;
	private PackageStatusDropDown packageStatusDropDown1;
}
