﻿using CO;
using CO.Packaging;
using CO.Plugins;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LoadOrderTool.Util;

namespace LoadOrderTool {
    public partial class LoadOrderWindow : Form {
        enum EnabledFilter {
            None=0,
            Enabled,
            Disabled,
        }
        enum IncludedFilter {
            None = 0,
            Included,
            Excluded,
        }
        enum BuiltInFilter {
            None = 0,
            BuiltIn,
            Local,
        }

        public static LoadOrderWindow Instance;

        ModList ModList;

        AssetList AssetList;

        public LoadOrderWindow() {
            InitializeComponent();
            ComboBoxIncluded.SetItems<IncludedFilter>();
            this.dataGridViewMods.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            Instance = this;
            LoadMods();
            LoadAsssets();
        }

        public bool ModPredicate(PluginManager.PluginInfo p) {
            {
                var filter = ComboBoxIncluded.GetSelectedItem<IncludedFilter>();
                if (filter != IncludedFilter.None) {
                    bool toggle = filter == IncludedFilter.Included;
                    if (p.IsIncludedPending != toggle)
                        return false;
                }
            }

            return true;
        }

        public void LoadMods() {
            PluginManager.instance.LoadPlugins();
            RefreshModList();
        }

        public void RefreshModList() {
            ModList = ModList.GetAllMods(ModPredicate);
            ModList.SortBy(ModList.HarmonyComparison);
            PopulateMods();
        }

        public void LoadAsssets() {
            PackageManager.instance.LoadPackages();
            AssetList = AssetList.GetAllAssets();
            PopulateAssets();
        }

        public void PopulateAssets() {
            Log.Info("Populating assets");
            foreach (var asset in AssetList) {
                CheckedListBoxAssets.Items.Add(asset.DisplayText, asset.IsIncludedPending);
            }
        }

        public void PopulateMods() {
            SuspendLayout();
            var rows = this.dataGridViewMods.Rows;
            rows.Clear();
            Log.Info("Populating");
            foreach (var p in ModList) {
                string savedKey = p.savedEnabledKey_;
                Log.Debug($"plugin info: dllName={p.dllName} harmonyVersion={ ModList.GetHarmonyOrder(p)} " +
                     $"savedKey={savedKey} modPath={p.ModPath}");
            }
            foreach (var mod in ModList) {
                rows.Add(mod.LoadOrder, mod.IsIncludedPending, mod.IsEnabledPending, mod.DisplayText);
                Log.Debug("row added: " + mod.ToString());
            }
            ResumeLayout();
        }

        private void U32TextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void U32TextBox_Submit(object? sender, EventArgs e) {
            if (sender is TextBox tb) {
                if (tb.Text == "")
                    tb.Text = "0";
                else
                    tb.Text = UInt32.Parse(tb.Text).ToString();
            }
        }

        private void dataGridViewMods_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (dataGridViewMods.CurrentCell.ColumnIndex == 0 && e.Control is TextBox tb) // Desired Column
            {
                tb.KeyPress -= U32TextBox_KeyPress;
                tb.Leave -= U32TextBox_Submit;
                tb.KeyPress += U32TextBox_KeyPress;
                tb.Leave += U32TextBox_Submit;
            }
        }

        private void dataGridViewMods_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            Log.Debug("dataGridViewMods_CellValueChanged() called");
            var plugin = ModList[e.RowIndex];
            var cell = dataGridViewMods.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var col = cell.OwningColumn;

            if (col == LoadIndex) {
                int newVal = Int32.Parse(cell.Value as string);
                int oldVal = e.RowIndex;
                ModList.MoveItem(oldVal, newVal);
                PopulateMods();
            } else if (col == ModEnabled) {
                plugin.IsEnabledPending = (bool)cell.Value;
            } else if (col == IsIncluded) {
                plugin.IsIncludedPending = (bool)cell.Value;
            } else {
                return;
            }
        }


        private void dataGridViewMods_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewMods.CurrentCell is DataGridViewCheckBoxCell) {
                dataGridViewMods.EndEdit();
            }
        }

        private void SortByHarmony_Click(object sender, EventArgs e) {
            foreach (var p in ModList)
                p.LoadOrder = LoadOrderShared.LoadOrderConfig.DefaultLoadOrder;
            ModList.SortBy(ModList.HarmonyComparison);
            PopulateMods();
        }

        private void EnableAllMods_Click(object sender, EventArgs e) {
            foreach (var p in ModList)
                p.IsEnabledPending = true;
            PopulateMods();

        }

        private void DisableAllMods_Click(object sender, EventArgs e) {
            foreach (var p in ModList)
                p.IsEnabledPending = false;
            PopulateMods();
        }

        private void IncludeAllMods_Click(object sender, EventArgs e) {
            foreach (var p in ModList)
                p.IsIncludedPending = true;
            PopulateMods();
        }

        private void ExcludeAllMods_Click(object sender, EventArgs e) {
            foreach (var p in ModList)
                p.IsIncludedPending = false;
            PopulateMods();
        }

        private void ReverseOrder_Click(object sender, EventArgs e) {
            ModList.ReverseOrder();
            PopulateMods();
        }

        private void RandomizeOrder_Click(object sender, EventArgs e) {
            ModList.RandomizeOrder();
            PopulateMods();
        }

        private void LoadOrder_FormClosing(object sender, FormClosingEventArgs e) {
            GameSettings.SaveAll();
        }

        private void SaveProfile_Click(object sender, EventArgs e) {
            SaveFileDialog diaglog = new SaveFileDialog();
            diaglog.Filter = "xml files (*.xml)|*.xml";
            diaglog.InitialDirectory = LoadOrderProfile.DIR;
            if (diaglog.ShowDialog() == DialogResult.OK) {
                ModList.SaveProfile(diaglog.FileName);
            }
        }

        private void LoadProfile_Click(object sender, EventArgs e) {
            using (OpenFileDialog diaglog = new OpenFileDialog()) {
                diaglog.Filter = "xml files (*.xml)|*.xml";
                diaglog.InitialDirectory = LoadOrderProfile.DIR;
                if (diaglog.ShowDialog() == DialogResult.OK) {
                    ModList.LoadProfile(diaglog.FileName);
                    PopulateMods();
                }
            }
        }

        private void Save_Click(object sender, EventArgs e) {
            PluginManager.instance.ConfigWrapper.SaveConfig();
        }

        private void AutoSave_CheckedChanged(object sender, EventArgs e) {
            PluginManager.instance.ConfigWrapper.AutoSave = AutoSave.Checked;
        }

        private void ReloadAll_Click(object sender, EventArgs e) {
            LoadMods();
            LoadAsssets();
        }

        private void dataGridViewMods_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            //Log.Info($"e.ColumnIndex={e.ColumnIndex} Description.Index={Description.Index}");
            if (e.ColumnIndex == Description.Index && e.Value != null) {
                var cell = dataGridViewMods.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = ModList[e.RowIndex].ModInfo.Description;
            }
        }

        private void CheckedListBoxAssets_ItemCheck(object sender, ItemCheckEventArgs e) {
            AssetList[e.Index].IsIncludedPending = e.NewValue != CheckState.Unchecked;
        }

        private void IncludeAllAssets_Click(object sender, EventArgs e) {
            foreach (var asset in AssetList)
                asset.IsIncluded = true;
            PopulateAssets();
        }

        private void ExcludeAllAssets_Click(object sender, EventArgs e) {
            foreach (var asset in AssetList)
                asset.IsIncluded = false;
            PopulateAssets();
        }

        private void RefreshModList(object sender, EventArgs e) => RefreshModList();
    }
}