using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Terraria {
    public class World {
        public static float MaxBottomWorld = 38400f;
        public static float MaxRightWorld = 134400f;
        public static int MaxTilesX = ((((int)MaxRightWorld) / 16) + 1);
        public static int MaxTilesY = ((((int)MaxBottomWorld) / 16) + 1);
        public static Random genRand = new Random();
        public static int CurrentRelease = 2;
        public static int NumTiles = 255;
        public static bool[] tileSolid = new bool[NumTiles];
        public static bool[] tileFrameImportant = new bool[NumTiles];
        public static bool[] tileStone = new bool[NumTiles];
        public static bool[] tileNoAttach = new bool[NumTiles];
        public static bool[] tileBlockLight = new bool[NumTiles];
        public static bool[] tileNoFail = new bool[NumTiles];
        public static bool[] tileSolidTop = new bool[NumTiles];
        public static bool[] tileDungeon = new bool[NumTiles];
        public static bool[] tileTable = new bool[NumTiles];
        public static bool[] tileWaterDeath = new bool[NumTiles];
        public static bool[] tileLavaDeath = new bool[NumTiles];
        public static int[] tileShine = new int[NumTiles];
        public static string[] tileName = new string[NumTiles];
        public static bool[] tileCut = new bool[NumTiles];
        public static bool[] tileAlch = new bool[NumTiles];

        public Tile[,] tile = new Tile[MaxTilesX, MaxTilesY];
        public string Name = "";
        public int worldVersion;
        public int ID = -1;
        public float leftWorld = 0f;
        public float rightWorld = 0f;
        public float topWorld = 0f;
        public float bottomWorld = 0f;

        public byte moonType;
        public int[] treeX = new int[3];
        public int[] treeStyles = new int[4];
        public int[] caveX = new int[3];
        public int[] caveBackStyles = new int[4];
        public int iceBackStyle;
        public int jungleBackStyle;
        public int hellBackStyle;

        public Size Size;
        public Point Spawn;
        public Point Dungeon;

        public double worldSurface;
        public double rockLayer;

        public bool crimsonWorld;

        public bool downedBoss1;
        public bool downedBoss2;
        public bool downedBoss3;

        public bool downedBee;
        public bool downedDestroyer;
        public bool downedTwins;
        public bool downedPrime;
        public bool downedHardmodeBoss;
        public bool downedPlantera;
        public bool downedGolem;

        public bool savedGoblinTinkerer;
        public bool savedWizard;
        public bool savedMechanic;
        public bool defeatedGoblinInvasion;
        public bool killedClown;
        public bool defeatedFrostLegion;

        public bool defeatedPirates;

        public bool shadowOrbSmashed;
        public bool spawnMeteor;
        public byte shadowOrbCount;

        public int altarsSmashed;
        public bool hardMode;

        public int invasionDelay = 0;
        public int invasionSize = 0;
        public int invasionType = 0;
        public int invasionWarn = 0;
        public double invasionX;

        public bool isRaining;
        public int rainTime;
        public float maxRain;

        public int tier1;
        public int tier2;
        public int tier3;

        public byte treeStyle;
        public byte corruptionStyle;
        public byte jungleStyle;
        public byte snowStyle;
        public byte hallowStyle;
        public byte crimsonStyle;
        public byte desertStyle;
        public byte oceanStyle;

        public int cloudBackground;
        public short numberOfClouds;
        public float windSpeed;

        public Chest[] chest = new Chest[1000];
        public Sign[] sign = new Sign[Sign.MaxSigns];
        public Npc[] npc = new Npc[1000];

        public int waterLine;

        private bool stopDrops = true;

        public static String[] itemNames = new String[603];

        static World() {
            tileShine[0x16] = 0x47e;
            tileShine[6] = 0x47e;
            tileShine[7] = 0x44c;
            tileShine[8] = 0x3e8;
            tileShine[9] = 0x41a;
            tileShine[12] = 0x3e8;
            tileShine[0x15] = 0x3e8;
            tileShine[0x3f] = 900;
            tileShine[0x40] = 900;
            tileShine[0x41] = 900;
            tileShine[0x42] = 900;
            tileShine[0x43] = 900;
            tileShine[0x44] = 900;
            tileShine[0x2d] = 0x76c;
            tileShine[0x2e] = 0x7d0;
            tileShine[0x2f] = 0x834;

            tileCut[3] = true;
            tileCut[0x18] = true;
            tileCut[0x1c] = true;
            tileCut[0x20] = true;
            tileCut[0x33] = true;
            tileCut[0x34] = true;
            tileCut[0x3d] = true;
            tileCut[0x3e] = true;
            tileCut[0x45] = true;
            tileCut[0x47] = true;
            tileCut[0x49] = true;
            tileCut[0x4a] = true;
            tileCut[0x52] = true;
            tileCut[0x53] = true;
            tileCut[0x54] = true;
            tileAlch[0x52] = true;
            tileAlch[0x53] = true;
            tileAlch[0x54] = true;

            tileSolid[0] = true;
            tileSolid[1] = true;
            tileSolid[2] = true;
            tileSolid[3] = false;
            tileSolid[4] = false;
            tileSolid[5] = false;
            tileSolid[6] = true;
            tileSolid[7] = true;
            tileSolid[8] = true;
            tileSolid[9] = true;
            tileSolid[10] = true;
            tileSolid[11] = false;
            tileSolid[0x13] = true;
            tileSolid[0x16] = true;
            tileSolid[0x17] = true;
            tileSolid[0x19] = true;
            tileSolid[30] = true;
            tileSolid[0x25] = true;
            tileSolid[0x26] = true;
            tileSolid[0x27] = true;
            tileSolid[40] = true;
            tileSolid[0x29] = true;
            tileSolid[0x2b] = true;
            tileSolid[0x2c] = true;
            tileSolid[0x2d] = true;
            tileSolid[0x2e] = true;
            tileSolid[0x2f] = true;
            tileSolid[0x30] = true;
            tileSolid[0x35] = true;
            tileSolid[54] = true;
            tileSolid[0x38] = true;
            tileSolid[0x39] = true;
            tileSolid[0x3a] = true;
            tileSolid[0x3b] = true;
            tileSolid[60] = true;
            tileSolid[0x3f] = true;
            tileSolid[0x40] = true;
            tileSolid[0x41] = true;
            tileSolid[0x42] = true;
            tileSolid[0x43] = true;
            tileSolid[0x44] = true;
            tileSolid[0x4b] = true;
            tileSolid[0x4c] = true;
            tileSolid[70] = true;

            tileBlockLight[0] = true;
            tileBlockLight[1] = true;
            tileBlockLight[2] = true;
            tileNoAttach[3] = true;
            tileNoFail[3] = true;
            tileNoAttach[4] = true;
            tileNoFail[4] = true;
            tileNoFail[0x18] = true;
            tileBlockLight[6] = true;
            tileBlockLight[7] = true;
            tileBlockLight[8] = true;
            tileBlockLight[9] = true;
            tileBlockLight[10] = true;
            tileNoAttach[10] = true;
            tileBlockLight[10] = true;
            tileSolidTop[0x13] = true;
            tileNoFail[0x20] = true;
            tileBlockLight[0x20] = true;
            tileSolid[0x25] = true;
            tileBlockLight[0x25] = true;
            tileSolid[0x26] = true;
            tileBlockLight[0x26] = true;
            tileSolid[0x27] = true;
            tileBlockLight[0x27] = true;
            tileSolid[40] = true;
            tileBlockLight[40] = true;
            tileSolid[0x29] = true;
            tileBlockLight[0x29] = true;
            tileSolid[0x2b] = true;
            tileBlockLight[0x2b] = true;
            tileSolid[0x2c] = true;
            tileBlockLight[0x2c] = true;
            tileSolid[0x2d] = true;
            tileBlockLight[0x2d] = true;
            tileSolid[0x2e] = true;
            tileBlockLight[0x2e] = true;
            tileSolid[0x2f] = true;
            tileBlockLight[0x2f] = true;
            tileSolid[0x30] = true;
            tileBlockLight[0x30] = true;
            tileSolid[0x35] = true;
            tileBlockLight[0x35] = true;
            tileSolid[54] = true;
            tileBlockLight[0x34] = true;
            tileSolid[0x38] = true;
            tileBlockLight[0x38] = true;
            tileSolid[0x39] = true;
            tileBlockLight[0x39] = true;
            tileSolid[0x3a] = true;
            tileBlockLight[0x3a] = true;
            tileSolid[0x3b] = true;
            tileBlockLight[0x3b] = true;
            tileSolid[60] = true;
            tileBlockLight[60] = true;
            tileSolid[0x3f] = true;
            tileBlockLight[0x3f] = true;
            tileStone[0x3f] = true;
            tileSolid[0x40] = true;
            tileBlockLight[0x40] = true;
            tileStone[0x40] = true;
            tileSolid[0x41] = true;
            tileBlockLight[0x41] = true;
            tileStone[0x41] = true;
            tileSolid[0x42] = true;
            tileBlockLight[0x42] = true;
            tileStone[0x42] = true;
            tileSolid[0x43] = true;
            tileBlockLight[0x43] = true;
            tileStone[0x43] = true;
            tileSolid[0x44] = true;
            tileBlockLight[0x44] = true;
            tileStone[0x44] = true;
            tileSolid[0x4b] = true;
            tileBlockLight[0x4b] = true;
            tileSolid[0x4c] = true;
            tileBlockLight[0x4c] = true;
            tileSolid[70] = true;
            tileBlockLight[70] = true;
            tileBlockLight[0x33] = true;
            tileNoFail[50] = true;
            tileNoAttach[50] = true;
            tileDungeon[0x29] = true;
            tileDungeon[0x2b] = true;
            tileDungeon[0x2c] = true;
            tileBlockLight[30] = true;
            tileBlockLight[0x19] = true;
            tileBlockLight[0x17] = true;
            tileBlockLight[0x16] = true;
            tileBlockLight[0x3e] = true;
            tileSolidTop[18] = true;
            tileSolidTop[14] = true;
            tileSolidTop[0x10] = true;
            tileNoAttach[20] = true;
            tileNoAttach[0x13] = true;
            tileNoAttach[13] = true;
            tileNoAttach[14] = true;
            tileNoAttach[15] = true;
            tileNoAttach[0x10] = true;
            tileNoAttach[0x11] = true;
            tileNoAttach[18] = true;
            tileNoAttach[0x13] = true;
            tileNoAttach[0x15] = true;
            tileNoAttach[0x1b] = true;

            tileFrameImportant[3] = true;

            tileFrameImportant[4] = true;

            tileFrameImportant[5] = true;
            tileFrameImportant[10] = true;
            tileFrameImportant[11] = true;
            tileFrameImportant[12] = true;
            tileFrameImportant[13] = true;
            tileFrameImportant[14] = true;
            tileFrameImportant[15] = true;
            tileFrameImportant[16] = true;
            tileFrameImportant[17] = true;
            tileFrameImportant[18] = true;

            tileFrameImportant[19] = true;

            tileFrameImportant[20] = true;
            tileFrameImportant[21] = true;
            tileFrameImportant[24] = true;
            tileFrameImportant[26] = true;
            tileFrameImportant[27] = true;
            tileFrameImportant[28] = true;
            tileFrameImportant[29] = true;
            tileFrameImportant[31] = true;
            tileFrameImportant[33] = true;
            tileFrameImportant[34] = true;
            tileFrameImportant[35] = true;
            tileFrameImportant[36] = true;
            tileFrameImportant[42] = true;
            tileFrameImportant[50] = true;
            tileFrameImportant[55] = true;
            tileFrameImportant[61] = true;
            tileFrameImportant[71] = true;
            tileFrameImportant[72] = true;
            tileFrameImportant[73] = true;
            tileFrameImportant[74] = true;
            tileFrameImportant[77] = true;
            tileFrameImportant[78] = true;
            tileFrameImportant[79] = true;
            tileFrameImportant[81] = true;
            tileFrameImportant[82] = true;
            tileFrameImportant[83] = true;
            tileFrameImportant[84] = true;
            tileFrameImportant[85] = true;

            tileFrameImportant[86] = true;
            tileFrameImportant[87] = true;
            tileFrameImportant[88] = true;
            tileFrameImportant[89] = true;
            tileFrameImportant[90] = true;
            tileFrameImportant[91] = true;
            tileFrameImportant[92] = true;
            tileFrameImportant[93] = true;
            tileFrameImportant[94] = true;
            tileFrameImportant[95] = true;
            tileFrameImportant[96] = true;
            tileFrameImportant[97] = true;
            tileFrameImportant[98] = true;
            tileFrameImportant[99] = true;
            tileFrameImportant[100] = true;
            tileFrameImportant[101] = true;
            tileFrameImportant[102] = true;
            tileFrameImportant[103] = true;
            tileFrameImportant[104] = true;
            tileFrameImportant[105] = true;
            tileFrameImportant[106] = true;
            tileFrameImportant[110] = true;
            tileFrameImportant[113] = true;
            tileFrameImportant[114] = true;
            tileFrameImportant[125] = true;
            tileFrameImportant[126] = true;
            tileFrameImportant[128] = true;
            tileFrameImportant[129] = true;
            tileFrameImportant[132] = true;
            tileFrameImportant[133] = true;
            tileFrameImportant[134] = true;
            tileFrameImportant[135] = true;
            tileFrameImportant[136] = true;
            tileFrameImportant[137] = true;
            tileFrameImportant[138] = true;
            tileFrameImportant[139] = true;
            tileFrameImportant[141] = true;
            tileFrameImportant[142] = true;
            tileFrameImportant[143] = true;
            tileFrameImportant[144] = true;
            tileFrameImportant[149] = true;

            tileFrameImportant[165] = true;
            tileFrameImportant[170] = true;
            tileFrameImportant[171] = true;
            tileFrameImportant[172] = true;
            tileFrameImportant[173] = true;
            tileFrameImportant[174] = true;
            tileFrameImportant[178] = true;
            tileFrameImportant[184] = true;
            tileFrameImportant[185] = true;
            tileFrameImportant[186] = true;
            tileFrameImportant[187] = true;
            tileFrameImportant[201] = true;
            tileFrameImportant[207] = true;
            tileFrameImportant[209] = true;
            tileFrameImportant[210] = true;
            tileFrameImportant[212] = true;
            tileFrameImportant[215] = true;
            tileFrameImportant[216] = true;
            tileFrameImportant[217] = true;
            tileFrameImportant[218] = true;
            tileFrameImportant[219] = true;
            tileFrameImportant[220] = true;
            tileFrameImportant[227] = true;
            tileFrameImportant[228] = true;
            tileFrameImportant[231] = true;
            tileFrameImportant[233] = true;
            tileFrameImportant[235] = true;
            tileFrameImportant[236] = true;
            tileFrameImportant[237] = true;
            tileFrameImportant[238] = true;
            tileFrameImportant[239] = true;
            tileFrameImportant[240] = true;
            tileFrameImportant[241] = true;
            tileFrameImportant[242] = true;
            tileFrameImportant[243] = true;
            tileFrameImportant[244] = true;
            tileFrameImportant[245] = true;
            tileFrameImportant[246] = true;
            tileFrameImportant[247] = true;
            tileFrameImportant[254] = true;

            tileTable[14] = true;
            tileTable[18] = true;
            tileTable[0x13] = true;

            tileWaterDeath[4] = true;
            tileWaterDeath[0x33] = true;

            tileLavaDeath[3] = true;
            tileLavaDeath[5] = true;
            tileLavaDeath[10] = true;
            tileLavaDeath[11] = true;
            tileLavaDeath[12] = true;
            tileLavaDeath[13] = true;
            tileLavaDeath[14] = true;
            tileLavaDeath[15] = true;
            tileLavaDeath[0x10] = true;
            tileLavaDeath[0x11] = true;
            tileLavaDeath[18] = true;
            tileLavaDeath[0x13] = true;
            tileLavaDeath[20] = true;
            tileLavaDeath[0x1b] = true;
            tileLavaDeath[0x1c] = true;
            tileLavaDeath[0x1d] = true;
            tileLavaDeath[0x20] = true;
            tileLavaDeath[0x21] = true;
            tileLavaDeath[0x22] = true;
            tileLavaDeath[0x23] = true;
            tileLavaDeath[36] = true;
            tileLavaDeath[0x2a] = true;
            tileLavaDeath[0x31] = true;
            tileLavaDeath[50] = true;
            tileLavaDeath[0x34] = true;
            tileLavaDeath[0x37] = true;
            tileLavaDeath[0x3d] = true;
            tileLavaDeath[0x3e] = true;
            tileLavaDeath[0x45] = true;
            tileLavaDeath[0x47] = true;
            tileLavaDeath[72] = true;
            tileLavaDeath[0x49] = true;
            tileLavaDeath[0x4a] = true;
            tileLavaDeath[0x4e] = true;
            tileLavaDeath[0x4f] = true;

            tileDungeon[0x29] = true;
            tileDungeon[0x2b] = true;
            tileDungeon[0x2c] = true;

            itemNames[1] = "Iron Pickaxe";
            itemNames[1] = "Gold Pickaxe";
            itemNames[1] = "Silver Pickaxe";
            itemNames[1] = "Copper Pickaxe";
            itemNames[2] = "Dirt Block";
            itemNames[3] = "Stone Block";
            itemNames[4] = "Iron Broadsword";
            itemNames[4] = "Gold Broadsword";
            itemNames[4] = "Silver Broadsword";
            itemNames[4] = "Copper Broadsword";
            itemNames[5] = "Mushroom";
            itemNames[6] = "Iron Shortsword";
            itemNames[6] = "Gold Shortsword";
            itemNames[6] = "Silver Shortsword";
            itemNames[6] = "Copper Shortsword";
            itemNames[7] = "Iron Hammer";
            itemNames[7] = "Gold Hammer";
            itemNames[7] = "Silver Hammer";
            itemNames[7] = "Copper Hammer";
            itemNames[8] = "Torch";
            itemNames[9] = "Wood";
            itemNames[10] = "Iron Axe";
            itemNames[10] = "Gold Axe";
            itemNames[10] = "Silver Axe";
            itemNames[10] = "Copper Axe";
            itemNames[11] = "Iron Ore";
            itemNames[12] = "Copper Ore";
            itemNames[13] = "Gold Ore";
            itemNames[14] = "Silver Ore";
            itemNames[15] = "Copper Watch";
            itemNames[16] = "Silver Watch";
            itemNames[17] = "Gold Watch";
            itemNames[18] = "Depth Meter";
            itemNames[19] = "Gold Bar";
            itemNames[20] = "Copper Bar";
            itemNames[21] = "Silver Bar";
            itemNames[22] = "Iron Bar";
            itemNames[23] = "Gel";
            itemNames[24] = "Wooden Sword";
            itemNames[25] = "Wooden Door";
            itemNames[26] = "Stone Wall";
            itemNames[27] = "Acorn";
            itemNames[28] = "Lesser Healing Potion";
            itemNames[29] = "Life Crystal";
            itemNames[30] = "Dirt Wall";
            itemNames[31] = "Bottle";
            itemNames[32] = "Wooden Table";
            itemNames[33] = "Furnace";
            itemNames[34] = "Wooden Chair";
            itemNames[35] = "Iron Anvil";
            itemNames[36] = "Work Bench";
            itemNames[37] = "Goggles";
            itemNames[38] = "Lens";
            itemNames[39] = "Wooden Bow";
            itemNames[40] = "Wooden Arrow";
            itemNames[41] = "Flaming Arrow";
            itemNames[42] = "Shuriken";
            itemNames[43] = "Suspicious Looking Eye";
            itemNames[44] = "Demon Bow";
            itemNames[45] = "War Axe of the Night";
            itemNames[46] = "Light's Bane";
            itemNames[47] = "Unholy Arrow";
            itemNames[48] = "Chest";
            itemNames[49] = "Band of Regeneration";
            itemNames[50] = "Magic Mirror";
            itemNames[51] = "Jester's Arrow";
            itemNames[52] = "Angel Statue";
            itemNames[53] = "Cloud in a Bottle";
            itemNames[54] = "Hermes Boots";
            itemNames[55] = "Enchanted Boomerang";
            itemNames[56] = "Demonite Ore";
            itemNames[57] = "Demonite Bar";
            itemNames[58] = "Heart";
            itemNames[59] = "Corrupt Seeds";
            itemNames[60] = "Vile Mushroom";
            itemNames[61] = "Ebonstone Block";
            itemNames[62] = "Grass Seeds";
            itemNames[63] = "Sunflower";
            itemNames[64] = "Vilethorn";
            itemNames[65] = "Starfury";
            itemNames[66] = "Purification Powder";
            itemNames[67] = "Vile Powder";
            itemNames[68] = "Rotten Chunk";
            itemNames[69] = "Worm Tooth";
            itemNames[70] = "Worm Food";
            itemNames[71] = "Copper Coin";
            itemNames[72] = "Silver Coin";
            itemNames[73] = "Gold Coin";
            itemNames[74] = "Platinum Coin";
            itemNames[75] = "Fallen Star";
            itemNames[76] = "Copper Greaves";
            itemNames[77] = "Iron Greaves";
            itemNames[78] = "Silver Greaves";
            itemNames[79] = "Gold Greaves";
            itemNames[80] = "Copper Chainmail";
            itemNames[81] = "Iron Chainmail";
            itemNames[82] = "Silver Chainmail";
            itemNames[83] = "Gold Chainmail";
            itemNames[84] = "Grappling Hook";
            itemNames[85] = "Iron Chain";
            itemNames[86] = "Shadow Scale";
            itemNames[87] = "Piggy Bank";
            itemNames[88] = "Mining Helmet";
            itemNames[89] = "Copper Helmet";
            itemNames[90] = "Iron Helmet";
            itemNames[91] = "Silver Helmet";
            itemNames[92] = "Gold Helmet";
            itemNames[93] = "Wood Wall";
            itemNames[94] = "Wood Platform";
            itemNames[95] = "Flintlock Pistol";
            itemNames[96] = "Musket";
            itemNames[97] = "Musket Ball";
            itemNames[98] = "Minishark";
            itemNames[99] = "Iron Bow";
            itemNames[99] = "Gold Bow";
            itemNames[99] = "Silver Bow";
            itemNames[99] = "Copper Bow";
            itemNames[100] = "Shadow Greaves";
            itemNames[101] = "Shadow Scalemail";
            itemNames[102] = "Shadow Helmet";
            itemNames[103] = "Nightmare Pickaxe";
            itemNames[104] = "The Breaker";
            itemNames[105] = "Candle";
            itemNames[106] = "Copper Chandelier";
            itemNames[107] = "Silver Chandelier";
            itemNames[108] = "Gold Chandelier";
            itemNames[109] = "Mana Crystal";
            itemNames[110] = "Lesser Mana Potion";
            itemNames[111] = "Band of Starpower";
            itemNames[112] = "Flower of Fire";
            itemNames[113] = "Magic Missile";
            itemNames[114] = "Dirt Rod";
            itemNames[115] = "Orb of Light";
            itemNames[116] = "Meteorite";
            itemNames[117] = "Meteorite Bar";
            itemNames[118] = "Hook";
            itemNames[119] = "Flamarang";
            itemNames[120] = "Molten Fury";
            itemNames[121] = "Fiery Greatsword";
            itemNames[122] = "Molten Pickaxe";
            itemNames[123] = "Meteor Helmet";
            itemNames[124] = "Meteor Suit";
            itemNames[125] = "Meteor Leggings";
            itemNames[126] = "Bottled Water";
            itemNames[127] = "Space Gun";
            itemNames[128] = "Rocket Boots";
            itemNames[129] = "Gray Brick";
            itemNames[130] = "Gray Brick Wall";
            itemNames[131] = "Red Brick";
            itemNames[132] = "Red Brick Wall";
            itemNames[133] = "Clay Block";
            itemNames[134] = "Blue Brick";
            itemNames[135] = "Blue Brick Wall";
            itemNames[136] = "Chain Lantern";
            itemNames[137] = "Green Brick";
            itemNames[138] = "Green Brick Wall";
            itemNames[139] = "Pink Brick";
            itemNames[140] = "Pink Brick Wall";
            itemNames[141] = "Gold Brick";
            itemNames[142] = "Gold Brick Wall";
            itemNames[143] = "Silver Brick";
            itemNames[144] = "Silver Brick Wall";
            itemNames[145] = "Copper Brick";
            itemNames[146] = "Copper Brick Wall";
            itemNames[147] = "Spike";
            itemNames[148] = "Water Candle";
            itemNames[149] = "Book";
            itemNames[150] = "Cobweb";
            itemNames[151] = "Necro Helmet";
            itemNames[152] = "Necro Breastplate";
            itemNames[153] = "Necro Greaves";
            itemNames[154] = "Bone";
            itemNames[155] = "Muramasa";
            itemNames[156] = "Cobalt Shield";
            itemNames[157] = "Aqua Scepter";
            itemNames[158] = "Lucky Horseshoe";
            itemNames[159] = "Shiny Red Balloon";
            itemNames[160] = "Harpoon";
            itemNames[161] = "Spiky Ball";
            itemNames[162] = "Ball 'O Hurt";
            itemNames[163] = "Blue Moon";
            itemNames[164] = "Handgun";
            itemNames[165] = "Water Bolt";
            itemNames[166] = "Bomb";
            itemNames[167] = "Dynamite";
            itemNames[168] = "Grenade";
            itemNames[169] = "Sand Block";
            itemNames[170] = "Glass";
            itemNames[171] = "Sign";
            itemNames[172] = "Ash Block";
            itemNames[173] = "Obsidian";
            itemNames[174] = "Hellstone";
            itemNames[175] = "Hellstone Bar";
            itemNames[176] = "Mud Block";
            itemNames[177] = "Sapphire";
            itemNames[178] = "Ruby";
            itemNames[179] = "Emerald";
            itemNames[180] = "Topaz";
            itemNames[181] = "Amethyst";
            itemNames[182] = "Diamond";
            itemNames[183] = "Glowing Mushroom";
            itemNames[184] = "Star";
            itemNames[185] = "Ivy Whip";
            itemNames[186] = "Breathing Reed";
            itemNames[187] = "Flipper";
            itemNames[188] = "Healing Potion";
            itemNames[189] = "Mana Potion";
            itemNames[190] = "Blade of Grass";
            itemNames[191] = "Thorn Chakram";
            itemNames[192] = "Obsidian Brick";
            itemNames[193] = "Obsidian Skull";
            itemNames[194] = "Mushroom Grass Seeds";
            itemNames[195] = "Jungle Grass Seeds";
            itemNames[196] = "Wooden Hammer";
            itemNames[197] = "Star Cannon";
            itemNames[198] = "Blue Phaseblade";
            itemNames[198] = "Blue Phasesaber";
            itemNames[199] = "Red Phaseblade";
            itemNames[199] = "Red Phasesaber";
            itemNames[200] = "Green Phaseblade";
            itemNames[200] = "Green Phasesaber";
            itemNames[201] = "Purple Phaseblade";
            itemNames[201] = "Purple Phasesaber";
            itemNames[202] = "White Phaseblade";
            itemNames[202] = "White Phasesaber";
            itemNames[203] = "Yellow Phaseblade";
            itemNames[203] = "Yellow Phasesaber";
            itemNames[204] = "Meteor Hamaxe";
            itemNames[205] = "Empty Bucket";
            itemNames[206] = "Water Bucket";
            itemNames[207] = "Lava Bucket";
            itemNames[208] = "Jungle Rose";
            itemNames[209] = "Stinger";
            itemNames[210] = "Vine";
            itemNames[211] = "Feral Claws";
            itemNames[212] = "Anklet of the Wind";
            itemNames[213] = "Staff of Regrowth";
            itemNames[214] = "Hellstone Brick";
            itemNames[215] = "Whoopie Cushion";
            itemNames[216] = "Shackle";
            itemNames[217] = "Molten Hamaxe";
            itemNames[218] = "Flamelash";
            itemNames[219] = "Phoenix Blaster";
            itemNames[220] = "Sunfury";
            itemNames[221] = "Hellforge";
            itemNames[222] = "Clay Pot";
            itemNames[223] = "Nature's Gift";
            itemNames[224] = "Bed";
            itemNames[225] = "Silk";
            itemNames[226] = "Lesser Restoration Potion";
            itemNames[227] = "Restoration Potion";
            itemNames[228] = "Jungle Hat";
            itemNames[229] = "Jungle Shirt";
            itemNames[230] = "Jungle Pants";
            itemNames[231] = "Molten Helmet";
            itemNames[232] = "Molten Breastplate";
            itemNames[233] = "Molten Greaves";
            itemNames[234] = "Meteor Shot";
            itemNames[235] = "Sticky Bomb";
            itemNames[236] = "Black Lens";
            itemNames[237] = "Sunglasses";
            itemNames[238] = "Wizard Hat";
            itemNames[239] = "Top Hat";
            itemNames[240] = "Tuxedo Shirt";
            itemNames[241] = "Tuxedo Pants";
            itemNames[242] = "Summer Hat";
            itemNames[243] = "Bunny Hood";
            itemNames[244] = "Plumber's Hat";
            itemNames[245] = "Plumber's Shirt";
            itemNames[246] = "Plumber's Pants";
            itemNames[247] = "Hero's Hat";
            itemNames[248] = "Hero's Shirt";
            itemNames[249] = "Hero's Pants";
            itemNames[250] = "Fish Bowl";
            itemNames[251] = "Archaeologist's Hat";
            itemNames[252] = "Archaeologist's Jacket";
            itemNames[253] = "Archaeologist's Pants";
            itemNames[254] = "Black Dye";
            itemNames[255] = "Green Dye";
            itemNames[256] = "Ninja Hood";
            itemNames[257] = "Ninja Shirt";
            itemNames[258] = "Ninja Pants";
            itemNames[259] = "Leather";
            itemNames[260] = "Red Hat";
            itemNames[261] = "Goldfish";
            itemNames[262] = "Robe";
            itemNames[263] = "Robot Hat";
            itemNames[264] = "Gold Crown";
            itemNames[265] = "Hellfire Arrow";
            itemNames[266] = "Sandgun";
            itemNames[267] = "Guide Voodoo Doll";
            itemNames[268] = "Diving Helmet";
            itemNames[269] = "Familiar Shirt";
            itemNames[270] = "Familiar Pants";
            itemNames[271] = "Familiar Wig";
            itemNames[272] = "Demon Scythe";
            itemNames[273] = "Night's Edge";
            itemNames[274] = "Dark Lance";
            itemNames[275] = "Coral";
            itemNames[276] = "Cactus";
            itemNames[277] = "Trident";
            itemNames[278] = "Silver Bullet";
            itemNames[279] = "Throwing Knife";
            itemNames[280] = "Spear";
            itemNames[281] = "Blowpipe";
            itemNames[282] = "Glowstick";
            itemNames[283] = "Seed";
            itemNames[284] = "Wooden Boomerang";
            itemNames[285] = "Aglet";
            itemNames[286] = "Sticky Glowstick";
            itemNames[287] = "Poisoned Knife";
            itemNames[288] = "Obsidian Skin Potion";
            itemNames[289] = "Regeneration Potion";
            itemNames[290] = "Swiftness Potion";
            itemNames[291] = "Gills Potion";
            itemNames[292] = "Ironskin Potion";
            itemNames[293] = "Mana Regeneration Potion";
            itemNames[294] = "Magic Power Potion";
            itemNames[295] = "Featherfall Potion";
            itemNames[296] = "Spelunker Potion";
            itemNames[297] = "Invisibility Potion";
            itemNames[298] = "Shine Potion";
            itemNames[299] = "Night Owl Potion";
            itemNames[300] = "Battle Potion";
            itemNames[301] = "Thorns Potion";
            itemNames[302] = "Water Walking Potion";
            itemNames[303] = "Archery Potion";
            itemNames[304] = "Hunter Potion";
            itemNames[305] = "Gravitation Potion";
            itemNames[306] = "Gold Chest";
            itemNames[307] = "Daybloom Seeds";
            itemNames[308] = "Moonglow Seeds";
            itemNames[309] = "Blinkroot Seeds";
            itemNames[310] = "Deathweed Seeds";
            itemNames[311] = "Waterleaf Seeds";
            itemNames[312] = "Fireblossom Seeds";
            itemNames[313] = "Daybloom";
            itemNames[314] = "Moonglow";
            itemNames[315] = "Blinkroot";
            itemNames[316] = "Deathweed";
            itemNames[317] = "Waterleaf";
            itemNames[318] = "Fireblossom";
            itemNames[319] = "Shark Fin";
            itemNames[320] = "Feather";
            itemNames[321] = "Tombstone";
            itemNames[322] = "Mime Mask";
            itemNames[323] = "Antlion Mandible";
            itemNames[324] = "Illegal Gun Parts";
            itemNames[325] = "The Doctor's Shirt";
            itemNames[326] = "The Doctor's Pants";
            itemNames[327] = "Golden Key";
            itemNames[328] = "Shadow Chest";
            itemNames[329] = "Shadow Key";
            itemNames[330] = "Obsidian Brick Wall";
            itemNames[331] = "Jungle Spores";
            itemNames[332] = "Loom";
            itemNames[333] = "Piano";
            itemNames[334] = "Dresser";
            itemNames[335] = "Bench";
            itemNames[336] = "Bathtub";
            itemNames[337] = "Red Banner";
            itemNames[338] = "Green Banner";
            itemNames[339] = "Blue Banner";
            itemNames[340] = "Yellow Banner";
            itemNames[341] = "Lamp Post";
            itemNames[342] = "Tiki Torch";
            itemNames[343] = "Barrel";
            itemNames[344] = "Chinese Lantern";
            itemNames[345] = "Cooking Pot";
            itemNames[346] = "Safe";
            itemNames[347] = "Skull Lantern";
            itemNames[348] = "Trash Can";
            itemNames[349] = "Candelabra";
            itemNames[350] = "Pink Vase";
            itemNames[351] = "Mug";
            itemNames[352] = "Keg";
            itemNames[353] = "Ale";
            itemNames[354] = "Bookcase";
            itemNames[355] = "Throne";
            itemNames[356] = "Bowl";
            itemNames[357] = "Bowl of Soup";
            itemNames[358] = "Toilet";
            itemNames[359] = "Grandfather Clock";
            itemNames[360] = "Armor Statue";
            itemNames[361] = "Goblin Battle Standard";
            itemNames[362] = "Tattered Cloth";
            itemNames[363] = "Sawmill";
            itemNames[364] = "Cobalt Ore";
            itemNames[365] = "Mythril Ore";
            itemNames[366] = "Adamantite Ore";
            itemNames[367] = "Pwnhammer";
            itemNames[368] = "Excalibur";
            itemNames[369] = "Hallowed Seeds";
            itemNames[370] = "Ebonsand Block";
            itemNames[371] = "Cobalt Hat";
            itemNames[372] = "Cobalt Helmet";
            itemNames[373] = "Cobalt Mask";
            itemNames[374] = "Cobalt Breastplate";
            itemNames[375] = "Cobalt Leggings";
            itemNames[376] = "Mythril Hood";
            itemNames[377] = "Mythril Helmet";
            itemNames[378] = "Mythril Hat";
            itemNames[379] = "Mythril Chainmail";
            itemNames[380] = "Mythril Greaves";
            itemNames[381] = "Cobalt Bar";
            itemNames[382] = "Mythril Bar";
            itemNames[383] = "Cobalt Chainsaw";
            itemNames[384] = "Mythril Chainsaw";
            itemNames[385] = "Cobalt Drill";
            itemNames[386] = "Mythril Drill";
            itemNames[387] = "Adamantite Chainsaw";
            itemNames[388] = "Adamantite Drill";
            itemNames[389] = "Dao of Pow";
            itemNames[390] = "Mythril Halberd";
            itemNames[391] = "Adamantite Bar";
            itemNames[392] = "Glass Wall";
            itemNames[393] = "Compass";
            itemNames[394] = "Diving Gear";
            itemNames[395] = "GPS";
            itemNames[396] = "Obsidian Horseshoe";
            itemNames[397] = "Obsidian Shield";
            itemNames[398] = "Tinkerer's Workshop";
            itemNames[399] = "Cloud in a Balloon";
            itemNames[400] = "Adamantite Headgear";
            itemNames[401] = "Adamantite Helmet";
            itemNames[402] = "Adamantite Mask";
            itemNames[403] = "Adamantite Breastplate";
            itemNames[404] = "Adamantite Leggings";
            itemNames[405] = "Spectre Boots";
            itemNames[406] = "Adamantite Glaive";
            itemNames[407] = "Toolbelt";
            itemNames[408] = "Pearlsand Block";
            itemNames[409] = "Pearlstone Block";
            itemNames[410] = "Mining Shirt";
            itemNames[411] = "Mining Pants";
            itemNames[412] = "Pearlstone Brick";
            itemNames[413] = "Iridescent Brick";
            itemNames[414] = "Mudstone Block";
            itemNames[415] = "Cobalt Brick";
            itemNames[416] = "Mythril Brick";
            itemNames[417] = "Pearlstone Brick Wall";
            itemNames[418] = "Iridescent Brick Wall";
            itemNames[419] = "Mudstone Brick Wall";
            itemNames[420] = "Cobalt Brick Wall";
            itemNames[421] = "Mythril Brick Wall";
            itemNames[422] = "Holy Water";
            itemNames[423] = "Unholy Water";
            itemNames[424] = "Silt Block";
            itemNames[425] = "Fairy Bell";
            itemNames[426] = "Breaker Blade";
            itemNames[427] = "Blue Torch";
            itemNames[428] = "Red Torch";
            itemNames[429] = "Green Torch";
            itemNames[430] = "Purple Torch";
            itemNames[431] = "White Torch";
            itemNames[432] = "Yellow Torch";
            itemNames[433] = "Demon Torch";
            itemNames[434] = "Clockwork Assault Rifle";
            itemNames[435] = "Cobalt Repeater";
            itemNames[436] = "Mythril Repeater";
            itemNames[437] = "Dual Hook";
            itemNames[438] = "Star Statue";
            itemNames[439] = "Sword Statue";
            itemNames[440] = "Slime Statue";
            itemNames[441] = "Goblin Statue";
            itemNames[442] = "Shield Statue";
            itemNames[443] = "Bat Statue";
            itemNames[444] = "Fish Statue";
            itemNames[445] = "Bunny Statue";
            itemNames[446] = "Skeleton Statue";
            itemNames[447] = "Reaper Statue";
            itemNames[448] = "Woman Statue";
            itemNames[449] = "Imp Statue";
            itemNames[450] = "Gargoyle Statue";
            itemNames[451] = "Gloom Statue";
            itemNames[452] = "Hornet Statue";
            itemNames[453] = "Bomb Statue";
            itemNames[454] = "Crab Statue";
            itemNames[455] = "Hammer Statue";
            itemNames[456] = "Potion Statue";
            itemNames[457] = "Spear Statue";
            itemNames[458] = "Cross Statue";
            itemNames[459] = "Jellyfish Statue";
            itemNames[460] = "Bow Statue";
            itemNames[461] = "Boomerang Statue";
            itemNames[462] = "Boot Statue";
            itemNames[463] = "Chest Statue";
            itemNames[464] = "Bird Statue";
            itemNames[465] = "Axe Statue";
            itemNames[466] = "Corrupt Statue";
            itemNames[467] = "Tree Statue";
            itemNames[468] = "Anvil Statue";
            itemNames[469] = "Pickaxe Statue";
            itemNames[470] = "Mushroom Statue";
            itemNames[471] = "Eyeball Statue";
            itemNames[472] = "Pillar Statue";
            itemNames[473] = "Heart Statue";
            itemNames[474] = "Pot Statue";
            itemNames[475] = "Sunflower Statue";
            itemNames[476] = "King Statue";
            itemNames[477] = "Queen Statue";
            itemNames[478] = "Piranha Statue";
            itemNames[479] = "Planked Wall";
            itemNames[480] = "Wooden Beam";
            itemNames[481] = "Adamantite Repeater";
            itemNames[482] = "Adamantite Sword";
            itemNames[483] = "Cobalt Sword";
            itemNames[484] = "Mythril Sword";
            itemNames[485] = "Moon Charm";
            itemNames[486] = "Ruler";
            itemNames[487] = "Crystal Ball";
            itemNames[488] = "Disco Ball";
            itemNames[489] = "Sorcerer Emblem";
            itemNames[490] = "Warrior Emblem";
            itemNames[491] = "Ranger Emblem";
            itemNames[492] = "Demon Wings";
            itemNames[493] = "Angel Wings";
            itemNames[494] = "Magical Harp";
            itemNames[495] = "Rainbow Rod";
            itemNames[496] = "Ice Rod";
            itemNames[497] = "Neptune's Shell";
            itemNames[498] = "Mannequin";
            itemNames[499] = "Greater Healing Potion";
            itemNames[500] = "Greater Mana Potion";
            itemNames[501] = "Pixie Dust";
            itemNames[502] = "Crystal Shard";
            itemNames[503] = "Clown Hat";
            itemNames[504] = "Clown Shirt";
            itemNames[505] = "Clown Pants";
            itemNames[506] = "Flamethrower";
            itemNames[507] = "Bell";
            itemNames[508] = "Harp";
            itemNames[509] = "Wrench";
            itemNames[510] = "Wire Cutter";
            itemNames[511] = "Active Stone Block";
            itemNames[512] = "Inactive Stone Block";
            itemNames[513] = "Lever";
            itemNames[514] = "Laser Rifle";
            itemNames[515] = "Crystal Bullet";
            itemNames[516] = "Holy Arrow";
            itemNames[517] = "Magic Dagger";
            itemNames[518] = "Crystal Storm";
            itemNames[519] = "Cursed Flames";
            itemNames[520] = "Soul of Light";
            itemNames[521] = "Soul of Night";
            itemNames[522] = "Cursed Flame";
            itemNames[523] = "Cursed Torch";
            itemNames[524] = "Adamantite Forge";
            itemNames[525] = "Mythril Anvil";
            itemNames[526] = "Unicorn Horn";
            itemNames[527] = "Dark Shard";
            itemNames[528] = "Light Shard";
            itemNames[529] = "Red Pressure Plate";
            itemNames[530] = "Wire";
            itemNames[531] = "Spell Tome";
            itemNames[532] = "Star Cloak";
            itemNames[533] = "Megashark";
            itemNames[534] = "Shotgun";
            itemNames[535] = "Philosopher's Stone";
            itemNames[536] = "Titan Glove";
            itemNames[537] = "Cobalt Naginata";
            itemNames[538] = "Switch";
            itemNames[539] = "Dart Trap";
            itemNames[540] = "Boulder";
            itemNames[541] = "Green Pressure Plate";
            itemNames[542] = "Gray Pressure Plate";
            itemNames[543] = "Brown Pressure Plate";
            itemNames[544] = "Mechanical Eye";
            itemNames[545] = "Cursed Arrow";
            itemNames[546] = "Cursed Bullet";
            itemNames[547] = "Soul of Fright";
            itemNames[548] = "Soul of Might";
            itemNames[549] = "Soul of Sight";
            itemNames[550] = "Gungnir";
            itemNames[551] = "Hallowed Plate Mail";
            itemNames[552] = "Hallowed Greaves";
            itemNames[553] = "Hallowed Helmet";
            itemNames[554] = "Cross Necklace";
            itemNames[555] = "Mana Flower";
            itemNames[556] = "Mechanical Worm";
            itemNames[557] = "Mechanical Skull";
            itemNames[558] = "Hallowed Headgear";
            itemNames[559] = "Hallowed Mask";
            itemNames[560] = "Slime Crown";
            itemNames[561] = "Light Disc";
            itemNames[562] = "Music Box (Overworld Day)";
            itemNames[563] = "Music Box (Eerie)";
            itemNames[564] = "Music Box (Night)";
            itemNames[565] = "Music Box (Title)";
            itemNames[566] = "Music Box (Underground)";
            itemNames[567] = "Music Box (Boss 1)";
            itemNames[568] = "Music Box (Jungle)";
            itemNames[569] = "Music Box (Corruption)";
            itemNames[570] = "Music Box (Underground Corruption)";
            itemNames[571] = "Music Box (The Hallow)";
            itemNames[572] = "Music Box (Boss 2)";
            itemNames[573] = "Music Box (Underground Hallow)";
            itemNames[574] = "Music Box (Boss 3)";
            itemNames[575] = "Soul of Flight";
            itemNames[576] = "Music Box";
            itemNames[577] = "Demonite Brick";
            itemNames[578] = "Hallowed Repeater";
            itemNames[579] = "Hamdrax";
            itemNames[580] = "Explosives";
            itemNames[581] = "Inlet Pump";
            itemNames[582] = "Outlet Pump";
            itemNames[583] = "1 Second Timer";
            itemNames[584] = "3 Second Timer";
            itemNames[585] = "5 Second Timer";
            itemNames[586] = "Candy Cane Block";
            itemNames[587] = "Candy Cane Wall";
            itemNames[588] = "Santa Hat";
            itemNames[589] = "Santa Shirt";
            itemNames[590] = "Santa Pants";
            itemNames[591] = "Green Candy Cane Block";
            itemNames[592] = "Green Candy Cane Wall";
            itemNames[593] = "Snow Block";
            itemNames[594] = "Snow Brick";
            itemNames[595] = "Snow Brick Wall";
            itemNames[596] = "Blue Light";
            itemNames[597] = "Red Light";
            itemNames[598] = "Green Light";
            itemNames[599] = "Blue Present";
            itemNames[600] = "Green Present";
            itemNames[601] = "Yellow Present";
            itemNames[602] = "Snow Globe";
        }

        public World(string path) {
            if (genRand == null) {
                genRand = new Random((int)DateTime.Now.Ticks);
            }

            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    worldVersion = reader.ReadInt32();
                    Console.WriteLine("World version: " + worldVersion);

                    if (worldVersion >= 28) {
                        tileFrameImportant[4] = true;
                    }

                    Name = reader.ReadString();
                    Console.WriteLine("World name: " + Name);
                    ID = reader.ReadInt32();
                    Console.WriteLine("World ID: " + ID);
                    leftWorld = reader.ReadInt32();
                    Console.WriteLine("World left: " + leftWorld);
                    rightWorld = reader.ReadInt32();
                    Console.WriteLine("World right: " + rightWorld);
                    topWorld = reader.ReadInt32();
                    Console.WriteLine("World top: " + topWorld);
                    bottomWorld = reader.ReadInt32();
                    Console.WriteLine("World bottom: " + bottomWorld);

                    int tilesY = reader.ReadInt32();
                    Console.WriteLine("Tiles Y: " + tilesY);
                    int tilesX = reader.ReadInt32();
                    Console.WriteLine("Tiles X: " + tilesX);
                    Size = new Size(tilesX, tilesY);

                    if (worldVersion >= 63) {
                        moonType = reader.ReadByte();
                        Console.WriteLine("What is the moon type? " + moonType);
                    }

                    if (worldVersion >= 44) {
                        treeX[0] = reader.ReadInt32();
                        treeX[1] = reader.ReadInt32();
                        treeX[2] = reader.ReadInt32();
                        Console.WriteLine("What are the tree type X coords? " + treeX[0] + " " + treeX[1] + " " + treeX[2]);

                        treeStyles[0] = reader.ReadInt32();
                        treeStyles[1] = reader.ReadInt32();
                        treeStyles[2] = reader.ReadInt32();
                        treeStyles[3] = reader.ReadInt32();
                        Console.WriteLine("What are the tree styles? " + treeStyles[0] + " " + treeStyles[1] + " " + treeStyles[2] + " " + treeStyles[3]);
                    }

                    if (worldVersion >= 60) {
                        caveX[0] = reader.ReadInt32();
                        caveX[1] = reader.ReadInt32();
                        caveX[2] = reader.ReadInt32();
                        Console.WriteLine("What are the cave back X coords? " + caveX[0] + " " + caveX[1] + " " + caveX[2]);

                        caveBackStyles[0] = reader.ReadInt32();
                        caveBackStyles[1] = reader.ReadInt32();
                        caveBackStyles[2] = reader.ReadInt32();
                        caveBackStyles[3] = reader.ReadInt32();
                        Console.WriteLine("What are the cave back styles? " + caveBackStyles[0] + " " + caveBackStyles[1] + " " + caveBackStyles[2] + " " + caveBackStyles[3]);

                        iceBackStyle = reader.ReadInt32();
                        Console.WriteLine("What is the ice back style? " + iceBackStyle);
                    }

                    if (worldVersion >= 61) {
                        jungleBackStyle = reader.ReadInt32();
                        Console.WriteLine("What is the jungle back style? " + jungleBackStyle);

                        hellBackStyle = reader.ReadInt32();
                        Console.WriteLine("What is the hell back style? " + hellBackStyle);
                    }

                    Spawn = new Point(reader.ReadInt32(), reader.ReadInt32());
                    Console.WriteLine("Spawn X: " + Spawn.X);
                    Console.WriteLine("Spawn Y: " + Spawn.Y);

                    worldSurface = reader.ReadDouble();
                    Console.WriteLine("World surface: " + worldSurface);
                    rockLayer = reader.ReadDouble();
                    Console.WriteLine("Rock layer: " + rockLayer);

                    double tempTime = reader.ReadDouble();
                    Console.WriteLine("Time: " + tempTime);
                    bool tempDayTime = reader.ReadBoolean();
                    Console.WriteLine("Is it day? " + tempDayTime);
                    int tempMoonPhase = reader.ReadInt32();
                    Console.WriteLine("Moon phase: " + tempMoonPhase);
                    bool tempBloodMoon = reader.ReadBoolean();
                    Console.WriteLine("Is it a blood moon? " + tempBloodMoon);

                    if (worldVersion >= 70) {
                        bool tempEclipse = reader.ReadBoolean();
                        Console.WriteLine("Is it an eclipse? " + tempEclipse);
                    }

                    Dungeon = new Point(reader.ReadInt32(), reader.ReadInt32());
                    Console.WriteLine("Dungeon X: " + Dungeon.X);
                    Console.WriteLine("Dungeon Y: " + Dungeon.Y);

                    if (worldVersion >= 56) {
                        crimsonWorld = reader.ReadBoolean();
                        Console.WriteLine("Crimson world? " + crimsonWorld);
                    }

                    downedBoss1 = reader.ReadBoolean();
                    Console.WriteLine("Is the Eye Of Cthulu dead? " + downedBoss1);
                    downedBoss2 = reader.ReadBoolean();
                    Console.WriteLine("Is the Eater Of Worlds dead? " + downedBoss2);
                    downedBoss3 = reader.ReadBoolean();
                    Console.WriteLine("Is Skeletron dead? " + downedBoss3);

                    if (worldVersion >= 66) {
                        downedBee = reader.ReadBoolean();
                        Console.WriteLine("Is the Queen Bee dead? " + downedBee);
                    }

                    if (worldVersion >= 44) {
                        downedDestroyer = reader.ReadBoolean();
                        Console.WriteLine("Is The Destroyer dead? " + downedDestroyer);
                        downedTwins = reader.ReadBoolean();
                        Console.WriteLine("Are The Twins dead? " + downedTwins);
                        downedPrime = reader.ReadBoolean();
                        Console.WriteLine("Is Skeletron Prime dead? " + downedPrime);
                        downedHardmodeBoss = reader.ReadBoolean();
                        Console.WriteLine("Is any hardmode boss dead? " + downedHardmodeBoss);
                    }

                    if (worldVersion >= 64) {
                        downedPlantera = reader.ReadBoolean();
                        Console.WriteLine("Is Plantera dead? " + downedPlantera);
                        downedGolem = reader.ReadBoolean();
                        Console.WriteLine("Is the Golem dead? " + downedGolem);
                    }

                    if (worldVersion >= 29) {
                        savedGoblinTinkerer = reader.ReadBoolean();
                        Console.WriteLine("Is the goblin tinkerer saved? " + savedGoblinTinkerer);
                        savedWizard = reader.ReadBoolean();
                        Console.WriteLine("Is the wizard saved? " + savedWizard);

                        if (worldVersion >= 34) {
                            savedMechanic = reader.ReadBoolean();
                            Console.WriteLine("Is the mechanic saved? " + savedMechanic);
                        }

                        defeatedGoblinInvasion = reader.ReadBoolean();
                        Console.WriteLine("Is the goblin invasion defeated? " + defeatedGoblinInvasion);

                        if (worldVersion >= 32) {
                            killedClown = reader.ReadBoolean();
                            Console.WriteLine("Is the clown dead? " + killedClown);
                        }

                        if (worldVersion >= 37) {
                            defeatedFrostLegion = reader.ReadBoolean();
                            Console.WriteLine("Is the frost legion defeated? " + defeatedFrostLegion);
                        }
                    }

                    if (worldVersion >= 56) {
                        defeatedPirates = reader.ReadBoolean();
                        Console.WriteLine("Are the pirates defeated? " + defeatedPirates);
                    }

                    shadowOrbSmashed = reader.ReadBoolean();
                    Console.WriteLine("Is an orb broken? " + shadowOrbSmashed);
                    spawnMeteor = reader.ReadBoolean();
                    Console.WriteLine("Is a meteor spawned? " + spawnMeteor);
                    shadowOrbCount = reader.ReadByte();
                    Console.WriteLine("Orbs broken: " + shadowOrbCount);

                    if (worldVersion >= 23) {
                        altarsSmashed = reader.ReadInt32();
                        Console.WriteLine("Altars smashed: " + altarsSmashed);
                        hardMode = reader.ReadBoolean();
                        Console.WriteLine("Is it hard mode? " + hardMode);
                    }

                    invasionDelay = reader.ReadInt32();
                    Console.WriteLine("Invasion delay: " + invasionDelay);
                    invasionSize = reader.ReadInt32();
                    Console.WriteLine("Invasion size: " + invasionSize);
                    invasionType = reader.ReadInt32();
                    Console.WriteLine("Invasion type: " + invasionType);
                    invasionX = reader.ReadDouble();
                    Console.WriteLine("Invasion X: " + invasionX);

                    if (worldVersion >= 53) {
                        isRaining = reader.ReadBoolean();
                        Console.WriteLine("Is raining? " + isRaining);
                        rainTime = reader.ReadInt32();
                        Console.WriteLine("Rain time: " + rainTime);
                        maxRain = reader.ReadSingle();
                        Console.WriteLine("Maximum rain: " + maxRain);
                    }

                    if (worldVersion >= 54) {
                        tier1 = reader.ReadInt32();
                        Console.WriteLine("What is the tier 1 ore? " + tier1);
                        tier2 = reader.ReadInt32();
                        Console.WriteLine("What is the tier 2 ore? " + tier2);
                        tier3 = reader.ReadInt32();
                        Console.WriteLine("What is the tier 3 ore? " + tier3);
                    }

                    if (worldVersion >= 55) {
                        treeStyle = reader.ReadByte();
                        Console.WriteLine("What is the tree style? " + treeStyle);
                        corruptionStyle = reader.ReadByte();
                        Console.WriteLine("What is the corruption style? " + corruptionStyle);
                        jungleStyle = reader.ReadByte();
                        Console.WriteLine("What is the jungle style? " + jungleStyle);
                    }

                    if (worldVersion >= 60) {
                        snowStyle = reader.ReadByte();
                        Console.WriteLine("What is the snow style? " + snowStyle);
                        hallowStyle = reader.ReadByte();
                        Console.WriteLine("What is the hallow style? " + hallowStyle);
                        crimsonStyle = reader.ReadByte();
                        Console.WriteLine("What is the crimson style? " + crimsonStyle);
                        desertStyle = reader.ReadByte();
                        Console.WriteLine("What is the desert style? " + desertStyle);
                        oceanStyle = reader.ReadByte();
                        Console.WriteLine("What is the ocean style? " + oceanStyle);
                        cloudBackground = reader.ReadInt32();
                        Console.WriteLine("What is the cloud background? " + cloudBackground);
                    }

                    if (worldVersion >= 62) {
                        numberOfClouds = reader.ReadInt16();
                        Console.WriteLine("Number of clouds: " + numberOfClouds);
                        windSpeed = reader.ReadSingle();
                        Console.WriteLine("Wind speed: " + windSpeed);
                    }

                    for (int num4 = 0; num4 < MaxTilesX; num4++) {
                        for (int num6 = 0; num6 < MaxTilesY; num6++) {
                            tile[num4, num6] = new Tile();
                        }
                    }

                    for (int i = 0; i < tilesX; i++) {
                        // float num3 = ((float)i) / ((float)tilesX);

                        short repeatTile = 0;
                        for (int n = 0; n < tilesY; n++) {
                            if (repeatTile > 0) {
                                tile[i, n] = tile[i, n - 1].Clone();
                                --repeatTile;
                            } else {

                                tile[i, n].Active = reader.ReadBoolean();

                                if (tile[i, n].Active) {
                                    tile[i, n].Type = reader.ReadByte();
                                    if (tileFrameImportant[tile[i, n].Type]) {
                                        if ((worldVersion < 28 && tile[i, n].Type == 4) || (worldVersion < 40 && tile[i, n].Type == 19)) {
                                            tile[i, n].FrameX = -1;
                                            tile[i, n].FrameY = -1;
                                        }
                                        else {
                                            tile[i, n].FrameX = reader.ReadInt16();
                                            tile[i, n].FrameY = reader.ReadInt16();
                                        }
                                    } else {
                                        tile[i, n].FrameX = -1;
                                        tile[i, n].FrameY = -1;
                                    }
                                    if (worldVersion >= 48) {
                                        bool colorPresent = reader.ReadBoolean();
                                        if (colorPresent) {
                                            tile[i, n].Color = reader.ReadByte();
                                        }
                                    }
                                }
                                if (worldVersion < 26) // This no longer exists after world version 25.
                                {
                                    tile[i, n].Lighted = reader.ReadBoolean();
                                }
                                if (reader.ReadBoolean()) {
                                    tile[i, n].Wall = reader.ReadByte();
                                    if (worldVersion >= 48) {
                                        bool wallColorPresent = reader.ReadBoolean();
                                        if (wallColorPresent) {
                                            tile[i, n].WallColor = reader.ReadByte();
                                        }
                                    }
                                }
                                if (reader.ReadBoolean()) {
                                    tile[i, n].Liquid = reader.ReadByte();
                                    tile[i, n].Lava = reader.ReadBoolean();
                                    if (worldVersion >= 51) {
                                        tile[i, n].Honey = reader.ReadBoolean();
                                    }
                                }
                                if (worldVersion >= 33) {
                                    tile[i, n].Wire = reader.ReadBoolean();
                                }

                                if (worldVersion >= 43) {
                                    tile[i, n].WireGreen = reader.ReadBoolean();
                                    tile[i, n].WireBlue = reader.ReadBoolean();
                                }

                                if (worldVersion >= 41) {
                                    tile[i, n].HalfTile = reader.ReadBoolean();
                                }

                                if (worldVersion >= 49) {
                                    tile[i, n].TileSlope = reader.ReadByte();
                                }

                                if (worldVersion >= 42) {
                                    tile[i, n].ActuatorPresent = reader.ReadBoolean();
                                    tile[i, n].InActive = reader.ReadBoolean();
                                }

                                if (worldVersion >= 25) {
                                    repeatTile = reader.ReadInt16();
                                    //if (repeatTile + n > tilesY) Console.WriteLine("WARNING: Repeating " + repeatTile + " times and there's only " + (tilesY - n) + " left.");
                                }
                            }
                        }
                    }

                    for (int j = 0; j < 1000; j++) {
                        if (reader.ReadBoolean()) {
                            chest[j] = new Chest();
                            chest[j].Position = new Microsoft.Xna.Framework.Vector2(reader.ReadInt32(), reader.ReadInt32());
                            for (int num6 = 0; num6 < Chest.MaxItems; num6++) {
                                byte lowByte = reader.ReadByte();
                                short num7 = lowByte;
                                if (worldVersion >= 59) {
                                    byte highByte = reader.ReadByte();
                                    num7 = (short)((highByte << 8) | lowByte);
                                }
                                if (num7 > 0) {
                                    chest[j].Items[num6] = new Item();
                                    if (worldVersion < 38) {
                                        string itemName = reader.ReadString();
                                        chest[j].Items[num6].Name = itemName;
                                    } else {
                                        int itemID = reader.ReadInt32();
                                        chest[j].Items[num6].Name = itemID.ToString(); // This belongs in a new field.
                                    }
                                    if (worldVersion >= 36) {
                                        byte itemPrefixID = reader.ReadByte();
                                        // Do we need to save this?
                                    }
                                    chest[j].Items[num6].Count = num7;
                                    //Console.WriteLine("Chest contains " + chest[j].Items[num6].Count + " of " + chest[j].Items[num6].Name + ".");
                                }
                            }
                            if (worldVersion >= 58) {
                                for (int num6 = 0; num6 < Chest.MaxItems; num6++) {
                                    byte lowByte = reader.ReadByte();
                                    short num7 = lowByte;
                                    if (worldVersion >= 59) {
                                        byte highByte = reader.ReadByte();
                                        num7 = (short)((highByte << 8) | lowByte);
                                    }
                                    if (num7 > 0) {
                                        chest[j].ItemsMore[num6] = new Item();
                                        if (worldVersion < 38) {
                                            string itemName = reader.ReadString();
                                            chest[j].ItemsMore[num6].Name = itemName;
                                        }
                                        else {
                                            int itemID = reader.ReadInt32();
                                            chest[j].ItemsMore[num6].Name = itemID.ToString(); // This belongs in a new field.
                                        }
                                        if (worldVersion >= 36) {
                                            byte itemPrefixID = reader.ReadByte();
                                            // Do we need to save this?
                                        }
                                        chest[j].ItemsMore[num6].Count = num7;
                                        //Console.WriteLine("Chest contains " + chest[j].Items[num6].Count + " of " + chest[j].Items[num6].Name + ".");
                                    }
                                }
                            }
                        }
                    }
                    for (int k = 0; k < 1000; k++) {
                        if (reader.ReadBoolean()) {
                            string str2 = reader.ReadString();
                            int num9 = reader.ReadInt32();
                            int num10 = reader.ReadInt32();
                            /*if (tile[num9, num10].Active && (tile[num9, num10].Type == 0x37)) {
                                sign[k] = new Sign();
                                sign[k].Position = new Microsoft.Xna.Framework.Vector2(num9, num10);
                                sign[k].Text = str2;
                            }*/
                        }
                    }
                    bool flag = reader.ReadBoolean();
                    for (int m = 0; flag; m++) {
                        npc[m] = new Npc();
                        npc[m].Name = reader.ReadString();
                        npc[m].Position.X = reader.ReadSingle();
                        npc[m].Position.Y = reader.ReadSingle();
                        npc[m].IsHomeless = reader.ReadBoolean();
                        npc[m].HomePosition = new Microsoft.Xna.Framework.Vector2(reader.ReadInt32(), reader.ReadInt32());
                        flag = reader.ReadBoolean();
                    }

                    if (worldVersion >= 31) {
                        String merchantName = reader.ReadString();
                        String nurseName = reader.ReadString();
                        String armsDealerName = reader.ReadString();
                        String dryadName = reader.ReadString();
                        String guideName = reader.ReadString();
                        String clothierName = reader.ReadString();
                        String demolitionistName = reader.ReadString();
                        String goblinTinkererName = reader.ReadString();
                        String wizardName = reader.ReadString();
                        if (worldVersion >= 34) {
                            String mechanicName = reader.ReadString();
                        }
                        if (worldVersion >= 65) {
                            String truffleName = reader.ReadString();
                            String steampunkerName = reader.ReadString();
                            String dyeTraderName = reader.ReadString();
                            String partyGirlName = reader.ReadString();
                            String cyborgName = reader.ReadString();
                            String painterName = reader.ReadString();
                            String witchDoctorName = reader.ReadString();
                            String pirateName = reader.ReadString();
                        }
                    }

                    if (worldVersion >= 7) {
                        Console.WriteLine("Success? " + reader.ReadBoolean());
                        String validateWorldName = reader.ReadString();
                        Console.WriteLine("World name matched? " + (Name == validateWorldName));
                        int validateWorldID = reader.ReadInt32();
                        Console.WriteLine("World ID matched? " + (ID == validateWorldID));
                    }

                    Console.WriteLine((stream.Length - stream.Position) + " bytes left.");

                    reader.Close();

                    waterLine = tilesY;
                }
            }

            FrameTiles();
        }

        private void FrameTiles() {
            for (int i = 0; i < Size.Width; i++) {
                for (int j = 0; j < Size.Height; j++) {
                    TileFrame(i, j, true, false);
                    WallFrame(i, j, true);
                }
            }
        }

        private bool mergeUp = false;
        private bool mergeDown = false;
        private bool mergeLeft = false;
        private bool mergeRight = false;
        private bool destroyObject = false;

        public void TileFrame(int i, int j, bool resetFrame = false, bool noBreak = false) {
            if ((((i >= 0) && (j >= 0)) && ((i < Size.Width) && (j < Size.Height))) && (tile[i, j] != null)) {
                //Liquid.AddWater(i, j);

                if (tile[i, j].Active && (!noBreak || !tileFrameImportant[tile[i, j].Type])) {
                    Rectangle rectangle = new Rectangle();
                    int index = -1;
                    int num2 = -1;
                    int num3 = -1;
                    int num4 = -1;
                    int num5 = -1;
                    int num6 = -1;
                    int num7 = -1;
                    int num8 = -1;
                    int type = tile[i, j].Type;
                    if (tileStone[type]) {
                        type = 1;
                    }
                    int frameX = tile[i, j].FrameX;
                    int frameY = tile[i, j].FrameY;
                    rectangle.X = -1;
                    rectangle.Y = -1;
                    if ((((type == 3) || (type == 0x18)) || ((type == 0x3d) || (type == 0x47))) || ((type == 0x49) || (type == 0x4a))) {
                        PlantCheck(i, j);
                    } else {
                        mergeUp = false;
                        mergeDown = false;
                        mergeLeft = false;
                        mergeRight = false;
                        if ((i - 1) < 0) {
                            index = type;
                            num4 = type;
                            num6 = type;
                        }
                        if ((i + 1) >= Size.Width) {
                            num3 = type;
                            num5 = type;
                            num8 = type;
                        }
                        if ((j - 1) < 0) {
                            index = type;
                            num2 = type;
                            num3 = type;
                        }
                        if ((j + 1) >= Size.Height) {
                            num6 = type;
                            num7 = type;
                            num8 = type;
                        }
                        if ((((i - 1) >= 0) && (tile[i - 1, j] != null)) && tile[i - 1, j].Active) {
                            num4 = tile[i - 1, j].Type;
                        }
                        if ((((i + 1) < Size.Width) && (tile[i + 1, j] != null)) && tile[i + 1, j].Active) {
                            num5 = tile[i + 1, j].Type;
                        }
                        if ((((j - 1) >= 0) && (tile[i, j - 1] != null)) && tile[i, j - 1].Active) {
                            num2 = tile[i, j - 1].Type;
                        }
                        if ((((j + 1) < Size.Height) && (tile[i, j + 1] != null)) && tile[i, j + 1].Active) {
                            num7 = tile[i, j + 1].Type;
                        }
                        if ((((i - 1) >= 0) && ((j - 1) >= 0)) && ((tile[i - 1, j - 1] != null) && tile[i - 1, j - 1].Active)) {
                            index = tile[i - 1, j - 1].Type;
                        }
                        if ((((i + 1) < Size.Width) && ((j - 1) >= 0)) && ((tile[i + 1, j - 1] != null) && tile[i + 1, j - 1].Active)) {
                            num3 = tile[i + 1, j - 1].Type;
                        }
                        if ((((i - 1) >= 0) && ((j + 1) < Size.Height)) && ((tile[i - 1, j + 1] != null) && tile[i - 1, j + 1].Active)) {
                            num6 = tile[i - 1, j + 1].Type;
                        }
                        if ((((i + 1) < Size.Width) && ((j + 1) < Size.Height)) && ((tile[i + 1, j + 1] != null) && tile[i + 1, j + 1].Active)) {
                            num8 = tile[i + 1, j + 1].Type;
                        }
                        if ((num4 >= 0) && tileStone[num4]) {
                            num4 = 1;
                        }
                        if ((num5 >= 0) && tileStone[num5]) {
                            num5 = 1;
                        }
                        if ((num2 >= 0) && tileStone[num2]) {
                            num2 = 1;
                        }
                        if ((num7 >= 0) && tileStone[num7]) {
                            num7 = 1;
                        }
                        if ((index >= 0) && tileStone[index]) {
                            index = 1;
                        }
                        if ((num3 >= 0) && tileStone[num3]) {
                            num3 = 1;
                        }
                        if ((num6 >= 0) && tileStone[num6]) {
                            num6 = 1;
                        }
                        if ((num8 >= 0) && tileStone[num8]) {
                            num8 = 1;
                        }
                        if (type == 4) {
                            if (((num7 >= 0) && tileSolid[num7]) && !tileNoAttach[num7]) {
                                tile[i, j].FrameX = 0;
                            } else if ((((num4 >= 0) && tileSolid[num4]) && !tileNoAttach[num4]) || (((num4 == 5) && (index == 5)) && (num6 == 5))) {
                                tile[i, j].FrameX = 0x16;
                            } else if ((((num5 >= 0) && tileSolid[num5]) && !tileNoAttach[num5]) || (((num5 == 5) && (num3 == 5)) && (num8 == 5))) {
                                tile[i, j].FrameX = 0x2c;
                            } else {
                                KillTile(i, j, false, false, false);
                            }
                        } else if (type == 80) {
                            CactusFrame(i, j);
                        } else if ((type == 12) || (type == 0x1f)) {
                            if (!destroyObject) {
                                int num12 = i;
                                int num13 = j;
                                if (tile[i, j].FrameX == 0) {
                                    num12 = i;
                                } else {
                                    num12 = i - 1;
                                }
                                if (tile[i, j].FrameY == 0) {
                                    num13 = j;
                                } else {
                                    num13 = j - 1;
                                }
                                if ((((tile[num12, num13] != null) && (tile[num12 + 1, num13] != null)) && ((tile[num12, num13 + 1] != null) && (tile[num12 + 1, num13 + 1] != null))) && (((!tile[num12, num13].Active || (tile[num12, num13].Type != type)) || (!tile[num12 + 1, num13].Active || (tile[num12 + 1, num13].Type != type))) || ((!tile[num12, num13 + 1].Active || (tile[num12, num13 + 1].Type != type)) || (!tile[num12 + 1, num13 + 1].Active || (tile[num12 + 1, num13 + 1].Type != type))))) {
                                    destroyObject = true;
                                    if (tile[num12, num13].Type == type) {
                                        KillTile(num12, num13, false, false, false);
                                    }
                                    if (tile[num12 + 1, num13].Type == type) {
                                        KillTile(num12 + 1, num13, false, false, false);
                                    }
                                    if (tile[num12, num13 + 1].Type == type) {
                                        KillTile(num12, num13 + 1, false, false, false);
                                    }
                                    if (tile[num12 + 1, num13 + 1].Type == type) {
                                        KillTile(num12 + 1, num13 + 1, false, false, false);
                                    }
                                    if (type == 12) {
                                    } else if (type == 0x1f) {
                                        if (genRand.Next(2) == 0) {
                                            spawnMeteor = true;
                                        }
                                        int num14 = genRand.Next(5);
                                        if (!shadowOrbSmashed) {
                                            num14 = 0;
                                        }
                                        shadowOrbSmashed = true;
                                        shadowOrbCount++;
                                        if (shadowOrbCount >= 3) {
                                            shadowOrbCount = 0;
                                        }
                                    }
                                    destroyObject = false;
                                }
                            }
                        } else {
                            if (type == 0x13) {
                                if ((num4 == type) && (num5 == type)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 0;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 0;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 0;
                                        rectangle.Y = 36;
                                    }
                                } else if ((num4 == type) && (num5 == -1)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 18;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 18;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 18;
                                        rectangle.Y = 36;
                                    }
                                } else if ((num4 == -1) && (num5 == type)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 36;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 36;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 36;
                                        rectangle.Y = 36;
                                    }
                                } else if ((num4 != type) && (num5 == type)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 54;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 54;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 54;
                                        rectangle.Y = 36;
                                    }
                                } else if ((num4 == type) && (num5 != type)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 72;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 72;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 72;
                                        rectangle.Y = 36;
                                    }
                                } else if (((num4 != type) && (num4 != -1)) && (num5 == -1)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 108;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 108;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 108;
                                        rectangle.Y = 36;
                                    }
                                } else if (((num4 == -1) && (num5 != type)) && (num5 != -1)) {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 126;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 126;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 126;
                                        rectangle.Y = 36;
                                    }
                                } else {
                                    if (tile[i, j].FrameNumber == 0) {
                                        rectangle.X = 90;
                                        rectangle.Y = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        rectangle.X = 90;
                                        rectangle.Y = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        rectangle.X = 90;
                                        rectangle.Y = 36;
                                    }
                                }
                            } else {
                                if (type == 10) {
                                    if (!destroyObject) {
                                        int num22 = tile[i, j].FrameY;
                                        int num23 = j;
                                        bool flag = false;
                                        switch (num22) {
                                            case 0:
                                                num23 = j;
                                                break;

                                            case 18:
                                                num23 = j - 1;
                                                break;

                                            case 36:
                                                num23 = j - 2;
                                                break;
                                        }
                                        if (tile[i, num23 - 1] == null) {
                                            tile[i, num23 - 1] = new Tile();
                                        }
                                        if (tile[i, num23 + 3] == null) {
                                            tile[i, num23 + 3] = new Tile();
                                        }
                                        if (tile[i, num23 + 2] == null) {
                                            tile[i, num23 + 2] = new Tile();
                                        }
                                        if (tile[i, num23 + 1] == null) {
                                            tile[i, num23 + 1] = new Tile();
                                        }
                                        if (tile[i, num23] == null) {
                                            tile[i, num23] = new Tile();
                                        }
                                        if (!tile[i, num23 - 1].Active || !tileSolid[tile[i, num23 - 1].Type]) {
                                            flag = true;
                                        }
                                        if (!tile[i, num23 + 3].Active || !tileSolid[tile[i, num23 + 3].Type]) {
                                            flag = true;
                                        }
                                        if (!tile[i, num23].Active || (tile[i, num23].Type != type)) {
                                            flag = true;
                                        }
                                        if (!tile[i, num23 + 1].Active || (tile[i, num23 + 1].Type != type)) {
                                            flag = true;
                                        }
                                        if (!tile[i, num23 + 2].Active || (tile[i, num23 + 2].Type != type)) {
                                            flag = true;
                                        }
                                        if (flag) {
                                            destroyObject = true;
                                            KillTile(i, num23, false, false, false);
                                            KillTile(i, num23 + 1, false, false, false);
                                            KillTile(i, num23 + 2, false, false, false);
                                        }
                                        destroyObject = false;
                                    }
                                    return;
                                }
                                if (type == 11) {
                                    if (!destroyObject) {
                                        int num24 = 0;
                                        int num25 = i;
                                        int num26 = j;
                                        int num27 = tile[i, j].FrameX;
                                        int num28 = tile[i, j].FrameY;
                                        bool flag2 = false;
                                        switch (num27) {
                                            case 0:
                                                num25 = i;
                                                num24 = 1;
                                                break;

                                            case 18:
                                                num25 = i - 1;
                                                num24 = 1;
                                                break;

                                            case 36:
                                                num25 = i + 1;
                                                num24 = -1;
                                                break;

                                            case 54:
                                                num25 = i;
                                                num24 = -1;
                                                break;
                                        }
                                        if (num28 == 0) {
                                            num26 = j;
                                        } else if (num28 == 18) {
                                            num26 = j - 1;
                                        } else if (num28 == 36) {
                                            num26 = j - 2;
                                        }
                                        if (tile[num25, num26 + 3] == null) {
                                            tile[num25, num26 + 3] = new Tile();
                                        }
                                        if (tile[num25, num26 - 1] == null) {
                                            tile[num25, num26 - 1] = new Tile();
                                        }
                                        if ((!tile[num25, num26 - 1].Active || !tileSolid[tile[num25, num26 - 1].Type]) || (!tile[num25, num26 + 3].Active || !tileSolid[tile[num25, num26 + 3].Type])) {
                                            flag2 = true;
                                            destroyObject = true;
                                        }
                                        int num29 = num25;
                                        if (num24 == -1) {
                                            num29 = num25 - 1;
                                        }
                                        for (int m = num29; m < (num29 + 2); m++) {
                                            for (int n = num26; n < (num26 + 3); n++) {
                                                if (!flag2 && ((tile[m, n].Type != 11) || !tile[m, n].Active)) {
                                                    destroyObject = true;
                                                    flag2 = true;
                                                    m = num29;
                                                    n = num26;
                                                }
                                                if (flag2) {
                                                    KillTile(m, n, false, false, false);
                                                }
                                            }
                                        }
                                        destroyObject = false;
                                    }
                                    return;
                                }
                                if (((type == 0x22) || (type == 0x23)) || (type == 36)) {
                                    Check3x3(i, j, (byte)type);
                                    return;
                                }
                                if ((type == 15) || (type == 20)) {
                                    Check1x2(i, j, (byte)type);
                                    return;
                                }
                                if (((type == 14) || (type == 0x11)) || ((type == 0x1a) || (type == 0x4d))) {
                                    Check3x2(i, j, (byte)type);
                                    return;
                                }
                                if (((type == 0x10) || (type == 18)) || (type == 0x1d)) {
                                    Check2x1(i, j, (byte)type);
                                    return;
                                }
                                if (((type == 13) || (type == 0x21)) || (((type == 0x31) || (type == 50)) || (type == 0x4e))) {
                                    CheckOnTable1x1(i, j, (byte)type);
                                    return;
                                }
                                if (type == 0x15) {
                                    CheckChest(i, j, (byte)type);
                                    return;
                                }
                                if (type == 0x1b) {
                                    CheckSunflower(i, j, 0x1b);
                                    return;
                                }
                                if (type == 0x1c) {
                                    CheckPot(i, j, 0x1c);
                                    return;
                                }
                                if (type == 0x2a) {
                                    Check1x2Top(i, j, (byte)type);
                                    return;
                                }
                                if (type == 0x37) {
                                    CheckSign(i, j, type);
                                    return;
                                }
                                if (type == 0x4f) {
                                    Check4x2(i, j, type);
                                    return;
                                }
                            }
                            if (type == 72) {
                                if ((num7 != type) && (num7 != 70)) {
                                    KillTile(i, j, false, false, false);
                                } else if ((num2 != type) && (tile[i, j].FrameX == 0)) {
                                    tile[i, j].FrameNumber = (byte)genRand.Next(3);
                                    if (tile[i, j].FrameNumber == 0) {
                                        tile[i, j].FrameX = 18;
                                        tile[i, j].FrameY = 0;
                                    }
                                    if (tile[i, j].FrameNumber == 1) {
                                        tile[i, j].FrameX = 18;
                                        tile[i, j].FrameY = 18;
                                    }
                                    if (tile[i, j].FrameNumber == 2) {
                                        tile[i, j].FrameX = 18;
                                        tile[i, j].FrameY = 36;
                                    }
                                }
                            }
                            if (type == 5) {
                                switch (num7) {
                                    case 0x17:
                                        num7 = 2;
                                        break;

                                    case 60:
                                        num7 = 2;
                                        break;
                                }
                                if (((tile[i, j].FrameX >= 0x16) && (tile[i, j].FrameX <= 0x2c)) && ((tile[i, j].FrameY >= 0x84) && (tile[i, j].FrameY <= 0xb0))) {
                                    if (((num4 != type) && (num5 != type)) || (num7 != 2)) {
                                        KillTile(i, j, false, false, false);
                                    }
                                } else if (((((tile[i, j].FrameX == 0x58) && (tile[i, j].FrameY >= 0)) && (tile[i, j].FrameY <= 0x2c)) || (((tile[i, j].FrameX == 0x42) && (tile[i, j].FrameY >= 0x42)) && (tile[i, j].FrameY <= 130))) || ((((tile[i, j].FrameX == 110) && (tile[i, j].FrameY >= 0x42)) && (tile[i, j].FrameY <= 110)) || (((tile[i, j].FrameX == 0x84) && (tile[i, j].FrameY >= 0)) && (tile[i, j].FrameY <= 0xb0)))) {
                                    if ((num4 == type) && (num5 == type)) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 0x42;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 0x58;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 110;
                                        }
                                    } else if (num4 == type) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0x16;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0x2c;
                                        }
                                    } else if (num5 == type) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 0x42;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 0x58;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 110;
                                        }
                                    } else {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x16;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x2c;
                                        }
                                    }
                                }
                                if (((tile[i, j].FrameY >= 0x84) && (tile[i, j].FrameY <= 0xb0)) && (((tile[i, j].FrameX == 0) || (tile[i, j].FrameX == 0x42)) || (tile[i, j].FrameX == 0x58))) {
                                    if (num7 != 2) {
                                        KillTile(i, j, false, false, false);
                                    }
                                    if ((num4 != type) && (num5 != type)) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x16;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x2c;
                                        }
                                    } else if (num4 != type) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x84;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0x9a;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0;
                                            tile[i, j].FrameY = 0xb0;
                                        }
                                    } else if (num5 != type) {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 0x84;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 0x9a;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0x42;
                                            tile[i, j].FrameY = 0xb0;
                                        }
                                    } else {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0x84;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0x9a;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 0x58;
                                            tile[i, j].FrameY = 0xb0;
                                        }
                                    }
                                }
                                if ((((tile[i, j].FrameX == 0x42) && (((tile[i, j].FrameY == 0) || (tile[i, j].FrameY == 0x16)) || (tile[i, j].FrameY == 0x2c))) || ((tile[i, j].FrameX == 0x58) && (((tile[i, j].FrameY == 0x42) || (tile[i, j].FrameY == 0x58)) || (tile[i, j].FrameY == 110)))) || (((tile[i, j].FrameX == 0x2c) && (((tile[i, j].FrameY == 198) || (tile[i, j].FrameY == 220)) || (tile[i, j].FrameY == 0xf2))) || ((tile[i, j].FrameX == 0x42) && (((tile[i, j].FrameY == 198) || (tile[i, j].FrameY == 220)) || (tile[i, j].FrameY == 0xf2))))) {
                                    if ((num4 != type) && (num5 != type)) {
                                        KillTile(i, j, false, false, false);
                                    }
                                } else if ((num7 == -1) || (num7 == 0x17)) {
                                    KillTile(i, j, false, false, false);
                                } else if (((num2 != type) && (tile[i, j].FrameY < 198)) && (((tile[i, j].FrameX != 0x16) && (tile[i, j].FrameX != 0x2c)) || (tile[i, j].FrameY < 0x84))) {
                                    if ((num4 == type) || (num5 == type)) {
                                        if (num7 == type) {
                                            if ((num4 == type) && (num5 == type)) {
                                                if (tile[i, j].FrameNumber == 0) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x84;
                                                }
                                                if (tile[i, j].FrameNumber == 1) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x9a;
                                                }
                                                if (tile[i, j].FrameNumber == 2) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0xb0;
                                                }
                                            } else if (num4 == type) {
                                                if (tile[i, j].FrameNumber == 0) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0;
                                                }
                                                if (tile[i, j].FrameNumber == 1) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x16;
                                                }
                                                if (tile[i, j].FrameNumber == 2) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x2c;
                                                }
                                            } else if (num5 == type) {
                                                if (tile[i, j].FrameNumber == 0) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x42;
                                                }
                                                if (tile[i, j].FrameNumber == 1) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 0x58;
                                                }
                                                if (tile[i, j].FrameNumber == 2) {
                                                    tile[i, j].FrameX = 0x84;
                                                    tile[i, j].FrameY = 110;
                                                }
                                            }
                                        } else if ((num4 == type) && (num5 == type)) {
                                            if (tile[i, j].FrameNumber == 0) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x84;
                                            }
                                            if (tile[i, j].FrameNumber == 1) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x9a;
                                            }
                                            if (tile[i, j].FrameNumber == 2) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0xb0;
                                            }
                                        } else if (num4 == type) {
                                            if (tile[i, j].FrameNumber == 0) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0;
                                            }
                                            if (tile[i, j].FrameNumber == 1) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x16;
                                            }
                                            if (tile[i, j].FrameNumber == 2) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x2c;
                                            }
                                        } else if (num5 == type) {
                                            if (tile[i, j].FrameNumber == 0) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x42;
                                            }
                                            if (tile[i, j].FrameNumber == 1) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 0x58;
                                            }
                                            if (tile[i, j].FrameNumber == 2) {
                                                tile[i, j].FrameX = 0x9a;
                                                tile[i, j].FrameY = 110;
                                            }
                                        }
                                    } else {
                                        if (tile[i, j].FrameNumber == 0) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 0;
                                        }
                                        if (tile[i, j].FrameNumber == 1) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 0x16;
                                        }
                                        if (tile[i, j].FrameNumber == 2) {
                                            tile[i, j].FrameX = 110;
                                            tile[i, j].FrameY = 0x2c;
                                        }
                                    }
                                }
                                rectangle.X = tile[i, j].FrameX;
                                rectangle.Y = tile[i, j].FrameY;
                            }
                            if (!tileFrameImportant[tile[i, j].Type]) {
                                int frameNumber = 0;
                                if (resetFrame) {
                                    frameNumber = genRand.Next(0, 3);
                                    tile[i, j].FrameNumber = (byte)frameNumber;
                                } else {
                                    frameNumber = tile[i, j].FrameNumber;
                                }
                                if (type == 0) {
                                    for (int num33 = 0; num33 < NumTiles; num33++) {
                                        switch (num33) {
                                            case 1:
                                            case 6:
                                            case 7:
                                            case 8:
                                            case 9:
                                            case 0x16:
                                            case 0x19:
                                            case 0x25:
                                            case 40:
                                            case 0x35:
                                            case 0x38:
                                                if (num2 == num33) {
                                                    TileFrame(i, j - 1, false, false);
                                                    if (mergeDown) {
                                                        num2 = type;
                                                    }
                                                }
                                                if (num7 == num33) {
                                                    TileFrame(i, j + 1, false, false);
                                                    if (mergeUp) {
                                                        num7 = type;
                                                    }
                                                }
                                                if (num4 == num33) {
                                                    TileFrame(i - 1, j, false, false);
                                                    if (mergeRight) {
                                                        num4 = type;
                                                    }
                                                }
                                                if (num5 == num33) {
                                                    TileFrame(i + 1, j, false, false);
                                                    if (mergeLeft) {
                                                        num5 = type;
                                                    }
                                                }
                                                if (index == num33) {
                                                    index = type;
                                                }
                                                if (num3 == num33) {
                                                    num3 = type;
                                                }
                                                if (num6 == num33) {
                                                    num6 = type;
                                                }
                                                if (num8 == num33) {
                                                    num8 = type;
                                                }
                                                break;
                                        }
                                    }
                                    if (num2 == 2) {
                                        num2 = type;
                                    }
                                    if (num7 == 2) {
                                        num7 = type;
                                    }
                                    if (num4 == 2) {
                                        num4 = type;
                                    }
                                    if (num5 == 2) {
                                        num5 = type;
                                    }
                                    if (index == 2) {
                                        index = type;
                                    }
                                    if (num3 == 2) {
                                        num3 = type;
                                    }
                                    if (num6 == 2) {
                                        num6 = type;
                                    }
                                    if (num8 == 2) {
                                        num8 = type;
                                    }
                                    if (num2 == 0x17) {
                                        num2 = type;
                                    }
                                    if (num7 == 0x17) {
                                        num7 = type;
                                    }
                                    if (num4 == 0x17) {
                                        num4 = type;
                                    }
                                    if (num5 == 0x17) {
                                        num5 = type;
                                    }
                                    if (index == 0x17) {
                                        index = type;
                                    }
                                    if (num3 == 0x17) {
                                        num3 = type;
                                    }
                                    if (num6 == 0x17) {
                                        num6 = type;
                                    }
                                    if (num8 == 0x17) {
                                        num8 = type;
                                    }
                                } else if (type == 0x39) {
                                    if (num2 == 0x3a) {
                                        TileFrame(i, j - 1, false, false);
                                        if (mergeDown) {
                                            num2 = type;
                                        }
                                    }
                                    if (num7 == 0x3a) {
                                        TileFrame(i, j + 1, false, false);
                                        if (mergeUp) {
                                            num7 = type;
                                        }
                                    }
                                    if (num4 == 0x3a) {
                                        TileFrame(i - 1, j, false, false);
                                        if (mergeRight) {
                                            num4 = type;
                                        }
                                    }
                                    if (num5 == 0x3a) {
                                        TileFrame(i + 1, j, false, false);
                                        if (mergeLeft) {
                                            num5 = type;
                                        }
                                    }
                                    if (index == 0x3a) {
                                        index = type;
                                    }
                                    if (num3 == 0x3a) {
                                        num3 = type;
                                    }
                                    if (num6 == 0x3a) {
                                        num6 = type;
                                    }
                                    if (num8 == 0x3a) {
                                        num8 = type;
                                    }
                                } else if (type == 0x3b) {
                                    if (num2 == 60) {
                                        num2 = type;
                                    }
                                    if (num7 == 60) {
                                        num7 = type;
                                    }
                                    if (num4 == 60) {
                                        num4 = type;
                                    }
                                    if (num5 == 60) {
                                        num5 = type;
                                    }
                                    if (index == 60) {
                                        index = type;
                                    }
                                    if (num3 == 60) {
                                        num3 = type;
                                    }
                                    if (num6 == 60) {
                                        num6 = type;
                                    }
                                    if (num8 == 60) {
                                        num8 = type;
                                    }
                                    if (num2 == 70) {
                                        num2 = type;
                                    }
                                    if (num7 == 70) {
                                        num7 = type;
                                    }
                                    if (num4 == 70) {
                                        num4 = type;
                                    }
                                    if (num5 == 70) {
                                        num5 = type;
                                    }
                                    if (index == 70) {
                                        index = type;
                                    }
                                    if (num3 == 70) {
                                        num3 = type;
                                    }
                                    if (num6 == 70) {
                                        num6 = type;
                                    }
                                    if (num8 == 70) {
                                        num8 = type;
                                    }
                                } else if (type == 1) {
                                    if (num2 == 0x3b) {
                                        TileFrame(i, j - 1, false, false);
                                        if (mergeDown) {
                                            num2 = type;
                                        }
                                    }
                                    if (num7 == 0x3b) {
                                        TileFrame(i, j + 1, false, false);
                                        if (mergeUp) {
                                            num7 = type;
                                        }
                                    }
                                    if (num4 == 0x3b) {
                                        TileFrame(i - 1, j, false, false);
                                        if (mergeRight) {
                                            num4 = type;
                                        }
                                    }
                                    if (num5 == 0x3b) {
                                        TileFrame(i + 1, j, false, false);
                                        if (mergeLeft) {
                                            num5 = type;
                                        }
                                    }
                                    if (index == 0x3b) {
                                        index = type;
                                    }
                                    if (num3 == 0x3b) {
                                        num3 = type;
                                    }
                                    if (num6 == 0x3b) {
                                        num6 = type;
                                    }
                                    if (num8 == 0x3b) {
                                        num8 = type;
                                    }
                                }
                                if ((((type == 1) || (type == 6)) || ((type == 7) || (type == 8))) || ((((type == 9) || (type == 0x16)) || ((type == 0x19) || (type == 0x25))) || (((type == 40) || (type == 0x35)) || (type == 0x38)))) {
                                    for (int num34 = 0; num34 < NumTiles; num34++) {
                                        switch (num34) {
                                            case 1:
                                            case 6:
                                            case 7:
                                            case 8:
                                            case 9:
                                            case 0x16:
                                            case 0x19:
                                            case 0x25:
                                            case 40:
                                            case 0x35:
                                            case 0x38:
                                                if (num2 == 0) {
                                                    num2 = -2;
                                                }
                                                if (num7 == 0) {
                                                    num7 = -2;
                                                }
                                                if (num4 == 0) {
                                                    num4 = -2;
                                                }
                                                if (num5 == 0) {
                                                    num5 = -2;
                                                }
                                                if (index == 0) {
                                                    index = -2;
                                                }
                                                if (num3 == 0) {
                                                    num3 = -2;
                                                }
                                                if (num6 == 0) {
                                                    num6 = -2;
                                                }
                                                if (num8 == 0) {
                                                    num8 = -2;
                                                }
                                                break;
                                        }
                                    }
                                } else if (type == 0x3a) {
                                    if (num2 == 0x39) {
                                        num2 = -2;
                                    }
                                    if (num7 == 0x39) {
                                        num7 = -2;
                                    }
                                    if (num4 == 0x39) {
                                        num4 = -2;
                                    }
                                    if (num5 == 0x39) {
                                        num5 = -2;
                                    }
                                    if (index == 0x39) {
                                        index = -2;
                                    }
                                    if (num3 == 0x39) {
                                        num3 = -2;
                                    }
                                    if (num6 == 0x39) {
                                        num6 = -2;
                                    }
                                    if (num8 == 0x39) {
                                        num8 = -2;
                                    }
                                } else if (type == 0x3b) {
                                    if (num2 == 1) {
                                        num2 = -2;
                                    }
                                    if (num7 == 1) {
                                        num7 = -2;
                                    }
                                    if (num4 == 1) {
                                        num4 = -2;
                                    }
                                    if (num5 == 1) {
                                        num5 = -2;
                                    }
                                    if (index == 1) {
                                        index = -2;
                                    }
                                    if (num3 == 1) {
                                        num3 = -2;
                                    }
                                    if (num6 == 1) {
                                        num6 = -2;
                                    }
                                    if (num8 == 1) {
                                        num8 = -2;
                                    }
                                }
                                if ((type == 0x20) && (num7 == 0x17)) {
                                    num7 = type;
                                }
                                if ((type == 0x45) && (num7 == 60)) {
                                    num7 = type;
                                }
                                if (type == 0x33) {
                                    if ((num2 > -1) && !tileNoAttach[num2]) {
                                        num2 = type;
                                    }
                                    if ((num7 > -1) && !tileNoAttach[num7]) {
                                        num7 = type;
                                    }
                                    if ((num4 > -1) && !tileNoAttach[num4]) {
                                        num4 = type;
                                    }
                                    if ((num5 > -1) && !tileNoAttach[num5]) {
                                        num5 = type;
                                    }
                                    if ((index > -1) && !tileNoAttach[index]) {
                                        index = type;
                                    }
                                    if ((num3 > -1) && !tileNoAttach[num3]) {
                                        num3 = type;
                                    }
                                    if ((num6 > -1) && !tileNoAttach[num6]) {
                                        num6 = type;
                                    }
                                    if ((num8 > -1) && !tileNoAttach[num8]) {
                                        num8 = type;
                                    }
                                }
                                if (((num2 > -1) && !tileSolid[num2]) && (num2 != type)) {
                                    num2 = -1;
                                }
                                if (((num7 > -1) && !tileSolid[num7]) && (num7 != type)) {
                                    num7 = -1;
                                }
                                if (((num4 > -1) && !tileSolid[num4]) && (num4 != type)) {
                                    num4 = -1;
                                }
                                if (((num5 > -1) && !tileSolid[num5]) && (num5 != type)) {
                                    num5 = -1;
                                }
                                if (((index > -1) && !tileSolid[index]) && (index != type)) {
                                    index = -1;
                                }
                                if (((num3 > -1) && !tileSolid[num3]) && (num3 != type)) {
                                    num3 = -1;
                                }
                                if (((num6 > -1) && !tileSolid[num6]) && (num6 != type)) {
                                    num6 = -1;
                                }
                                if (((num8 > -1) && !tileSolid[num8]) && (num8 != type)) {
                                    num8 = -1;
                                }
                                if (((type == 2) || (type == 0x17)) || ((type == 60) || (type == 70))) {
                                    int num35 = 0;
                                    if ((type == 60) || (type == 70)) {
                                        num35 = 0x3b;
                                    } else if (type == 2) {
                                        if (num2 == 0x17) {
                                            num2 = num35;
                                        }
                                        if (num7 == 0x17) {
                                            num7 = num35;
                                        }
                                        if (num4 == 0x17) {
                                            num4 = num35;
                                        }
                                        if (num5 == 0x17) {
                                            num5 = num35;
                                        }
                                        if (index == 0x17) {
                                            index = num35;
                                        }
                                        if (num3 == 0x17) {
                                            num3 = num35;
                                        }
                                        if (num6 == 0x17) {
                                            num6 = num35;
                                        }
                                        if (num8 == 0x17) {
                                            num8 = num35;
                                        }
                                    } else if (type == 0x17) {
                                        if (num2 == 2) {
                                            num2 = num35;
                                        }
                                        if (num7 == 2) {
                                            num7 = num35;
                                        }
                                        if (num4 == 2) {
                                            num4 = num35;
                                        }
                                        if (num5 == 2) {
                                            num5 = num35;
                                        }
                                        if (index == 2) {
                                            index = num35;
                                        }
                                        if (num3 == 2) {
                                            num3 = num35;
                                        }
                                        if (num6 == 2) {
                                            num6 = num35;
                                        }
                                        if (num8 == 2) {
                                            num8 = num35;
                                        }
                                    }
                                    if (((num2 != type) && (num2 != num35)) && ((num7 == type) || (num7 == num35))) {
                                        if ((num4 == num35) && (num5 == type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 198;
                                                    break;
                                            }
                                        } else if ((num4 == type) && (num5 == num35)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 198;
                                                    break;
                                            }
                                        }
                                    } else if (((num7 != type) && (num7 != num35)) && ((num2 == type) || (num2 == num35))) {
                                        if ((num4 == num35) && (num5 == type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 216;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 216;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 216;
                                                    break;
                                            }
                                        } else if ((num4 == type) && (num5 == num35)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 216;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 216;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 216;
                                                    break;
                                            }
                                        }
                                    } else if (((num4 != type) && (num4 != num35)) && ((num5 == type) || (num5 == num35))) {
                                        if ((num2 == num35) && (num7 == type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 162;
                                                    break;

                                                case 2:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if ((num7 == type) && (num5 == num2)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 126;
                                                    break;
                                            }
                                        }
                                    } else if (((num5 != type) && (num5 != num35)) && ((num4 == type) || (num4 == num35))) {
                                        if ((num2 == num35) && (num7 == type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 162;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if ((num7 == type) && (num5 == num2)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 126;
                                                    break;
                                            }
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == type))) {
                                        if (((index != type) && (num3 != type)) && ((num6 != type) && (num8 != type))) {
                                            if (num8 == num35) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 0x144;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 0x144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 144;
                                                        rectangle.Y = 0x144;
                                                        break;
                                                }
                                            } else if (num3 == num35) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 0x156;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 0x156;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 144;
                                                        rectangle.Y = 0x156;
                                                        break;
                                                }
                                            } else if (num6 == num35) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 360;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 360;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 144;
                                                        rectangle.Y = 360;
                                                        break;
                                                }
                                            } else if (index == num35) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 0x17a;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 0x17a;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 144;
                                                        rectangle.Y = 0x17a;
                                                        break;
                                                }
                                            } else {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 144;
                                                        rectangle.Y = 0xea;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 198;
                                                        rectangle.Y = 0xea;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 0xfc;
                                                        rectangle.Y = 0xea;
                                                        break;
                                                }
                                            }
                                        } else if ((index != type) && (num8 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 0x132;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 0x132;
                                                    break;

                                                case 2:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 0x132;
                                                    break;
                                            }
                                        } else if ((num3 != type) && (num6 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 0x132;
                                                    break;

                                                case 1:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 0x132;
                                                    break;

                                                case 2:
                                                    rectangle.X = 126;
                                                    rectangle.Y = 0x132;
                                                    break;
                                            }
                                        } else if (((index != type) && (num3 == type)) && ((num6 == type) && (num8 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if (((index == type) && (num3 != type)) && ((num6 == type) && (num8 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if (((index == type) && (num3 == type)) && ((num6 != type) && (num8 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        } else if (((index == type) && (num3 == type)) && ((num6 == type) && (num8 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        }
                                    } else if ((((num2 == type) && (num7 == num35)) && ((num4 == type) && (num5 == type))) && ((index == -1) && (num3 == -1))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 108;
                                                rectangle.Y = 18;
                                                break;

                                            case 1:
                                                rectangle.X = 126;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 144;
                                                rectangle.Y = 18;
                                                break;
                                        }
                                    } else if ((((num2 == num35) && (num7 == type)) && ((num4 == type) && (num5 == type))) && ((num6 == -1) && (num8 == -1))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 108;
                                                rectangle.Y = 36;
                                                break;

                                            case 1:
                                                rectangle.X = 126;
                                                rectangle.Y = 36;
                                                break;

                                            case 2:
                                                rectangle.X = 144;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if ((((num2 == type) && (num7 == type)) && ((num4 == num35) && (num5 == type))) && ((num3 == -1) && (num8 == -1))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 198;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 198;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 198;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if ((((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == num35))) && ((index == -1) && (num6 == -1))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 180;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 180;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 180;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 == num35)) && ((num4 == type) && (num5 == type))) {
                                        if (num3 == -1) {
                                            if (index != -1) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                            }
                                        } else {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        }
                                    } else if (((num2 == num35) && (num7 == type)) && ((num4 == type) && (num5 == type))) {
                                        if (num8 == -1) {
                                            if (num6 != -1) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 126;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 162;
                                                        break;
                                                }
                                            }
                                        } else {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == num35))) {
                                        if (index == -1) {
                                            if (num6 != -1) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                            }
                                        } else {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 == num35) && (num5 == type))) {
                                        if (num3 == -1) {
                                            if (num8 != -1) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                            }
                                        } else {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        }
                                    } else if (((((num2 == num35) && (num7 == type)) && ((num4 == type) && (num5 == type))) || (((num2 == type) && (num7 == num35)) && ((num4 == type) && (num5 == type)))) || ((((num2 == type) && (num7 == type)) && ((num4 == num35) && (num5 == type))) || (((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == num35))))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 18;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 54;
                                                rectangle.Y = 18;
                                                break;
                                        }
                                    }
                                    if ((((num2 == type) || (num2 == num35)) && ((num7 == type) || (num7 == num35))) && (((num4 == type) || (num4 == num35)) && ((num5 == type) || (num5 == num35)))) {
                                        if (((((index != type) && (index != num35)) && ((num3 == type) || (num3 == num35))) && ((num6 == type) || (num6 == num35))) && ((num8 == type) || (num8 == num35))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if (((((num3 != type) && (num3 != num35)) && ((index == type) || (index == num35))) && ((num6 == type) || (num6 == num35))) && ((num8 == type) || (num8 == num35))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                        } else if (((((num6 != type) && (num6 != num35)) && ((index == type) || (index == num35))) && ((num3 == type) || (num3 == num35))) && ((num8 == type) || (num8 == num35))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        } else if (((((num8 != type) && (num8 != num35)) && ((index == type) || (index == num35))) && ((num6 == type) || (num6 == num35))) && ((num3 == type) || (num3 == num35))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                        }
                                    }
                                    if ((((num2 != num35) && (num2 != type)) && ((num7 == type) && (num4 != num35))) && (((num4 != type) && (num5 == type)) && ((num8 != num35) && (num8 != type)))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 90;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 108;
                                                rectangle.Y = 270;
                                                break;

                                            case 2:
                                                rectangle.X = 126;
                                                rectangle.Y = 270;
                                                break;
                                        }
                                    } else if ((((num2 != num35) && (num2 != type)) && ((num7 == type) && (num4 == type))) && (((num5 != num35) && (num5 != type)) && ((num6 != num35) && (num6 != type)))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 144;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 162;
                                                rectangle.Y = 270;
                                                break;

                                            case 2:
                                                rectangle.X = 180;
                                                rectangle.Y = 270;
                                                break;
                                        }
                                    } else if ((((num7 != num35) && (num7 != type)) && ((num2 == type) && (num4 != num35))) && (((num4 != type) && (num5 == type)) && ((num3 != num35) && (num3 != type)))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 90;
                                                rectangle.Y = 180;
                                                break;

                                            case 1:
                                                rectangle.X = 108;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 126;
                                                rectangle.Y = 180;
                                                break;
                                        }
                                    } else if ((((num7 != num35) && (num7 != type)) && ((num2 == type) && (num4 == type))) && (((num5 != num35) && (num5 != type)) && ((index != num35) && (index != type)))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 144;
                                                rectangle.Y = 180;
                                                break;

                                            case 1:
                                                rectangle.X = 162;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 180;
                                                rectangle.Y = 180;
                                                break;
                                        }
                                    } else if (((((num2 != type) && (num2 != num35)) && ((num7 == type) && (num4 == type))) && (((num5 == type) && (num6 != type)) && ((num6 != num35) && (num8 != type)))) && (num8 != num35)) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 144;
                                                rectangle.Y = 216;
                                                break;

                                            case 1:
                                                rectangle.X = 198;
                                                rectangle.Y = 216;
                                                break;

                                            case 2:
                                                rectangle.X = 0xfc;
                                                rectangle.Y = 216;
                                                break;
                                        }
                                    } else if (((((num7 != type) && (num7 != num35)) && ((num2 == type) && (num4 == type))) && (((num5 == type) && (index != type)) && ((index != num35) && (num3 != type)))) && (num3 != num35)) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 144;
                                                rectangle.Y = 0xfc;
                                                break;

                                            case 1:
                                                rectangle.X = 198;
                                                rectangle.Y = 0xfc;
                                                break;

                                            case 2:
                                                rectangle.X = 0xfc;
                                                rectangle.Y = 0xfc;
                                                break;
                                        }
                                    } else if (((((num4 != type) && (num4 != num35)) && ((num7 == type) && (num2 == type))) && (((num5 == type) && (num3 != type)) && ((num3 != num35) && (num8 != type)))) && (num8 != num35)) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 126;
                                                rectangle.Y = 0xea;
                                                break;

                                            case 1:
                                                rectangle.X = 180;
                                                rectangle.Y = 0xea;
                                                break;

                                            case 2:
                                                rectangle.X = 0xea;
                                                rectangle.Y = 0xea;
                                                break;
                                        }
                                    } else if (((((num5 != type) && (num5 != num35)) && ((num7 == type) && (num2 == type))) && (((num4 == type) && (index != type)) && ((index != num35) && (num6 != type)))) && (num6 != num35)) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 162;
                                                rectangle.Y = 0xea;
                                                break;

                                            case 1:
                                                rectangle.X = 216;
                                                rectangle.Y = 0xea;
                                                break;

                                            case 2:
                                                rectangle.X = 270;
                                                rectangle.Y = 0xea;
                                                break;
                                        }
                                    } else if ((((num2 != num35) && (num2 != type)) && ((num7 == num35) || (num7 == type))) && ((num4 == num35) && (num5 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 36;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 54;
                                                rectangle.Y = 270;
                                                break;

                                            case 2:
                                                rectangle.X = 72;
                                                rectangle.Y = 270;
                                                break;
                                        }
                                    } else if ((((num7 != num35) && (num7 != type)) && ((num2 == num35) || (num2 == type))) && ((num4 == num35) && (num5 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                                break;

                                            case 1:
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 72;
                                                rectangle.Y = 180;
                                                break;
                                        }
                                    } else if ((((num4 != num35) && (num4 != type)) && ((num5 == num35) || (num5 == type))) && ((num2 == num35) && (num7 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 0;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 0;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 0;
                                                rectangle.Y = 0x132;
                                                break;
                                        }
                                    } else if ((((num5 != num35) && (num5 != type)) && ((num4 == num35) || (num4 == type))) && ((num2 == num35) && (num7 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 18;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 18;
                                                rectangle.Y = 0x132;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 == num35)) && ((num4 == num35) && (num5 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 198;
                                                rectangle.Y = 180;
                                                break;

                                            case 1:
                                                rectangle.X = 216;
                                                rectangle.Y = 180;
                                                break;

                                            case 2:
                                                rectangle.X = 0xea;
                                                rectangle.Y = 180;
                                                break;
                                        }
                                    } else if (((num2 == num35) && (num7 == type)) && ((num4 == num35) && (num5 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 198;
                                                rectangle.Y = 270;
                                                break;

                                            case 1:
                                                rectangle.X = 216;
                                                rectangle.Y = 270;
                                                break;

                                            case 2:
                                                rectangle.X = 0xea;
                                                rectangle.Y = 270;
                                                break;
                                        }
                                    } else if (((num2 == num35) && (num7 == num35)) && ((num4 == type) && (num5 == num35))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 198;
                                                rectangle.Y = 0x132;
                                                break;

                                            case 1:
                                                rectangle.X = 216;
                                                rectangle.Y = 0x132;
                                                break;

                                            case 2:
                                                rectangle.X = 0xea;
                                                rectangle.Y = 0x132;
                                                break;
                                        }
                                    } else if (((num2 == num35) && (num7 == num35)) && ((num4 == num35) && (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 144;
                                                rectangle.Y = 0x132;
                                                break;

                                            case 1:
                                                rectangle.X = 162;
                                                rectangle.Y = 0x132;
                                                break;

                                            case 2:
                                                rectangle.X = 180;
                                                rectangle.Y = 0x132;
                                                break;
                                        }
                                    }
                                    if ((((num2 != type) && (num2 != num35)) && ((num7 == type) && (num4 == type))) && (num5 == type)) {
                                        if (((num6 == num35) || (num6 == type)) && ((num8 != num35) && (num8 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 0x144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 0x144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 0x144;
                                                    break;
                                            }
                                        } else if (((num8 == num35) || (num8 == type)) && ((num6 != num35) && (num6 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 0x144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 0x144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 0x144;
                                                    break;
                                            }
                                        }
                                    } else if ((((num7 != type) && (num7 != num35)) && ((num2 == type) && (num4 == type))) && (num5 == type)) {
                                        if (((index == num35) || (index == type)) && ((num3 != num35) && (num3 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 0x156;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 0x156;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 0x156;
                                                    break;
                                            }
                                        } else if (((num3 == num35) || (num3 == type)) && ((index != num35) && (index != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 0x156;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 0x156;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 0x156;
                                                    break;
                                            }
                                        }
                                    } else if ((((num4 != type) && (num4 != num35)) && ((num2 == type) && (num7 == type))) && (num5 == type)) {
                                        if (((num3 == num35) || (num3 == type)) && ((num8 != num35) && (num8 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 360;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 360;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 360;
                                                    break;
                                            }
                                        } else if (((num8 == num35) || (num8 == type)) && ((num3 != num35) && (num3 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 360;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 360;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 360;
                                                    break;
                                            }
                                        }
                                    } else if ((((num5 != type) && (num5 != num35)) && ((num2 == type) && (num7 == type))) && (num4 == type)) {
                                        if (((index == num35) || (index == type)) && ((num6 != num35) && (num6 != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 0x17a;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 0x17a;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 0x17a;
                                                    break;
                                            }
                                        } else if (((num6 == num35) || (num6 == type)) && ((index != num35) && (index != type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 0x17a;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 0x17a;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 0x17a;
                                                    break;
                                            }
                                        }
                                    }
                                    if (((((num2 == type) || (num2 == num35)) && ((num7 == type) || (num7 == num35))) && (((num4 == type) || (num4 == num35)) && ((num5 == type) || (num5 == num35)))) && (((index != -1) && (num3 != -1)) && ((num6 != -1) && (num8 != -1)))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 18;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 54;
                                                rectangle.Y = 18;
                                                break;
                                        }
                                    }
                                    if (num2 == num35) {
                                        num2 = -2;
                                    }
                                    if (num7 == num35) {
                                        num7 = -2;
                                    }
                                    if (num4 == num35) {
                                        num4 = -2;
                                    }
                                    if (num5 == num35) {
                                        num5 = -2;
                                    }
                                    if (index == num35) {
                                        index = -2;
                                    }
                                    if (num3 == num35) {
                                        num3 = -2;
                                    }
                                    if (num6 == num35) {
                                        num6 = -2;
                                    }
                                    if (num8 == num35) {
                                        num8 = -2;
                                    }
                                }
                                if ((((((type == 1) || (type == 2)) || ((type == 6) || (type == 7))) || (((type == 8) || (type == 9)) || ((type == 0x16) || (type == 0x17)))) || ((((type == 0x19) || (type == 0x25)) || ((type == 40) || (type == 0x35))) || (((type == 0x38) || (type == 0x3a)) || (((type == 0x3b) || (type == 60)) || (type == 70))))) && ((rectangle.X == -1) && (rectangle.Y == -1))) {
                                    if ((num2 >= 0) && (num2 != type)) {
                                        num2 = -1;
                                    }
                                    if ((num7 >= 0) && (num7 != type)) {
                                        num7 = -1;
                                    }
                                    if ((num4 >= 0) && (num4 != type)) {
                                        num4 = -1;
                                    }
                                    if ((num5 >= 0) && (num5 != type)) {
                                        num5 = -1;
                                    }
                                    if (((num2 != -1) && (num7 != -1)) && ((num4 != -1) && (num5 != -1))) {
                                        if (((num2 == -2) && (num7 == type)) && ((num4 == type) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 108;
                                                    break;
                                            }
                                            mergeUp = true;
                                        } else if (((num2 == type) && (num7 == -2)) && ((num4 == type) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 2:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 90;
                                                    break;
                                            }
                                            mergeDown = true;
                                        } else if (((num2 == type) && (num7 == type)) && ((num4 == -2) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 1:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                            mergeLeft = true;
                                        } else if (((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 1:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                            mergeRight = true;
                                        } else if (((num2 == -2) && (num7 == type)) && ((num4 == -2) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeLeft = true;
                                        } else if (((num2 == -2) && (num7 == type)) && ((num4 == type) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeRight = true;
                                        } else if (((num2 == type) && (num7 == -2)) && ((num4 == -2) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeDown = true;
                                            mergeLeft = true;
                                        } else if (((num2 == type) && (num7 == -2)) && ((num4 == type) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 1:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeDown = true;
                                            mergeRight = true;
                                        } else if (((num2 == type) && (num7 == type)) && ((num4 == -2) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 126;
                                                    break;

                                                case 1:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 2:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 162;
                                                    break;
                                            }
                                            mergeLeft = true;
                                            mergeRight = true;
                                        } else if (((num2 == -2) && (num7 == -2)) && ((num4 == type) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 180;
                                                    break;

                                                case 1:
                                                    rectangle.X = 162;
                                                    rectangle.Y = 180;
                                                    break;

                                                case 2:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeDown = true;
                                        } else if (((num2 == -2) && (num7 == type)) && ((num4 == -2) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 126;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeLeft = true;
                                            mergeRight = true;
                                        } else if (((num2 == type) && (num7 == -2)) && ((num4 == -2) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 162;
                                                    break;

                                                case 2:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeDown = true;
                                            mergeLeft = true;
                                            mergeRight = true;
                                        } else if (((num2 == -2) && (num7 == -2)) && ((num4 == type) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 162;
                                                    break;

                                                case 2:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeDown = true;
                                            mergeRight = true;
                                        } else if (((num2 == -2) && (num7 == -2)) && ((num4 == -2) && (num5 == type))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 216;
                                                    rectangle.Y = 126;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeDown = true;
                                            mergeLeft = true;
                                        } else if (((num2 == -2) && (num7 == -2)) && ((num4 == -2) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 1:
                                                    rectangle.X = 126;
                                                    rectangle.Y = 198;
                                                    break;

                                                case 2:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 198;
                                                    break;
                                            }
                                            mergeUp = true;
                                            mergeDown = true;
                                            mergeLeft = true;
                                            mergeRight = true;
                                        } else if (((num2 == type) && (num7 == type)) && ((num4 == type) && (num5 == type))) {
                                            if (index == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                            }
                                            if (num3 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                            }
                                            if (num6 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 126;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 162;
                                                        break;
                                                }
                                            }
                                            if (num8 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 126;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 162;
                                                        break;
                                                }
                                            }
                                        }
                                    } else {
                                        if (((type != 2) && (type != 0x17)) && ((type != 60) && (type != 70))) {
                                            if (((num2 == -1) && (num7 == -2)) && ((num4 == type) && (num5 == type))) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0xea;
                                                        rectangle.Y = 0;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0xfc;
                                                        rectangle.Y = 0;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 270;
                                                        rectangle.Y = 0;
                                                        break;
                                                }
                                                mergeDown = true;
                                            } else if (((num2 == -2) && (num7 == -1)) && ((num4 == type) && (num5 == type))) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0xea;
                                                        rectangle.Y = 18;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0xfc;
                                                        rectangle.Y = 18;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 270;
                                                        rectangle.Y = 18;
                                                        break;
                                                }
                                                mergeUp = true;
                                            } else if (((num2 == type) && (num7 == type)) && ((num4 == -1) && (num5 == -2))) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0xea;
                                                        rectangle.Y = 36;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0xfc;
                                                        rectangle.Y = 36;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 270;
                                                        rectangle.Y = 36;
                                                        break;
                                                }
                                                mergeRight = true;
                                            } else if (((num2 == type) && (num7 == type)) && ((num4 == -2) && (num5 == -1))) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0xea;
                                                        rectangle.Y = 54;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 0xfc;
                                                        rectangle.Y = 54;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 270;
                                                        rectangle.Y = 54;
                                                        break;
                                                }
                                                mergeLeft = true;
                                            }
                                        }
                                        if (((num2 != -1) && (num7 != -1)) && ((num4 == -1) && (num5 == type))) {
                                            if ((num2 == -2) && (num7 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 162;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                                mergeUp = true;
                                            } else if ((num7 == -2) && (num2 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 126;
                                                        break;
                                                }
                                                mergeDown = true;
                                            }
                                        } else if (((num2 != -1) && (num7 != -1)) && ((num4 == type) && (num5 == -1))) {
                                            if ((num2 == -2) && (num7 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 162;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                                mergeUp = true;
                                            } else if ((num7 == -2) && (num2 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 126;
                                                        break;
                                                }
                                                mergeDown = true;
                                            }
                                        } else if (((num2 == -1) && (num7 == type)) && ((num4 != -1) && (num5 != -1))) {
                                            if ((num4 == -2) && (num5 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 198;
                                                        break;
                                                }
                                                mergeLeft = true;
                                            } else if ((num5 == -2) && (num4 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 198;
                                                        break;
                                                }
                                                mergeRight = true;
                                            }
                                        } else if (((num2 == type) && (num7 == -1)) && ((num4 != -1) && (num5 != -1))) {
                                            if ((num4 == -2) && (num5 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 216;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 216;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 216;
                                                        break;
                                                }
                                                mergeLeft = true;
                                            } else if ((num5 == -2) && (num4 == type)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 216;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 216;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 216;
                                                        break;
                                                }
                                                mergeRight = true;
                                            }
                                        } else if (((num2 != -1) && (num7 != -1)) && ((num4 == -1) && (num5 == -1))) {
                                            if ((num2 == -2) && (num7 == -2)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 216;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 0xea;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 108;
                                                        rectangle.Y = 0xfc;
                                                        break;
                                                }
                                                mergeUp = true;
                                                mergeDown = true;
                                            } else if (num2 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 144;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 162;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 180;
                                                        break;
                                                }
                                                mergeUp = true;
                                            } else if (num7 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 90;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 108;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 126;
                                                        rectangle.Y = 126;
                                                        break;
                                                }
                                                mergeDown = true;
                                            }
                                        } else if (((num2 == -1) && (num7 == -1)) && ((num4 != -1) && (num5 != -1))) {
                                            if ((num4 == -2) && (num5 == -2)) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 162;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 180;
                                                        rectangle.Y = 198;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 198;
                                                        rectangle.Y = 198;
                                                        break;
                                                }
                                                mergeLeft = true;
                                                mergeRight = true;
                                            } else if (num4 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 0;
                                                        rectangle.Y = 0xfc;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 18;
                                                        rectangle.Y = 0xfc;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 36;
                                                        rectangle.Y = 0xfc;
                                                        break;
                                                }
                                                mergeLeft = true;
                                            } else if (num5 == -2) {
                                                switch (frameNumber) {
                                                    case 0:
                                                        rectangle.X = 54;
                                                        rectangle.Y = 0xfc;
                                                        break;

                                                    case 1:
                                                        rectangle.X = 72;
                                                        rectangle.Y = 0xfc;
                                                        break;

                                                    case 2:
                                                        rectangle.X = 90;
                                                        rectangle.Y = 0xfc;
                                                        break;
                                                }
                                                mergeRight = true;
                                            }
                                        } else if (((num2 == -2) && (num7 == -1)) && ((num4 == -1) && (num5 == -1))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 144;
                                                    break;

                                                case 1:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 162;
                                                    break;

                                                case 2:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 180;
                                                    break;
                                            }
                                            mergeUp = true;
                                        } else if (((num2 == -1) && (num7 == -2)) && ((num4 == -1) && (num5 == -1))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 90;
                                                    break;

                                                case 1:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 108;
                                                    break;

                                                case 2:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 126;
                                                    break;
                                            }
                                            mergeDown = true;
                                        } else if (((num2 == -1) && (num7 == -1)) && ((num4 == -2) && (num5 == -1))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 0;
                                                    rectangle.Y = 0xea;
                                                    break;

                                                case 1:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 0xea;
                                                    break;

                                                case 2:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 0xea;
                                                    break;
                                            }
                                            mergeLeft = true;
                                        } else if (((num2 == -1) && (num7 == -1)) && ((num4 == -1) && (num5 == -2))) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 0xea;
                                                    break;

                                                case 1:
                                                    rectangle.X = 72;
                                                    rectangle.Y = 0xea;
                                                    break;

                                                case 2:
                                                    rectangle.X = 90;
                                                    rectangle.Y = 0xea;
                                                    break;
                                            }
                                            mergeRight = true;
                                        }
                                    }
                                }
                                if ((rectangle.X < 0) || (rectangle.Y < 0)) {
                                    if (((type == 2) || (type == 0x17)) || ((type == 60) || (type == 70))) {
                                        if (num2 == -2) {
                                            num2 = type;
                                        }
                                        if (num7 == -2) {
                                            num7 = type;
                                        }
                                        if (num4 == -2) {
                                            num4 = type;
                                        }
                                        if (num5 == -2) {
                                            num5 = type;
                                        }
                                        if (index == -2) {
                                            index = type;
                                        }
                                        if (num3 == -2) {
                                            num3 = type;
                                        }
                                        if (num6 == -2) {
                                            num6 = type;
                                        }
                                        if (num8 == -2) {
                                            num8 = type;
                                        }
                                    }
                                    if (((num2 == type) && (num7 == type)) && ((num4 == type) & (num5 == type))) {
                                        if ((index != type) && (num3 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 1:
                                                    rectangle.X = 126;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 2:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 18;
                                                    break;
                                            }
                                        } else if ((num6 != type) && (num8 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 108;
                                                    rectangle.Y = 36;
                                                    break;

                                                case 1:
                                                    rectangle.X = 126;
                                                    rectangle.Y = 36;
                                                    break;

                                                case 2:
                                                    rectangle.X = 144;
                                                    rectangle.Y = 36;
                                                    break;
                                            }
                                        } else if ((index != type) && (num6 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 0;
                                                    break;

                                                case 1:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 2:
                                                    rectangle.X = 180;
                                                    rectangle.Y = 36;
                                                    break;
                                            }
                                        } else if ((num3 != type) && (num8 != type)) {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 0;
                                                    break;

                                                case 1:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 2:
                                                    rectangle.X = 198;
                                                    rectangle.Y = 36;
                                                    break;
                                            }
                                        } else {
                                            switch (frameNumber) {
                                                case 0:
                                                    rectangle.X = 18;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 1:
                                                    rectangle.X = 36;
                                                    rectangle.Y = 18;
                                                    break;

                                                case 2:
                                                    rectangle.X = 54;
                                                    rectangle.Y = 18;
                                                    break;
                                            }
                                        }
                                    } else if (((num2 != type) && (num7 == type)) && ((num4 == type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 0;
                                                break;

                                            case 2:
                                                rectangle.X = 54;
                                                rectangle.Y = 0;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 != type)) && ((num4 == type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 36;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 36;
                                                break;

                                            case 2:
                                                rectangle.X = 54;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 != type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 0;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 0;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 0;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 == type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 72;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 72;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 72;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 == type)) && ((num4 != type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 0;
                                                rectangle.Y = 54;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 54;
                                                break;

                                            case 2:
                                                rectangle.X = 72;
                                                rectangle.Y = 54;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 == type)) && ((num4 == type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 54;
                                                break;

                                            case 1:
                                                rectangle.X = 54;
                                                rectangle.Y = 54;
                                                break;

                                            case 2:
                                                rectangle.X = 90;
                                                rectangle.Y = 54;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 != type)) && ((num4 != type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 0;
                                                rectangle.Y = 72;
                                                break;

                                            case 1:
                                                rectangle.X = 36;
                                                rectangle.Y = 72;
                                                break;

                                            case 2:
                                                rectangle.X = 72;
                                                rectangle.Y = 72;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 != type)) && ((num4 == type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 18;
                                                rectangle.Y = 72;
                                                break;

                                            case 1:
                                                rectangle.X = 54;
                                                rectangle.Y = 72;
                                                break;

                                            case 2:
                                                rectangle.X = 90;
                                                rectangle.Y = 72;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 == type)) && ((num4 != type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 90;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 90;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 90;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 != type)) && ((num4 == type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 108;
                                                rectangle.Y = 72;
                                                break;

                                            case 1:
                                                rectangle.X = 126;
                                                rectangle.Y = 72;
                                                break;

                                            case 2:
                                                rectangle.X = 144;
                                                rectangle.Y = 72;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 == type)) && ((num4 != type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 108;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 126;
                                                rectangle.Y = 0;
                                                break;

                                            case 2:
                                                rectangle.X = 144;
                                                rectangle.Y = 0;
                                                break;
                                        }
                                    } else if (((num2 == type) && (num7 != type)) && ((num4 != type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 108;
                                                rectangle.Y = 54;
                                                break;

                                            case 1:
                                                rectangle.X = 126;
                                                rectangle.Y = 54;
                                                break;

                                            case 2:
                                                rectangle.X = 144;
                                                rectangle.Y = 54;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 != type)) && ((num4 != type) & (num5 == type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 162;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 162;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 162;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 != type)) && ((num4 == type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 216;
                                                rectangle.Y = 0;
                                                break;

                                            case 1:
                                                rectangle.X = 216;
                                                rectangle.Y = 18;
                                                break;

                                            case 2:
                                                rectangle.X = 216;
                                                rectangle.Y = 36;
                                                break;
                                        }
                                    } else if (((num2 != type) && (num7 != type)) && ((num4 != type) & (num5 != type))) {
                                        switch (frameNumber) {
                                            case 0:
                                                rectangle.X = 162;
                                                rectangle.Y = 54;
                                                break;

                                            case 1:
                                                rectangle.X = 180;
                                                rectangle.Y = 54;
                                                break;

                                            case 2:
                                                rectangle.X = 198;
                                                rectangle.Y = 54;
                                                break;
                                        }
                                    }
                                }
                                if ((rectangle.X <= -1) || (rectangle.Y <= -1)) {
                                    if (frameNumber <= 0) {
                                        rectangle.X = 18;
                                        rectangle.Y = 18;
                                    }
                                    if (frameNumber == 1) {
                                        rectangle.X = 36;
                                        rectangle.Y = 18;
                                    }
                                    if (frameNumber >= 2) {
                                        rectangle.X = 54;
                                        rectangle.Y = 18;
                                    }
                                }
                                tile[i, j].FrameX = (short)rectangle.X;
                                tile[i, j].FrameY = (short)rectangle.Y;
                                if ((type == 0x34) || (type == 0x3e)) {
                                    if (tile[i, j - 1] != null) {
                                        if (!tile[i, j - 1].Active) {
                                            num2 = -1;
                                        } else {
                                            num2 = tile[i, j - 1].Type;
                                        }
                                    } else {
                                        num2 = type;
                                    }
                                    if (((num2 != type) && (num2 != 2)) && (num2 != 60)) {
                                        KillTile(i, j, false, false, false);
                                    }
                                }
                                if (type == 0x35) {
                                }
                                if (((rectangle.X != frameX) && (rectangle.Y != frameY)) && ((frameX >= 0) && (frameY >= 0))) {
                                    bool oldMergeUp = mergeUp;
                                    bool oldMergeDown = mergeDown;
                                    bool oldMergeLeft = mergeLeft;
                                    bool oldMergeRight = mergeRight;
                                    TileFrame(i - 1, j, false, false);
                                    TileFrame(i + 1, j, false, false);
                                    TileFrame(i, j - 1, false, false);
                                    TileFrame(i, j + 1, false, false);
                                    mergeUp = oldMergeUp;
                                    mergeDown = oldMergeDown;
                                    mergeLeft = oldMergeLeft;
                                    mergeRight = oldMergeRight;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void WallFrame(int i, int j, bool resetFrame = false) {
            if ((((i >= 0) && (j >= 0)) && ((i < Size.Width) && (j < Size.Height))) && ((tile[i, j] != null) && (tile[i, j].Wall > 0))) {
                int leftTopWallTile = -1;
                int topWallTile = -1;
                int rightTopWallTile = -1;
                int leftWallTile = -1;
                int rightWallTile = -1;
                int leftBottomWallTile = -1;
                int bottomWallTile = -1;
                int rightBottomWallTile = -1;
                int wallTile = tile[i, j].Wall;

                if (wallTile != 0) {
                    Rectangle frameRectangle = new Rectangle();
                    byte wallFrameX = tile[i, j].WallFrameX;
                    byte wallFrameY = tile[i, j].WallFrameY;
                    frameRectangle.X = -1;
                    frameRectangle.Y = -1;
                    if ((i - 1) < 0) {
                        leftTopWallTile = wallTile;
                        leftWallTile = wallTile;
                        leftBottomWallTile = wallTile;
                    }
                    if ((i + 1) >= Size.Width) {
                        rightTopWallTile = wallTile;
                        rightWallTile = wallTile;
                        rightBottomWallTile = wallTile;
                    }
                    if ((j - 1) < 0) {
                        leftTopWallTile = wallTile;
                        topWallTile = wallTile;
                        rightTopWallTile = wallTile;
                    }
                    if ((j + 1) >= Size.Height) {
                        leftBottomWallTile = wallTile;
                        bottomWallTile = wallTile;
                        rightBottomWallTile = wallTile;
                    }
                    if (((i - 1) >= 0) && (tile[i - 1, j] != null)) {
                        leftWallTile = tile[i - 1, j].Wall;
                    }
                    if (((i + 1) < Size.Width) && (tile[i + 1, j] != null)) {
                        rightWallTile = tile[i + 1, j].Wall;
                    }
                    if (((j - 1) >= 0) && (tile[i, j - 1] != null)) {
                        topWallTile = tile[i, j - 1].Wall;
                    }
                    if (((j + 1) < Size.Height) && (tile[i, j + 1] != null)) {
                        bottomWallTile = tile[i, j + 1].Wall;
                    }
                    if ((((i - 1) >= 0) && ((j - 1) >= 0)) && (tile[i - 1, j - 1] != null)) {
                        leftTopWallTile = tile[i - 1, j - 1].Wall;
                    }
                    if ((((i + 1) < Size.Width) && ((j - 1) >= 0)) && (tile[i + 1, j - 1] != null)) {
                        rightTopWallTile = tile[i + 1, j - 1].Wall;
                    }
                    if ((((i - 1) >= 0) && ((j + 1) < Size.Height)) && (tile[i - 1, j + 1] != null)) {
                        leftBottomWallTile = tile[i - 1, j + 1].Wall;
                    }
                    if ((((i + 1) < Size.Width) && ((j + 1) < Size.Height)) && (tile[i + 1, j + 1] != null)) {
                        rightBottomWallTile = tile[i + 1, j + 1].Wall;
                    }
                    if (wallTile == 2) {
                        if (j == ((int)worldSurface)) {
                            bottomWallTile = wallTile;
                            leftBottomWallTile = wallTile;
                            rightBottomWallTile = wallTile;
                        } else if (j >= ((int)worldSurface)) {
                            bottomWallTile = wallTile;
                            leftBottomWallTile = wallTile;
                            rightBottomWallTile = wallTile;
                            topWallTile = wallTile;
                            leftTopWallTile = wallTile;
                            rightTopWallTile = wallTile;
                            leftWallTile = wallTile;
                            rightWallTile = wallTile;
                        }
                    }
                    int wallFrameNumber = 0;
                    if (resetFrame) {
                        wallFrameNumber = genRand.Next(0, 3);
                        tile[i, j].WallFrameNumber = (byte)wallFrameNumber;
                    } else {
                        wallFrameNumber = tile[i, j].WallFrameNumber;
                    }
                    if ((frameRectangle.X < 0) || (frameRectangle.Y < 0)) {
                        if (((topWallTile == wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile == wallTile) & (rightWallTile == wallTile))) {
                            if ((leftTopWallTile != wallTile) && (rightTopWallTile != wallTile)) {
                                switch (wallFrameNumber) {
                                    case 0:
                                        frameRectangle.X = 108;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 1:
                                        frameRectangle.X = 126;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 2:
                                        frameRectangle.X = 144;
                                        frameRectangle.Y = 18;
                                        break;
                                }
                            } else if ((leftBottomWallTile != wallTile) && (rightBottomWallTile != wallTile)) {
                                switch (wallFrameNumber) {
                                    case 0:
                                        frameRectangle.X = 108;
                                        frameRectangle.Y = 36;
                                        break;

                                    case 1:
                                        frameRectangle.X = 126;
                                        frameRectangle.Y = 36;
                                        break;

                                    case 2:
                                        frameRectangle.X = 144;
                                        frameRectangle.Y = 36;
                                        break;
                                }
                            } else if ((leftTopWallTile != wallTile) && (leftBottomWallTile != wallTile)) {
                                switch (wallFrameNumber) {
                                    case 0:
                                        frameRectangle.X = 180;
                                        frameRectangle.Y = 0;
                                        break;

                                    case 1:
                                        frameRectangle.X = 180;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 2:
                                        frameRectangle.X = 180;
                                        frameRectangle.Y = 36;
                                        break;
                                }
                            } else if ((rightTopWallTile != wallTile) && (rightBottomWallTile != wallTile)) {
                                switch (wallFrameNumber) {
                                    case 0:
                                        frameRectangle.X = 198;
                                        frameRectangle.Y = 0;
                                        break;

                                    case 1:
                                        frameRectangle.X = 198;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 2:
                                        frameRectangle.X = 198;
                                        frameRectangle.Y = 36;
                                        break;
                                }
                            } else {
                                switch (wallFrameNumber) {
                                    case 0:
                                        frameRectangle.X = 18;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 1:
                                        frameRectangle.X = 36;
                                        frameRectangle.Y = 18;
                                        break;

                                    case 2:
                                        frameRectangle.X = 54;
                                        frameRectangle.Y = 18;
                                        break;
                                }
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile == wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 18;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 36;
                                    frameRectangle.Y = 0;
                                    break;

                                case 2:
                                    frameRectangle.X = 54;
                                    frameRectangle.Y = 0;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile == wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 18;
                                    frameRectangle.Y = 36;
                                    break;

                                case 1:
                                    frameRectangle.X = 36;
                                    frameRectangle.Y = 36;
                                    break;

                                case 2:
                                    frameRectangle.X = 54;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile != wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 0;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 0;
                                    frameRectangle.Y = 18;
                                    break;

                                case 2:
                                    frameRectangle.X = 0;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile == wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 72;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 72;
                                    frameRectangle.Y = 18;
                                    break;

                                case 2:
                                    frameRectangle.X = 72;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile != wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 0;
                                    frameRectangle.Y = 54;
                                    break;

                                case 1:
                                    frameRectangle.X = 36;
                                    frameRectangle.Y = 54;
                                    break;

                                case 2:
                                    frameRectangle.X = 72;
                                    frameRectangle.Y = 54;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile == wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 18;
                                    frameRectangle.Y = 54;
                                    break;

                                case 1:
                                    frameRectangle.X = 54;
                                    frameRectangle.Y = 54;
                                    break;

                                case 2:
                                    frameRectangle.X = 90;
                                    frameRectangle.Y = 54;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile != wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 0;
                                    frameRectangle.Y = 72;
                                    break;

                                case 1:
                                    frameRectangle.X = 36;
                                    frameRectangle.Y = 72;
                                    break;

                                case 2:
                                    frameRectangle.X = 72;
                                    frameRectangle.Y = 72;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile == wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 18;
                                    frameRectangle.Y = 72;
                                    break;

                                case 1:
                                    frameRectangle.X = 54;
                                    frameRectangle.Y = 72;
                                    break;

                                case 2:
                                    frameRectangle.X = 90;
                                    frameRectangle.Y = 72;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile != wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 90;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 90;
                                    frameRectangle.Y = 18;
                                    break;

                                case 2:
                                    frameRectangle.X = 90;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile == wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 108;
                                    frameRectangle.Y = 72;
                                    break;

                                case 1:
                                    frameRectangle.X = 126;
                                    frameRectangle.Y = 72;
                                    break;

                                case 2:
                                    frameRectangle.X = 144;
                                    frameRectangle.Y = 72;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile == wallTile)) && ((leftWallTile != wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 108;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 126;
                                    frameRectangle.Y = 0;
                                    break;

                                case 2:
                                    frameRectangle.X = 144;
                                    frameRectangle.Y = 0;
                                    break;
                            }
                        } else if (((topWallTile == wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile != wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 108;
                                    frameRectangle.Y = 54;
                                    break;

                                case 1:
                                    frameRectangle.X = 126;
                                    frameRectangle.Y = 54;
                                    break;

                                case 2:
                                    frameRectangle.X = 144;
                                    frameRectangle.Y = 54;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile != wallTile) & (rightWallTile == wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 162;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 162;
                                    frameRectangle.Y = 18;
                                    break;

                                case 2:
                                    frameRectangle.X = 162;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile == wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 216;
                                    frameRectangle.Y = 0;
                                    break;

                                case 1:
                                    frameRectangle.X = 216;
                                    frameRectangle.Y = 18;
                                    break;

                                case 2:
                                    frameRectangle.X = 216;
                                    frameRectangle.Y = 36;
                                    break;
                            }
                        } else if (((topWallTile != wallTile) && (bottomWallTile != wallTile)) && ((leftWallTile != wallTile) & (rightWallTile != wallTile))) {
                            switch (wallFrameNumber) {
                                case 0:
                                    frameRectangle.X = 162;
                                    frameRectangle.Y = 54;
                                    break;

                                case 1:
                                    frameRectangle.X = 180;
                                    frameRectangle.Y = 54;
                                    break;

                                case 2:
                                    frameRectangle.X = 198;
                                    frameRectangle.Y = 54;
                                    break;
                            }
                        }
                    }
                    if ((frameRectangle.X <= -1) || (frameRectangle.Y <= -1)) {
                        if (wallFrameNumber <= 0) {
                            frameRectangle.X = 18;
                            frameRectangle.Y = 18;
                        }
                        if (wallFrameNumber == 1) {
                            frameRectangle.X = 36;
                            frameRectangle.Y = 18;
                        }
                        if (wallFrameNumber >= 2) {
                            frameRectangle.X = 54;
                            frameRectangle.Y = 18;
                        }
                    }
                    tile[i, j].WallFrameX = (byte)frameRectangle.X;
                    tile[i, j].WallFrameY = (byte)frameRectangle.Y;
                }
            }
        }

        public void PlantCheck(int i, int j) {
            int num = -1;
            int type = tile[i, j].Type;
            int num1 = i - 1;
            int maxTilesX = Size.Width;
            int num4 = i + 1;
            int num5 = j - 1;
            if ((j + 1) >= Size.Height) {
                num = type;
            }
            if ((((i - 1) >= 0) && (tile[i - 1, j] != null)) && tile[i - 1, j].Active) {
                byte num6 = tile[i - 1, j].Type;
            }
            if ((((i + 1) < Size.Width) && (tile[i + 1, j] != null)) && tile[i + 1, j].Active) {
                byte num7 = tile[i + 1, j].Type;
            }
            if ((((j - 1) >= 0) && (tile[i, j - 1] != null)) && tile[i, j - 1].Active) {
                byte num8 = tile[i, j - 1].Type;
            }
            if ((((j + 1) < Size.Height) && (tile[i, j + 1] != null)) && tile[i, j + 1].Active) {
                num = tile[i, j + 1].Type;
            }
            if ((((i - 1) >= 0) && ((j - 1) >= 0)) && ((tile[i - 1, j - 1] != null) && tile[i - 1, j - 1].Active)) {
                byte num9 = tile[i - 1, j - 1].Type;
            }
            if ((((i + 1) < Size.Width) && ((j - 1) >= 0)) && ((tile[i + 1, j - 1] != null) && tile[i + 1, j - 1].Active)) {
                byte num10 = tile[i + 1, j - 1].Type;
            }
            if ((((i - 1) >= 0) && ((j + 1) < Size.Height)) && ((tile[i - 1, j + 1] != null) && tile[i - 1, j + 1].Active)) {
                byte num11 = tile[i - 1, j + 1].Type;
            }
            if ((((i + 1) < Size.Width) && ((j + 1) < Size.Height)) && ((tile[i + 1, j + 1] != null) && tile[i + 1, j + 1].Active)) {
                byte num12 = tile[i + 1, j + 1].Type;
            }
            if ((((((type == 3) && (num != 2)) && (num != 0x4e)) || ((type == 0x18) && (num != 0x17))) || (((type == 0x3d) && (num != 60)) || ((type == 0x47) && (num != 70)))) || ((((type == 0x49) && (num != 2)) && (num != 0x4e)) || ((type == 0x4a) && (num != 60)))) {
                KillTile(i, j, false, false, false);
            }
        }

        public void KillTile(int i, int j, bool fail = false, bool effectOnly = false, bool noItem = false) {
            if (((i >= 0) && (j >= 0)) && ((i < Size.Width) && (j < Size.Height))) {
                if (tile[i, j] == null) {
                    tile[i, j] = new Tile();
                }
                if (tile[i, j].Active) {
                    if ((j >= 1) && (tile[i, j - 1] == null)) {
                        tile[i, j - 1] = new Tile();
                    }
                    if ((((j < 1) || !tile[i, j - 1].Active) || ((((tile[i, j - 1].Type != 5) || (tile[i, j].Type == 5)) && ((tile[i, j - 1].Type != 0x15) || (tile[i, j].Type == 0x15))) && ((((tile[i, j - 1].Type != 0x1a) || (tile[i, j].Type == 0x1a)) && ((tile[i, j - 1].Type != 72) || (tile[i, j].Type == 72))) && ((tile[i, j - 1].Type != 12) || (tile[i, j].Type == 12))))) || ((tile[i, j - 1].Type == 5) && (((((tile[i, j - 1].FrameX == 0x42) && (tile[i, j - 1].FrameY >= 0)) && (tile[i, j - 1].FrameY <= 0x2c)) || (((tile[i, j - 1].FrameX == 0x58) && (tile[i, j - 1].FrameY >= 0x42)) && (tile[i, j - 1].FrameY <= 110))) || (tile[i, j - 1].FrameY >= 198)))) {
                        if (!effectOnly) {
                            if (fail) {
                                if ((tile[i, j].Type == 2) || (tile[i, j].Type == 0x17)) {
                                    tile[i, j].Type = 0;
                                }
                                if (tile[i, j].Type == 60) {
                                    tile[i, j].Type = 0x3b;
                                }
                                SquareTileFrame(i, j, true);
                            } else {
                                if (!noItem && !stopDrops) {
                                    int num6 = 0;
                                    if ((tile[i, j].Type == 0) || (tile[i, j].Type == 2)) {
                                        num6 = 2;
                                    } else if (tile[i, j].Type == 1) {
                                        num6 = 3;
                                    } else if (tile[i, j].Type == 4) {
                                        num6 = 8;
                                    } else if (tile[i, j].Type == 5) {
                                        if ((tile[i, j].FrameX >= 0x16) && (tile[i, j].FrameY >= 198)) {
                                            if (genRand.Next(2) == 0) {
                                                num6 = 0x1b;
                                            } else {
                                                num6 = 9;
                                            }
                                        } else {
                                            num6 = 9;
                                        }
                                    } else if (tile[i, j].Type == 6) {
                                        num6 = 11;
                                    } else if (tile[i, j].Type == 7) {
                                        num6 = 12;
                                    } else if (tile[i, j].Type == 8) {
                                        num6 = 13;
                                    } else if (tile[i, j].Type == 9) {
                                        num6 = 14;
                                    } else if (tile[i, j].Type == 13) {
                                        if (tile[i, j].FrameX == 18) {
                                            num6 = 0x1c;
                                        } else if (tile[i, j].FrameX == 36) {
                                            num6 = 110;
                                        } else {
                                            num6 = 0x1f;
                                        }
                                    } else if (tile[i, j].Type == 0x13) {
                                        num6 = 0x5e;
                                    } else if (tile[i, j].Type == 0x16) {
                                        num6 = 0x38;
                                    } else if (tile[i, j].Type == 0x17) {
                                        num6 = 2;
                                    } else if (tile[i, j].Type == 0x19) {
                                        num6 = 0x3d;
                                    } else if (tile[i, j].Type == 30) {
                                        num6 = 9;
                                    } else if (tile[i, j].Type == 0x21) {
                                        num6 = 0x69;
                                    } else if (tile[i, j].Type == 0x25) {
                                        num6 = 0x74;
                                    } else if (tile[i, j].Type == 0x26) {
                                        num6 = 0x81;
                                    } else if (tile[i, j].Type == 0x27) {
                                        num6 = 0x83;
                                    } else if (tile[i, j].Type == 40) {
                                        num6 = 0x85;
                                    } else if (tile[i, j].Type == 0x29) {
                                        num6 = 0x86;
                                    } else if (tile[i, j].Type == 0x2b) {
                                        num6 = 0x89;
                                    } else if (tile[i, j].Type == 0x2c) {
                                        num6 = 0x8b;
                                    } else if (tile[i, j].Type == 0x2d) {
                                        num6 = 0x8d;
                                    } else if (tile[i, j].Type == 0x2e) {
                                        num6 = 0x8f;
                                    } else if (tile[i, j].Type == 0x2f) {
                                        num6 = 0x91;
                                    } else if (tile[i, j].Type == 0x30) {
                                        num6 = 0x93;
                                    } else if (tile[i, j].Type == 0x31) {
                                        num6 = 0x94;
                                    } else if (tile[i, j].Type == 0x33) {
                                        num6 = 150;
                                    } else if (tile[i, j].Type == 0x35) {
                                        num6 = 0xa9;
                                    } else if (tile[i, j].Type == 54) {

                                    } else if (tile[i, j].Type == 0x38) {
                                        num6 = 0xad;
                                    } else if (tile[i, j].Type == 0x39) {
                                        num6 = 0xac;
                                    } else if (tile[i, j].Type == 0x3a) {
                                        num6 = 0xae;
                                    } else if (tile[i, j].Type == 60) {
                                        num6 = 0xb0;
                                    } else if (tile[i, j].Type == 70) {
                                        num6 = 0xb0;
                                    } else if (tile[i, j].Type == 0x4b) {
                                        num6 = 0xc0;
                                    } else if (tile[i, j].Type == 0x4c) {
                                        num6 = 0xd6;
                                    } else if (tile[i, j].Type == 0x4e) {
                                        num6 = 0xde;
                                    } else if ((tile[i, j].Type == 0x3d) || (tile[i, j].Type == 0x4a)) {
                                        if (tile[i, j].FrameX == 162) {
                                            num6 = 0xdf;
                                        } else if (((tile[i, j].FrameX >= 108) && (tile[i, j].FrameX <= 126)) && (genRand.Next(2) == 0)) {
                                            num6 = 0xd0;
                                        }
                                    } else if ((tile[i, j].Type == 0x3b) || (tile[i, j].Type == 60)) {
                                        num6 = 0xb0;
                                    } else if ((tile[i, j].Type == 0x47) || (tile[i, j].Type == 72)) {
                                        if (genRand.Next(50) == 0) {
                                            num6 = 0xc2;
                                        } else {
                                            num6 = 0xb7;
                                        }
                                    } else if ((tile[i, j].Type == 0x4a) && (genRand.Next(100) == 0)) {
                                        num6 = 0xc3;
                                    } else if ((tile[i, j].Type >= 0x3f) && (tile[i, j].Type <= 0x44)) {
                                        num6 = (tile[i, j].Type - 0x3f) + 0xb1;
                                    } else if (tile[i, j].Type == 50) {
                                        if (tile[i, j].FrameX == 90) {
                                            num6 = 0xa5;
                                        } else {
                                            num6 = 0x95;
                                        }
                                    }
                                }
                                tile[i, j].Active = false;
                                if (tileSolid[tile[i, j].Type]) {
                                    tile[i, j].Lighted = false;
                                }
                                tile[i, j].FrameX = -1;
                                tile[i, j].FrameY = -1;
                                tile[i, j].FrameNumber = 0;
                                tile[i, j].Type = 0;
                                SquareTileFrame(i, j, true);
                            }
                        }
                    }
                }
            }
        }

        public void Check1x2(int x, int j, byte type) {
            if (!destroyObject) {
                int num = j;
                bool flag = true;
                if (tile[x, num] == null) {
                    tile[x, num] = new Tile();
                }
                if (tile[x, num + 1] == null) {
                    tile[x, num + 1] = new Tile();
                }
                if (tile[x, num].FrameY == 18) {
                    num--;
                }
                if (tile[x, num] == null) {
                    tile[x, num] = new Tile();
                }
                if (((tile[x, num].FrameY == 0) && (tile[x, num + 1].FrameY == 18)) && ((tile[x, num].Type == type) && (tile[x, num + 1].Type == type))) {
                    flag = false;
                }
                if (tile[x, num + 2] == null) {
                    tile[x, num + 2] = new Tile();
                }
                if (!tile[x, num + 2].Active || !tileSolid[tile[x, num + 2].Type]) {
                    flag = true;
                }
                if ((tile[x, num + 2].Type != 2) && (tile[x, num].Type == 20)) {
                    flag = true;
                }
                if (flag) {
                    destroyObject = true;
                    if (tile[x, num].Type == type) {
                        KillTile(x, num, false, false, false);
                    }
                    if (tile[x, num + 1].Type == type) {
                        KillTile(x, num + 1, false, false, false);
                    }
                    destroyObject = false;
                }
            }
        }

        public void Check1x2Top(int x, int j, byte type) {
            if (!destroyObject) {
                int num = j;
                bool flag = true;
                if (tile[x, num] == null) {
                    tile[x, num] = new Tile();
                }
                if (tile[x, num + 1] == null) {
                    tile[x, num + 1] = new Tile();
                }
                if (tile[x, num].FrameY == 18) {
                    num--;
                }
                if (tile[x, num] == null) {
                    tile[x, num] = new Tile();
                }
                if (((tile[x, num].FrameY == 0) && (tile[x, num + 1].FrameY == 18)) && ((tile[x, num].Type == type) && (tile[x, num + 1].Type == type))) {
                    flag = false;
                }
                if (tile[x, num - 1] == null) {
                    tile[x, num - 1] = new Tile();
                }
                if ((!tile[x, num - 1].Active || !tileSolid[tile[x, num - 1].Type]) || tileSolidTop[tile[x, num - 1].Type]) {
                    flag = true;
                }
                if (flag) {
                    destroyObject = true;
                    if (tile[x, num].Type == type) {
                        KillTile(x, num, false, false, false);
                    }
                    if (tile[x, num + 1].Type == type) {
                        KillTile(x, num + 1, false, false, false);
                    }
                    destroyObject = false;
                }
            }
        }

        public void Check2x1(int i, int y, byte type) {
            if (!destroyObject) {
                int num = i;
                bool flag = true;
                if (tile[num, y] == null) {
                    tile[num, y] = new Tile();
                }
                if (tile[num + 1, y] == null) {
                    tile[num + 1, y] = new Tile();
                }
                if (tile[num, y + 1] == null) {
                    tile[num, y + 1] = new Tile();
                }
                if (tile[num + 1, y + 1] == null) {
                    tile[num + 1, y + 1] = new Tile();
                }
                if (tile[num, y].FrameX == 18) {
                    num--;
                }
                if (tile[num, y] == null) {
                    tile[num, y] = new Tile();
                }
                if (((tile[num, y].FrameX == 0) && (tile[num + 1, y].FrameX == 18)) && ((tile[num, y].Type == type) && (tile[num + 1, y].Type == type))) {
                    flag = false;
                }
                if (type == 0x1d) {
                    if (!tile[num, y + 1].Active || !tileTable[tile[num, y + 1].Type]) {
                        flag = true;
                    }
                    if (!tile[num + 1, y + 1].Active || !tileTable[tile[num + 1, y + 1].Type]) {
                        flag = true;
                    }
                } else {
                    if (!tile[num, y + 1].Active || !tileSolid[tile[num, y + 1].Type]) {
                        flag = true;
                    }
                    if (!tile[num + 1, y + 1].Active || !tileSolid[tile[num + 1, y + 1].Type]) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    if (tile[num, y].Type == type) {
                        KillTile(num, y, false, false, false);
                    }
                    if (tile[num + 1, y].Type == type) {
                        KillTile(num + 1, y, false, false, false);
                    }
                    destroyObject = false;
                }
            }
        }

        public void Check3x2(int i, int j, int type) {
            if (!destroyObject) {
                bool flag = false;
                int num = i;
                int num2 = j;
                num += (tile[i, j].FrameX / 18) % 3 * -1;
                num2 += (tile[i, j].FrameY / 18) % 2 * -1;
                for (int k = num; k < (num + 3); k++) {
                    for (int m = num2; m < (num2 + 2); m++) {
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((tile[k, m].FrameX != ((k - num) * 18)) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                    if (tile[k, num2 + 2] == null) {
                        tile[k, num2 + 2] = new Tile();
                    }
                    if (!tile[k, num2 + 2].Active || !tileSolid[tile[k, num2 + 2].Type]) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    for (int n = num; n < (num + 3); n++) {
                        for (int num6 = num2; num6 < (num2 + 3); num6++) {
                            if ((tile[n, num6].Type == type) && tile[n, num6].Active) {
                                KillTile(n, num6, false, false, false);
                            }
                        }
                    }
                    destroyObject = false;
                    for (int num7 = num - 1; num7 < (num + 4); num7++) {
                        for (int num8 = num2 - 1; num8 < (num2 + 4); num8++) {
                            TileFrame(num7, num8, false, false);
                        }
                    }
                }
            }
        }

        public void Check3x3(int i, int j, int type) {
            if (!destroyObject) {
                bool flag = false;
                int num = i;
                int num2 = j;
                num += (tile[i, j].FrameX / 18) % 3 * -1;
                num2 += (tile[i, j].FrameY / 18) % 3 * -1;
                for (int k = num; k < (num + 3); k++) {
                    for (int m = num2; m < (num2 + 3); m++) {
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((tile[k, m].FrameX != ((k - num) * 18)) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                }
                if (tile[num + 1, num2 - 1] == null) {
                    tile[num + 1, num2 - 1] = new Tile();
                }
                if ((!tile[num + 1, num2 - 1].Active || !tileSolid[tile[num + 1, num2 - 1].Type]) || tileSolidTop[tile[num + 1, num2 - 1].Type]) {
                    flag = true;
                }
                if (flag) {
                    destroyObject = true;
                    for (int n = num; n < (num + 3); n++) {
                        for (int num6 = num2; num6 < (num2 + 3); num6++) {
                            if ((tile[n, num6].Type == type) && tile[n, num6].Active) {
                                KillTile(n, num6, false, false, false);
                            }
                        }
                    }
                    destroyObject = false;
                    for (int num7 = num - 1; num7 < (num + 4); num7++) {
                        for (int num8 = num2 - 1; num8 < (num2 + 4); num8++) {
                            TileFrame(num7, num8, false, false);
                        }
                    }
                }
            }
        }

        public void Check4x2(int i, int j, int type) {
            if (!destroyObject) {
                bool flag = false;
                int num = i;
                int num2 = j;
                num += (tile[i, j].FrameX / 18) % 4 * -1;
                if ((type == 0x4f) && (tile[i, j].FrameX >= 72)) {
                    // num += 4;
                }
                num2 += (tile[i, j].FrameY / 18) % 2 * -1;
                for (int k = num; k < (num + 4); k++) {
                    for (int m = num2; m < (num2 + 2); m++) {
                        int num5 = (k - num) * 18;
                        if ((type == 0x4f) && (tile[i, j].FrameX >= 72)) {
                            // num5 = ((k - num) + 4) * 18;
                        }
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((tile[k, m].FrameX != num5) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                    if (tile[k, num2 + 2] == null) {
                        tile[k, num2 + 2] = new Tile();
                    }
                    if (!tile[k, num2 + 2].Active || !tileSolid[tile[k, num2 + 2].Type]) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    for (int n = num; n < (num + 4); n++) {
                        for (int num7 = num2; num7 < (num2 + 3); num7++) {
                            if ((tile[n, num7].Type == type) && tile[n, num7].Active) {
                                KillTile(n, num7, false, false, false);
                            }
                        }
                    }
                    destroyObject = false;
                    for (int num8 = num - 1; num8 < (num + 4); num8++) {
                        for (int num9 = num2 - 1; num9 < (num2 + 4); num9++) {
                            TileFrame(num8, num9, false, false);
                        }
                    }
                }
            }
        }

        public void CheckChest(int i, int j, int type) {
            if (!destroyObject) {
                bool flag = false;
                int num = i;
                int num2 = j;
                num += (tile[i, j].FrameX / 18) % 2 * -1;
                num2 += (tile[i, j].FrameY / 18) % 2 * -1;
                Console.WriteLine("Tile: " + i + ", " + j);
                Console.WriteLine("Frame: " + tile[i, j].FrameX + ", " + tile[i, j].FrameY);
                Console.WriteLine("Nums: " + num + ", " + num2);
                for (int k = num; k < (num + 2); k++) {
                    for (int m = num2; m < (num2 + 2); m++) {
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((tile[k, m].FrameX != ((k - num) * 18)) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                    if (tile[k, num2 + 2] == null) {
                        tile[k, num2 + 2] = new Tile();
                    }
                    if (!tile[k, num2 + 2].Active || !tileSolid[tile[k, num2 + 2].Type]) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    /*for (int n = num; n < (num + 2); n++) {
                        for (int num6 = num2; num6 < (num2 + 3); num6++) {
                            if ((tile[n, num6].Type == type) && tile[n, num6].Active) {
                                KillTile(n, num6, false, false, false);
                            }
                        }
                    }*/
                    destroyObject = false;
                }
            }
        }

        public void CheckOnTable1x1(int x, int y, int type) {
            if ((tile[x, y + 1] != null) && (!tile[x, y + 1].Active || !tileTable[tile[x, y + 1].Type])) {
                if (type == 0x4e) {
                    if (!tile[x, y + 1].Active || !tileSolid[tile[x, y + 1].Type]) {
                        KillTile(x, y, false, false, false);
                    }
                } else {
                    KillTile(x, y, false, false, false);
                }
            }
        }

        public void CheckPot(int i, int j, int type = 0x1c) {
            if (!destroyObject) {
                bool flag = false;
                int num = 0;
                int num2 = j;
                num += tile[i, j].FrameX / 18;
                num2 += (tile[i, j].FrameY / 18) * -1;
                while (num > 1) {
                    num -= 2;
                }
                num *= -1;
                num += i;
                for (int k = num; k < (num + 2); k++) {
                    for (int m = num2; m < (num2 + 2); m++) {
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        int num5 = tile[k, m].FrameX / 18;
                        while (num5 > 1) {
                            num5 -= 2;
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((num5 != (k - num)) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                    if (tile[k, num2 + 2] == null) {
                        tile[k, num2 + 2] = new Tile();
                    }
                    if (!tile[k, num2 + 2].Active || !tileSolid[tile[k, num2 + 2].Type]) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    for (int n = num; n < (num + 2); n++) {
                        for (int num7 = num2; num7 < (num2 + 2); num7++) {
                            if ((tile[n, num7].Type == type) && tile[n, num7].Active) {
                                KillTile(n, num7, false, false, false);
                            }
                        }
                    }
                    int num8 = genRand.Next(10);
                    destroyObject = false;
                }
            }
        }

        public void CheckRoom(int x, int y) {
        }

        public void CheckSign(int x, int y, int type) {
            if (!destroyObject) {
                int num = x - 2;
                int num2 = x + 3;
                int num3 = y - 2;
                int num4 = y + 3;
                if ((((num >= 0) && (num2 <= Size.Width)) && (num3 >= 0)) && (num4 <= Size.Height)) {
                    bool flag = false;
                    for (int i = num; i < num2; i++) {
                        for (int k = num3; k < num4; k++) {
                            if (tile[i, k] == null) {
                                tile[i, k] = new Tile();
                            }
                        }
                    }
                    int num7 = tile[x, y].FrameX / 18;
                    int num8 = tile[x, y].FrameY / 18;
                    while (num7 > 1) {
                        num7 -= 2;
                    }
                    int num9 = x - num7;
                    int num10 = y - num8;
                    int num11 = (tile[num9, num10].FrameX / 18) / 2;
                    num = num9;
                    num2 = num9 + 2;
                    num3 = num10;
                    num4 = num10 + 2;
                    num7 = 0;
                    for (int j = num; j < num2; j++) {
                        num8 = 0;
                        for (int m = num3; m < num4; m++) {
                            if (!tile[j, m].Active || (tile[j, m].Type != type)) {
                                flag = true;
                                goto Label_017B;
                            }
                            if (((tile[j, m].FrameX / 18) != (num7 + (num11 * 2))) || ((tile[j, m].FrameY / 18) != num8)) {
                                flag = true;
                                goto Label_017B;
                            }
                            num8++;
                        }
                    Label_017B:
                        num7++;
                    }
                    if (!flag) {
                        if ((tile[num9, num10 + 2].Active && tileSolid[tile[num9, num10 + 2].Type]) && (tile[num9 + 1, num10 + 2].Active && tileSolid[tile[num9 + 1, num10 + 2].Type])) {
                            num11 = 0;
                        } else if (((tile[num9, num10 - 1].Active && tileSolid[tile[num9, num10 - 1].Type]) && (!tileSolidTop[tile[num9, num10 - 1].Type] && tile[num9 + 1, num10 - 1].Active)) && (tileSolid[tile[num9 + 1, num10 - 1].Type] && !tileSolidTop[tile[num9 + 1, num10 - 1].Type])) {
                            num11 = 1;
                        } else if (((tile[num9 - 1, num10].Active && tileSolid[tile[num9 - 1, num10].Type]) && (!tileSolidTop[tile[num9 - 1, num10].Type] && tile[num9 - 1, num10 + 1].Active)) && (tileSolid[tile[num9 - 1, num10 + 1].Type] && !tileSolidTop[tile[num9 - 1, num10 + 1].Type])) {
                            num11 = 2;
                        } else if (((tile[num9 + 2, num10].Active && tileSolid[tile[num9 + 2, num10].Type]) && (!tileSolidTop[tile[num9 + 2, num10].Type] && tile[num9 + 2, num10 + 1].Active)) && (tileSolid[tile[num9 + 2, num10 + 1].Type] && !tileSolidTop[tile[num9 + 2, num10 + 1].Type])) {
                            num11 = 3;
                        } else {
                            flag = true;
                        }
                    }
                    if (flag) {
                        destroyObject = true;
                        for (int n = num; n < num2; n++) {
                            for (int num15 = num3; num15 < num4; num15++) {
                                if (tile[n, num15].Type == type) {
                                    KillTile(n, num15, false, false, false);
                                }
                            }
                        }
                        destroyObject = false;
                    } else {
                        int num16 = 36 * num11;
                        for (int num17 = 0; num17 < 2; num17++) {
                            for (int num18 = 0; num18 < 2; num18++) {
                                tile[num9 + num17, num10 + num18].Active = true;
                                tile[num9 + num17, num10 + num18].Type = (byte)type;
                                tile[num9 + num17, num10 + num18].FrameX = (short)(num16 + (18 * num17));
                                tile[num9 + num17, num10 + num18].FrameY = (short)(18 * num18);
                            }
                        }
                    }
                }
            }
        }

        public void CheckSunflower(int i, int j, int type = 0x1b) {
            if (!destroyObject) {
                bool flag = false;
                int num = 0;
                int num2 = j;
                num += tile[i, j].FrameX / 18;
                num2 += (tile[i, j].FrameY / 18) * -1;
                while (num > 1) {
                    num -= 2;
                }
                num *= -1;
                num += i;
                for (int k = num; k < (num + 2); k++) {
                    for (int m = num2; m < (num2 + 4); m++) {
                        if (tile[k, m] == null) {
                            tile[k, m] = new Tile();
                        }
                        int num5 = tile[k, m].FrameX / 18;
                        while (num5 > 1) {
                            num5 -= 2;
                        }
                        if ((!tile[k, m].Active || (tile[k, m].Type != type)) || ((num5 != (k - num)) || (tile[k, m].FrameY != ((m - num2) * 18)))) {
                            flag = true;
                        }
                    }
                    if (tile[k, num2 + 4] == null) {
                        tile[k, num2 + 4] = new Tile();
                    }
                    if (!tile[k, num2 + 4].Active || (tile[k, num2 + 4].Type != 2)) {
                        flag = true;
                    }
                }
                if (flag) {
                    destroyObject = true;
                    for (int n = num; n < (num + 2); n++) {
                        for (int num7 = num2; num7 < (num2 + 4); num7++) {
                            if ((tile[n, num7].Type == type) && tile[n, num7].Active) {
                                KillTile(n, num7, false, false, false);
                            }
                        }
                    }
                    destroyObject = false;
                }
            }
        }

        public void SquareTileFrame(int i, int j, bool resetFrame = true) {
            TileFrame(i - 1, j - 1, false, false);
            TileFrame(i - 1, j, false, false);
            TileFrame(i - 1, j + 1, false, false);
            TileFrame(i, j - 1, false, false);
            TileFrame(i, j, resetFrame, false);
            TileFrame(i, j + 1, false, false);
            TileFrame(i + 1, j - 1, false, false);
            TileFrame(i + 1, j, false, false);
            TileFrame(i + 1, j + 1, false, false);
        }

        public bool CheckCactus(int i, int j) {
            int num = j;
            int num2 = i;
            while (tile[num2, num].Active && (tile[num2, num].Type == 80)) {
                num++;
                if (!tile[num2, num].Active || (tile[num2, num].Type != 80)) {
                    if (((tile[num2 - 1, num].Active && (tile[num2 - 1, num].Type == 80)) && (tile[num2 - 1, num - 1].Active && (tile[num2 - 1, num - 1].Type == 80))) && (num2 >= i)) {
                        num2--;
                    }
                    if (((tile[num2 + 1, num].Active && (tile[num2 + 1, num].Type == 80)) && (tile[num2 + 1, num - 1].Active && (tile[num2 + 1, num - 1].Type == 80))) && (num2 <= i)) {
                        num2++;
                    }
                }
            }
            if (!tile[num2, num].Active || (tile[num2, num].Type != 0x35)) {
                KillTile(i, j, false, false, false);
                return true;
            }
            if (i != num2) {
                if (((!tile[i, j + 1].Active || (tile[i, j + 1].Type != 80)) && (!tile[i - 1, j].Active || (tile[i - 1, j].Type != 80))) && (!tile[i + 1, j].Active || (tile[i + 1, j].Type != 80))) {
                    KillTile(i, j, false, false, false);
                    return true;
                }
            } else if ((i == num2) && (!tile[i, j + 1].Active || ((tile[i, j + 1].Type != 80) && (tile[i, j + 1].Type != 0x35)))) {
                KillTile(i, j, false, false, false);
                return true;
            }
            return false;
        }

        public void CactusFrame(int i, int j) {
            try {
                int num = j;
                int num2 = i;
                if (!CheckCactus(i, j)) {
                    goto Label_010C;
                }
                return;
            Label_0015:
                num++;
                if (!tile[num2, num].Active || (tile[num2, num].Type != 80)) {
                    if (((tile[num2 - 1, num].Active && (tile[num2 - 1, num].Type == 80)) && (tile[num2 - 1, num - 1].Active && (tile[num2 - 1, num - 1].Type == 80))) && (num2 >= i)) {
                        num2--;
                    }
                    if (((tile[num2 + 1, num].Active && (tile[num2 + 1, num].Type == 80)) && (tile[num2 + 1, num - 1].Active && (tile[num2 + 1, num - 1].Type == 80))) && (num2 <= i)) {
                        num2++;
                    }
                }
            Label_010C:
                if (tile[num2, num].Active && (tile[num2, num].Type == 80)) {
                    goto Label_0015;
                }
                num--;
                int num3 = i - num2;
                num2 = i;
                num = j;
                int type = tile[i - 2, j].Type;
                int num5 = tile[i - 1, j].Type;
                int num6 = tile[i + 1, j].Type;
                int num7 = tile[i, j - 1].Type;
                int index = tile[i, j + 1].Type;
                int num9 = tile[i - 1, j + 1].Type;
                int num10 = tile[i + 1, j + 1].Type;
                if (!tile[i - 1, j].Active) {
                    num5 = -1;
                }
                if (!tile[i + 1, j].Active) {
                    num6 = -1;
                }
                if (!tile[i, j - 1].Active) {
                    num7 = -1;
                }
                if (!tile[i, j + 1].Active) {
                    index = -1;
                }
                if (!tile[i - 1, j + 1].Active) {
                    num9 = -1;
                }
                if (!tile[i + 1, j + 1].Active) {
                    num10 = -1;
                }
                short frameX = tile[i, j].FrameX;
                short frameY = tile[i, j].FrameY;
                switch (num3) {
                    case 0:
                        if (num7 == 80) {
                            if ((((num5 == 80) && (num6 == 80)) && ((num9 != 80) && (num10 != 80))) && (type != 80)) {
                                frameX = 90;
                                frameY = 0x24;
                            } else if (((num5 == 80) && (num9 != 80)) && (type != 80)) {
                                frameX = 0x48;
                                frameY = 0x24;
                            } else if ((num6 == 80) && (num10 != 80)) {
                                frameX = 0x12;
                                frameY = 0x24;
                            } else if ((index >= 0) && tileSolid[index]) {
                                frameX = 0;
                                frameY = 0x24;
                            } else {
                                frameX = 0;
                                frameY = 0x12;
                            }
                        } else if ((((num5 == 80) && (num6 == 80)) && ((num9 != 80) && (num10 != 80))) && (type != 80)) {
                            frameX = 90;
                            frameY = 0;
                        } else if (((num5 == 80) && (num9 != 80)) && (type != 80)) {
                            frameX = 0x48;
                            frameY = 0;
                        } else if ((num6 == 80) && (num10 != 80)) {
                            frameX = 0x12;
                            frameY = 0;
                        } else {
                            frameX = 0;
                            frameY = 0;
                        }
                        break;

                    case -1:
                        if (num6 == 80) {
                            if ((num7 != 80) && (index != 80)) {
                                frameX = 0x6c;
                                frameY = 0x24;
                            } else if (index != 80) {
                                frameX = 0x36;
                                frameY = 0x24;
                            } else if (num7 != 80) {
                                frameX = 0x36;
                                frameY = 0;
                            } else {
                                frameX = 0x36;
                                frameY = 0x12;
                            }
                        } else if (num7 != 80) {
                            frameX = 0x36;
                            frameY = 0;
                        } else {
                            frameX = 0x36;
                            frameY = 0x12;
                        }
                        break;

                    case 1:
                        if (num5 == 80) {
                            if ((num7 != 80) && (index != 80)) {
                                frameX = 0x6c;
                                frameY = 0x10;
                            } else if (index != 80) {
                                frameX = 0x24;
                                frameY = 0x24;
                            } else if (num7 != 80) {
                                frameX = 0x24;
                                frameY = 0;
                            } else {
                                frameX = 0x24;
                                frameY = 0x12;
                            }
                        } else if (num7 != 80) {
                            frameX = 0x24;
                            frameY = 0;
                        } else {
                            frameX = 0x24;
                            frameY = 0x12;
                        }
                        break;
                }
                if ((frameX != tile[i, j].FrameX) || (frameY != tile[i, j].FrameY)) {
                    tile[i, j].FrameX = frameX;
                    tile[i, j].FrameY = frameY;
                    SquareTileFrame(i, j, true);
                }
            } catch {
                tile[i, j].FrameX = 0;
                tile[i, j].FrameY = 0;
            }
        }
    }
}
