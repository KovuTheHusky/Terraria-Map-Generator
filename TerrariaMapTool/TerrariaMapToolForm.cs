using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Win32;

namespace TerrariaMapTool {
    public partial class TerrariaMapToolForm : Form {
        #region Externs

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr windowHandle, String pszSubAppName, int pszSubIdList);

        #endregion

        #region Properties

        protected MapGeneratorOptions Options {
            get {
                MapGeneratorOptions mapGeneratorOptions = new MapGeneratorOptions();

                mapGeneratorOptions.TerrariaDirectory = terrariaPathTextBox.Text;
                mapGeneratorOptions.WorldPath = worldTextBox.Text;
                mapGeneratorOptions.OutputDirectory = outputTextBox.Text;

                mapGeneratorOptions.BlockSize = new System.Drawing.Size((int) xBlockTilesNumericUpDown.Value, (int) yBlockTilesNumericUpDown.Value);

                mapGeneratorOptions.WriteMapData = generateMapDataCheckBox.Checked;

                mapGeneratorOptions.ParallelizeGeneration = parallelizeGenerationCheckBox.Checked;

                foreach (ListViewItem zoomLevelItem in zoomLevelsListView.CheckedItems) {
                    mapGeneratorOptions.ZoomLevels.Add((float) zoomLevelItem.Tag);
                }

                foreach (ListViewItem layerPropertiesItem in layersListView.Items) {
                    mapGeneratorOptions.Layers.Add(layerPropertiesItem.Tag as MapGeneratorLayerOptions);
                }

                return mapGeneratorOptions;
            }
            set {
                terrariaPathTextBox.Text = value.TerrariaDirectory;
                worldTextBox.Text = value.WorldPath;
                outputTextBox.Text = value.OutputDirectory;

                xBlockTilesNumericUpDown.Value = value.BlockSize.Width;
                yBlockTilesNumericUpDown.Value = value.BlockSize.Height;

                generateMapDataCheckBox.Checked = value.WriteMapData;
                parallelizeGenerationCheckBox.Checked = value.ParallelizeGeneration;

                zoomLevelsListView.Items[0].Checked = value.ZoomLevels.Contains(2.0f);
                zoomLevelsListView.Items[1].Checked = value.ZoomLevels.Contains(1.0f);
                zoomLevelsListView.Items[2].Checked = value.ZoomLevels.Contains(0.5f);
                zoomLevelsListView.Items[3].Checked = value.ZoomLevels.Contains(0.25f);
                zoomLevelsListView.Items[4].Checked = value.ZoomLevels.Contains(0.125f);
                zoomLevelsListView.Items[5].Checked = value.ZoomLevels.Contains(0.0625f);
                zoomLevelsListView.Items[6].Checked = value.ZoomLevels.Contains(0.03125f);

                layersListView.Items.Clear();

                foreach (MapGeneratorLayerOptions layerProperties in value.Layers) {
                    ListViewItem layerPropertiesListViewItem = new ListViewItem();
                    layerPropertiesListViewItem.Text = (1 + layersListView.Items.Count).ToString();
                    layerPropertiesListViewItem.SubItems.Add(layerProperties.ToString());
                    layerPropertiesListViewItem.Tag = layerProperties;
                    layersListView.Items.Add(layerPropertiesListViewItem);
                }
            }
        }

        #endregion

        #region Constructors

        public TerrariaMapToolForm() {
            InitializeComponent();

            try {
                SetWindowTheme(zoomLevelsListView.Handle, "explorer", 0);
                SetWindowTheme(layersListView.Handle, "explorer", 0);
            } catch {
            }
        }

        #endregion

        #region Event Handling

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            versionLabel.Text = "Version: " + Assembly.GetEntryAssembly().GetName().Version.ToString();

            zoomLevelsListView.Items[0].Tag = 2.0f;
            zoomLevelsListView.Items[1].Tag = 1.0f;
            zoomLevelsListView.Items[2].Tag = 0.5f;
            zoomLevelsListView.Items[3].Tag = 0.25f;
            zoomLevelsListView.Items[4].Tag = 0.125f;
            zoomLevelsListView.Items[5].Tag = 0.0625f;
            zoomLevelsListView.Items[6].Tag = 0.03125f;

            try {
                LoadSettings();
            } catch {
                MapGeneratorLayerOptions defaultLayer = new MapGeneratorLayerOptions();
                defaultLayer.DrawWalls = true;
                defaultLayer.DrawBackgroundWater = true;
                defaultLayer.DrawForegroundWater = true;
                defaultLayer.DrawBackgroundTiles = true;
                defaultLayer.DrawForegroundTiles = true;

                ListViewItem layerPropertiesListViewItem = new ListViewItem();
                layerPropertiesListViewItem.Text = "1";
                layerPropertiesListViewItem.SubItems.Add(defaultLayer.ToString());
                layerPropertiesListViewItem.Tag = defaultLayer;
                layersListView.Items.Add(layerPropertiesListViewItem);
            }

            if (string.IsNullOrWhiteSpace(terrariaPathTextBox.Text)) {
                terrariaPathTextBox.Text = GetTerrariaInstallationPath();
            }

