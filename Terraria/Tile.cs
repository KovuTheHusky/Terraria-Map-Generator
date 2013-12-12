using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria {
    public class Tile {
        #region Members

        public bool Active;
        public bool CheckingLiquid;
        public byte Color;
        public byte FrameNumber;
        public short FrameX;
        public short FrameY;
        public bool Honey;
        public bool Lava;
        public bool Lighted;
        public byte Liquid;
        public bool SkipLiquid;
        public byte Type;
        public byte Wall;
        public byte WallColor;
        public byte WallFrameNumber;
        public byte WallFrameX;
        public byte WallFrameY;
        public bool Wire;

        public bool WireGreen;
        public bool WireBlue;
        public bool HalfTile;
        public byte TileSlope;
        public bool ActuatorPresent;
        public bool InActive;

        #endregion

        public Tile Clone() {
            return (Tile)this.MemberwiseClone();
        }
    }
}
