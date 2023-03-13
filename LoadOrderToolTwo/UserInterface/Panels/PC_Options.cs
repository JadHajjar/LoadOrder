using LoadOrderToolTwo.Utilities;

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
public partial class PC_Options : PanelContent
{
	public PC_Options()
	{
		InitializeComponent();
	}

	protected override void LocaleChanged()
	{
		Text = Locale.Options;
	}
}
