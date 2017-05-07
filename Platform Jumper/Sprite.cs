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
        public static Sprite Background = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\background.gif"));
        public static Sprite Wall1 = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\wall1.png"));
        public static Sprite Wall1Back = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\wall1Back.png"));
        public static Sprite PlayerRight = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\playerRight.png"));
        public static Sprite PlayerLeft = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\playerLeft.png"));
        public static Sprite Backgound1 = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\bg1.png"));
        public static Sprite Coin= new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\Coin.png"));
        public static Sprite Score = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\score.png"));
        public static Sprite GoblinLeft = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\goblinLeft.png"));
        public static Sprite GoblinRight = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\goblinRight.png"));
        public static Sprite Firehead = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\firehead.png"));
        public static Sprite Hearth = new Sprite(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Textures\hearth.png"));

       
        public Sprite(Bitmap bitmap)
        {
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
