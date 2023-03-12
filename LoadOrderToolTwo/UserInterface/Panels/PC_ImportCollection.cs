using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;

using SlickControls;

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_ImportCollection : PanelContent
{
	private readonly ulong _id;

	public PC_ImportCollection(Dictionary<ulong, Domain.Steam.SteamWorkshopItem> contents)
	{
		InitializeComponent();

		Text = Locale.CollectionTitle;
		_id = contents.First().Key;
		L_Title.Text = contents.First().Value.Title;
		PB_Icon.Collection = true;
		PB_Icon.LoadImage(contents.First().Value.PreviewURL);

		TLP_Contents.RowCount = 0;
		TLP_Contents.RowStyles.Clear();

		foreach (var item in contents.Values.Skip(1))
		{
			if (Controls.Count % 2 == 0)
			{
				TLP_Contents.RowCount++;
				TLP_Contents.RowStyles.Add(new());
			}

			TLP_Contents.Controls.Add(new SteamPackageViewControl(item), Controls.Count % 2, TLP_Contents.RowCount - 1);
		}
	}
	protected override void UIChanged()
	{
		base.UIChanged();

		PB_Icon.Width = TLP_Top.Height = (int)(128 * UI.FontScale);
		TLP_Top.Height += 20;
		L_Title.Font = UI.Font(14F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(7), UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		BackColor = design.AccentBackColor;
		panel1.BackColor = panel2.BackColor = P_Back.BackColor = design.BackColor;
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.AccentBackColor;
	}

	private void B_SteamPage_Click(object sender, System.EventArgs e)
	{
		try
		{ Process.Start($"https://steamcommunity.com/workshop/filedetails/?id={_id}"); }
		catch { }
	}
}
