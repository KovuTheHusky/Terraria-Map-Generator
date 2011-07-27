using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerrariaMapTool {
    public partial class LayerPropertiesForm : Form {
        #region Properties

        public MapGeneratorLayerOptions LayerProperties {
            get {
                MapGeneratorLayerOptions options = new MapGeneratorLayerOptions();

                options.DrawBackground = listView1.Items[0].Checked;
                options.DrawWalls = listView1.Items[1].Checked;
                options.DrawBackgroundWater = listView1.Items[2].Checked;
                options.DrawForegroundWater = listView1.Items[3].Checked;
                options.DrawBackgroundTiles = listView1.Items[4].Checked;
                options.DrawForegroundTiles = listView1.Items[5].Checked;

                return options;
            }
            set {
                listView1.Items[0].Checked = value.DrawBackground;
                listView1.Items[1].Checked = value.DrawWalls;
                listView1.Items[2].Checked = value.DrawBackgroundWater;
                listView1.Items[3].Checked = value.DrawForegroundWater;
                listView1.Items[4].Checked = value.DrawBackgroundTiles;
                listView1.Items[5].Checked = value.DrawForegroundTiles;
            }
        }

        #endregion

        #region Constructors

        public LayerPropertiesForm() {
            InitializeComponent();
        }

        #endregion
    }
}
