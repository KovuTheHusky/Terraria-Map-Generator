using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaMapTool {
    public abstract class MapGeneratorProgressHandler {
        #region Members

        private bool cancel = false;

        private string taskName = "";

        private int taskCount = 0;

        private int tasksCompleted = 0;

        private string subTaskName = "";

        private int subTaskCount = 0;

        private int subTasksCompleted = 0;

        #endregion

        #region Properties

        public bool Cancel {
            get {
                return cancel;
            }
            set {
                cancel = value;
            }
        }

        public string TaskName {
            get {
                return taskName;
            }
            set {
                if (taskName != value) {
                    taskName = value;
                    OnTaskNameChanged(EventArgs.Empty);
                }
            }
        }

        public int TaskCount {
            get {
                return taskCount;
            }
            set {
                if (taskCount != value) {
                    taskCount = value;
                    OnTaskCountChanged(EventArgs.Empty);
                }
            }
        }

        public int TasksCompleted {
            get {
                return tasksCompleted;
            }
            set {
                if (value < 0) {
                    value = 0;
                }

                if (value > TaskCount) {
                    value = TaskCount;
                }

                if (tasksCompleted != value) {
                    tasksCompleted = value;
                    OnTasksCompletedChanged(EventArgs.Empty);
                }
            }
        }

        public string SubTaskName {
            get {
                return subTaskName;
            }
            set {
                if (subTaskName != value) {
                    subTaskName = value;
                    OnSubTaskNameChanged(EventArgs.Empty);
                }
            }
        }

        public int SubTaskCount {
            get {
                return subTaskCount;
            }
            set {
                if (subTaskCount != value) {
                    subTaskCount = value;
                    OnSubTaskCountChanged(EventArgs.Empty);
                }
            }
        }

        public int SubTasksCompleted {
            get {
                return subTasksCompleted;
            }
            set {
                if (value < 0) {
                    value = 0;
                }

                if (value > SubTaskCount) {
                    value = SubTaskCount;
                }

                if (subTasksCompleted != value) {
                    subTasksCompleted = value;
                    OnSubTasksCompletedChanged(EventArgs.Empty);
                }
            }
        }
        
        #endregion

        #region Events

        public event EventHandler TaskNameChanged;

        public event EventHandler TaskCountChanged;

        public event EventHandler TasksCompletedChanged;

        public event EventHandler SubTaskNameChanged;
        
        public event EventHandler SubTaskCountChanged;

        public event EventHandler SubTasksCompletedChanged;

        #endregion

        #region Event Handling

        protected virtual void OnTaskNameChanged(EventArgs e) {
            if (TaskNameChanged != null) {
                TaskNameChanged(this, e);
            }
        }

        protected virtual void OnTaskCountChanged(EventArgs e) {
            if (TaskCountChanged != null) {
                TaskCountChanged(this, e);
            }
        }

        protected virtual void OnTasksCompletedChanged(EventArgs e) {
            if (TasksCompletedChanged != null) {
                TasksCompletedChanged(this, e);
            }
        }

        protected virtual void OnSubTaskNameChanged(EventArgs e) {
            if (SubTaskNameChanged != null) {
                SubTaskNameChanged(this, e);
            }
        }

        protected virtual void OnSubTaskCountChanged(EventArgs e) {
            if (SubTaskCountChanged != null) {
                SubTaskCountChanged(this, e);
            }
        }

        protected virtual void OnSubTasksCompletedChanged(EventArgs e) {
            if (SubTasksCompletedChanged != null) {
                SubTasksCompletedChanged(this, e);
            }
        }

        #endregion

        #region Members

        public virtual void WriteLine(string message) {
        }

        public void WriteLine(string format, params object[] arguments) {
            WriteLine(string.Format(format, arguments));
        }

        #endregion
    }
}
