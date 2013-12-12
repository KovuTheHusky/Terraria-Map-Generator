using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Terraria {
    public class Chest {
        #region Constants

        public static int MaxItems = 20;

        #endregion

        #region Members

        public Item[] Items = new Item[MaxItems];
        public Item[] ItemsMore = new Item[MaxItems];
        public Vector2 Position;

        #endregion
    }
}
