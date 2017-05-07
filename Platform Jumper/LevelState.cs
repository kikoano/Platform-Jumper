using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Platform_Jumper
{
    public class LevelState : GameState
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int[] Tiles { get; private set; }
        private Sprite background = Sprite.Backgound1;
        private string path;
        public Player Player { get; private set; }
        public Dictionary<int, Entity> Entities { get; set; } = new Dictionary<int, Entity>();
        public List<Entity> ScreenEntities { get; set; } = new List<Entity>();
        public PlayerData PlayerData { get; set; } = new PlayerData();
        private Hud hud;
        private int playerOldX=0;
        private int playerOldY=0;
        public LevelState(GameStateManager gsm, string path) : base(gsm)
        {
            this.path = path;

        }
        public override void Init()
        {
            base.Init();
            gsm.Ingame = true;
            Tiles = LoadMapToTiles(path);
            hud = new Hud(controls, this);
        }
        private void checkIfPlayerDead()
        {

        }
        private void findScreenEntities()
        {
            int diff = Form1.HEIGHT;
            if (Player.X > playerOldX + diff || Player.X < playerOldX - diff || Player.Y > playerOldY + diff || Player.Y < playerOldY - diff)
            {
                Console.WriteLine("update");
                playerOldX = (int)Player.X;
                playerOldY = (int)Player.Y;
                ScreenEntities = new List<Entity>();
                for (int y = gsm.screen.YOffset- Form1.HEIGHT; y < gsm.screen.YOffset + Form1.HEIGHT+ Form1.HEIGHT; y += 3)
                {
                    for (int x = gsm.screen.XOffset - 16- Form1.WIDTH; x < gsm.screen.XOffset + Form1.WIDTH+ Form1.WIDTH; x += 4)
                    {
                        Entity e;
                        if (Entities.TryGetValue((x) + (y * Width), out e))
                        {
                            ScreenEntities.Add(e);
                        }
                    }
                }
            }
        }

        public override void Render()
        {
            gsm.screen.RenderSprite(0, 0, background, true);
            renderTiles();
            Player.Render(gsm.screen);
            foreach (Entity e in ScreenEntities)
            {
                e.Render(gsm.screen);
            }
            hud.Render(gsm.screen);
        }
        public override void Update()
        {
            findScreenEntities();
            updateOffsets();
            //updates ONLY WHAT IS ON SCREEN!
            Player.Update(this);
           for(int i=0;i< ScreenEntities.Count;i++)
            {
                ScreenEntities[i].Update(this);
                if (ScreenEntities[i].Removed)
                {
                    Entities.Remove((int)(ScreenEntities[i].X + ScreenEntities[i].Y * Width));
                    ScreenEntities.RemoveAt(i);
                }
            }
            hud.Update();
        }
        private void updateOffsets()
        {
            gsm.screen.XOffset = (int)Player.X - Form1.WIDTH / 2;
            gsm.screen.YOffset = (int)(Player.Y - Form1.HEIGHT / 1.5);
        }
        private void renderTiles()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Tiles[x + y * Width] == 2)
                        gsm.screen.RenderSprite(x * 16, y * 16, Sprite.Wall1, false);
                    else if (Tiles[x + y * Width] == 3)
                        gsm.screen.RenderSprite(x * 16, y * 16, Sprite.Wall1Back, false);
                }
            }
        }
        private int[] LoadMapToTiles(string path)
        {
            int[] tiles;
            Bitmap bitmap = new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Levels\" + path);
            unsafe
            {
                BitmapData mapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(mapData.PixelFormat) / 8; // 4
                int heightInPixels = mapData.Height;
                int widthInBytes = mapData.Width * bytesPerPixel;
                Width = mapData.Width;
                Height = mapData.Height;
                tiles = new int[mapData.Width * mapData.Height];

                byte* PtrFirstPixel = (byte*)mapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                //Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * mapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Screen.Colors colors = new Screen.Colors();
                        colors.Blue = currentLine[x];
                        colors.Green = currentLine[x + 1];
                        colors.Red = currentLine[x + 2];
                        colors.Alpha = currentLine[x + 3];
                        int position = (int)((x * 4) + (y * 16 * Width));
                        if (colors.isGreen())
                        {
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 1;
                        }
                        else if (colors.isWall())
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 2;
                        else if (colors.isWallBack())
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 3;
                        else if (colors.isPlayer())
                        {
                            Player = new Player(x * 4, y * 16);

                            //add Keyboard handles
                            gsm.Form.KeyDown += Player.KeyDown;
                            gsm.Form.KeyUp += Player.KeyUp;
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 0;
                        }
                        else if (colors.isCoin())
                        {
                            Entities.Add(position, new Coin(x * 4, y * 16));
                        }
                        else if (colors.isCoinBack())
                        {
                            Entities.Add(position, new Coin(x * 4, y * 16));
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 3;
                        }
                        else if (colors.isGoblin())
                        {
                            Entities.Add(position, new Goblin(x * 4, y * 16));
                        }
                        else if (colors.isGoblinBack())
                        {
                            Entities.Add(position, new Goblin(x * 4, y * 16));
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 3;
                        }
                        else if (colors.isFirehead())
                        {
                            Entities.Add(position, new Firehead(x * 4, y * 16));
                        }
                        else
                            tiles[(x / bytesPerPixel) + y * mapData.Width] = 0;
                    }
                }
                bitmap.UnlockBits(mapData);
            }
            return tiles;
        }

    }
}
