using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaMapTool {
    public class ConsoleMapGeneratorProgressHandler : MapGeneratorProgressHandler {
        protected override void OnSubTaskNameChanged(EventArgs e) {
            System.Console.WriteLine(SubTaskName);
        }

        public override void WriteLine(string message) {
            System.Console.WriteLine(message);
        }
    }
}
