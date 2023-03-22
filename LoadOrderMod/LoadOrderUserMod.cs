extern alias Injections;

using CitiesHarmony.API;

using ColossalFramework;
using ColossalFramework.IO;
using ColossalFramework.PlatformServices;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

using ICities;

using KianCommons;

using LoadOrderMod.Data;
using LoadOrderMod.UI;
using LoadOrderMod.UI.EntryAction;
using LoadOrderMod.UI.EntryStatus;
using LoadOrderMod.Util;

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.SceneManagement;

using SteamUtilities = Injections.LoadOrderInjections.SteamUtilities;

namespace LoadOrderMod;
public class LoadOrderUserMod : IUserMod
{
	public static Version ModVersion => typeof(LoadOrderUserMod).Assembly.GetName().Version;
	public static string VersionString => ModVersion.ToString(2);
	public string Name => "Load Order Mod " + VersionString;
	public string Description => "use LoadOrderTool.exe to manage the order in which mods are loaded.";
	public static string HARMONY_ID = "CS.Kian.LoadOrder";

	//static LoadOrderMod() => Log.Debug("Static Ctor "   + Environment.StackTrace);
	//public LoadOrderMod() => Log.Debug("Instance Ctor " + Environment.StackTrace);

	static bool HasDuplicate()
	{
		var currentASM = typeof(LoadOrderUserMod).Assembly;
		foreach (var plugin in PluginManager.instance.GetPluginsInfo())
		{
			foreach (var a in plugin.GetAssemblies())
			{
				if (a != currentASM && a.Name() == currentASM.Name())
				{
					return true;
				}
			}
		}
		return false;
	}

	void CheckDuplicate()
	{
		if (HasDuplicate())
		{
			var m = "There are multiple versions of Load Order Mod. Please exluclude all but one.";
			Log.DisplayError(m);
			throw new Exception(m);
		}
	}

	public void OnEnabled()
	{
		CheckDuplicate();
		try
		{
			Log.Called();


			Util.LoadOrderUtil.ApplyGameLoggingImprovements();
			Log.Info("Cloud.enabled=" + (PlatformService.cloud?.enabled).ToSTR(), true);

			var args = Environment.GetCommandLineArgs();
			Log.Info("command line args are: " + string.Join(" ", args));

			Log.ShowGap = true;
#if DEBUG
			Log.Buffered = true;
#else
            Log.Buffered = false;
#endif
			var items = PlatformService.workshop.GetSubscribedItems();
			Log.Info("Subscribed Items are: " + items.ToSTR());

			//Log.Debug("Testing StackTrace:\n" + new StackTrace(true).ToString(), copyToGameLog: true);
			//KianCommons.UI.TextureUtil.EmbededResources = false;
			//HelpersExtensions.VERBOSE = false;
			//foreach(var p in ColossalFramework.Plugins.PluginManager.instance.GetPluginsInfo()) {
			//    string savedKey = p.name + p.modPath.GetHashCode().ToString() + ".enabled";
			//    Log.Debug($"plugin info: savedKey={savedKey} cachedName={p.name} modPath={p.modPath}");
			//}
			CheckPatchLoader();

			HarmonyHelper.DoOnHarmonyReady(() =>
			{
				//HarmonyLib.Harmony.DEBUG = true;
				HarmonyUtil.InstallHarmony(HARMONY_ID, null, null); // continue on error.
			});
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.activeSceneChanged += OnActiveSceneChanged;

			LoadingManager.instance.m_introLoaded += LoadOrderUtil.TurnOffSteamPanels;
			LoadingManager.instance.m_introLoaded += CheckPatchLoader;

			LoadOrderUtil.TurnOffSteamPanels();

			var introLoaded = ContentManagerUtil.IsIntroLoaded;
			if (introLoaded)
			{
				CacheUtil.CacheData();
			}
			else
			{
				var resetIsEnabledForAssets = Environment.GetCommandLineArgs().Any(_arg => _arg == "-reset-assets");
				if (resetIsEnabledForAssets)
				{
					LoadOrderUtil.ResetIsEnabledForAssets();
				}
				LoadingManager.instance.m_introLoaded += CacheUtil.CacheData;
			}

			if (!Settings.ConfigUtil.Config.IgnoranceIsBliss)
			{
				CheckSubsUtil.RegisterEvents();
			}

			try
			{
				PrepareTool();
			}
			catch (Exception ex) { Debug.LogException(ex); }

			SceneManager.sceneLoaded += MainMenuLoaded;

			MainMenuLoaded(default, default);

			Log.Flush();
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
		}
	}

