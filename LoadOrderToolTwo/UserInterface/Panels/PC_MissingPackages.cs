using Extensions;

using LoadOrderToolTwo.Domain;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_MissingPackages : PanelContent
{
	public PC_MissingPackages(List<Profile.Mod> missingMods, List<Profile.Asset> missingAssets)
	{
		InitializeComponent();

		MissingMods = missingMods;
		MissingAssets = missingAssets;
	}

	public List<Profile.Mod> MissingMods { get; }
	public List<Profile.Asset> MissingAssets { get; }

	internal static void PromptMissingPackages(BasePanelForm form, List<Profile.Mod> missingMods, List<Profile.Asset> missingAssets)
	{
		var pauseEvent = new AutoResetEvent(false);

		form.TryInvoke(() =>
		{
			var panel = new PC_MissingPackages(missingMods, missingAssets);

			form.PushPanel(null, panel);

			panel.Disposed += (s, e) =>
			{
				pauseEvent.Set();
			};
		});

		pauseEvent.WaitOne();
	}
}
