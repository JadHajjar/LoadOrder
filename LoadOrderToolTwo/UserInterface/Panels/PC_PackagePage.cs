using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_PackagePage : PanelContent
{
	public PC_PackagePage(Package package)
	{
		InitializeComponent();

		Package = package;

		Text = Locale.Back;
		L_Title.Text = package.GetName().RegexRemove(@"v?\d+\.\d+(\.\d+)?(\.\d+)?");

		if (!string.IsNullOrWhiteSpace(package.IconUrl))
			PB_Icon.LoadImage(package.IconUrl);

		P_Info.SetPackage(package);
	}

	public Package Package { get; }

	protected override void UIChanged()
	{
		base.UIChanged();

		PB_Icon.Width = TLP_Top.Height = (int)(128 * UI.FontScale);
		L_Title.Font = UI.Font(15F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(7), UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_Content.BackColor = P_Back.BackColor = design.AccentBackColor;
	}

	private void B_Redownload_Click(object sender, EventArgs e)
	{
		SteamUtil.ReDownload(Package.SteamId);
	}

	private void B_SteamPage_Click(object sender, EventArgs e)
	{
		try
		{ Process.Start(Package.SteamPage); }
		catch { }
	}

	private void B_Folder_Click(object sender, EventArgs e)
	{
		try
		{ Process.Start(Package.Folder); }
		catch { }
	}
}
