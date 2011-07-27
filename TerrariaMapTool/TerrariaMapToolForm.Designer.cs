namespace TerrariaMapTool {
    partial class TerrariaMapToolForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("2");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("1/2");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("1/4");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("1/8");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("1/16");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("1/32");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerrariaMapToolForm));
            this.terrariaPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.worldTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.generateMapDataCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.layersListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.terrariaPathFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outputDirectoryFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.worldOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.zoomLevelsListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.xBlockTilesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.yBlockTilesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.removeLayerButton = new System.Windows.Forms.Button();
            this.addLayerButton = new System.Windows.Forms.Button();
            this.parallelizeGenerationCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.xBlockTilesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.yBlockTilesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // terrariaPathTextBox
            // 
            this.terrariaPathTextBox.Location = new System.Drawing.Point(21, 109);
            this.terrariaPathTextBox.Name = "terrariaPathTextBox";
            this.terrariaPathTextBox.Size = new System.Drawing.Size(262, 20);
            this.terrariaPathTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Terraria Directory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "World File:";
            // 
            // worldTextBox
            // 
            this.worldTextBox.Location = new System.Drawing.Point(21, 156);
            this.worldTextBox.Name = "worldTextBox";
            this.worldTextBox.Size = new System.Drawing.Size(262, 20);
            this.worldTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output Directory:";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(21, 203);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(262, 20);
            this.outputTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Block Size:";
            // 
            // generateMapDataCheckBox
            // 
            this.generateMapDataCheckBox.AutoSize = true;
            this.generateMapDataCheckBox.Location = new System.Drawing.Point(21, 290);
            this.generateMapDataCheckBox.Name = "generateMapDataCheckBox";
            this.generateMapDataCheckBox.Size = new System.Drawing.Size(145, 17);
            this.generateMapDataCheckBox.TabIndex = 9;
            this.generateMapDataCheckBox.Text = "Generate Map Data XML";
            this.generateMapDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.versionLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 69);
            this.panel1.TabIndex = 10;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(20, 38);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(45, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(14, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Terraria Map Generator";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 21);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnBrowseForTerrariaPath);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 155);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(21, 21);
            this.button2.TabIndex = 12;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnBrowseForWorldFile);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(289, 202);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 21);
            this.button3.TabIndex = 13;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnBrowseForOutputDirectory);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(0, 384);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(599, 53);
            this.panel2.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(501, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 25);
            this.button5.TabIndex = 16;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.OnClose);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(420, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 25);
            this.button4.TabIndex = 15;
            this.button4.Text = "Generate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnGenerate);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Zoom Levels:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(328, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Layers:";
            // 
            // layersListView
            // 
            this.layersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.layersListView.FullRowSelect = true;
            this.layersListView.Location = new System.Drawing.Point(331, 231);
            this.layersListView.Name = "layersListView";
            this.layersListView.ShowItemToolTips = true;
            this.layersListView.Size = new System.Drawing.Size(245, 128);
            this.layersListView.TabIndex = 18;
            this.layersListView.UseCompatibleStateImageBehavior = false;
            this.layersListView.View = System.Windows.Forms.View.Details;
            this.layersListView.DoubleClick += new System.EventHandler(this.OnLayersDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Properties";
            this.columnHeader2.Width = 196;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "x";
            // 
            // terrariaPathFolderBrowserDialog
            // 
            this.terrariaPathFolderBrowserDialog.Description = "Select the Terraria Installation Directory:";
            // 
            // outputDirectoryFolderBrowserDialog
            // 
            this.outputDirectoryFolderBrowserDialog.Description = "Select the folder to generate the map tiles to:";
            // 
            // worldOpenFileDialog
            // 
            this.worldOpenFileDialog.Filter = "Terraria World|*.wld|All Files|*.*";
            // 
            // zoomLevelsListView
            // 
            this.zoomLevelsListView.CheckBoxes = true;
            this.zoomLevelsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.zoomLevelsListView.FullRowSelect = true;
            this.zoomLevelsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            this.zoomLevelsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.zoomLevelsListView.Location = new System.Drawing.Point(331, 109);
            this.zoomLevelsListView.Name = "zoomLevelsListView";
            this.zoomLevelsListView.Size = new System.Drawing.Size(245, 91);
            this.zoomLevelsListView.TabIndex = 24;
            this.zoomLevelsListView.UseCompatibleStateImageBehavior = false;
            this.zoomLevelsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 197;
            // 
            // xBlockTilesNumericUpDown
            // 
            this.xBlockTilesNumericUpDown.Location = new System.Drawing.Point(21, 251);
            this.xBlockTilesNumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.xBlockTilesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xBlockTilesNumericUpDown.Name = "xBlockTilesNumericUpDown";
            this.xBlockTilesNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.xBlockTilesNumericUpDown.TabIndex = 25;
            this.xBlockTilesNumericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // yBlockTilesNumericUpDown
            // 
            this.yBlockTilesNumericUpDown.Location = new System.Drawing.Point(102, 251);
            this.yBlockTilesNumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.yBlockTilesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yBlockTilesNumericUpDown.Name = "yBlockTilesNumericUpDown";
            this.yBlockTilesNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.yBlockTilesNumericUpDown.TabIndex = 26;
            this.yBlockTilesNumericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.FlatAppearance.BorderSize = 0;
            this.removeLayerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeLayerButton.Image = global::TerrariaMapTool.Properties.Resources.Remove;
            this.removeLayerButton.Location = new System.Drawing.Point(546, 216);
            this.removeLayerButton.Margin = new System.Windows.Forms.Padding(0);
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(30, 12);
            this.removeLayerButton.TabIndex = 22;
            this.removeLayerButton.UseVisualStyleBackColor = true;
            this.removeLayerButton.Click += new System.EventHandler(this.OnRemoveLayer);
            // 
            // addLayerButton
            // 
            this.addLayerButton.FlatAppearance.BorderSize = 0;
            this.addLayerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addLayerButton.Image = global::TerrariaMapTool.Properties.Resources.Add;
            this.addLayerButton.Location = new System.Drawing.Point(516, 216);
            this.addLayerButton.Margin = new System.Windows.Forms.Padding(0);
            this.addLayerButton.Name = "addLayerButton";
            this.addLayerButton.Size = new System.Drawing.Size(30, 12);
            this.addLayerButton.TabIndex = 21;
            this.addLayerButton.UseVisualStyleBackColor = true;
            this.addLayerButton.Click += new System.EventHandler(this.OnAddLayer);
            // 
            // parallelizeGenerationCheckBox
            // 
            this.parallelizeGenerationCheckBox.AutoSize = true;
            this.parallelizeGenerationCheckBox.Location = new System.Drawing.Point(21, 328);
            this.parallelizeGenerationCheckBox.Name = "parallelizeGenerationCheckBox";
            this.parallelizeGenerationCheckBox.Size = new System.Drawing.Size(128, 17);
            this.parallelizeGenerationCheckBox.TabIndex = 27;
            this.parallelizeGenerationCheckBox.Text = "Parallelize Generation";
            this.parallelizeGenerationCheckBox.UseVisualStyleBackColor = true;
            // 
            // TerrariaMapToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (242)))), ((int) (((byte) (242)))), ((int) (((byte) (255)))));
            this.ClientSize = new System.Drawing.Size(599, 435);
            this.Controls.Add(this.parallelizeGenerationCheckBox);
            this.Controls.Add(this.yBlockTilesNumericUpDown);
            this.Controls.Add(this.xBlockTilesNumericUpDown);
            this.Controls.Add(this.zoomLevelsListView);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.removeLayerButton);
            this.Controls.Add(this.addLayerButton);
            this.Controls.Add(this.layersListView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.generateMapDataCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.worldTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.terrariaPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TerrariaMapToolForm";
            this.Text = "Terraria Map Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.xBlockTilesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.yBlockTilesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox terrariaPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox worldTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox generateMapDataCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView layersListView;
        private System.Windows.Forms.Button removeLayerButton;
        private System.Windows.Forms.Button addLayerButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FolderBrowserDialog terrariaPathFolderBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog outputDirectoryFolderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog worldOpenFileDialog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView zoomLevelsListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.NumericUpDown xBlockTilesNumericUpDown;
        private System.Windows.Forms.NumericUpDown yBlockTilesNumericUpDown;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox parallelizeGenerationCheckBox;
    }
}