            terrariaPathFolderBrowserDialog.SelectedPath = terrariaPathTextBox.Text;
            worldOpenFileDialog.InitialDirectory = GetWorldsPath();
            outputDirectoryFolderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            SaveSettings();
        }

        protected override void OnPaint (PaintEventArgs e) {
            base.OnPaint(e);

            Color line = BackColor.Lerp(Color.Black, 0.1f);

            using (Pen pen = new Pen(line)) {
                e.Graphics.DrawLine(pen, 0, panel1.Height, Width + 1, panel1.Height);
                e.Graphics.DrawLine(pen, 0, panel2.Top - 1, Width + 1, panel2.Top - 1);
            }
        }

        private void OnBrowseForTerrariaPath(object sender, EventArgs e) {
            if (terrariaPathFolderBrowserDialog.ShowDialog() == DialogResult.OK) {
                terrariaPathTextBox.Text = terrariaPathFolderBrowserDialog.SelectedPath;
            }
        }

        private void OnBrowseForWorldFile(object sender, EventArgs e) {
            if (worldOpenFileDialog.ShowDialog() == DialogResult.OK) {
                worldTextBox.Text = worldOpenFileDialog.FileName;
            }
        }

        private void OnBrowseForOutputDirectory(object sender, EventArgs e) {
            if (outputDirectoryFolderBrowserDialog.ShowDialog() == DialogResult.OK) {
                outputTextBox.Text = outputDirectoryFolderBrowserDialog.SelectedPath;
            }
        }

        private void OnAddLayer(object sender, EventArgs e) {
            LayerPropertiesForm propertiesForm = new LayerPropertiesForm();

            if (propertiesForm.ShowDialog() == DialogResult.OK) {
                MapGeneratorLayerOptions layerOptions = propertiesForm.LayerProperties;

                ListViewItem layerPropertiesListViewItem = new ListViewItem();
                layerPropertiesListViewItem.Text = (1 + layersListView.Items.Count).ToString();
                layerPropertiesListViewItem.SubItems.Add(layerOptions.ToString());
                layerPropertiesListViewItem.Tag = layerOptions;
                layersListView.Items.Add(layerPropertiesListViewItem);
            }
        }

        private void OnRemoveLayer(object sender, EventArgs e) {
            while (layersListView.SelectedItems.Count > 0) {
                layersListView.Items.Remove(layersListView.SelectedItems[0]);
            }
        }

        private void OnLayersDoubleClick (object sender, EventArgs e) {
            Point mousePosition = layersListView.PointToClient(MousePosition);

            ListViewItem item = layersListView.GetItemAt(mousePosition.X, mousePosition.Y);

            if (item != null) {
                LayerPropertiesForm propertiesForm = new LayerPropertiesForm();
                propertiesForm.LayerProperties = item.Tag as MapGeneratorLayerOptions;

                if (propertiesForm.ShowDialog() == DialogResult.OK) {
                    MapGeneratorLayerOptions layerOptions = propertiesForm.LayerProperties;

                    item.SubItems[1].Text = layerOptions.ToString();
                    item.Tag = layerOptions;
                }
            }
        }

        private void OnGenerate(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(terrariaPathTextBox.Text)) {
                MessageBox.Show("The Terraria installation path has not been set. Please select the base directory of your Terraria installation.", "Terraria Map Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(terrariaPathTextBox.Text)) {
                MessageBox.Show("The Terraria installation path is not valid. Please select the base directory of your Terraria installation.", "Terraria Map Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(worldTextBox.Text)) {
                MessageBox.Show("The world file has not been set. Please enter the path to the world to generate the map tiles for.", "Terraria Map Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(worldTextBox.Text)) {
                MessageBox.Show("The world file does not exist. Please enter a valid path for the world to generate the map tiles for.", "Terraria Map Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(outputTextBox.Text)) {
                MessageBox.Show("The output directory was not specified. Please select the directory to write the map tiles to.", "Terraria Map Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetControlsEnabled(false);

            MapGeneratorForm generatorForm = new MapGeneratorForm();

            generatorForm.Generate(Options);
            generatorForm.ShowDialog(this);

            SetControlsEnabled(true);
        }

        private void OnClose(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region Members

        private void SetControlsEnabled(bool state) {
            terrariaPathTextBox.Enabled = state;
            button1.Enabled = state;

            worldTextBox.Enabled = state;
            button2.Enabled = state;

            outputTextBox.Enabled = state;
            button3.Enabled = state;

            xBlockTilesNumericUpDown.Enabled = state;
            yBlockTilesNumericUpDown.Enabled = state;

            generateMapDataCheckBox.Enabled = state;
            parallelizeGenerationCheckBox.Enabled = state;

            zoomLevelsListView.Enabled = state;

            addLayerButton.Enabled = state;
            removeLayerButton.Enabled = state;
            layersListView.Enabled = state;

            button4.Enabled = state;
            button5.Enabled = state;
        }

        private string GetTerrariaInstallationPath() {
            try {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 105600");

                if (key.GetValueKind("InstallLocation") == RegistryValueKind.String) {
                    return key.GetValue("InstallLocation") as string;
                }
            } catch {
            }

            return string.Empty;
        }

        private string GetWorldsPath() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Terraria", "Worlds");
        }

        private void LoadSettings() {
            string rulesFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TerrariaMapGenerator.xml");

            if (File.Exists(rulesFiles)) {
                XmlSerializer deserializer = new XmlSerializer(typeof(MapGeneratorOptions));

                using (TextReader textReader = new StreamReader(rulesFiles)) {
                    Options = (MapGeneratorOptions) deserializer.Deserialize(textReader);
                }
            }
        }

        private void SaveSettings() {
            string rulesFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TerrariaMapGenerator.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(MapGeneratorOptions));

            using (TextWriter textWriter = new StreamWriter(rulesFiles)) {
                serializer.Serialize(textWriter, Options);
            }
        }

        #endregion
    }
}
