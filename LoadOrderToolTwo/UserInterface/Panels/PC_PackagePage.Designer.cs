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
			this.TLP_Content = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Top = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new LoadOrderToolTwo.UserInterface.PackageIcon();
			this.L_Title = new System.Windows.Forms.Label();
			this.P_Back = new System.Windows.Forms.Panel();
			this.P_Info = new LoadOrderToolTwo.UserInterface.PackageDescription();
			this.B_Redownload = new SlickControls.SlickButton();
			this.B_Folder = new SlickControls.SlickButton();
			this.B_SteamPage = new SlickControls.SlickButton();
			this.TLP_Top.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(42, 26);
			this.base_Text.Text = "Back";
			// 
			// TLP_Content
			// 
			this.TLP_Content.ColumnCount = 2;
			this.TLP_Content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Content.Location = new System.Drawing.Point(0, 130);
			this.TLP_Content.Name = "TLP_Content";
			this.TLP_Content.RowCount = 2;
			this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Content.Size = new System.Drawing.Size(783, 308);
			this.TLP_Content.TabIndex = 13;
			// 
			// TLP_Top
			// 
			this.TLP_Top.ColumnCount = 6;
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.Controls.Add(this.PB_Icon, 1, 0);
			this.TLP_Top.Controls.Add(this.L_Title, 2, 0);
			this.TLP_Top.Controls.Add(this.P_Back, 0, 1);
			this.TLP_Top.Controls.Add(this.P_Info, 2, 1);
			this.TLP_Top.Controls.Add(this.B_Redownload, 3, 0);
			this.TLP_Top.Controls.Add(this.B_Folder, 5, 0);
			this.TLP_Top.Controls.Add(this.B_SteamPage, 4, 0);
			this.TLP_Top.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Top.Location = new System.Drawing.Point(0, 30);
			this.TLP_Top.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Top.Name = "TLP_Top";
			this.TLP_Top.RowCount = 2;
			this.TLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Top.Size = new System.Drawing.Size(783, 100);
			this.TLP_Top.TabIndex = 0;
			// 
			// PB_Icon
			// 
			this.PB_Icon.Dock = System.Windows.Forms.DockStyle.Left;
			this.PB_Icon.Location = new System.Drawing.Point(32, 0);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Icon.Name = "PB_Icon";
			this.TLP_Top.SetRowSpan(this.PB_Icon, 2);
			this.PB_Icon.Size = new System.Drawing.Size(100, 100);
			this.PB_Icon.TabIndex = 0;
			this.PB_Icon.TabStop = false;
			// 
			// L_Title
			// 
			this.L_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L_Title.AutoSize = true;
			this.L_Title.Location = new System.Drawing.Point(135, 27);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(55, 23);
			this.L_Title.TabIndex = 1;
			this.L_Title.Text = "label1";
			// 
			// P_Back
			// 
			this.P_Back.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Back.Location = new System.Drawing.Point(0, 50);
			this.P_Back.Margin = new System.Windows.Forms.Padding(0);
			this.P_Back.Name = "P_Back";
			this.P_Back.Size = new System.Drawing.Size(32, 50);
			this.P_Back.TabIndex = 2;
			// 
			// P_Info
			// 
			this.TLP_Top.SetColumnSpan(this.P_Info, 4);
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(132, 50);
			this.P_Info.Margin = new System.Windows.Forms.Padding(0);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(651, 50);
			this.P_Info.TabIndex = 3;
			// 
			// B_Redownload
			// 
			this.B_Redownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Redownload.ColorShade = null;
			this.B_Redownload.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Redownload.Image = global::LoadOrderToolTwo.Properties.Resources.I_ReDownload;
			this.B_Redownload.Location = new System.Drawing.Point(661, 3);
			this.B_Redownload.Name = "B_Redownload";
			this.B_Redownload.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Redownload.Size = new System.Drawing.Size(35, 44);
			this.B_Redownload.SpaceTriggersClick = true;
			this.B_Redownload.TabIndex = 4;
			this.B_Redownload.Click += new System.EventHandler(this.B_Redownload_Click);
			// 
			// B_Folder
			// 
			this.B_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Folder.ColorShade = null;
			this.B_Folder.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Folder.Image = global::LoadOrderToolTwo.Properties.Resources.I_Folder;
			this.B_Folder.Location = new System.Drawing.Point(739, 3);
			this.B_Folder.Name = "B_Folder";
			this.B_Folder.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Folder.Size = new System.Drawing.Size(41, 44);
			this.B_Folder.SpaceTriggersClick = true;
			this.B_Folder.TabIndex = 4;
			this.B_Folder.Click += new System.EventHandler(this.B_Folder_Click);
			// 
			// B_SteamPage
			// 
			this.B_SteamPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_SteamPage.ColorShade = null;
			this.B_SteamPage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_SteamPage.Image = global::LoadOrderToolTwo.Properties.Resources.I_Steam;
			this.B_SteamPage.Location = new System.Drawing.Point(702, 3);
			this.B_SteamPage.Name = "B_SteamPage";
			this.B_SteamPage.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_SteamPage.Size = new System.Drawing.Size(31, 44);
			this.B_SteamPage.SpaceTriggersClick = true;
			this.B_SteamPage.TabIndex = 4;
			this.B_SteamPage.Click += new System.EventHandler(this.B_SteamPage_Click);
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.TLP_Content);
			this.Controls.Add(this.TLP_Top);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_PackagePage";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Top, 0);
			this.Controls.SetChildIndex(this.TLP_Content, 0);
			this.TLP_Top.ResumeLayout(false);
			this.TLP_Top.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_Top;
	private PackageIcon PB_Icon;
	private System.Windows.Forms.Label L_Title;
	private System.Windows.Forms.TableLayoutPanel TLP_Content;
	private System.Windows.Forms.Panel P_Back;
	private PackageDescription P_Info;
	private SlickControls.SlickButton B_Redownload;
	private SlickControls.SlickButton B_Folder;
	private SlickControls.SlickButton B_SteamPage;
}
