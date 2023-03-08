using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Drawing;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Profiles : PanelContent
{
	public PC_Profiles()
	{
		InitializeComponent();
		Text = Locale.ProfileBubble;

		if (CentralManager.CurrentProfile.Temporary)
		{
			label1.Text = Locale.NoProfileSelected;
			label1.Font = UI.Font(10.5F);
			label1.Location = Size.Center(label1.Size);
		}
		else
			label1.Visible = false;

		LoadProfile(CentralManager.CurrentProfile);
	}

	private void LoadProfile(Profile profile)
	{
		L_CurrentProfile.Text = profile.Name;
		CB_AutoSave.Checked = profile.AutoSave;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		L_CurrentProfile.Font = UI.Font(12.75F, FontStyle.Bold);
		slickButton1.Font = slickButton2.Font = UI.Font(9.75F);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		tableLayoutPanel1.BackColor = design.ButtonColor;
	}
}
