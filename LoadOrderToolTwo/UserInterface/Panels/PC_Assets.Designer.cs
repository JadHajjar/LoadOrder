using LoadOrderToolTwo.UserInterface.ListControl;

namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_Assets
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
			this.slickGroupBox1 = new SlickControls.SlickGroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.OT_Workshop = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Included = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.LC_Assets = new LoadOrderToolTwo.UserInterface.ListControl.AssetsListControl();
			this.TLP_Main.SuspendLayout();
			this.slickGroupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.TLP_Main.Controls.Add(this.slickGroupBox1, 0, 0);
			this.TLP_Main.Controls.Add(this.LC_Assets, 0, 2);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(778, 408);
			this.TLP_Main.TabIndex = 13;
			// 
			// slickGroupBox1
			// 
			this.slickGroupBox1.AutoSize = true;
			this.slickGroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.slickGroupBox1.Controls.Add(this.tableLayoutPanel1);
			this.slickGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickGroupBox1.Icon = global::LoadOrderToolTwo.Properties.Resources.I_Filter;
			this.slickGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.slickGroupBox1.Name = "slickGroupBox1";
			this.slickGroupBox1.Size = new System.Drawing.Size(382, 154);
			this.slickGroupBox1.TabIndex = 2;
			this.slickGroupBox1.TabStop = false;
			this.slickGroupBox1.Text = "Filters";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.OT_Workshop, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.OT_Included, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(376, 129);
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
			this.TB_Search.Size = new System.Drawing.Size(370, 39);
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
			this.OT_Workshop.Location = new System.Drawing.Point(3, 90);
			this.OT_Workshop.Name = "OT_Workshop";
			this.OT_Workshop.Option1 = "Local";
			this.OT_Workshop.Option2 = "Workshop";
			this.OT_Workshop.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_Workshop.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_Workshop.Size = new System.Drawing.Size(370, 36);
			this.OT_Workshop.TabIndex = 1;
			this.OT_Workshop.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
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
			this.OT_Included.Size = new System.Drawing.Size(370, 36);
			this.OT_Included.TabIndex = 1;
			this.OT_Included.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// LC_Assets
			// 
			this.LC_Assets.AutoInvalidate = false;
			this.LC_Assets.AutoScroll = true;
			this.TLP_Main.SetColumnSpan(this.LC_Assets, 2);
			this.LC_Assets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LC_Assets.HighlightOnHover = true;
			this.LC_Assets.ItemHeight = 28;
			this.LC_Assets.Location = new System.Drawing.Point(0, 160);
			this.LC_Assets.Margin = new System.Windows.Forms.Padding(0);
			this.LC_Assets.Name = "LC_Assets";
			this.LC_Assets.SeparateWithLines = true;
			this.LC_Assets.Size = new System.Drawing.Size(778, 248);
			this.LC_Assets.TabIndex = 3;
			// 
			// PC_Assets
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_Main);
			this.Name = "PC_Assets";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.slickGroupBox1.ResumeLayout(false);
			this.slickGroupBox1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private SlickControls.SlickGroupBox slickGroupBox1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private ThreeOptionToggle OT_Included;
	private ThreeOptionToggle OT_Workshop;
	private SlickControls.SlickTextBox TB_Search;
	private AssetsListControl LC_Assets;
}
