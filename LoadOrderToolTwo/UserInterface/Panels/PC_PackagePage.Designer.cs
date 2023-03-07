namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_PackagePage
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.packageIcon1 = new LoadOrderToolTwo.UserInterface.PackageIcon();
			this.L_Title = new System.Windows.Forms.Label();
			this.P_Back = new System.Windows.Forms.Panel();
			this.P_Info = new SlickControls.DBPanel();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.packageIcon1)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(42, 26);
			this.base_Text.Text = "Back";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 130);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 308);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
			this.tableLayoutPanel2.Controls.Add(this.packageIcon1, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.L_Title, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.P_Back, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.P_Info, 2, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 30);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(783, 100);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// packageIcon1
			// 
			this.packageIcon1.Dock = System.Windows.Forms.DockStyle.Left;
			this.packageIcon1.Location = new System.Drawing.Point(34, 0);
			this.packageIcon1.Margin = new System.Windows.Forms.Padding(0);
			this.packageIcon1.Name = "packageIcon1";
			this.tableLayoutPanel2.SetRowSpan(this.packageIcon1, 2);
			this.packageIcon1.Size = new System.Drawing.Size(100, 100);
			this.packageIcon1.TabIndex = 0;
			this.packageIcon1.TabStop = false;
			// 
			// L_Title
			// 
			this.L_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L_Title.AutoSize = true;
			this.L_Title.Location = new System.Drawing.Point(137, 31);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(45, 19);
			this.L_Title.TabIndex = 1;
			this.L_Title.Text = "label1";
			// 
			// P_Back
			// 
			this.P_Back.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Back.Location = new System.Drawing.Point(0, 50);
			this.P_Back.Margin = new System.Windows.Forms.Padding(0);
			this.P_Back.Name = "P_Back";
			this.P_Back.Size = new System.Drawing.Size(34, 50);
			this.P_Back.TabIndex = 2;
			// 
			// P_Info
			// 
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(134, 50);
			this.P_Info.Margin = new System.Windows.Forms.Padding(0);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(649, 50);
			this.P_Info.TabIndex = 3;
			this.P_Info.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Info_Paint);
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.tableLayoutPanel2);
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_PackagePage";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.packageIcon1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private PackageIcon packageIcon1;
	private System.Windows.Forms.Label L_Title;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Panel P_Back;
	private SlickControls.DBPanel P_Info;
}
