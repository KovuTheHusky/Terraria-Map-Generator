using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Terraria;

namespace TerrariaMapTool {
    public class Program {
        #region Externs

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        #endregion

        #region Members

        public static void ConsoleMain(string[] args) {
            IntPtr foregroundWindow = GetForegroundWindow();
            int processId;

            GetWindowThreadProcessId(foregroundWindow, out processId);

            if (!AttachConsole(processId)) {
                AllocConsole();
            }

            try {
                MapGeneratorOptions options = MapGeneratorOptions.ParseOptions(args);
                MapGenerator generator = new MapGenerator(options);
                ConsoleMapGeneratorProgressHandler handler = new ConsoleMapGeneratorProgressHandler();

                generator.Generate(handler);
            } catch (ArgumentException e) {
                System.Console.WriteLine("Failed to parse arguments:\n\n" + e.Message);
            } catch (Exception e) {
                System.Console.WriteLine("Unhandled exception:\n\n" + e);
            } finally {
                FreeConsole();
            }
        }

        public static void GuiMain(string[] args) {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TerrariaMapToolForm());
            } catch (Exception e) {
                MessageBox.Show("An unhandled exception occured and the application will now exit:\n\n" + e, "Terraria Map Generator", MessageBoxButtons.OK);
            }
        }

        [STAThread]
        public static void Main(string[] args) {
            if (args.Length > 0) {
                ConsoleMain(args);
            } else {
                GuiMain(args);
            }
        }

        #endregion
    }
}
