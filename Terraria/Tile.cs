using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria {
    public class Tile {
        #region Members

        public bool Active;
        public bool CheckingLiquid;
        public byte FrameNumber;
        public short FrameX;
        public short FrameY;
        public bool Lava;
        public bool Lighted;
        public byte Liquid;
        public bool SkipLiquid;
        public byte Type;
        public byte Wall;
        public byte WallFrameNumber;
        public byte WallFrameX;
        public byte WallFrameY;

        #endregion
    }
}
