namespace LoadOrderToolTwo.UserInterface.Panels;

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
			this.TLP_GeneralSettings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_LinkModAssets = new SlickControls.SlickCheckbox();
			this.tableLayoutPanel1.SuspendLayout();
			this.TLP_GeneralSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.TLP_GeneralSettings, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 403);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// TLP_GeneralSettings
			// 
			this.TLP_GeneralSettings.AutoSize = true;
			this.TLP_GeneralSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_GeneralSettings.ColumnCount = 2;
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_GeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_GeneralSettings.Controls.Add(this.CB_LinkModAssets, 0, 0);
			this.TLP_GeneralSettings.Image = ((System.Drawing.Image)(resources.GetObject("TLP_GeneralSettings.Image")));
			this.TLP_GeneralSettings.Location = new System.Drawing.Point(3, 3);
			this.TLP_GeneralSettings.Name = "TLP_GeneralSettings";
			this.TLP_GeneralSettings.Padding = new System.Windows.Forms.Padding(7, 38, 7, 7);
			this.TLP_GeneralSettings.RowCount = 1;
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_GeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_GeneralSettings.Size = new System.Drawing.Size(138, 84);
			this.TLP_GeneralSettings.TabIndex = 18;
			// 
			// CB_LinkModAssets
			// 
			this.CB_LinkModAssets.ActiveColor = null;
			this.CB_LinkModAssets.AutoSize = true;
			this.CB_LinkModAssets.Center = false;
			this.CB_LinkModAssets.Checked = false;
			this.CB_LinkModAssets.CheckedText = null;
			this.TLP_GeneralSettings.SetColumnSpan(this.CB_LinkModAssets, 2);
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
			this.CB_LinkModAssets.Size = new System.Drawing.Size(118, 26);
			this.CB_LinkModAssets.SpaceTriggersClick = true;
			this.CB_LinkModAssets.TabIndex = 0;
			this.CB_LinkModAssets.Tag = "LinkModAssets";
			this.CB_LinkModAssets.Text = "LinkModAssets";
			this.CB_LinkModAssets.UncheckedText = null;
			this.CB_LinkModAssets.CheckChanged += new System.EventHandler(this.CB_CheckChanged);
			// 
			// PC_Options
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_Options";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.TLP_GeneralSettings.ResumeLayout(false);
			this.TLP_GeneralSettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_GeneralSettings;
	private SlickControls.SlickCheckbox CB_LinkModAssets;
}
