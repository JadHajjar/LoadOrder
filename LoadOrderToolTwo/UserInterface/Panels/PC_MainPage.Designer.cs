﻿namespace LoadOrderToolTwo.UserInterface.Panels;

partial class PC_MainPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_MainPage));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_StartStop = new SlickControls.SlickButton();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.profileBubble = new LoadOrderToolTwo.UserInterface.StatusBubbles.ProfileBubble();
			this.modsBubble = new LoadOrderToolTwo.UserInterface.StatusBubbles.ModsBubble();
			this.assetsBubble = new LoadOrderToolTwo.UserInterface.StatusBubbles.AssetsBubble();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(85, 26);
			this.base_Text.Text = "Dashboard";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.B_StartStop, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 403);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// B_StartStop
			// 
			this.B_StartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_StartStop.ColorShade = null;
			this.B_StartStop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_StartStop.Enabled = false;
			this.B_StartStop.Image = ((System.Drawing.Image)(resources.GetObject("B_StartStop.Image")));
			this.B_StartStop.Location = new System.Drawing.Point(577, 355);
			this.B_StartStop.Name = "B_StartStop";
			this.B_StartStop.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
			this.B_StartStop.Size = new System.Drawing.Size(193, 45);
			this.B_StartStop.SpaceTriggersClick = true;
			this.B_StartStop.TabIndex = 0;
			this.B_StartStop.Text = "Launch Cities: Skylines";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
			this.flowLayoutPanel1.Controls.Add(this.profileBubble);
			this.flowLayoutPanel1.Controls.Add(this.modsBubble);
			this.flowLayoutPanel1.Controls.Add(this.assetsBubble);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(773, 127);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// profileBubble
			// 
			this.profileBubble.Cursor = System.Windows.Forms.Cursors.Hand;
			this.profileBubble.Image = ((System.Drawing.Image)(resources.GetObject("profileBubble.Image")));
			this.profileBubble.Location = new System.Drawing.Point(3, 3);
			this.profileBubble.Name = "profileBubble";
			this.profileBubble.Size = new System.Drawing.Size(150, 47);
			this.profileBubble.TabIndex = 0;
			this.profileBubble.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ProfileBubble_MouseClick);
			// 
			// modsBubble
			// 
			this.modsBubble.Cursor = System.Windows.Forms.Cursors.Hand;
			this.modsBubble.Image = ((System.Drawing.Image)(resources.GetObject("modsBubble.Image")));
			this.modsBubble.Location = new System.Drawing.Point(159, 3);
			this.modsBubble.Name = "modsBubble";
			this.modsBubble.Size = new System.Drawing.Size(150, 121);
			this.modsBubble.TabIndex = 1;
			this.modsBubble.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ModsBubble_MouseClick);
			// 
			// assetsBubble
			// 
			this.assetsBubble.Cursor = System.Windows.Forms.Cursors.Hand;
			this.assetsBubble.Image = ((System.Drawing.Image)(resources.GetObject("assetsBubble.Image")));
			this.assetsBubble.Location = new System.Drawing.Point(315, 3);
			this.assetsBubble.Name = "assetsBubble";
			this.assetsBubble.Size = new System.Drawing.Size(150, 64);
			this.assetsBubble.TabIndex = 2;
			this.assetsBubble.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AssetsBubble_MouseClick);
			// 
			// PC_MainPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_MainPage";
			this.Text = "Dashboard";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickControls.SlickButton B_StartStop;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private UserInterface.StatusBubbles.ProfileBubble profileBubble;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private UserInterface.StatusBubbles.ModsBubble modsBubble;
	private UserInterface.StatusBubbles.AssetsBubble assetsBubble;
}
