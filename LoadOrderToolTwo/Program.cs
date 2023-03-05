using Extensions;

using SlickControls;

using System;
using System.Windows.Forms;

namespace LoadOrderToolTwo;
internal static class Program
{
	internal static bool IsRunning { get; private set; }

	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			IsRunning = true;

			SlickCursors.Initialize();
			ConnectionHandler.Start();

			if (Environment.OSVersion.Version.Major == 6)
			{
				SetProcessDPIAware();
			}

			//AppDomain.CurrentDomain.TypeResolve += AssemblyUtil.CurrentDomain_AssemblyResolve;
			//AppDomain.CurrentDomain.AssemblyResolve += AssemblyUtil.CurrentDomain_AssemblyResolve;
			//AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyUtil.ReflectionResolveInterface;
			//AppDomain.CurrentDomain.AssemblyResolve += AssemblyUtil.ResolveInterface;

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
