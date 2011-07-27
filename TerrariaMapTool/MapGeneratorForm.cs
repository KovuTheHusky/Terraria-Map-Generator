using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TerrariaMapTool {
    public partial class MapGeneratorForm : Form {
        #region Members

        private Thread workerThread = null;

        private MapGeneratorOptions options = null;

        private GuiMapGeneratorProgressHandler progressHandler = null;

        private DateTime startTime;

        private string mapHtmlPath = null;

        #endregion

        #region Events

        public event EventHandler GenerationDone;

        #endregion

        public MapGeneratorForm() {
            InitializeComponent();
        }

        #region Event Handling

        protected virtual void OnGenerationDone(EventArgs e) {
            if (GenerationDone != null) {
                GenerationDone(this, e);
            }
        }

        private void OnCancel(object sender, EventArgs e) {
            progressHandler.Cancel = true;
            cancelButton.Enabled = false;
        }

        private void OnUpdateTime(object sender, EventArgs e) {
            TimeSpan elapsedTime = (DateTime.Now - startTime);

            elapsedTimeLabel.Text = new TimeSpan(elapsedTime.Days, elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds).ToString("g");
        }

        private void OnOpenMap(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(mapHtmlPath)) {
                try {
                    Process.Start(mapHtmlPath);
                } catch {
                }
            }
        }

        #endregion

        #region Members

        public void Generate(MapGeneratorOptions options) {
            if (workerThread != null) {
                throw new InvalidOperationException("Map generation already in progress.");
            }

            if (options == null) {
                throw new ArgumentNullException("options");
            }

            this.options = options;
            this.startTime = DateTime.Now;

            elapsedTimer.Enabled = true;

            progressHandler = new GuiMapGeneratorProgressHandler(this);

            workerThread = new Thread(new ThreadStart(GenerateWorker));
            workerThread.Start();
        }

        public void GenerateWorker() {
            MapGenerator generator = new MapGenerator(options);
            generator.Generate(progressHandler);
            FinishGeneration();
        }

        protected void FinishGeneration() {
            if (InvokeRequired) {
                Invoke(new Action(FinishGeneration), new object[] {});
                return;
            }

            mapHtmlPath = Path.Combine(options.OutputDirectory, "Map.html");

            workerThread = null;
            progressHandler = null;
            options = null;

            elapsedTimer.Enabled = false;

            cancelButton.Visible = false;
            closeButton.Visible = true;
            openMapButton.Visible = true;

            overallProgressBar.Value = overallProgressBar.Maximum;
            subTaskProgressBar.Value = subTaskProgressBar.Maximum;
            subTaskLabel.Text = "Done";

            SystemSounds.Beep.Play();

            OnGenerationDone(EventArgs.Empty);
        }

        #endregion

        #region GuiMapGeneratorProgressHandler

        protected class GuiMapGeneratorProgressHandler : MapGeneratorProgressHandler {
            #region Members

            private MapGeneratorForm form = null;

            #endregion

            #region Constructors

            public GuiMapGeneratorProgressHandler(MapGeneratorForm form) {
                if (form == null) {
                    throw new ArgumentNullException("form");
                }

                this.form = form;
            }

            #endregion

            #region Event Handling

            protected override void OnTaskNameChanged(EventArgs e) {
            }

            protected override void OnTaskCountChanged(EventArgs e) {
                if (form.InvokeRequired) {
                    form.Invoke(new Action<EventArgs>(OnTaskCountChanged), new object[] { e });
                    return;
                }

                form.overallProgressBar.Maximum = TaskCount;
            }

            protected override void OnTasksCompletedChanged(EventArgs e) {
                if (form.InvokeRequired) {
                    form.Invoke(new Action<EventArgs>(OnTasksCompletedChanged), new object[] { e });
                    return;
                }

                form.overallProgressBar.Value = TasksCompleted;
            }

            protected override void OnSubTaskNameChanged(EventArgs e) {
                if (form.InvokeRequired) {
                    form.Invoke(new Action<EventArgs>(OnSubTaskNameChanged), new object[] { e });
                    return;
                }

                form.subTaskLabel.Text = SubTaskName;
            }

            protected override void OnSubTaskCountChanged(EventArgs e) {
                if (form.InvokeRequired) {
                    form.Invoke(new Action<EventArgs>(OnSubTaskCountChanged), new object[] { e });
                    return;
                }

                form.subTaskProgressBar.Maximum = SubTaskCount;
            }

            protected override void OnSubTasksCompletedChanged(EventArgs e) {
                if (form.InvokeRequired) {
                    form.Invoke(new Action<EventArgs>(OnSubTasksCompletedChanged), new object[] { e });
                    return;
                }

                form.subTaskProgressBar.Value = SubTasksCompleted;
            }

            #endregion
        }

        #endregion
    }
}