	private void PrepareTool()
	{
		var currentToolFolder = Path.Combine(PluginManager.instance.FindPluginInfo(Assembly.GetExecutingAssembly())?.modPath, "Tool");
		var toolFolder = Path.Combine(Path.Combine(DataLocation.localApplicationData, "LoadOrder"), "Tool");

		Directory.CreateDirectory(toolFolder);

		foreach (var item in Directory.GetFiles(currentToolFolder))
		{
			var correctName = Path.GetFileName(item).Replace("_", ".");
			var newPath = Path.Combine(toolFolder, correctName);

			if (correctName == "LoadOrderToolTwo.exe.config")
			{
				if (!File.Exists(newPath))
				{
					File.Copy(item, newPath, true);

					PrepareFirstTimeConfig(Path.Combine(toolFolder, correctName));
				}
			}
			else
			{
				File.Copy(item, newPath, true);
			}
		}
	}

	private void PrepareFirstTimeConfig(string configFilePath)
	{
		// Load the external application's .config file
		var configFileMap = new ExeConfigurationFileMap
		{
			ExeConfigFilename = configFilePath
		};
		var externalConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

		// Get the appsettings section from the external config
		var appSettings = externalConfig.AppSettings;

		// Iterate through the appsettings keys and values
		foreach (var key in appSettings.Settings.AllKeys)
		{
			switch (key)
			{
				case "GamePath":
					appSettings.Settings[key].Value = DataLocation.applicationBase;
					break;
				case "AppDataPath":
					appSettings.Settings[key].Value = DataLocation.localApplicationData;
					break;
				case "Platform":
					if (Application.platform is RuntimePlatform.OSXEditor or RuntimePlatform.OSXPlayer)
					{
						appSettings.Settings[key].Value = "MacOSX";
					}
					else if (Application.platform == RuntimePlatform.LinuxPlayer)
					{
						appSettings.Settings[key].Value = "Linux";
					}
					break;
			}
		}

		externalConfig.Save();
	}

	public void OnDisabled()
	{
		try
		{
			foreach (var item in GameObject.FindObjectsOfType<EntryStatusPanel>())
			{
				GameObject.DestroyImmediate(item?.gameObject);
			}
			foreach (var item in GameObject.FindObjectsOfType<EntryActionPanel>())
			{
				GameObject.DestroyImmediate(item?.gameObject);
			}

			LoadingManager.instance.m_introLoaded -= CacheUtil.CacheData;
			LoadingManager.instance.m_introLoaded -= LoadOrderUtil.TurnOffSteamPanels;
			LoadingManager.instance.m_introLoaded -= CheckPatchLoader;
			HarmonyUtil.UninstallHarmony(HARMONY_ID);
			MonoStatus.Release();
			LOMAssetDataExtension.Release();

			Settings.ConfigUtil.Terminate();
			CheckSubsUtil.RemoveEvents();
			Log.Buffered = false;

			SceneManager.sceneLoaded -= MainMenuLoaded;

			MainMenuLoaded(default, (LoadSceneMode)(-1));
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
		}
	}

