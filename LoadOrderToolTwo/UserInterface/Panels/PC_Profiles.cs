using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Profiles : PanelContent
{
	private bool loadingProfile;
	public PC_Profiles()
	{
		InitializeComponent();

		Text = Locale.ProfileBubble;
		L_TempProfile.Text = Locale.TemporaryProfileCanNotBeEdited;
		TLP_LaunchSettings.Text = Locale.LaunchSettings;
		TLP_GeneralSettings.Text = Locale.Settings;

		LoadProfile(CentralManager.CurrentProfile);

		foreach (var profile in ProfileManager.Profiles)
		{
			var ctrl = new ProfilePreviewControl(profile);

			FLP_Profiles.Controls.Add(ctrl);
			FLP_Profiles.SetFlowBreak(ctrl, true);

			ctrl.MouseClick += Profile_MouseClick;
		}

		ProfileManager.ProfileChanged += (p) => this.TryInvoke(() => LoadProfile(p));
	}

	private void Profile_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			I_ProfileIcon.Loading = true;
			L_CurrentProfile.Text = (sender as ProfilePreviewControl)!.Profile.Name;
			FLP_Options.Enabled = B_EditName.Visible = false;
			ProfileManager.SetProfile((sender as ProfilePreviewControl)!.Profile);
			AnimationHandler.Animate(P_Profiles, UI.Scale(new Size(0, 1), UI.FontScale), 2, AnimationOption.IgnoreHeight);
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		TB_SavePath.Image = Properties.Resources.I_FolderSearch;
	}

	private void LoadProfile(Profile profile)
	{
		loadingProfile = true;

		L_TempProfile.Visible = I_TempProfile.Visible = profile.Temporary;
		FLP_Options.Enabled = B_EditName.Visible = !profile.Temporary;

		I_ProfileIcon.Loading = false;
		L_CurrentProfile.Text = profile.Name;
		CB_AutoSave.Checked = profile.AutoSave;
		CB_NoWorkshop.Checked = profile.LaunchSettings.NoWorkshop;
		CB_NoAssets.Checked = profile.LaunchSettings.NoAssets;
		CB_NoMods.Checked = profile.LaunchSettings.NoMods;
		CB_LHT.Checked = profile.LaunchSettings.LHT;
		CB_LoadSave.Checked = profile.LaunchSettings.LoadSave;
		TB_SavePath.Text = profile.LaunchSettings.SaveToLoad;

		loadingProfile = false;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		L_TempProfile.Font = UI.Font(10.5F);
		L_CurrentProfile.Font = UI.Font(12.75F, FontStyle.Bold);
		B_LoadProfiles.Font = B_NewProfile.Font = UI.Font(9.75F);
		TLP_GeneralSettings.Margin = TLP_LaunchSettings.Margin = UI.Scale(new Padding(10), UI.UIScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Profiles.BackColor = FormState.NormalFocused.Color();
		P_Profiles2.BackColor = design.BackColor;
		TLP_ProfileName.BackColor = design.ButtonColor;
		L_TempProfile.ForeColor = design.YellowColor;
		P_ScrollPanel.BackColor = design.AccentBackColor;
		TLP_LaunchSettings.BackColor = TLP_GeneralSettings.BackColor = design.Type == FormDesignType.Light ? design.BackColor : design.ButtonColor;
	}

	private void ValueChanged(object sender, EventArgs e)
	{
		if (loadingProfile)
		{
			return;
		}

		CentralManager.CurrentProfile.AutoSave = CB_AutoSave.Checked;
		CentralManager.CurrentProfile.LaunchSettings.NoWorkshop = CB_NoWorkshop.Checked;
		CentralManager.CurrentProfile.LaunchSettings.NoAssets = CB_NoAssets.Checked;
		CentralManager.CurrentProfile.LaunchSettings.NoMods = CB_NoMods.Checked;
		CentralManager.CurrentProfile.LaunchSettings.LHT = CB_LHT.Checked;
		CentralManager.CurrentProfile.LaunchSettings.LoadSave = CB_LoadSave.Checked;
		CentralManager.CurrentProfile.LaunchSettings.SaveToLoad = TB_SavePath.Text;

		ProfileManager.Save(CentralManager.CurrentProfile);
	}

	private void B_LoadProfiles_Click(object sender, EventArgs e)
	{
		if (!I_ProfileIcon.Loading)
		{
			AnimationHandler.Animate(P_Profiles, UI.Scale(new Size(P_Profiles.Width == 0 ? 320 : 0, 1), UI.FontScale), 2, AnimationOption.IgnoreHeight);
		}
	}

	public override void GlobalMouseMove(Point p)
	{
		if (P_Profiles.Width == 0)
		{
			return;
		}

		var animationOpening = AnimationHandler.GetAnimation(P_Profiles, AnimationOption.IgnoreHeight | AnimationOption.IgnoreY | AnimationOption.IgnoreX);

		if (animationOpening != null && animationOpening.Animating && animationOpening.NewBounds.Width != 0)
		{
			return;
		}

		var shouldClose = !new Rectangle(P_Profiles.PointToScreen(Point.Empty), P_Profiles.Size).Pad(-100).Contains(p);

		AnimationHandler.Animate(P_Profiles, UI.Scale(new Size(shouldClose ? 0 : 320, 1), UI.FontScale), 2, AnimationOption.IgnoreHeight);
	}
}
