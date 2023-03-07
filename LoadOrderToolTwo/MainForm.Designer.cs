namespace LoadOrderToolTwo
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.PI_Dashboard = new SlickControls.PanelItem();
			this.PI_Mods = new SlickControls.PanelItem();
			this.PI_Assets = new SlickControls.PanelItem();
			this.L_Version = new System.Windows.Forms.Label();
			this.L_Text = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.base_P_SideControls.SuspendLayout();
			this.base_P_Container.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.Size = new System.Drawing.Size(987, 562);
			// 
			// base_P_SideControls
			// 
			this.base_P_SideControls.Controls.Add(this.tableLayoutPanel1);
			this.base_P_SideControls.Font = new System.Drawing.Font("Nirmala UI", 10.5F);
			this.base_P_SideControls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(129)))), ((int)(((byte)(150)))));
			this.base_P_SideControls.Location = new System.Drawing.Point(0, 411);
			this.base_P_SideControls.Size = new System.Drawing.Size(239, 50);
			// 
			// base_P_Container
			// 
			this.base_P_Container.Size = new System.Drawing.Size(989, 564);
			// 
			// PI_Dashboard
			// 
			this.PI_Dashboard.ForceReopen = false;
			this.PI_Dashboard.Group = "Load Order Tool 2";
			this.PI_Dashboard.Highlighted = false;
			this.PI_Dashboard.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Dashboard.Icon")));
			this.PI_Dashboard.Selected = false;
			this.PI_Dashboard.Text = "Dashboard";
			this.PI_Dashboard.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Dashboard_OnClick);
			// 
			// PI_Mods
			// 
			this.PI_Mods.ForceReopen = false;
			this.PI_Mods.Group = "Load Order Tool 2";
			this.PI_Mods.Highlighted = false;
			this.PI_Mods.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Mods.Icon")));
			this.PI_Mods.Selected = false;
			this.PI_Mods.Text = "Mods";
			this.PI_Mods.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Mods_OnClick);
			// 
			// PI_Assets
			// 
			this.PI_Assets.ForceReopen = false;
			this.PI_Assets.Group = "Load Order Tool 2";
			this.PI_Assets.Highlighted = false;
			this.PI_Assets.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Assets.Icon")));
			this.PI_Assets.Selected = false;
			this.PI_Assets.Text = "Assets";
			this.PI_Assets.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Assets_OnClick);
			// 
			// L_Version
			// 
			this.L_Version.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.L_Version.AutoSize = true;
			this.L_Version.Location = new System.Drawing.Point(181, 0);
			this.L_Version.Margin = new System.Windows.Forms.Padding(0);
			this.L_Version.Name = "L_Version";
			this.L_Version.Padding = new System.Windows.Forms.Padding(2);
			this.L_Version.Size = new System.Drawing.Size(58, 23);
			this.L_Version.TabIndex = 30;
			this.L_Version.Text = "Version";
			// 
			// L_Text
			// 
			this.L_Text.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Text.AutoSize = true;
			this.L_Text.Location = new System.Drawing.Point(0, 0);
			this.L_Text.Margin = new System.Windows.Forms.Padding(0);
			this.L_Text.Name = "L_Text";
			this.L_Text.Padding = new System.Windows.Forms.Padding(2);
			this.L_Text.Size = new System.Drawing.Size(112, 23);
			this.L_Text.TabIndex = 31;
			this.L_Text.Text = "Load Order Tool";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.L_Text, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Version, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 23);
			this.tableLayoutPanel1.TabIndex = 32;
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1000, 575);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.FormIcon = ((System.Drawing.Image)(resources.GetObject("$this.FormIcon")));
			this.IconBounds = new System.Drawing.Rectangle(112, 29, 14, 42);
			this.MaximizeBox = true;
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 1920, 1032);
			this.MinimizeBox = true;
			this.Name = "MainForm";
			this.SidebarItems = new SlickControls.PanelItem[] {
        this.PI_Dashboard,
        this.PI_Mods,
        this.PI_Assets};
			this.Text = "Load Order Tool";
			this.base_P_SideControls.ResumeLayout(false);
			this.base_P_SideControls.PerformLayout();
			this.base_P_Container.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		internal SlickControls.PanelItem PI_Dashboard;
		internal SlickControls.PanelItem PI_Mods;
		internal SlickControls.PanelItem PI_Assets;
		private System.Windows.Forms.Label L_Version;
		private System.Windows.Forms.Label L_Text;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}