	private void MainMenuLoaded(Scene arg0, LoadSceneMode arg1)
	{
		var centerPanel = GameObject.Find("MenuContainer")?.GetComponent<UIPanel>().Find<UISlicedSprite>("CenterPart")?.Find<UIPanel>("MenuArea")?.Find<UIPanel>("Menu");

		if ((int)arg1 == -1)
		{
			var lotButton = centerPanel?.Find<UIButton>("LOTBUTTON");

			if (lotButton != null)
			{
				(GameObject.Find("MenuContainer")?.GetComponent<UIPanel>().Find<UISlicedSprite>("CenterPart")).height -= lotButton.height;

				lotButton.OnDestroy();
			}
			return;
		}
		
		var continueButton = centerPanel?.Find<UIButton>("Exit");

		if (continueButton != null)
		{
			var newButton = centerPanel.AddUIComponent<UIButton>();

			continueButton.ShalowClone(newButton, true);
			newButton.text = "LOAD ORDER TOOL";
			newButton.name = newButton.cachedName = "LOTBUTTON";
			newButton.stringUserData = "";
			newButton.atlas = continueButton.atlas;
			newButton.font = continueButton.font;
			newButton.disabledBgSprite = continueButton.disabledBgSprite;
			newButton.disabledFgSprite = continueButton.disabledFgSprite;
			newButton.focusedBgSprite = continueButton.focusedBgSprite;
			newButton.focusedFgSprite = continueButton.focusedFgSprite;
			newButton.hoveredBgSprite = continueButton.hoveredBgSprite;
			newButton.hoveredFgSprite = continueButton.hoveredFgSprite;
			newButton.normalBgSprite = continueButton.normalBgSprite;
			newButton.normalFgSprite = continueButton.normalFgSprite;
			newButton.pressedBgSprite = continueButton.pressedBgSprite;
			newButton.pressedFgSprite = continueButton.pressedFgSprite;
			newButton.zOrder -= 3;

			newButton.eventClick += LOT_eventClick;

			(GameObject.Find("MenuContainer")?.GetComponent<UIPanel>().Find<UISlicedSprite>("CenterPart")).height += newButton.height;
		}
	}

	private void LOT_eventClick(UIComponent component, UIMouseEventParameter eventParam)
	{
		var toolPath = Path.Combine(Path.Combine(Path.Combine(DataLocation.localApplicationData, "LoadOrder"), "Tool"), "LoadOrderToolTwo.exe");
		var openTools = false;

		if (Application.platform is RuntimePlatform.OSXEditor or RuntimePlatform.OSXPlayer or RuntimePlatform.LinuxPlayer)
		{
			System.Diagnostics.Process.Start(Directory.GetParent(toolPath).FullName);
			return;
		}

		try
		{
			openTools = System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(toolPath)).Length > 0;
		}
		catch { }

		try
		{
			if (openTools)
			{
				File.WriteAllText(Path.Combine(Directory.GetParent(toolPath).FullName, "Wake"), "It's time to wake up");
			}
			else if (File.Exists(toolPath))
			{
				System.Diagnostics.Process.Start(toolPath, "-stub");
			}
			else
			{
				var panel = UIView.library.ShowModal<ExceptionPanel>("ToolMissing");
				panel.SetMessage("Load Order Tool Missing", "The Load Order Tool application is missing from your computer.\r\n\r\nThis may be caused by missing files in the mod folder, or that your antivirus removed it.", true);
			}
		}
		catch (Exception ex) { UnityEngine.Debug.LogException(ex); }
	}

	public void CheckPatchLoader()
	{
		Log.Info("SteamUtilities.Initialized=" + SteamUtilities.Initialized);
		if (!SteamUtilities.Initialized && PatchLoaderStatus.Instance.IsAvailbleAndEnabled)
		{
			Log.DisplayWarning(SteamUtilities.Initialized + " Patch Loader Ineffective. Some LOM features might not work!\n\n" + PatchLoaderStatus.WindowsCriticalErrorSolutions);
		}
	}


	public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Log.Info($"OnSceneLoaded({scene.name}, {mode})", true);
		if (scene.name == "MainMenu")
		{
			MonoStatus.Ensure();
		}
		Log.Flush();
	}

	public static void OnActiveSceneChanged(Scene from, Scene to)
	{
		Log.Info($"OnActiveSceneChanged({from.name}, {to.name})", true);
		Log.Flush();
		if (Helpers.InStartupMenu)
		{
			LoadOrderUtil.TurnOffSteamPanels();
		}
	}

	public void OnSettingsUI(UIHelperBase helper)
	{
		Settings.Settings.OnSettingsUI(helper as UIHelper);
	}
}
