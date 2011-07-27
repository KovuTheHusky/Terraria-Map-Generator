using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TerrariaMapTool {
    class GraphicsDeviceService : IGraphicsDeviceService {
        public event EventHandler<EventArgs> DeviceCreated;

        public event EventHandler<EventArgs> DeviceDisposing;

        public event EventHandler<EventArgs> DeviceReset;

        public event EventHandler<EventArgs> DeviceResetting;

        private GraphicsDevice g;

        public GraphicsDevice GraphicsDevice {
            get {
                return g;
            }
        }

        public GraphicsDeviceService(GraphicsDevice g) {
            this.g = g;
        }
    }
}
