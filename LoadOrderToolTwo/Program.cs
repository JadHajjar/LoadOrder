using System;
using System.Windows.Forms;

namespace LoadOrderToolTwo;
internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			SlickControls.SlickCursors.Initialize();

			if (Environment.OSVersion.Version.Major == 6)
				SetProcessDPIAware();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		catch (Exception ex)
		{ MessageBox.Show(ex.ToString(), "App failed to start"); }
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}
