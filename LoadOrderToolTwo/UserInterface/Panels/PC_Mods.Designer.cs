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
			this.slickTextBox1 = new SlickControls.SlickTextBox();
			this.threeOptionToggle1 = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.threeOptionToggle2 = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.threeOptionToggle3 = new LoadOrderToolTwo.UserInterface.ThreeOptionToggle();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.TLP_Main.Controls.Add(this.slickGroupBox1, 0, 0);
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
			this.TLP_Main.SetColumnSpan(this.slickGroupBox1, 3);
			this.slickGroupBox1.Controls.Add(this.tableLayoutPanel1);
			this.slickGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickGroupBox1.Icon = global::LoadOrderToolTwo.Properties.Resources.I_Filter;
			this.slickGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.slickGroupBox1.Name = "slickGroupBox1";
			this.slickGroupBox1.Size = new System.Drawing.Size(884, 111);
			this.slickGroupBox1.TabIndex = 2;
			this.slickGroupBox1.TabStop = false;
			this.slickGroupBox1.Text = "Filters";
			// 
			// slickTextBox1
			// 
			this.slickTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slickTextBox1.Image = global::LoadOrderToolTwo.Properties.Resources.I_Search;
			this.slickTextBox1.LabelText = "Search";
			this.slickTextBox1.Location = new System.Drawing.Point(3, 48);
			this.slickTextBox1.Name = "slickTextBox1";
			this.slickTextBox1.Placeholder = "SearchMods";
			this.slickTextBox1.SelectedText = "";
			this.slickTextBox1.SelectionLength = 0;
			this.slickTextBox1.SelectionStart = 0;
			this.slickTextBox1.Size = new System.Drawing.Size(433, 39);
			this.slickTextBox1.TabIndex = 0;
			// 
			// threeOptionToggle1
			// 
			this.threeOptionToggle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threeOptionToggle1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.threeOptionToggle1.Image1 = global::LoadOrderToolTwo.Properties.Resources.I_Local;
			this.threeOptionToggle1.Image2 = global::LoadOrderToolTwo.Properties.Resources.I_Steam;
			this.threeOptionToggle1.Location = new System.Drawing.Point(3, 3);
			this.threeOptionToggle1.Name = "threeOptionToggle1";
			this.threeOptionToggle1.Option1 = "Local";
			this.threeOptionToggle1.Option2 = "Workshop";
			this.threeOptionToggle1.OptionStyle1 = Extensions.ColorStyle.Active;
			this.threeOptionToggle1.OptionStyle2 = Extensions.ColorStyle.Active;
			this.threeOptionToggle1.Size = new System.Drawing.Size(433, 36);
			this.threeOptionToggle1.TabIndex = 1;
			// 
			// threeOptionToggle2
			// 
			this.threeOptionToggle2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threeOptionToggle2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.threeOptionToggle2.Image1 = global::LoadOrderToolTwo.Properties.Resources.I_X;
			this.threeOptionToggle2.Image2 = global::LoadOrderToolTwo.Properties.Resources.I_Check;
			this.threeOptionToggle2.Location = new System.Drawing.Point(442, 3);
			this.threeOptionToggle2.Name = "threeOptionToggle2";
			this.threeOptionToggle2.Option1 = "Disabled";
			this.threeOptionToggle2.Option2 = "Enabled";
			this.threeOptionToggle2.Size = new System.Drawing.Size(433, 36);
			this.threeOptionToggle2.TabIndex = 1;
			// 
			// threeOptionToggle3
			// 
			this.threeOptionToggle3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threeOptionToggle3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.threeOptionToggle3.Image1 = global::LoadOrderToolTwo.Properties.Resources.I_X;
			this.threeOptionToggle3.Image2 = global::LoadOrderToolTwo.Properties.Resources.I_Check;
			this.threeOptionToggle3.Location = new System.Drawing.Point(442, 48);
			this.threeOptionToggle3.Name = "threeOptionToggle3";
			this.threeOptionToggle3.Option1 = "Excluded";
			this.threeOptionToggle3.Option2 = "Included";
			this.threeOptionToggle3.Size = new System.Drawing.Size(433, 36);
			this.threeOptionToggle3.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.threeOptionToggle3, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.threeOptionToggle2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.threeOptionToggle1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.slickTextBox1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(878, 90);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// PC_Mods
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
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
	private SlickControls.SlickTextBox slickTextBox1;
	private ThreeOptionToggle threeOptionToggle1;
	private ThreeOptionToggle threeOptionToggle2;
	private ThreeOptionToggle threeOptionToggle3;
	private SlickControls.SlickGroupBox slickGroupBox1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
}
