namespace TerrariaMapTool {
    partial class MapGeneratorForm {
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.subTaskLabel = new System.Windows.Forms.Label();
            this.overallProgressBar = new System.Windows.Forms.ProgressBar();
            this.subTaskProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.elapsedTimer = new System.Windows.Forms.Timer(this.components);
            this.openMapButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Overall";
            // 
            // subTaskLabel
            // 
            this.subTaskLabel.AutoSize = true;
            this.subTaskLabel.Location = new System.Drawing.Point(12, 70);
            this.subTaskLabel.Name = "subTaskLabel";
            this.subTaskLabel.Size = new System.Drawing.Size(40, 13);
            this.subTaskLabel.TabIndex = 1;
            this.subTaskLabel.Text = "Overall";
            // 
            // overallProgressBar
            // 
            this.overallProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.overallProgressBar.Location = new System.Drawing.Point(15, 36);
            this.overallProgressBar.Name = "overallProgressBar";
            this.overallProgressBar.Size = new System.Drawing.Size(373, 18);
            this.overallProgressBar.TabIndex = 2;
            // 
            // subTaskProgressBar
            // 
            this.subTaskProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.subTaskProgressBar.Location = new System.Drawing.Point(15, 91);
            this.subTaskProgressBar.Name = "subTaskProgressBar";
            this.subTaskProgressBar.Size = new System.Drawing.Size(373, 18);
            this.subTaskProgressBar.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.openMapButton);
            this.panel2.Controls.Add(this.elapsedTimeLabel);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.closeButton);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Location = new System.Drawing.Point(0, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 53);
            this.panel2.TabIndex = 16;
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Location = new System.Drawing.Point(46, 20);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(43, 13);
            this.elapsedTimeLabel.TabIndex = 18;
            this.elapsedTimeLabel.Text = "0 00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Time:";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(313, 14);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancel);
            // 
            // elapsedTimer
            // 
            this.elapsedTimer.Interval = 1000;
            this.elapsedTimer.Tick += new System.EventHandler(this.OnUpdateTime);
            // 
            // openMapButton
            // 
            this.openMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openMapButton.Location = new System.Drawing.Point(232, 14);
            this.openMapButton.Name = "openMapButton";
            this.openMapButton.Size = new System.Drawing.Size(75, 25);
            this.openMapButton.TabIndex = 19;
            this.openMapButton.Text = "Open Map";
            this.openMapButton.UseVisualStyleBackColor = true;
            this.openMapButton.Visible = false;
            this.openMapButton.Click += new System.EventHandler(this.OnOpenMap);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(313, 14);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 25);
            this.closeButton.TabIndex = 20;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Visible = false;
            // 
            // MapGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(402, 188);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.subTaskProgressBar);
            this.Controls.Add(this.overallProgressBar);
            this.Controls.Add(this.subTaskLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapGeneratorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terraria Map Generator";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label subTaskLabel;
        private System.Windows.Forms.ProgressBar overallProgressBar;
        private System.Windows.Forms.ProgressBar subTaskProgressBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Timer elapsedTimer;
        private System.Windows.Forms.Button openMapButton;
        private System.Windows.Forms.Button closeButton;
    }
}