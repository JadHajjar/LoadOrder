using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
public partial class PC_Mods : PanelContent
{
	public PC_Mods()
	{
		InitializeComponent();

		Text = $"{Locale.Mods} - {ProfileManager.CurrentProfile.Name}";
	}
}
