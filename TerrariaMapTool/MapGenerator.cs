using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Terraria;

namespace TerrariaMapTool {
    public class MapGenerator {
        #region Members

        private MapGeneratorOptions options = null;

        private TextureSet textures = new TextureSet();

        private Queue<TextureSet> availableTextureSet = new Queue<TextureSet>();

        private object availableTextureSetLock = new object();

        #endregion

        #region Constructors

        public MapGenerator(MapGeneratorOptions options) {
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            this.options = options;
        }

        #endregion
        
        #region Members

        private Image ConvertTextureToImage(Texture2D texture) {
            byte[] textureData = new byte[4 * texture.Width * texture.Height];
            texture.GetData<byte>(textureData);

            for (int i = 0; i < texture.Width * texture.Height; ++i) {
                int texel = i * 4;
                byte blue = textureData[texel + 0];
                byte red = textureData[texel + 2];

                textureData[texel + 0] = red;
                textureData[texel + 2] = blue;
            }

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(texture.Width, texture.Height, PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, texture.Width, texture.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            IntPtr safePtr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(textureData, 0, safePtr, textureData.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void LoadTextures() {
            bool exportTexturesToFiles = false;
            Form graphicsDeviceForm = new Form();
            graphicsDeviceForm.Size = new Size(256, 256);

            PresentationParameters presentationParameters = new PresentationParameters();
            presentationParameters.IsFullScreen = false;
            presentationParameters.DeviceWindowHandle = graphicsDeviceForm.Handle;

            // We don't need an advanced adaptor. One that can support DirectX 9
            // is prefectly fine for exporting the textures.
            var adaptorQuery =
                from adaptor in GraphicsAdapter.Adapters
                where adaptor.IsProfileSupported(GraphicsProfile.Reach)
                select adaptor;

            GraphicsAdapter adapter = adaptorQuery.FirstOrDefault();

            // Fallback to the default.
            if (adapter == null) {
                adapter = GraphicsAdapter.DefaultAdapter;
            }

            GraphicsDevice graphicsDevice = new GraphicsDevice(adapter, GraphicsProfile.Reach, presentationParameters);

            ServiceProvider serviceProvider = new ServiceProvider();
            serviceProvider.AddService(typeof(IGraphicsDeviceService), new GraphicsDeviceService(graphicsDevice));

            ContentManager content = new ContentManager(serviceProvider, Path.Combine(options.TerrariaDirectory, "Content"));

            for (int i = 0; i < World.NumTiles; i++) {
                textures.tileTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\tiles_" + i));

                if (exportTexturesToFiles) {
                    textures.tileTexture[i].Save("Tile" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 1; i < 116; i++) {
                textures.wallTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Wall_" + i));

                if (exportTexturesToFiles) {
                    textures.wallTexture[i].Save("Wall" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 32; i++) {
                textures.backgroundTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Background_" + i));

                if (exportTexturesToFiles) {
                    textures.backgroundTexture[i].Save("Background" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 147; i++) {
                textures.npcTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\NPC_" + i));

                if (exportTexturesToFiles) {
                    textures.npcTexture[i].Save("Npc" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 4; i++) {
                textures.cloudTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Cloud_" + i));

                if (exportTexturesToFiles) {
                    textures.cloudTexture[i].Save("Cloud" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 2; i++) {
                textures.liquidTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Liquid_" + i));

                if (exportTexturesToFiles) {
                    textures.liquidTexture[i].Save("Liquid" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 5; i++) {
                textures.treeTopTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Tree_Tops_" + i));

                if (exportTexturesToFiles) {
                    textures.treeTopTexture[i].Save("TreeTop" + i + ".png", ImageFormat.Png);
                }
            }

            for (int i = 0; i < 5; i++) {
                textures.treeBranchTexture[i] = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Tree_Branches_" + i));

                if (exportTexturesToFiles) {
                    textures.treeBranchTexture[i].Save("TreeBranch" + i + ".png", ImageFormat.Png);
                }
            }

            textures.sunTexture = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Sun"));

            if (exportTexturesToFiles) {
                textures.sunTexture.Save("SunTexture.png", ImageFormat.Png);
            }

            textures.moonTexture = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Moon"));

            if (exportTexturesToFiles) {
                textures.moonTexture.Save("MoonTexture.png", ImageFormat.Png);
            }

            textures.shroomCapTexture = ConvertTextureToImage(content.Load<Texture2D>(@"Images\Shroom_Tops"));

            if (exportTexturesToFiles) {
                textures.shroomCapTexture.Save("ShroomCapTexture.png", ImageFormat.Png);
            }
        }

        protected readonly System.Drawing.Color skyStartColor = System.Drawing.Color.FromArgb(25, 101, 255);
        protected readonly System.Drawing.Color skyEndColor = System.Drawing.Color.FromArgb(132, 170, 248);

        protected void DrawSky(World world, System.Drawing.Rectangle region, Graphics g) {
            float regionWidth = region.Width * 16;
            float regionHeight = region.Height * 16;

            // Draw the sky.
            int skyEnd = (int) world.worldSurface + 1;

            if (region.Top <= skyEnd) {
                float skyStart = region.Top / (float) skyEnd;

                float skyHeight = 0;
                float destinationHeight = 0;

                if (region.Bottom <= skyEnd) {
                    skyHeight = region.Height / (float) skyEnd;
                    destinationHeight = region.Height;
                } else {
                    skyHeight = 1.0f - skyStart;
                    destinationHeight = skyEnd - region.Top;
                }

                System.Drawing.Color firstColor = skyStartColor.Lerp(skyEndColor, skyStart);
                System.Drawing.Color secondColor = skyStartColor.Lerp(skyEndColor, skyStart + skyHeight);

                destinationHeight *= 16;

                using (Brush brush = new LinearGradientBrush(new PointF(0, 0), new PointF(0, destinationHeight), firstColor, secondColor)) {
                    g.FillRectangle(brush, new RectangleF(0, 0, regionWidth, destinationHeight));
                }
            }
        }

        protected void DrawBackground(World world, System.Drawing.Rectangle region, Graphics g, TextureSet textures) {
            float regionWidth = region.Width * 16;
            float regionHeight = region.Height * 16;

            int skyEnd = (int) world.worldSurface + 1;

            // Draw the Dirt Sky Blend
            if (region.Top < skyEnd && skyEnd <= region.Bottom) {
                float destinationTop = (skyEnd - region.Top - 1) * 16; // might need to +/- 8

                // How many across need to be rendered.
                int instances = (int) Math.Ceiling(regionWidth / textures.backgroundTexture[1].Width) + 1;
                int offset = ((region.Left % 6) + 3) * 16;

                for (int i = 0; i < instances; ++i) {
                    g.DrawImage(textures.backgroundTexture[1], new PointF(i * textures.backgroundTexture[1].Width - offset, destinationTop));
                }
            }

            int rockStart = (int) world.rockLayer + 38;

            // Draw the Dirt Background
            if (skyEnd < region.Bottom || region.Top <= rockStart) {
                int minBlockY = region.Top;
                float startOffsetY = 0.0f;

                if (skyEnd > region.Top) {
                    minBlockY = skyEnd;
                    startOffsetY = (skyEnd - region.Top) * 16;
                }

                int maxBlockY = (int) ((rockStart < region.Bottom) ? rockStart : region.Bottom);

                int blockCount = maxBlockY - minBlockY;

                int dirtBackgroundTextureWidth = textures.backgroundTexture[2].Width - 32;
                int dirtBackgroundTextureHeight = textures.backgroundTexture[2].Height;

                // How many vertical repeats.
                int instancesToDrawY = (int) Math.Ceiling(blockCount * 16.0f / dirtBackgroundTextureHeight) + 1;
                int instancesToDrawX = (int) Math.Ceiling((regionWidth / dirtBackgroundTextureWidth)) + 1;
                int offsetX = (((region.Left) % (dirtBackgroundTextureWidth / 16)) + 3) * 16;
                int offsetY = ((minBlockY - (int) skyEnd) % (dirtBackgroundTextureHeight / 16)) * 16;

                for (int i = 0; i < instancesToDrawX; ++i) {
                    for (int j = 0; j < instancesToDrawY; ++j) {
                        g.DrawImage(textures.backgroundTexture[2], new PointF(i * dirtBackgroundTextureWidth - offsetX, startOffsetY + j * dirtBackgroundTextureHeight - offsetY));
                    }
                }
            }

            // Draw the Dirt Rock Blend
            if (region.Top < rockStart && rockStart <= region.Bottom) {
                float destinationTop = (rockStart - region.Top) * 16 - 8;

                int dirRockBeackgroundTextureWidth = textures.backgroundTexture[4].Width - 32;

                // How many across need to be rendered.
                int instances = (int) Math.Ceiling(regionWidth / dirRockBeackgroundTextureWidth) + 1;
                int offset = ((region.Left % 6) + 3) * 16;

                for (int i = 0; i < instances; ++i) {
                    g.DrawImage(textures.backgroundTexture[4], new PointF(i * dirRockBeackgroundTextureWidth - offset, destinationTop));
                }
            }

            int hellStart = world.Size.Height - 230;

            // Draw the Rock Background
            if (region.Bottom > rockStart || region.Top < hellStart) {
                int minBlockY = region.Top;
                float startOffsetY = 0.0f;

                if (rockStart > region.Top) {
                    minBlockY = rockStart;
                    startOffsetY = (rockStart - region.Top) * 16;
                }

                int maxBlockY = (hellStart < region.Bottom) ? hellStart : region.Bottom;

                int blockCount = maxBlockY - minBlockY;

                int rockBackgroundTextureWidth = textures.backgroundTexture[3].Width - 32;
                int rockBackgroundTextureHeight = textures.backgroundTexture[3].Height;

                // How many vertical repeats.
                int instancesToDrawY = (int) Math.Ceiling(blockCount * 16.0f / rockBackgroundTextureHeight) + 1;
                int instancesToDrawX = (int) Math.Ceiling((regionWidth / rockBackgroundTextureWidth)) + 1;
                int offsetX = (((region.Left) % (rockBackgroundTextureWidth / 16)) + 3) * 16;
                int offsetY = ((minBlockY - (int) rockStart - 1) % (rockBackgroundTextureHeight / 16)) * 16 + 8;

                for (int i = 0; i < instancesToDrawX; ++i) {
                    for (int j = 0; j < instancesToDrawY; ++j) {
                        g.DrawImage(textures.backgroundTexture[3], new PointF(i * rockBackgroundTextureWidth - offsetX, startOffsetY + j * rockBackgroundTextureHeight - offsetY));
                    }
                }
            }

            // Draw the Rock Hell Blend
            if (region.Top < hellStart && hellStart <= region.Bottom) {
                float destinationTop = (hellStart - region.Top) * 16 + 24 - 8;

                int rockHellBackgroundTextureWidth = textures.backgroundTexture[6].Width - 32;

                // How many across need to be rendered.
                int instances = (int) Math.Ceiling(regionWidth / rockHellBackgroundTextureWidth) + 1;
                int offset = ((region.Left % 6) + 3) * 16;

                for (int i = 0; i < instances; ++i) {
                    g.DrawImage(textures.backgroundTexture[6], new PointF(i * rockHellBackgroundTextureWidth - offset, destinationTop));
                }
            }

            // Draw the Hell Background
            if (region.Bottom > hellStart) {
                int minBlockY = region.Top;
                float startOffsetY = 0.0f;

                if (hellStart > region.Top) {
                    minBlockY = hellStart;
                    startOffsetY = (hellStart - region.Top) * 16;
                }

                int maxBlockY = region.Bottom;

                int blockCount = maxBlockY - minBlockY;

                int rockBackgroundTextureWidth = textures.backgroundTexture[5].Width - 32;
                int rockBackgroundTextureHeight = textures.backgroundTexture[5].Height;

                // How many vertical repeats.
                int instancesToDrawY = (int) Math.Ceiling(blockCount * 16.0f / rockBackgroundTextureHeight) + 1;
                int instancesToDrawX = (int) Math.Ceiling((regionWidth / rockBackgroundTextureWidth)) + 1;
                int offsetX = (((region.Left) % (rockBackgroundTextureWidth / 16)) + 3) * 16;
                int offsetY = ((minBlockY - (int) hellStart - 1) % (rockBackgroundTextureHeight / 16)) * 16 - 16;

                for (int i = 0; i < instancesToDrawX; ++i) {
                    for (int j = 0; j < instancesToDrawY; ++j) {
                        g.DrawImage(textures.backgroundTexture[5], new PointF(i * rockBackgroundTextureWidth - offsetX, startOffsetY + j * rockBackgroundTextureHeight - offsetY));
                    }
                }
            }
        }

        protected void DrawWalls(World world, System.Drawing.Rectangle region, Graphics g, TextureSet textures) {
            for (int y = region.Top; y < region.Bottom; y++) {
                for (int x = region.Left; x < region.Right; x++) {
                    if (world.tile[x, y].Wall > 0) {
                        System.Drawing.RectangleF rectangle3 = new System.Drawing.RectangleF(world.tile[x, y].WallFrameX * 2, world.tile[x, y].WallFrameY * 2, 32, 32);
                        g.DrawImage(textures.wallTexture[world.tile[x, y].Wall], new System.Drawing.RectangleF((x - region.Left) * 16 - 8, (y - region.Top) * 16 - 8, 32, 32), rectangle3, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        protected void DrawWater(World world, System.Drawing.Rectangle region, bool background, Graphics g, TextureSet textures) {
            for (int y = region.Top; y < region.Bottom; y++) {
                for (int x = region.Left; x < region.Right; x++) {
                    if (y == 0) {
                        continue;
                    }

                    // Skip if the tile contains no liquid.
                    if (world.tile[x, y].Liquid <= 0) {
                        continue;
                    }

                    // What is the liquid level of the tile?
                    float liquidLevel = 256 - world.tile[x, y].Liquid;

                    liquidLevel /= 32f;
                    
                    int liquidTextureIndex = 0;

                    if (world.tile[x, y].Lava) {
                        liquidTextureIndex = 1;
                    }

                    float intensity = 0.5f;

                    if (background) {
                        intensity = 1f;
                    }

                    Vector2 vector = new Vector2((float) (x * 16), (float) ((y * 16) + (((int) liquidLevel) * 2)));
                    
                    Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 16, 16 - (((int) liquidLevel) * 2));
                    
                    if ((world.tile[x, y + 1].Liquid < 245) && ((!world.tile[x, y + 1].Active || !World.tileSolid[world.tile[x, y + 1].Type]) || World.tileSolidTop[world.tile[x, y + 1].Type])) {
                        float num10 = 256 - world.tile[x, y + 1].Liquid;
                        num10 /= 32f;
                        intensity = (0.5f * (8f - liquidLevel)) / 4f;
                        if (intensity > 0.55) {
                            intensity = 0.55f;
                        }
                        if (intensity < 0.35) {
                            intensity = 0.35f;
                        }
                        float num11 = liquidLevel / 2f;
                        if (world.tile[x, y + 1].Liquid < 200) {
                            if (background) {
                                continue;
                            }
                            if ((world.tile[x, y - 1].Liquid > 0) && (world.tile[x, y - 1].Liquid > 0)) {
                                rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
                                intensity = 0.5f;
                            } else if (world.tile[x, y - 1].Liquid > 0) {
                                vector = new Vector2((float) (x * 16), (float) ((y * 16) + 4));
                                rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
                                intensity = 0.5f;
                            } else if (world.tile[x, y + 1].Liquid > 0) {
                                vector = new Vector2((float) (x * 16), (float) (((y * 16) + (((int) liquidLevel) * 2)) + (((int) num10) * 2)));
                                rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (((int) liquidLevel) * 2));
                            } else {
                                vector = new Vector2((float) ((x * 16) + ((int) num11)), (float) (((y * 16) + (((int) num11) * 2)) + (((int) num10) * 2)));
                                rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16 - (((int) num11) * 2), 16 - (((int) num11) * 2));
                            }
                        } else {
                            intensity = 0.5f;
                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, (16 - (((int) liquidLevel) * 2)) + (((int) num10) * 2));
                        }
                    } else if (world.tile[x, y - 1].Liquid > 32) {
                        rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, rectangle.Width, rectangle.Height - 4);
                    } else if (((liquidLevel < 1f) && world.tile[x, y - 1].Active) && (World.tileSolid[world.tile[x, y - 1].Type] && !World.tileSolidTop[world.tile[x, y - 1].Type])) {
                        vector = new Vector2((float) (x * 16), (float) (y * 16));
                        rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - 4);
                    } else {
                        bool flag = true;
                        for (int k = y + 1; k < (y + 6); k++) {
                            if ((world.tile[x, k].Active && World.tileSolid[world.tile[x, k].Type]) && !World.tileSolidTop[world.tile[x, k].Type]) {
                                break;
                            }
                            if (world.tile[x, k].Liquid < 200) {
                                flag = false;
                                break;
                            }
                        }
                        if (!flag) {
                            intensity = 0.5f;
                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - 4);
                        } else if (world.tile[x, y - 1].Liquid > 0) {
                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 2, rectangle.Width, rectangle.Height - 2);
                        }
                    }
                    if (world.tile[x, y].Lava) {
                        intensity *= 1.6f;
                    }

                    if (intensity > 1f) {
                        intensity = 1f;
                    }

                    ColorMatrix colorMatrix = new ColorMatrix();
                    colorMatrix.Matrix33 = intensity;

                    ImageAttributes imageAttributes = new ImageAttributes();
                    imageAttributes.SetColorMatrix(colorMatrix);

                    int waterWidth = 16;
                    int waterHeight = 16 - ((int) vector.Y - (y * 16));

                    g.DrawImage(textures.liquidTexture[liquidTextureIndex], new System.Drawing.Rectangle((int) (vector.X - region.Left * 16), (int) (vector.Y - region.Top * 16), waterWidth, waterHeight), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
        }

        private void DrawTiles(World world, System.Drawing.Rectangle region, bool foreground, Graphics g, TextureSet textures) {
            int height = 16;
            int width = 16;
            bool solidOnly = !foreground;

            int frameOverlap = 4;
            int frameOverlapTimes2 = frameOverlap * 2;

            int startX = region.Left - frameOverlap;
            int startY = region.Top - frameOverlap;
            int endX = region.Right + frameOverlapTimes2;
            int endY = region.Bottom + frameOverlapTimes2;

            if (startX < 0) {
                startX = 0;
            }

            if (startY < 0) {
                startY = 0;
            }

            if (endX >= world.Size.Width) {
                endX = world.Size.Width - 1;
            }

            if (endY >= world.Size.Height) {
                endY = world.Size.Height - 1;
            }

            for (int y = startY; y < endY; y++) {
                for (int x = startX; x < endX; x++) {
                    if (world.tile[x, y].Active && (World.tileSolid[world.tile[x, y].Type] == solidOnly)) {
                        int num9 = 0;

                        // Pot
                        if (world.tile[x, y].Type == 78) {
                            num9 = 2;
                        }

                        // Red candle, blue candle.
                        if ((world.tile[x, y].Type == 33) || (world.tile[x, y].Type == 49)) {
                            num9 = -4;
                        }

                        // Flowers, torches, trees, fungus, red candle, blue candle, grass, mushrooms.
                        if ((((world.tile[x, y].Type == 3) || (world.tile[x, y].Type == 4)) || ((world.tile[x, y].Type == 5) || (world.tile[x, y].Type == 24))) || (((world.tile[x, y].Type == 33) || (world.tile[x, y].Type == 49)) || ((world.tile[x, y].Type == 61) || (world.tile[x, y].Type == 71)))) {
                            height = 20;

                        // Chairs, table, anvil, forge, workbench, sappling, chest, demon forge, sunflowers, corrupted thorns, thorns, ?, hell forge
                        } else if (((((world.tile[x, y].Type == 15) || (world.tile[x, y].Type == 14)) || ((world.tile[x, y].Type == 16) || (world.tile[x, y].Type == 17))) || (((world.tile[x, y].Type == 18) || (world.tile[x, y].Type == 20)) || ((world.tile[x, y].Type == 21) || (world.tile[x, y].Type == 26)))) || (((world.tile[x, y].Type == 27) || (world.tile[x, y].Type == 32)) || (((world.tile[x, y].Type == 69) || (world.tile[x, y].Type == 72)) || (world.tile[x, y].Type == 77) || (world.tile[x, y].Type == 80)))) {
                            height = 18;
                        } else {
                            height = 16;
                        }

                        // Torch, tree
                        if ((world.tile[x, y].Type == 4) || (world.tile[x, y].Type == 5)) {
                            width = 20;
                        } else {
                            width = 16;
                        }

                        // Grass, grass.
                        if ((world.tile[x, y].Type == 73) || (world.tile[x, y].Type == 74)) {
                            num9 -= 12;
                            height = 32;
                        }

                        // Draw tree tops and branches.
                        if (((world.tile[x, y].Type == 5) && (world.tile[x, y].FrameY >= 198)) && (world.tile[x, y].FrameX >= 22)) {
                            int num11 = 0;
                            if (world.tile[x, y].FrameX == 22) {
                                if (world.tile[x, y].FrameY == 220) {
                                    num11 = 1;
                                } else if (world.tile[x, y].FrameY == 242) {
                                    num11 = 2;
                                }
                                int num14 = 0;
                                int num15 = 80;
                                int num16 = 80;
                                int num17 = 0x20;
                                for (int k = y; k < (y + 100); k++) {
                                    if (world.tile[x, k].Type == 2) {
                                        num14 = 0;
                                        break;
                                    }
                                    if (world.tile[x, k].Type == 0x17) {
                                        num14 = 1;
                                        break;
                                    }
                                    if (world.tile[x, k].Type == 60) {
                                        num14 = 2;
                                        num15 = 0x72;
                                        num16 = 0x60;
                                        num17 = 0x30;
                                        break;
                                    }
                                }
                                g.DrawImage(textures.treeTopTexture[num14], (x - region.Left) * 16 - num17, (y - region.Top) * 16 - num16 + 16, new RectangleF(num11 * (num15 + 2), 0, num15, num16), GraphicsUnit.Pixel);
                            } else if (world.tile[x, y].FrameX == 44) {
                                if (world.tile[x, y].FrameY == 220) {
                                    num11 = 1;
                                } else if (world.tile[x, y].FrameY == 242) {
                                    num11 = 2;
                                }
                                int num19 = 0;
                                for (int m = y; m < (y + 100); m++) {
                                    if (world.tile[x, m].Type == 2) {
                                        num19 = 0;
                                        break;
                                    }
                                    if (world.tile[x, m].Type == 0x17) {
                                        num19 = 1;
                                        break;
                                    }
                                    if (world.tile[x, m].Type == 60) {
                                        num19 = 2;
                                        break;
                                    }
                                }
                                g.DrawImage(textures.treeBranchTexture[num19], (x - region.Left) * 16 - 24, (y - region.Top) * 16 - 12, new RectangleF(0, num11 * 42, 40, 40), GraphicsUnit.Pixel);
                            } else if (world.tile[x, y].FrameX == 66) {
                                if (world.tile[x, y].FrameY == 220) {
                                    num11 = 1;
                                } else if (world.tile[x, y].FrameY == 242) {
                                    num11 = 2;
                                }
                                int num21 = 0;
                                for (int n = y; n < (y + 100); n++) {
                                    if (world.tile[x, n].Type == 2) {
                                        num21 = 0;
                                        break;
                                    }
                                    if (world.tile[x, n].Type == 0x17) {
                                        num21 = 1;
                                        break;
                                    }
                                    if (world.tile[x, n].Type == 60) {
                                        num21 = 2;
                                        break;
                                    }
                                }
                                g.DrawImage(textures.treeBranchTexture[num21], (x - region.Left) * 16, (y - region.Top) * 16 - 12, new RectangleF(42, num11 * 42, 40, 40), GraphicsUnit.Pixel);
                            }
                        }

                        // Drawn mushroom tops.
                        if ((world.tile[x, y].Type == 72) && (world.tile[x, y].FrameX >= 36)) {
                            int num12 = 0;
                            if (world.tile[x, y].FrameY == 18) {
                                num12 = 1;
                            } else if (world.tile[x, y].FrameY == 36) {
                                num12 = 2;
                            }
                            g.DrawImage(textures.shroomCapTexture, (x - region.Left) * 16 - 22, (y - region.Top) * 16 - 26, new RectangleF(num12 * 62, 0, 60, 42), GraphicsUnit.Pixel);
                        }

                        // Draw liquids behind solids.
                        if (solidOnly && (x > 1 && y > 1 && x < world.Size.Width - 2 && y < world.Size.Height - 2) && (((world.tile[x - 1, y].Liquid > 0) || (world.tile[x + 1, y].Liquid > 0)) || ((world.tile[x, y - 1].Liquid > 0) || (world.tile[x, y + 1].Liquid > 0)))) {
                            int liquidLevel = 0;
                            bool liquidToLeft = false;
                            bool liquidToRight = false;
                            bool liquidAbove = false;
                            bool liquidBelow = false;
                            bool hasLava = false;
                            bool hasWater = false;

                            if (world.tile[x - 1, y].Liquid > 0) {
                                liquidToLeft = true;

                                if (world.tile[x - 1, y].Liquid > liquidLevel) {
                                    liquidLevel = world.tile[x - 1, y].Liquid;
                                }

                                if (world.tile[x - 1, y].Lava) {
                                    hasLava = true;
                                } else {
                                    hasWater = true;
                                }
                            }

                            if (world.tile[x + 1, y].Liquid > 0) {
                                liquidToRight = true;
                                liquidLevel = world.tile[x + 1, y].Liquid;

                                if (world.tile[x + 1, y].Lava) {
                                    hasLava = true;
                                } else {
                                    hasWater = true;
                                }
                            }

                            if (world.tile[x, y - 1].Liquid > 0) {
                                liquidAbove = true;

                                if (world.tile[x, y - 1].Lava) {
                                    hasLava = true;
                                } else {
                                    hasWater = true;
                                }
                            }

                            if (world.tile[x, y + 1].Liquid > 0) {
                                if (world.tile[x, y + 1].Lava) {
                                    hasLava = true;
                                } else {
                                    hasWater = true;
                                }

                                if (world.tile[x, y + 1].Liquid > 240) {
                                    liquidBelow = true;
                                }                                    
                            }

                            if (!hasWater || !hasLava) {
                                float num15 = 0f;
                                Vector2 vector = new Vector2((float) (x * 16), (float) (y * 16));
                                Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - 4);
                                if (liquidBelow && (liquidToLeft || liquidToRight)) {
                                    liquidToLeft = true;
                                    liquidToRight = true;
                                }
                                if ((!liquidAbove || (!liquidToLeft && !liquidToRight)) && (!liquidBelow || !liquidAbove)) {
                                    if (liquidAbove) {
                                        rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
                                    } else if ((liquidBelow && !liquidToLeft) && !liquidToRight) {
                                        vector = new Vector2((float) (x * 16), (float) ((y * 16) + 12));
                                        rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
                                    } else {
                                        num15 = 256 - liquidLevel;
                                        num15 /= 32f;
                                        if (liquidToLeft && liquidToRight) {
                                            vector = new Vector2((float) (x * 16), (float) ((y * 16) + (((int) num15) * 2)));
                                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (((int) num15) * 2) - 4);
                                        } else if (liquidToLeft) {
                                            vector = new Vector2((float) (x * 16), (float) ((y * 16) + (((int) num15) * 2)));
                                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 4, 16 - (((int) num15) * 2) - 4);
                                        } else {
                                            vector = new Vector2((float) (x * 16), (float) ((y * 16) + (((int) num15) * 2)));
                                            rectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 4, 16 - (((int) num15) * 2) - 4);
                                        }
                                    }
                                }

                                float intensity = 0.5f;
                                int liquidTextureIndex = hasWater ? 0 : 1;

                                if (liquidTextureIndex == 1) {
                                    intensity *= 1.6f;
                                }

                                if ((y < world.worldSurface) || (intensity > 1f)) {
                                    intensity = 1f;
                                }

                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = intensity;

                                ImageAttributes imageAttributes = new ImageAttributes();
                                imageAttributes.SetColorMatrix(colorMatrix);

                                int waterWidth = 16;
                                int waterHeight = 16 - ((int) vector.Y - (y * 16));

                                g.DrawImage(textures.liquidTexture[liquidTextureIndex], new System.Drawing.Rectangle((int) (vector.X - region.Left * 16), (int) (vector.Y - region.Top * 16), waterWidth, waterHeight), rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
                            }
                        }

                        g.DrawImage(textures.tileTexture[world.tile[x, y].Type], (x - region.Left) * 16 - (width - 16) / 2, (y - region.Top) * 16 + num9, new RectangleF(world.tile[x, y].FrameX, world.tile[x, y].FrameY, width, height), GraphicsUnit.Pixel);
                    }
                }
            }
        }

        private Image CreateBlockImage(World world, System.Drawing.Rectangle region, float zoomLevel, MapGeneratorLayerOptions layer) {
            Image blockImage = new Bitmap(16 * options.BlockSize.Width, 16 * options.BlockSize.Height, PixelFormat.Format32bppArgb);

            TextureSet textures = null;

            lock (availableTextureSetLock) {
                if (availableTextureSet.Count == 0) {
                    System.Diagnostics.Trace.WriteLine("Creating new texture set for thread: " + Thread.CurrentThread.ManagedThreadId);
                    textures = this.textures.Copy();
                } else {
                    textures = availableTextureSet.Dequeue();
                }
            }

            using (Graphics g = Graphics.FromImage(blockImage)) {
                g.Clip = new Region(new RectangleF(0, 0, region.Width * 16 * zoomLevel, region.Height * 16 * zoomLevel));
                g.ScaleTransform(zoomLevel, zoomLevel);

                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.CompositingMode = CompositingMode.SourceOver;

                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.CompositingQuality = CompositingQuality.HighSpeed;

                if (layer.DrawBackground) {
                    DrawSky(world, region, g);
                }

                if (layer.DrawBackgroundWater) {
                    DrawWater(world, region, true, g, textures);
                }

                if (layer.DrawBackground) {
                    DrawBackground(world, region, g, textures);
                }

                if (layer.DrawWalls) {
                    DrawWalls(world, region, g, textures);
                }

                if (layer.DrawBackgroundTiles) {
                    DrawTiles(world, region, false, g, textures);
                }

                if (layer.DrawForegroundTiles) {
                    DrawTiles(world, region, true, g, textures);
                }

                if (layer.DrawForegroundWater) {
                    DrawWater(world, region, false, g, textures);
                }
            }

            lock (availableTextureSetLock) {
                availableTextureSet.Enqueue(textures);
            }

            return blockImage;
        }

        private void CreateBlockImages(World world, int blockX, int blockY, int blockWidth, int blockHeight, float zoomLevel) {
            // Caluclate the first and last tiles
            int firstTileX = blockX * blockWidth;
            int firstTileY = blockY * blockHeight;
            int lastTileX = firstTileX + blockWidth;
            int lastTileY = firstTileY + blockHeight;

            if (lastTileX > world.Size.Width) {
                lastTileX = world.Size.Width;
            }

            if (lastTileY > world.Size.Height) {
                lastTileY = world.Size.Height;
            }

            // The region that should be drawn.
            System.Drawing.Rectangle region = System.Drawing.Rectangle.FromLTRB(firstTileX, firstTileY, lastTileX, lastTileY);

            int layerIndex = 0;

            foreach (MapGeneratorLayerOptions layer in options.Layers) {
                using (Image blockImage = CreateBlockImage(world, region, zoomLevel, layer)) {
                    // Construct the path to the target file.
                    string path = string.Format("{0}\\Zoom {1}\\Layer {2}\\{3}", options.OutputDirectory, zoomLevel, layerIndex, blockX);
                    Directory.CreateDirectory(path);

                    string blockFile = string.Format("{0}\\{1}.png", path, blockY);
                    blockImage.Save(blockFile, ImageFormat.Png);
                }

                ++layerIndex;
            }
        }

        private Dictionary<TileType, List<System.Drawing.Point>> FindMapObjects(World world) {
            Dictionary<TileType, List<System.Drawing.Point>> mapObjects = new Dictionary<TileType, List<System.Drawing.Point>>();

            mapObjects.Add(TileType.HeartContainer, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.Anvil, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.Forge, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.Workbench, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.Alter, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.ShadowOrb, new List<System.Drawing.Point>());
            mapObjects.Add(TileType.HellForge, new List<System.Drawing.Point>());

            for (int x = 0; x < world.Size.Width; ++x) {
                for (int y = 0; y < world.Size.Height; ++y) {
                    // Only record the top left coordinate.
                    if (world.tile[x, y].FrameX != 0 || world.tile[x, y].FrameY != 0) {
                        continue;
                    }

                    TileType tileType = (TileType) world.tile[x, y].Type;

                    if (mapObjects.ContainsKey(tileType)) {
                        mapObjects[tileType].Add(new System.Drawing.Point(x, y));
                    }
                }
            }

            return mapObjects;
        }

        private bool IsNameValid(string name) {
            return !name.Any(c => c < 32 || c > 128);
        }

        private void WriteMapData(World world) {
            string dataXmlPath = Path.Combine(options.OutputDirectory, "MapData.xml");

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "\t";

            using (XmlWriter writer = XmlWriter.Create(dataXmlPath, xmlWriterSettings)) {
                writer.WriteStartElement("World");
                writer.WriteAttributeString("Name", world.Name);
                writer.WriteAttributeString("Size", string.Format("{0},{1}", world.Size.Width, world.Size.Height));
                writer.WriteAttributeString("Bounds", string.Format("{0},{1},{2},{3}", world.leftWorld, world.topWorld, world.rightWorld, world.bottomWorld));
                writer.WriteAttributeString("Spawn", string.Format("{0},{1}", world.Spawn.X, world.Spawn.Y));
                writer.WriteAttributeString("Spawn_X", string.Format("{0}", world.Spawn.X));
                writer.WriteAttributeString("Spawn_Y", string.Format("{0}", world.Spawn.Y));

                writer.WriteStartElement("Npcs");

                foreach (Npc npc in world.npc) {
                    if (npc == null) {
                        continue;
                    }

                    writer.WriteStartElement("Npc");
                    writer.WriteAttributeString("Name", npc.Name);
                    writer.WriteAttributeString("Location", string.Format("{0},{1}", npc.Position.X, npc.Position.Y));
                    writer.WriteAttributeString("Location_X", npc.Position.X.ToString());
                    writer.WriteAttributeString("Location_Y", npc.Position.Y.ToString());
                    writer.WriteAttributeString("Home", string.Format("{0},{1}", npc.HomePosition.X, npc.HomePosition.Y));
                    writer.WriteAttributeString("Homeless", npc.IsHomeless.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteStartElement("Chests");

                foreach (Chest chest in world.chest) {
                    if (chest == null) {
                        continue;
                    }

                    writer.WriteStartElement("Chest");
                    writer.WriteAttributeString("Location", string.Format("{0},{1}", chest.Position.X, chest.Position.Y));
                    writer.WriteAttributeString("Location_X", chest.Position.X.ToString());
                    writer.WriteAttributeString("Location_Y", chest.Position.Y.ToString());
                    foreach (Item item in chest.Items) {
                        if (item == null) {
                            continue;
                        }

                        writer.WriteStartElement("Item");

                        if (!IsNameValid(item.Name)) {
                            writer.WriteAttributeString("Name", "Invalid");
                        } else {
                            writer.WriteAttributeString("Name", World.itemNames[Int32.Parse(item.Name)]);
                        }

                        writer.WriteAttributeString("Count", item.Count.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteStartElement("Signs");

                foreach (Sign sign in world.sign) {
                    if (sign == null) {
                        continue;
                    }

                    writer.WriteStartElement("Sign");
                    writer.WriteAttributeString("Location", string.Format("{0},{1}", sign.Position.X, sign.Position.Y));
                    writer.WriteAttributeString("Location_X", sign.Position.X.ToString());
                    writer.WriteAttributeString("Location_Y", sign.Position.Y.ToString());
                    writer.WriteCData(sign.Text);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                Dictionary<TileType, List<System.Drawing.Point>> mapObjects = FindMapObjects(world);

                foreach (var entry in mapObjects) {
                    TileTypeInfo tileTypeInfo = TileTypeInfo.GetTileTypeInfo(entry.Key);

                    writer.WriteStartElement(entry.Key.ToString() + "s");
                    writer.WriteAttributeString("Name", tileTypeInfo.Name);

                    foreach (System.Drawing.Point location in entry.Value) {
                        writer.WriteStartElement(entry.Key.ToString());
                        writer.WriteAttributeString("Location", string.Format("{0},{1}", location.X, location.Y));
                        writer.WriteAttributeString("Location_X", location.X.ToString());
                        writer.WriteAttributeString("Location_Y", location.Y.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        private void WriteMapHtml(World world) {
            string mapHtmlPath = Path.Combine(options.OutputDirectory, "Map.html");

            string basePath = "file:///" + options.OutputDirectory.Replace("\\", "/").Replace(" ", "%20");

            if (!basePath.EndsWith("/")) {
                basePath += "/";
            }

            File.WriteAllText(mapHtmlPath, Properties.Resources.Map.Replace("$(BasePath)", basePath));

            string terrariaJsPath = Path.Combine(options.OutputDirectory, "terraria.js");
            string utilJsPath = Path.Combine(options.OutputDirectory, "util.js");

            File.WriteAllText(terrariaJsPath, Properties.Resources.Terraria.Replace("var origin = translateGameXYToLatLng(0, 600);", "var origin = translateGameXYToLatLng(" + world.Spawn.X + ", " + world.Spawn.Y + ");"));
            File.WriteAllText(utilJsPath, Properties.Resources.Utilities);
        }

        public void Generate(MapGeneratorProgressHandler progressHandler) {
            LoadTextures();

            progressHandler.SubTaskName = "Loading world";

            World world = new World(options.WorldPath);

            progressHandler.WriteLine("  World Name:    {0}", world.Name);
            progressHandler.WriteLine("  World Version: {0}", world.worldVersion);
            progressHandler.WriteLine("  World Size:    ({0}, {1})", world.Size.Width, world.Size.Height);
            progressHandler.WriteLine("  World Bounds:  ({0}, {1}, {2}, {3})", world.leftWorld, world.topWorld, world.rightWorld, world.bottomWorld);

            Directory.CreateDirectory(options.OutputDirectory);

            if (options.WriteMapData) {
                progressHandler.WriteLine("Writing map data...");
                WriteMapData(world);
            }

            progressHandler.TaskCount = options.ZoomLevels.Count;
            progressHandler.TasksCompleted = 0;

            ParallelOptions parallelOptions = new ParallelOptions();

            if (options.ParallelizeGeneration) {
                parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            } else {
                parallelOptions.MaxDegreeOfParallelism = 1;
            }

            foreach (float zoomLevel in options.ZoomLevels.OrderBy(z => z)) {
                if (progressHandler.Cancel) {
                    return;
                }

                int blockWidth = (int) (options.BlockSize.Width / zoomLevel);
                int blockHeight = (int) (options.BlockSize.Height / zoomLevel);

                int xBlocks = (int) Math.Ceiling(world.Size.Width / (float) blockWidth);
                int yBlocks = (int) Math.Ceiling(world.Size.Height / (float) blockHeight);

                string taskName = string.Format("Generating Zoom Level {0} ({1}x{2})...", zoomLevel, xBlocks, yBlocks);
                progressHandler.SubTaskName = taskName;
                progressHandler.SubTasksCompleted = 0;
                progressHandler.SubTaskCount = xBlocks * yBlocks;

                Parallel.For(0, xBlocks, parallelOptions,
                    i => {
                        for (int j = 0; j < yBlocks; ++j) {
                            if (progressHandler.Cancel) {
                                return;
                            }
                            progressHandler.WriteLine(string.Format("  Writing Block {0},{1}", i, j));
                            CreateBlockImages(world, i, j, blockWidth, blockHeight, zoomLevel);
                            ++progressHandler.SubTasksCompleted;
                        }
                    });

                ++progressHandler.TasksCompleted;
            }

            WriteMapHtml(world);

            progressHandler.WriteLine("Done");
        }

        #endregion

        #region TextureSet Class

        public class TextureSet {
            #region Members

            private object copyLock = new object();

            public Image[] backgroundTexture = new Image[32];
            public Image[] cloudTexture = new Image[4];
            public Image[] liquidTexture = new Image[2];
            public Image[] tileTexture = new Image[World.NumTiles];
            public Image[] wallTexture = new Image[116];
            public Image[] npcTexture = new Image[147];
            public Image[] treeBranchTexture = new Image[5];
            public Image[] treeTopTexture = new Image[5];
            public Image sunTexture;
            public Image moonTexture;
            public Image shroomCapTexture;

            #endregion

            #region Members

            public TextureSet Copy() {
                lock (copyLock) {
                    TextureSet newSet = new TextureSet();

                    for (int i = 0; i < World.NumTiles; i++) {
                        newSet.tileTexture[i] = tileTexture[i].Clone() as Image;
                    }

                    for (int i = 1; i < wallTexture.Length; i++) {
                        newSet.wallTexture[i] = wallTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < backgroundTexture.Length; i++) {
                        newSet.backgroundTexture[i] = backgroundTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < npcTexture.Length; i++) {
                        newSet.npcTexture[i] = npcTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < cloudTexture.Length; i++) {
                        newSet.cloudTexture[i] = cloudTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < liquidTexture.Length; i++) {
                        newSet.liquidTexture[i] = liquidTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < treeTopTexture.Length; i++) {
                        newSet.treeTopTexture[i] = treeTopTexture[i].Clone() as Image;
                    }

                    for (int i = 0; i < treeBranchTexture.Length; i++) {
                        newSet.treeBranchTexture[i] = treeBranchTexture[i].Clone() as Image;
                    }

                    newSet.sunTexture = sunTexture.Clone() as Image;
                    newSet.moonTexture = moonTexture.Clone() as Image;
                    newSet.shroomCapTexture = shroomCapTexture.Clone() as Image;

                    return newSet;
                }
            }

            #endregion
        }

        #endregion
    }
}
