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
			this.tableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.L_CurrentProfile = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.slickIcon1 = new SlickControls.SlickIcon();
			this.B_Edit = new SlickControls.SlickIcon();
			this.slickButton1 = new SlickControls.SlickButton();
			this.slickButton2 = new SlickControls.SlickButton();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.CB_AutoSave = new SlickControls.SlickCheckbox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.slickIcon1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Edit)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.slickIcon1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_CurrentProfile, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Edit, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(137, 48);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// L_CurrentProfile
			// 
			this.L_CurrentProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_CurrentProfile.AutoSize = true;
			this.L_CurrentProfile.Location = new System.Drawing.Point(46, 14);
			this.L_CurrentProfile.Name = "L_CurrentProfile";
			this.L_CurrentProfile.Size = new System.Drawing.Size(45, 19);
			this.L_CurrentProfile.TabIndex = 0;
			this.L_CurrentProfile.Text = "label1";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.slickButton1, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.slickButton2, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(797, 451);
			this.tableLayoutPanel2.TabIndex = 14;
			// 
			// slickIcon1
			// 
			this.slickIcon1.ActiveColor = null;
			this.slickIcon1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.slickIcon1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickIcon1.Enabled = false;
			this.slickIcon1.Image = global::LoadOrderToolTwo.Properties.Resources.I_ProfileSettings;
			this.slickIcon1.Location = new System.Drawing.Point(8, 8);
			this.slickIcon1.MinimumIconSize = 32;
			this.slickIcon1.Name = "slickIcon1";
			this.slickIcon1.Size = new System.Drawing.Size(32, 32);
			this.slickIcon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.slickIcon1.TabIndex = 2;
			this.slickIcon1.TabStop = false;
			// 
			// B_Edit
			// 
			this.B_Edit.ActiveColor = null;
			this.B_Edit.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.B_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Edit.Image = ((System.Drawing.Image)(resources.GetObject("B_Edit.Image")));
			this.B_Edit.Location = new System.Drawing.Point(97, 8);
			this.B_Edit.MinimumIconSize = 32;
			this.B_Edit.Name = "B_Edit";
			this.B_Edit.Size = new System.Drawing.Size(32, 32);
			this.B_Edit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.B_Edit.TabIndex = 1;
			this.B_Edit.TabStop = false;
			// 
			// slickButton1
			// 
			this.slickButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.slickButton1.ColorShade = null;
			this.slickButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickButton1.Image = global::LoadOrderToolTwo.Properties.Resources.I_Import;
			this.slickButton1.Location = new System.Drawing.Point(567, 19);
			this.slickButton1.Margin = new System.Windows.Forms.Padding(10);
			this.slickButton1.Name = "slickButton1";
			this.slickButton1.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
			this.slickButton1.Size = new System.Drawing.Size(100, 30);
			this.slickButton1.SpaceTriggersClick = true;
			this.slickButton1.TabIndex = 14;
			this.slickButton1.Text = "LoadProfile";
			// 
			// slickButton2
			// 
			this.slickButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.slickButton2.ColorShade = null;
			this.slickButton2.ColorStyle = Extensions.ColorStyle.Green;
			this.slickButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickButton2.Image = global::LoadOrderToolTwo.Properties.Resources.I_Add;
			this.slickButton2.Location = new System.Drawing.Point(687, 19);
			this.slickButton2.Margin = new System.Windows.Forms.Padding(10);
			this.slickButton2.Name = "slickButton2";
			this.slickButton2.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
			this.slickButton2.Size = new System.Drawing.Size(100, 30);
			this.slickButton2.SpaceTriggersClick = true;
			this.slickButton2.TabIndex = 14;
			this.slickButton2.Text = "AddProfile";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.CB_AutoSave);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 78);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
			this.flowLayoutPanel1.TabIndex = 15;
			// 
			// CB_AutoSave
			// 
			this.CB_AutoSave.ActiveColor = null;
			this.CB_AutoSave.AutoSize = true;
			this.CB_AutoSave.Center = false;
			this.CB_AutoSave.Checked = false;
			this.CB_AutoSave.CheckedText = null;
			this.CB_AutoSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_AutoSave.HideText = false;
			this.CB_AutoSave.IconSize = 16;
			this.CB_AutoSave.Image = ((System.Drawing.Image)(resources.GetObject("CB_AutoSave.Image")));
			this.CB_AutoSave.Location = new System.Drawing.Point(3, 3);
			this.CB_AutoSave.Name = "CB_AutoSave";
			this.CB_AutoSave.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_AutoSave.Selected = false;
			this.CB_AutoSave.Size = new System.Drawing.Size(104, 30);
			this.CB_AutoSave.TabIndex = 0;
			this.CB_AutoSave.Text = "AutoSave";
			this.CB_AutoSave.UncheckedText = null;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(381, 234);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 19);
			this.label1.TabIndex = 15;
			this.label1.Text = "label1";
			// 
			// PC_Profiles
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tableLayoutPanel2);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Profiles";
			this.Size = new System.Drawing.Size(807, 486);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.slickIcon1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Edit)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickControls.RoundedTableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label L_CurrentProfile;
	private SlickControls.SlickIcon B_Edit;
	private SlickControls.SlickIcon slickIcon1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private SlickControls.SlickButton slickButton1;
	private SlickControls.SlickButton slickButton2;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private SlickControls.SlickCheckbox CB_AutoSave;
	private System.Windows.Forms.Label label1;
}
