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
    public class Screen
    {
        public Bitmap ScreenMap { get; private set; }
        private int width;
        private int height;
        public int XOffset { get; set; } = 0;
        public int YOffset { get; set; } = 0;
        public byte[] Pixels { get; private set; }

        public Screen(int width, int height)
        {
            this.width = width;
            this.height = height;
            Pixels = new byte[width * height * 4];
            for (int i = 0; i < width * height * 4; i++)
            {
                Pixels[i] = 0;
            }
            ScreenMap = new Bitmap(width, height);
            Sprite.Init();
        }
        public void RenderX(int xp, int yp, int lenght, bool isfixed)
        {
            //Parallel to be used in future
            if (!isfixed)
            {
                xp -= XOffset;
                yp -= YOffset;
            }
            for (int x = 0; x < lenght; x += 4)
            {
                int xa = x + xp * 4;
                if (xa < 0 || xa >= width * 4 || yp < 0 || yp >= height)
                    continue;
                //is pink to be added
                Colors colors = new Colors();
                colors.Blue = 0;
                colors.Green = 0;
                colors.Red = 255;
                colors.Alpha = 255;
                Pixels[(xa) + yp * width * 4] = colors.Blue;
                Pixels[(xa + 1) + yp * width * 4] = colors.Green;
                Pixels[(xa + 2) + yp * width * 4] = colors.Red;
                Pixels[(xa + 3) + yp * width * 4] = colors.Alpha;

            }
        }
        public void RenderY(int xp, int yp, int lenght, bool isfixed)
        {
            //Parallel to be used in future
            if (!isfixed)
            {
                xp -= XOffset;
                yp -= YOffset;
            }
            xp *= 4;
            for (int y = 0; y < lenght; y++)
            {
                int ya = y + yp;
                if (xp < 0 || xp >= width * 4 || ya < 0 || ya >= height)
                    continue;
                //is pink to be added
                Colors colors = new Colors();
                colors.Blue = 0;
                colors.Green = 0;
                colors.Red = 255;
                colors.Alpha = 255;
                Pixels[(xp) + ya * width * 4] = colors.Blue;
                Pixels[(xp + 1) + ya * width * 4] = colors.Green;
                Pixels[(xp + 2) + ya * width * 4] = colors.Red;
                Pixels[(xp + 3) + ya * width * 4] = colors.Alpha;

            }
        }
        public void RenderPixel(int xp, int yp, bool isfixed)
        {
            //Parallel to be used in future
            if (!isfixed)
            {
                xp -= XOffset;
                yp -= YOffset;
            }
            if (xp < 0 || xp >= width * 4 || yp < 0 || yp >= height)
                return;
            //is pink to be added
            Colors colors = new Colors();
            colors.Blue = 0;
            colors.Green = 0;
            colors.Red = 255;
            colors.Alpha = 255;
            Pixels[(xp) + yp * width * 4] = colors.Blue;
            Pixels[(xp + 1) + yp * width * 4] = colors.Green;
            Pixels[(xp + 2) + yp * width * 4] = colors.Red;
            Pixels[(xp + 3) + yp * width * 4] = colors.Alpha;

        }

        public void RenderSprite(int xp, int yp, Sprite sprite, bool isfixed)
        {
            //Parallel to be used in future
            if (!isfixed)
            {
                xp -= XOffset;
                yp -= YOffset;
            }
            int spritePixelsWidth = sprite.Width * 4;

            for (int y = 0; y < sprite.Height; y++)
            {
                int ya = y + yp;
                for (int x = 0; x < spritePixelsWidth; x += 4)
                {
                    int xa = x + xp * 4;
                    if (xa < 0 || xa >= width * 4 || ya < 0 || ya >= height)
                        continue;
                    //is pink to be added
                    Colors colors = new Colors();
                    colors.Blue = sprite.Pixels[(x) + y * spritePixelsWidth];
                    colors.Green = sprite.Pixels[(x + 1) + y * spritePixelsWidth];
                    colors.Red = sprite.Pixels[(x + 2) + y * spritePixelsWidth];
                    colors.Alpha = sprite.Pixels[(x + 3) + y * spritePixelsWidth];
                    if (colors.isPink())
                        continue;
                    Pixels[(xa) + ya * width * 4] = colors.Blue;
                    Pixels[(xa + 1) + ya * width * 4] = colors.Green;
                    Pixels[(xa + 2) + ya * width * 4] = colors.Red;
                    Pixels[(xa + 3) + ya * width * 4] = colors.Alpha;

                }
            }
            /* OLD RENDER
        public void RenderSprite(int xp, int yp, Sprite sprite, bool isfixed)
        {
            
            if (!isfixed)
            {
                xp -= XOffset;
                yp -= YOffset;
            }

            unsafe
            {
                BitmapData bitmapData = sprite.Bitmap.LockBits(new Rectangle(0, 0, sprite.Bitmap.Width, sprite.Bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData screen = ScreenMap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8; // 4
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                byte* PtrFirstPixelS = (byte*)screen.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    byte* currentLineS = PtrFirstPixelS + ((y + yp) * screen.Stride);
                    int ya = y + yp;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int xa = x + xp * bytesPerPixel;
                        if (xa < 0 || xa >= width  || ya < 0 || ya >= height)
                            continue;
                        Colors colors = new Colors();
                        colors.Blue = currentLine[x];
                        colors.Green = currentLine[x + 1];
                        colors.Red = currentLine[x + 2];
                        colors.Alpha = currentLine[x + 3];
                        if (colors.isPink())
                            continue;
                        currentLineS[xa] = colors.Blue;
                        currentLineS[xa + 1] = colors.Green;
                        currentLineS[xa + 2] = colors.Red;
                        currentLineS[xa + 3] = colors.Alpha;
                    }
                });
                ScreenMap.UnlockBits(screen);
                sprite.Bitmap.UnlockBits(bitmapData);
            }
        }*/
            /*Console.WriteLine("New");
            for (int y = 0; y < height; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < width * 4; x += 4)
                {
                    Console.Write(Pixels[x + y * width * 4] + " " + Pixels[(x + 1) + y * width * 4] + " " + Pixels[(x + 2) + y * width * 4] + " " + Pixels[(x + 3) + y * width * 4] + " ");
                }
            }*/
        }
        public void PixelsToBitmap()
        {

            unsafe
            {
                BitmapData bitmapData = ScreenMap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8; // 4
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;


                //Parallel.For(0, heightInPixels, y =>
                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        currentLine[x] = Pixels[(x) + y * widthInBytes];
                        currentLine[x + 1] = Pixels[(x + 1) + y * widthInBytes];
                        currentLine[x + 2] = Pixels[(x + 2) + y * widthInBytes];
                        currentLine[x + 3] = Pixels[(x + 3) + y * widthInBytes];

                    }
                }
                ScreenMap.UnlockBits(bitmapData);
            }
        }
        public struct Colors
        {
            public byte Alpha { get; set; }
            public byte Red { get; set; }
            public byte Green { get; set; }
            public byte Blue { get; set; }

            public bool isGreen()
            {
                if (Red == 0 && Green == 255 && Blue == 0)
                    return true;
                return false;
            }
            public bool isWall()
            {
                if (Red == 64 && Green == 64 && Blue == 64)
                    return true;
                return false;
            }
            public bool isPlayer()
            {
                if (Red == 0 && Green == 0 && Blue == 255)
                    return true;
                return false;
            }
            public bool isPink()
            {
                if (Red == 255 && Green == 0 && Blue == 255)
                    return true;
                return false;
            }
            public bool isCoin()
            {
                if (Red == 255 && Green == 255 && Blue == 0)
                    return true;
                return false;
            }
            public bool isCoinBack()
            {
                if (Red == 255 && Green == 255 && Blue == 200)
                    return true;
                return false;
            }
            public bool isWallBack()
            {
                if (Red == 150 && Green == 150 && Blue == 150)
                {
                    return true;
                }
                return false;
            }
            public bool isGoblin()
            {
                if (Red == 0 && Green == 255 && Blue == 50)
                {
                    return true;
                }
                return false;
            }
            public bool isGoblinBack()
            {
                if (Red == 0 && Green == 255 && Blue == 100)
                {
                    return true;
                }
                return false;
            }
            public bool isFirehead()
            {
                if (Red == 255 && Green == 100 && Blue == 0)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
