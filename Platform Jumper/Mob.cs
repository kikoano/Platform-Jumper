﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public abstract class Mob : Entity
    {
        protected Sprite sprite;
        protected bool[] movement = new bool[4];
        protected float gravity = 20f;
        public bool jump {  get;  set; }
        protected float force;
        protected float speed = 1f;
        protected bool hasHitTop = false;
        public bool falling { get; private set; } = false;
        public Mob(int x, int y) : base(x, y)
        {

        }
        public override void Update(LevelState ls)
        {
            if (jump)
            {
                //jumping
                force--;
                if (!checkCollisionTop(ls))
                    Y -= force;
                else
                {
                    force = 0;
                    hasHitTop = true;
                }
            }
            if (checkCollisionBot(ls) || Y > (ls.Height-2) * 16)
            {
                jump = false;
                hasHitTop = false;
                if(Y < (ls.Height - 2) * 16)
                insideWallBot(ls);
                falling = false;
            }
            else if (!hasHitTop)
            {
                Y += speed * 2;
                falling = true;
            }
            else
            {
                Y += 2;
                falling = true;
            }
        }
        protected bool checkCollisionBot(LevelState ls)
        {
            int xp = (int)(X + 7) / 16;
            int yp = (int)(Y) / 16;
            if (ls.Tiles[(int)xp + (int)(yp + 1) * ls.Width] > 5)
            {
                //insideWallBot(ls);
                return true;
            }
            return false;
        }
        protected bool checkCollisionTop(LevelState ls)
        {
            int xp = (int)(X+7) / 16;
            int yp = (int)(Y + 15) / 16;
            if (ls.Tiles[(int)xp + (int)(yp - 1) * ls.Width] > 5)
            {
                return true;
            }
            return false;
        }
        protected void insideWallBot(LevelState ls)
        {
            int yp = (int)Y / 16;
            int modul = (int)Y % 16;
            Y -= modul;
        }
        protected void insideWallTop(LevelState ls)
        {
            int xp = (int)X / 16;
            int yp = (int)Y / 16;
            int modul = (int)Y % 16;
            Y += modul;
        }
        protected bool checkCollisionRight(LevelState ls)
        {
            int xp = (int)(X - 3) / 16;
            int yp = (int)Y / 16;
            if (ls.Tiles[(int)xp + 1 + (int)yp * ls.Width] > 5)
            {
                return true;
            }
            return false;
        }
        protected bool checkCollisionLeft(LevelState ls)
        {
            int xp = (int)(X + 16) / 16;
            int yp = (int)Y / 16;
            if (ls.Tiles[(int)xp - 1 + (int)yp * ls.Width] > 5)
            {
                return true;
            }
            return false;
        }

        public override void Render(Screen screen)
        {
            screen.RenderSprite((int)X, (int)Y, sprite, false);
           // turn on if you want to see collision lines
            /*screen.RenderX((int)X, (int)Y - 1, 16 * 4, false);
            screen.RenderX((int)X, (int)Y + 16, 16 * 4, false);
            screen.RenderY((int)X - 1, (int)Y, 16, false);
            screen.RenderY((int)X + 16, (int)Y, 16, false);*/
        }
    }
}
