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
			this.TLP_Filters = new System.Windows.Forms.TableLayoutPanel();
			this.slickTextBox1 = new SlickControls.SlickTextBox();
			this.TLP_Main.SuspendLayout();
			this.TLP_Filters.SuspendLayout();
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
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.Controls.Add(this.TLP_Filters, 0, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.Size = new System.Drawing.Size(890, 486);
			this.TLP_Main.TabIndex = 0;
			// 
			// TLP_Filters
			// 
			this.TLP_Filters.ColumnCount = 2;
			this.TLP_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Filters.Controls.Add(this.slickTextBox1, 0, 1);
			this.TLP_Filters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Filters.Location = new System.Drawing.Point(0, 0);
			this.TLP_Filters.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Filters.Name = "TLP_Filters";
			this.TLP_Filters.RowCount = 2;
			this.TLP_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Filters.Size = new System.Drawing.Size(445, 243);
			this.TLP_Filters.TabIndex = 0;
			// 
			// slickTextBox1
			// 
			this.slickTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slickTextBox1.Image = global::LoadOrderToolTwo.Properties.Resources.I_Search;
			this.slickTextBox1.LabelText = "Search";
			this.slickTextBox1.Location = new System.Drawing.Point(3, 124);
			this.slickTextBox1.Name = "slickTextBox1";
			this.slickTextBox1.Placeholder = "Search through your mods..";
			this.slickTextBox1.SelectedText = "";
			this.slickTextBox1.SelectionLength = 0;
			this.slickTextBox1.SelectionStart = 0;
			this.slickTextBox1.Size = new System.Drawing.Size(216, 39);
			this.slickTextBox1.TabIndex = 0;
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
			this.TLP_Filters.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private System.Windows.Forms.TableLayoutPanel TLP_Filters;
	private SlickControls.SlickTextBox slickTextBox1;
}
