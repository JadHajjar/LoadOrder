using LoadOrderToolTwo.Utilities.IO;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace LoadOrderToolTwo.Utilities.Managers;
public static class CitiesManager
{
	public static bool CitiesAvailable()
	{
		var fileExe = CentralManager.CurrentProfile.LaunchSettings.UseSteamExe ? LocationManager.SteamExe : LocationManager.CitiesExe;
		var dir = CentralManager.CurrentProfile.LaunchSettings.UseSteamExe ? LocationManager.SteamPath : LocationManager.GamePath;

		return File.Exists(Path.Combine(dir, fileExe));
	}

	public static void Launch()
	{
		UpdateFiles(); // auto disabling FPS booster causes unsaved changes so this comes first.

		//if (!ConfigWrapper.AutoSave && ConfigWrapper.Dirty)
		//{
		//	var result = MessageBox.Show(
		//		caption: "Unsaved changes",
		//		text:
		//		"There are changes that are not saved to game and will not take efFfect. " +
		//		"Save changes to game before launching it?",
		//		buttons: MessageBoxButtons.YesNoCancel);
		//	switch (result)
		//	{
		//		case DialogResult.Cancel:
		//			return;
		//		case DialogResult.Yes:
		//			ConfigWrapper.SaveConfig();
		//			CO.GameSettings.SaveAll();
		//			break;
		//		case DialogResult.No:
		//			break;
		//		default:
		//			Log.Exception(new Exception("FormClosing: Unknown choice" + result));
		//			break;
		//	}
		//}
		var args = GetCommandArgs();

		var fileExe = CentralManager.CurrentProfile.LaunchSettings.UseSteamExe ? LocationManager.SteamExe : LocationManager.CitiesExe;
		var dir = CentralManager.CurrentProfile.LaunchSettings.UseSteamExe ? LocationManager.SteamPath : LocationManager.GamePath;
		IOUtil.Execute(dir, fileExe, string.Join(" ", args));
	}

	private static void UpdateFiles()
	{
		try
		{
			bool success;
			if (CentralManager.CurrentProfile.LaunchSettings.DebugMono)
			{
				var fpsBooster = CentralManager.Mods
					.FirstOrDefault(mod => Path.GetFileNameWithoutExtension(mod.FileName) == "FPS_Booster");
				if (fpsBooster != null &&
					fpsBooster.IsEnabled &&
					fpsBooster.IsIncluded)
				{
					var result = MessageBox.Show(
						caption: "Disable FPS Booster",
						text:
						"Disable FPS Booster to show logs?",
						buttons: MessageBoxButtons.YesNoCancel);
					if (result == DialogResult.Yes)
					{
						fpsBooster.IsIncluded = false;
					}
				}
				success = MonoFile.Instance.UseDebug();
			}
			else
			{
				success = MonoFile.Instance.UseRelease();
			}
			if (!success)
			{
				if (MonoFile.Instance.ReleaseIsUsed() is bool bReleaseMono)
				{
					Log.Warning("reverting CentralManager.CurrentProfile.LaunchSettings.ReleaseMono to " + bReleaseMono);
					CentralManager.CurrentProfile.LaunchSettings.DebugMono = !bReleaseMono;
				}
			}

			if (CentralManager.CurrentProfile.LaunchSettings.ProfilerCities)
			{
				success = CitiesFile.Instance.UseDebug();
			}
			else
			{
				success = CitiesFile.Instance.UseRelease();
			}
			if (!success)
			{
				if (CitiesFile.Instance.ReleaseIsUsed() is bool bReleaseCities)
				{
					Log.Warning("reverting CentralManager.CurrentProfile.LaunchSettings.ReleaseMono to " + bReleaseCities);
					CentralManager.CurrentProfile.LaunchSettings.ProfilerCities = !bReleaseCities;
				}
			}
		}
		catch (Exception ex)
		{
			ex.Log();
		}
	}

	private static string quote(string path)
	{
		return '"' + path + '"';
	}

	private static string[] GetCommandArgs()
	{
		var args = new List<string>();
		if (CentralManager.CurrentProfile.LaunchSettings.UseSteamExe)
		{
			args.Add("-applaunch 255710");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.NoWorkshop)
		{
			args.Add("-noWorkshop");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.ResetAssets)
		{
			args.Add("-reset-assets");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.NoAssets)
		{
			args.Add("-noAssets");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.NoMods)
		{
			args.Add("-disableMods");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.LHT)
		{
			args.Add("-LHT");
		}

		if (CentralManager.CurrentProfile.LaunchSettings.LoadSave)
		{
			var path = CentralManager.CurrentProfile.LaunchSettings.SaveToLoad;
			if (string.IsNullOrEmpty(path))
			{
				args.Add("-continuelastsave");
			}
			else
			{
				args.Add("--loadSave=" + quote(path));
			}
		}

		//var extraArgs = textBoxExtraArgs.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		//args.AddRange(extraArgs);

		return args.ToArray();
	}

	public static bool IsRunning()
	{
		try
		{
			return Process.GetProcessesByName("Cities").Length > 0;
		}
		catch { return false; }
	}

	public static void Kill()
	{
		try
		{
			foreach (var proc in Process.GetProcessesByName("Cities"))
			{
				KillProcessAndChildren(proc);
			}
		}
		catch (Exception ex) { ex.Log(); }
	}

	private static void KillProcessAndChildren(Process proc)
	{
		foreach (var childProc in GetChildProcesses(proc))
		{
			KillProcessAndChildren(childProc);
		}

		proc.Kill();
	}

	private static List<Process> GetChildProcesses(Process proc)
	{
		var childProcs = new List<Process>();

		var mos = new ManagementObjectSearcher(
			$"Select * From Win32_Process Where ParentProcessID={proc.Id}");

		foreach (var mo in mos.Get().Cast<ManagementObject>())
		{
			var childPid = Convert.ToInt32(mo["ProcessID"]);
			var childProc = Process.GetProcessById(childPid);
			childProcs.Add(childProc);
		}

		return childProcs;
	}
}
