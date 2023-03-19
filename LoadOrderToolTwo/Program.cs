using Extensions;

using LoadOrderToolTwo.Utilities;

using SlickControls;

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using static System.Environment;

namespace LoadOrderToolTwo;
#nullable disable
internal static class Program
{
	internal static bool IsRunning { get; private set; }
	internal static MainForm MainForm { get; private set; }

	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			if (args.Contains("-stub"))
			{
				Process.Start(Application.ExecutablePath);

				return;
			}

			IsRunning = true;

			SlickCursors.Initialize();
			ConnectionHandler.Start();
			ISave.CustomSaveDirectory = GetFolderPath(SpecialFolder.LocalApplicationData);
			BackgroundAction.BackgroundTaskError += (b, e) => Log.Exception(e, $"The background action ({b}) failed", false);

			if (OSVersion.Version.Major == 6)
			{
				SetProcessDPIAware();
			}

			//AppDomain.CurrentDomain.TypeResolve += AssemblyUtil.CurrentDomain_AssemblyResolve;
			//AppDomain.CurrentDomain.AssemblyResolve += AssemblyUtil.CurrentDomain_AssemblyResolve;
			//AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyUtil.ReflectionResolveInterface;
			//AppDomain.CurrentDomain.AssemblyResolve += AssemblyUtil.ResolveInterface;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm = new MainForm());
		}
		catch (Exception ex)
		{
			MessagePrompt.GetError(ex, "App failed to start", out var message, out var details);
			MessageBox.Show(details, message);
		}
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}
#nullable enable
