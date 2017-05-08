using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public class Sprite
    {
        public byte[] Pixels { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        //loaded Bitmaps 
        public static Sprite Background = new Sprite(@"\Textures\background.gif");
        public static Sprite Wall1 = new Sprite(@"\Textures\wall1.png");
        public static Sprite Wall1Back = new Sprite(@"\Textures\wall1Back.png");
        public static Sprite PlayerRight = new Sprite(@"\Textures\playerRight.png");
        public static Sprite PlayerLeft = new Sprite(@"\Textures\playerLeft.png");
        public static Sprite Backgound1 = new Sprite(@"\Textures\bg1.png");
        public static Sprite Backgound2 = new Sprite(@"\Textures\bg2.png");
        public static Sprite Backgound3 = new Sprite(@"\Textures\bg3.png");
        public static Sprite Coin= new Sprite(@"\Textures\Coin.png");
        public static Sprite Score = new Sprite(@"\Textures\score.png");
        public static Sprite GoblinLeft = new Sprite(@"\Textures\goblinLeft.png");
        public static Sprite GoblinRight = new Sprite(@"\Textures\goblinRight.png");
        public static Sprite Firehead = new Sprite(@"\Textures\firehead.png");
        public static Sprite Hearth = new Sprite( @"\Textures\hearth.png");
        public static Sprite Door = new Sprite(@"\Textures\door.png");
        public static Sprite Clock = new Sprite(@"\Textures\clock.png");
        public static Sprite BgScore = new Sprite(@"\Textures\bgScore.png");
        public static Sprite Title = new Sprite(@"\Textures\title.png");
        public static Sprite Grass = new Sprite(@"\Textures\grass.png");
        public static Sprite Bars = new Sprite(@"\Textures\bars.png");
        public static Sprite Box = new Sprite(@"\Textures\box.png");
        public static Sprite Dirt = new Sprite(@"\Textures\dirt.png");
        public static Sprite HowTo = new Sprite(@"\Textures\howTo.png");


        public Sprite(string path)
        {
            Bitmap bitmap= new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+path);
            unsafe
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8; // 4
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                Width = bitmap.Width;
                Height = bitmap.Height;
                Pixels = new byte[widthInBytes * heightInPixels];
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                //Parallel.For(0, heightInPixels, y =>
                for(int y=0;y<heightInPixels;y++)
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        Pixels[(x) + y * widthInBytes] = currentLine[x];
                        Pixels[(x + 1) + y * widthInBytes] = currentLine[x + 1];
                        Pixels[(x + 2) + y * widthInBytes] = currentLine[x + 2];
                        Pixels[(x + 3) + y * widthInBytes] = currentLine[x + 3];
                    }
                }
                bitmap.UnlockBits(bitmapData);
            }
            bitmap.Dispose();
        }
    }
}
