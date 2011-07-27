using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaMapTool {
    public class MapGeneratorLayerOptions {
        #region Members

        /// <summary>
        ///     Flag indicating if the background should be drawn.
        /// </summary>
        private bool drawBackground = false;

        /// <summary>
        ///     Flag indicating if walls should be drawn.
        /// </summary>
        private bool drawWalls = false;

        /// <summary>
        ///     Flag indicating if background water should be drawn.
        /// </summary>
        private bool drawBackgroundWater = false;

        /// <summary>
        ///     Flag indicating if foreground water should be drawn.
        /// </summary>
        private bool drawForegroundWater = false;

        /// <summary>
        ///     Flag indicating if background tiles should be drawn.
        /// </summary>
        private bool drawBackgroundTiles = false;

        /// <summary>
        ///     Flag indicating if foreground tiles should be drawn.
        /// </summary>
        private bool drawForegroundTiles = false;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a flag indicating if the background should be drawn.
        /// </summary>
        public bool DrawBackground {
            get {
                return drawBackground;
            }
            set {
                drawBackground = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if walls should be drawn.
        /// </summary>
        public bool DrawWalls {
            get {
                return drawWalls;
            }
            set {
                drawWalls = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if background water should be drawn.
        /// </summary>
        public bool DrawBackgroundWater {
            get {
                return drawBackgroundWater;
            }
            set {
                drawBackgroundWater = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if foreground water should be drawn.
        /// </summary>
        public bool DrawForegroundWater {
            get {
                return drawForegroundWater;
            }
            set {
                drawForegroundWater = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if background tiles should be drawn.
        /// </summary>
        public bool DrawBackgroundTiles {
            get {
                return drawBackgroundTiles;
            }
            set {
                drawBackgroundTiles = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if foreground tiles should be drawn.
        /// </summary>
        public bool DrawForegroundTiles {
            get {
                return drawForegroundTiles;
            }
            set {
                drawForegroundTiles = value;
            }
        }

        #endregion

        #region Members

        public static MapGeneratorLayerOptions FromString(string options) {
            MapGeneratorLayerOptions layer = new MapGeneratorLayerOptions();

            string[] optionParts = options.Split(new char[] { ',' });

            foreach (string option in optionParts) {
                string[] keyValuePair = option.Split(new char[] { '=' }, 2);
                string key = keyValuePair[0];
                string value = (keyValuePair.Length > 1) ? keyValuePair[1] : string.Empty;

                switch (key.ToLower()) {
                    case "drawbackground":
                        layer.DrawBackground = ParseBoolean(value, true);
                        break;

                    case "drawwalls":
                        layer.DrawWalls = ParseBoolean(value, true);
                        break;

                    case "drawbackgroundwater":
                        layer.DrawBackgroundWater = ParseBoolean(value, true);
                        break;

                    case "drawforegroundwater":
                        layer.DrawForegroundWater = ParseBoolean(value, true);
                        break;

                    case "drawbackgroundtiles":
                        layer.DrawBackgroundTiles = ParseBoolean(value, true);
                        break;

                    case "drawforegroundtiles":
                        layer.DrawForegroundTiles = ParseBoolean(value, true);
                        break;

                    default:
                        throw new ArgumentException("Unknown layer option: " + key, "options");
                }
            }

            return layer;
        }

        private static bool ParseBoolean(string value, bool defaultValue) {
            if (value.Length == 0) {
                return defaultValue;
            }

            switch (value.ToLower()) {
                case "true":
                case "1":
                case "yes":
                    return true;

                case "false":
                case "0":
                case "no":
                    return false;

                default:
                    throw new ArgumentException("Unknown boolean value: " + value, "value");
            }
        }

        public override string ToString() {
            List<string> flags = new List<string>();

            if (DrawBackground) {
                flags.Add("DrawBackground");
            }

            if (DrawWalls) {
                flags.Add("DrawWalls");
            }

            if (DrawBackgroundWater) {
                flags.Add("DrawBackgroundWater");
            }

            if (DrawForegroundWater) {
                flags.Add("DrawForegroundWater");
            }

            if (DrawBackgroundTiles) {
                flags.Add("DrawBackgroundTiles");
            }

            if (DrawForegroundTiles) {
                flags.Add("DrawForegroundTiles");
            }

            return string.Join(", ", flags);
        }

        #endregion
    }
}
