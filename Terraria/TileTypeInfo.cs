using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria {
    public class TileTypeInfo {
        #region Constants

        public static readonly TileTypeInfo HeartContainer = new TileTypeInfo(TileType.HeartContainer, "Heart Container");
        public static readonly TileTypeInfo Anvil = new TileTypeInfo(TileType.Anvil, "Anvil");
        public static readonly TileTypeInfo Forge = new TileTypeInfo(TileType.Forge, "Forge");
        public static readonly TileTypeInfo Workbench = new TileTypeInfo(TileType.Workbench, "Workbench");
        public static readonly TileTypeInfo Alter = new TileTypeInfo(TileType.Alter, "Alter");
        public static readonly TileTypeInfo ShadowOrb = new TileTypeInfo(TileType.ShadowOrb, "Shadow Orb");
        public static readonly TileTypeInfo HellForge = new TileTypeInfo(TileType.HellForge, "Hell Forge");

        #endregion

        #region Members

        private static Dictionary<TileType, TileTypeInfo> tileTypeInfos = null;

        private TileType type = (TileType) (-1);

        private string name = "";

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the type of the tile.
        /// </summary>
        public TileType Type {
            get {
                return type;
            }
        }

        /// <summary>
        ///     Gets the name of the tile.
        /// </summary>
        public string Name {
            get {
                return name;
            }
        }

        #endregion

        #region Constructors

        static TileTypeInfo() {
            tileTypeInfos = new Dictionary<TileType, TileTypeInfo>();

            tileTypeInfos.Add(HeartContainer.Type, HeartContainer);
            tileTypeInfos.Add(Anvil.Type, Anvil);
            tileTypeInfos.Add(Forge.Type, Forge);
            tileTypeInfos.Add(Workbench.Type, Workbench);
            tileTypeInfos.Add(Alter.Type, Alter);
            tileTypeInfos.Add(ShadowOrb.Type, ShadowOrb);
            tileTypeInfos.Add(HellForge.Type, HellForge);
        }

        internal TileTypeInfo(TileType type, string name) {
            this.type = type;
            this.name = name;
        }

        #endregion

        #region Members

        public static TileTypeInfo GetTileTypeInfo(TileType tileType) {
            TileTypeInfo info = null;
            tileTypeInfos.TryGetValue(tileType, out info);
            return info;
        }

        #endregion
    }
}
