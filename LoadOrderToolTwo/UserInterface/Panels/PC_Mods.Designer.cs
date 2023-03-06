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
			this.slickGroupBox1 = new SlickControls.SlickGroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.OT_Workshop = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Enabled = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.OT_Included = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.LC_Mods = new LoadOrderToolTwo.UserInterface.ModsListControl();
			this.TLP_Main.SuspendLayout();
			this.slickGroupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(49, 26);
			this.base_Text.Text = "Mods";
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Controls.Add(this.slickGroupBox1, 0, 0);
			this.TLP_Main.Controls.Add(this.LC_Mods, 0, 2);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(890, 486);
			this.TLP_Main.TabIndex = 0;
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
			this.slickGroupBox1.Size = new System.Drawing.Size(438, 190);
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
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.OT_Workshop, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.OT_Enabled, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.OT_Included, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 165);
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
			this.OT_Workshop.Location = new System.Drawing.Point(3, 128);
			this.OT_Workshop.Name = "OT_Workshop";
			this.OT_Workshop.Option1 = "Local";
			this.OT_Workshop.Option2 = "Workshop";
			this.OT_Workshop.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_Workshop.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_Workshop.Size = new System.Drawing.Size(426, 34);
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
			this.OT_Enabled.Location = new System.Drawing.Point(3, 88);
			this.OT_Enabled.Name = "OT_Enabled";
			this.OT_Enabled.Option1 = "Disabled";
			this.OT_Enabled.Option2 = "Enabled";
			this.OT_Enabled.Size = new System.Drawing.Size(426, 34);
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
			this.OT_Included.Size = new System.Drawing.Size(426, 34);
			this.OT_Included.TabIndex = 1;
			this.OT_Included.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// LC_Mods
			// 
			this.LC_Mods.AutoInvalidate = false;
			this.LC_Mods.AutoScroll = true;
			this.TLP_Main.SetColumnSpan(this.LC_Mods, 2);
			this.LC_Mods.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LC_Mods.HighlightOnHover = true;
			this.LC_Mods.ItemHeight = 28;
			this.LC_Mods.Location = new System.Drawing.Point(0, 196);
			this.LC_Mods.Margin = new System.Windows.Forms.Padding(0);
			this.LC_Mods.Name = "LC_Mods";
			this.LC_Mods.SeparateWithLines = true;
			this.LC_Mods.Size = new System.Drawing.Size(890, 290);
			this.LC_Mods.TabIndex = 3;
			// 
			// PC_Mods
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TLP_Main);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Mods";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(895, 516);
			this.Text = "Mods";
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
	private SlickControls.SlickTextBox TB_Search;
	private ThreeOptionToggle OT_Workshop;
	private ThreeOptionToggle OT_Enabled;
	private ThreeOptionToggle OT_Included;
	private SlickControls.SlickGroupBox slickGroupBox1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private ModsListControl LC_Mods;
}
