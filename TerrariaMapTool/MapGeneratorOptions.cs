using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TerrariaMapTool {
    public class MapGeneratorOptions {
        #region Constants

        /// <summary>
        ///     Removes the prefix from an option string
        /// </summary>
        private static readonly Regex optionPrefixRemover = new Regex(@"^(\/|-|--)");

        /// <summary>
        ///     Default size of the blocks generated.
        /// </summary>
        private static readonly Size DefaultBlockSize = new Size(16, 16);

        #endregion

        #region Members

        /// <summary>
        ///     The path to the terraria folder.
        /// </summary>
        private string terrariaDirectory = string.Empty;

        /// <summary>
        ///     The path to the world file to generate the map for.
        /// </summary>
        private string worldPath = string.Empty;

        /// <summary>
        ///     The directory to write the output to.
        /// </summary>
        private string outputDirectory = string.Empty;

        /// <summary>
        ///     The size of an image block in tiles.
        /// </summary>
        private Size blockSize = DefaultBlockSize;

        /// <summary>
        ///     The list of layers to generate.
        /// </summary>
        private List<MapGeneratorLayerOptions> layers = new List<MapGeneratorLayerOptions>();

        /// <summary>
        ///     The zoom levels to generate.
        /// </summary>
        private HashSet<float> zoomLevels = new HashSet<float>();

        /// <summary>
        ///     Writes out map data information.
        /// </summary>
        private bool writeMapData = false;

        /// <summary>
        ///     Flag to turn on and off the parallelization of map generation.
        /// </summary>
        private bool parallelizeGeneration = false;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the directory to output the map images to.
        /// </summary>
        public string TerrariaDirectory {
            get {
                return terrariaDirectory;
            }
            set {
                terrariaDirectory = value;
            }
        }

        /// <summary>
        ///     Gets or sets the path to the world file.
        /// </summary>
        public string WorldPath {
            get {
                return worldPath;
            }
            set {
                worldPath = value;
            }
        }

        /// <summary>
        ///     Gets or sets the directory to output the map images to.
        /// </summary>
        public string OutputDirectory {
            get {
                return outputDirectory;
            }
            set {
                outputDirectory = value;
            }
        }

        /// <summary>
        ///     Gets or sets the size of a block in tiles.
        /// </summary>
        public Size BlockSize {
            get {
                return blockSize;
            }
            set {
                blockSize = value;
            }
        }

        /// <summary>
        ///     Gets the layers that are generated.
        /// </summary>
        public List<MapGeneratorLayerOptions> Layers {
            get {
                return layers;
            }
        }

        /// <summary>
        ///     Gets the list of zoom levels to generate.
        /// </summary>
        public HashSet<float> ZoomLevels {
            get {
                return zoomLevels;
            }
        }

        /// <summary>
        ///     Gets a flag indicating if the map data xml file should be
        ///     written.
        /// </summary>
        public bool WriteMapData {
            get {
                return writeMapData;
            }
            set {
                writeMapData = value;
            }
        }

        /// <summary>
        ///     Gets or sets a flag indicating if parallel generation should be
        ///     used.
        /// </summary>
        public bool ParallelizeGeneration {
            get {
                return parallelizeGeneration;
            }
            set {
                parallelizeGeneration = value;
            }
        }

        #endregion

        #region Members

        public static MapGeneratorOptions ParseOptions(string[] options) {
            MapGeneratorOptions mapGeneratorOptions = new MapGeneratorOptions();

            for (int i = 0; i < options.Length; ++i) {
                string option = optionPrefixRemover.Replace(options[i], "");

                switch (option.ToLower()) {
                    case "terraria":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("Terraria directory was not specified.", "options");
                        }

                        mapGeneratorOptions.TerrariaDirectory = options[++i];
                        break;

                    case "world":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("World path was not specified.", "options");
                        }

                        mapGeneratorOptions.WorldPath = options[++i];
                        break;

                    case "output":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("Output directory was not specified.", "options");
                        }

                        mapGeneratorOptions.OutputDirectory = options[++i];
                        break;

                    case "blocksize":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("Block size was not specified.", "options");
                        }

                        string blockSize = options[++i];
                        string[] components = blockSize.Split(new char[] { ',' }, 2);

                        if (components.Length != 2) {
                            throw new ArgumentException("Failed to parse block size: " + blockSize, "options");
                        }

                        mapGeneratorOptions.BlockSize = new Size(int.Parse(components[0]), int.Parse(components[1]));
                        break;

                    case "layer":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("Layer options were not specified.", "options");
                        }

                        string layerOptions = options[++i];

                        mapGeneratorOptions.Layers.Add(MapGeneratorLayerOptions.FromString(layerOptions));
                        break;

                    case "zoomlevels":
                        if (i + 1 >= options.Length) {
                            throw new ArgumentException("Zoom levels were not specified.", "options");
                        }

                        string zoomLevelsString = options[++i];
                        string[] zoomLevels = zoomLevelsString.Split(new string[] { CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string zoomLevel in zoomLevels) {
                            mapGeneratorOptions.ZoomLevels.Add(float.Parse(zoomLevel));
                        }

                        break;

                    case "mapdata":
                        mapGeneratorOptions.WriteMapData = true;
                        break;

                    case "parallel":
                        mapGeneratorOptions.ParallelizeGeneration = true;
                        break;

                    default:
                        throw new ArgumentException("Unknown option: " + option, "options");
                }
            }

            if (mapGeneratorOptions.Layers.Count == 0) {
                mapGeneratorOptions.Layers.Add(new MapGeneratorLayerOptions());
            }

            if (mapGeneratorOptions.ZoomLevels.Count == 0) {
                mapGeneratorOptions.ZoomLevels.Add(1);
            }

            return mapGeneratorOptions;
        }

        #endregion
    }
}
