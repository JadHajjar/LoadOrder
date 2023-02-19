namespace LoadOrderTool.UI {
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using CO.IO;
    using System.Diagnostics;
    using LoadOrderTool.Util;
    using LoadOrderTool.Data;
    using CO.Plugins;
    using System.Linq;

    public partial class LaunchControl : UserControl {
        LoadOrderToolSettings settings_ => LoadOrderToolSettings.Instace;
        static ConfigWrapper ConfigWrapper => ConfigWrapper.instance;

        public LaunchControl() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            LoadSettings();
            UpdateCommand();

            foreach (var c in this.GetAll<TextBox>())
                c.TextChanged += OnChanged;

            foreach (var c in this.GetAll<CheckBox>())
                c.CheckedChanged += OnChanged;

            foreach (var c in this.GetAll<RadioButton>())
                c.CheckedChanged += OnChanged;

            checkBoxLSM.Hide();
            checkBoxNoMods.Hide();
            checkBoxNewAsset.SetTooltip("Create asset based on existing asset, as opposed to loading asset.");
            checkBoxLHT.SetTooltip("Traffic drives on left.");
            checkBoxResetAssets.SetTooltip("Enables all assets on load (that you disabled from CS content manager).");
            checkBoxNoAssets.SetTooltip("no asset is loaded (local or workshop).");
            checkBoxNoWorkshop.SetTooltip(
                "all workshop (not local) mods/assets are not loaded.\n" +
                "if Load Order Mod is on workshop then other options will not work.");

            string loadSaveTooltip =
                "empty => continue last save.\n" +
                "save name => load save by name\n" +
                "full path => load save by path.";
            textBoxSavePath.SetTooltip(loadSaveTooltip);
            radioButtonLoadSave.SetTooltip(loadSaveTooltip);
            buttonSavePath.SetTooltip(loadSaveTooltip);

            string loadMapTooltip = "empty => load first map.\n" +
                "map name => load map by name\n" +
                "full path => load map from path.";
            textBoxMapPath.SetTooltip(loadMapTooltip);
            radioButtonNewGame.SetTooltip(loadMapTooltip);
            buttonMapPath.SetTooltip(loadMapTooltip);

            radioButtonSteamExe.SetTooltip("steam features available in game. auto launches steam");
            radioButtonCitiesExe.SetTooltip("no steam features in game.");

            radioButtonDebugMono.SetTooltip("use this when you want to submit logs to modders");
            radioButtonReleaseMono.SetTooltip("this is fast but produces inferior logs");
        }

        public void OnAdvancedChanged() {
            var advancedItems = new Control[] {
                flowLayoutPanelLauncher, // steam.exe cities.exe
                flowLayoutPanelMono,
                flowLayoutPanelProfiler,
                tableLayoutPanelExtraArgs, };
            foreach (var item in advancedItems)
                item.Visible = ConfigWrapper.instance.Advanced;
        }

        public void LoadSettings() {
            if (UIUtil.DesignMode) return;

            checkBoxResetAssets .Checked = settings_.ResetAssets;
            checkBoxNoAssets.Checked = settings_.NoAssets;
            //checkBoxNoMods.Checked = settings_.NoMods;
            checkBoxNoWorkshop.Checked = settings_.NoWorkshop;

            checkBoxLHT.Checked = settings_.LHT;

            switch (settings_.AutoLoad) {
                case 0:
                    radioButtonMainMenu.Checked = true;
                    break;
                case 1:
                    radioButtonAssetEditor.Checked = true;
                    break;
                case 2:
                    radioButtonLoadSave.Checked = true;
                    break;
                case 3:
                    radioButtonNewGame.Checked = true;
                    break;
                default:
                    radioButtonMainMenu.Checked = true;
                    Log.Error("Unexpected settings_.AutoLoad=" + settings_.AutoLoad);
                    break;
            }

            if (settings_.DebugMono)
                radioButtonDebugMono.Checked = true;
            else
                radioButtonReleaseMono.Checked = true;

            if (settings_.ProfilerCities)
                radioButtonProfilerCities.Checked = true;
            else
                radioButtonReleaseCities.Checked = true;

            if (settings_.SteamExe)
                radioButtonSteamExe.Checked = true;
            else
                radioButtonCitiesExe.Checked = true;

            checkBoxNewAsset.Checked = settings_.NewAsset;
            checkBoxLSM.Checked = false;// settings_.LSM;

            textBoxSavePath.Text = settings_.SavedGamePath;
            textBoxMapPath.Text = settings_.MapPath;

            textBoxExtraArgs.Text = settings_.ExtraArgs;

            OnAdvancedChanged();
        }

        void SaveSettings() {
            settings_.ResetAssets = checkBoxResetAssets.Checked;
            settings_.NoAssets = checkBoxNoAssets.Checked;
            settings_.NoMods = checkBoxNoMods.Checked;
            settings_.NoWorkshop = checkBoxNoWorkshop.Checked;

            settings_.LHT = checkBoxLHT.Checked;

            if (radioButtonMainMenu.Checked)
                settings_.AutoLoad = 0;
            else if (radioButtonAssetEditor.Checked)
                settings_.AutoLoad = 1;
            else if (radioButtonLoadSave.Checked)
                settings_.AutoLoad = 2;
            else if (radioButtonNewGame.Checked)
                settings_.AutoLoad = 3;
            else
                settings_.AutoLoad = 0;

            settings_.NewAsset = checkBoxNewAsset.Checked;
            settings_.LSM = checkBoxLSM.Checked;

            settings_.SavedGamePath = textBoxSavePath.Text;
            settings_.MapPath = textBoxMapPath.Text;

            settings_.DebugMono = radioButtonDebugMono.Checked;
            settings_.ProfilerCities = radioButtonProfilerCities.Checked;

            settings_.SteamExe = radioButtonSteamExe.Checked;

            settings_.ExtraArgs = textBoxExtraArgs.Text;

            settings_.Serialize();

            UpdateFiles();
        }

        private void OnChanged(object sender, EventArgs e) {
            Log.Called((sender as Control)?.Name + " " + sender);
            UpdateCommand();
            SaveSettings();
        }

        private void UpdateFiles() {
            try {
                bool success;
                if (settings_.DebugMono) {
                    var fpsBooster = PluginManager.instance.GetMods()
                        .FirstOrDefault(mod => mod.dllName == "FPS_Booster");
                    if (fpsBooster != null &&
                        fpsBooster.IsEnabledPending &&
                        fpsBooster.IsIncludedPending) {
                        DialogResult result = MessageBox.Show(
                            caption: "Disable FPS Booster",
                            text:
                            "Disable FPS Booster to show logs?",
                            buttons: MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes) {
                            fpsBooster.IsIncluded = false;
                            LoadOrderWindow.Instance?.dataGridMods?.RefreshModList();
                        }
                    }
                    success = MonoFile.Instance.UseDebug();
                } else {
                    success = MonoFile.Instance.UseRelease();
                }
                if (!success) {
                    if (MonoFile.Instance.ReleaseIsUsed() is bool bReleaseMono) {
                        Log.Warning("reverting radioButtonReleaseMono.Checked to " + bReleaseMono);
                        radioButtonDebugMono.Checked = !bReleaseMono;
                        radioButtonReleaseMono.Checked = bReleaseMono;
                    }
                }

                if (settings_.ProfilerCities) {
                    success = CitiesFile.Instance.UseDebug();
                } else {
                    success = CitiesFile.Instance.UseRelease();
                }
                if (!success) {
                    if (CitiesFile.Instance.ReleaseIsUsed() is bool bReleaseCities) {
                        Log.Warning("reverting radioButtonReleaseMono.Checked to " + bReleaseCities);
                        radioButtonProfilerCities.Checked = !bReleaseCities;
                        radioButtonReleaseCities.Checked = bReleaseCities;
                    }
                }
            } catch (Exception ex) {
                ex.Log();
            }
        }

        private void UpdateCommand() {
            string fileExe = radioButtonSteamExe.Checked ? DataLocation.SteamExe : DataLocation.CitiesExe;
            labelCommand.Text = fileExe + " " + string.Join(" ", GetCommandArgs());
        }

        private static string quote(string path) => '"' + path + '"';

        private string[] GetCommandArgs() {
            List<string> args = new List<string>();
            if (radioButtonSteamExe.Checked)
                args.Add("-applaunch 255710");

            if (checkBoxNoWorkshop.Checked)
                args.Add("-noWorkshop");
            if (checkBoxResetAssets.Checked)
                args.Add("-reset-assets");
            if (checkBoxNoAssets.Checked)
                args.Add("-noAssets");
            if (checkBoxNoMods.Checked)
                args.Add("-disableMods");
            if (checkBoxLHT.Checked)
                args.Add("-LHT");
            if (radioButtonMainMenu.Checked) {
                ;
            } else if (radioButtonAssetEditor.Checked) {
                if (checkBoxNewAsset.Checked)
                    args.Add("-newAsset");
                else 
                    args.Add("-loadAsset");
                if (checkBoxLSM.Checked)
                    args.Add("-LSM");
            } else if (radioButtonNewGame.Checked) {
                string path = textBoxMapPath.Text;
                if (string.IsNullOrEmpty(path))
                    args.Add("-newGame");
                else
                    args.Add("--newGame=" + quote(path));
            } else if (radioButtonLoadSave.Checked) {
                string path = textBoxSavePath.Text;
                if (string.IsNullOrEmpty(path))
                    args.Add("-continuelastsave");
                else
                    args.Add("--loadSave=" + quote(path));
            }

            var extraArgs = textBoxExtraArgs.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            args.AddRange(extraArgs);

            return args.ToArray();
        }

        private void buttonSavePath_Click(object sender, EventArgs e) {
            var file = OpenCRP(DataLocation.saveLocation, "Load saved game");
            if (!string.IsNullOrEmpty(file))
                textBoxSavePath.Text = file;
        }


        private void buttonMapPath_Click(object sender, EventArgs e) {
            var file = OpenCRP(DataLocation.mapLocation, "Load map");
            if (!string.IsNullOrEmpty(file))
                textBoxMapPath.Text = file;
        }
        private static string OpenCRP(string InitialDirectory, string title) {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "crp file (*.crp)|*.crp";
                ofd.Multiselect = false;
                ofd.CheckPathExists = true;
                ofd.AddExtension = true;
                ofd.InitialDirectory = InitialDirectory;
                ofd.Title = title;
                ofd.CustomPlaces.Add(DataLocation.saveLocation);
                ofd.CustomPlaces.Add(DataLocation.mapLocation);
                ofd.CustomPlaces.Add(DataLocation.WorkshopContentPath);
                if (ofd.ShowDialog() == DialogResult.OK) {
                    return ofd.FileName;
                }
            }
            return null;
        }

        private void buttonLaunch_Click(object sender, EventArgs e) {
            Launch();
        }

        private void FocusSteam() {
            ContentUtil.Execute(DataLocation.SteamPath, DataLocation.SteamExe, null);
        }

        private void Launch() {
            UpdateFiles(); // auto disabling FPS booster causes unsaved changes so this comes first.

            if (!ConfigWrapper.AutoSave && ConfigWrapper.Dirty) {
                var result = MessageBox.Show(
                    caption: "Unsaved changes",
                    text:
                    "There are changes that are not saved to game and will not take efFfect. " +
                    "Save changes to game before launching it?",
                    buttons: MessageBoxButtons.YesNoCancel);
                switch (result) {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        ConfigWrapper.SaveConfig();
                        CO.GameSettings.SaveAll();
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        Log.Exception(new Exception("FormClosing: Unknown choice" + result));
                        break;
                }
            }
            var args = GetCommandArgs();

            string fileExe = radioButtonSteamExe.Checked ? DataLocation.SteamExe : DataLocation.CitiesExe;
            string dir = radioButtonSteamExe.Checked ? DataLocation.SteamPath : DataLocation.GamePath;
            ContentUtil.Execute(dir, fileExe, string.Join(" ", args));
        }

        private void btnTerminate_Click(object sender, EventArgs e) {
            try {
                foreach (var proc in Process.GetProcessesByName("Cities")) {
                    Log.Info("killing " + proc);
                    proc.Kill();
                }
            } catch(Exception ex) { ex.Log(); }
        }
    }
}
