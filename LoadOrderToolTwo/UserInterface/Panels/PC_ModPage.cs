using LoadOrderToolTwo.Domain;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.Panels;
internal class PC_ModPage : PanelContent
{
    public PC_ModPage(Mod mod)
    {
		Mod = mod;

		Text = mod.Name;
	}

	public Mod Mod { get; }
}
