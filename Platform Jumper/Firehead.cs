using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public class Firehead : Mob
    {
        private int moveDir;
        private static Random rnd = new Random();
        public Firehead(int x, int y) : base(x, y)
        {
            sprite = Sprite.Firehead;
            speed = 2f;
            moveDir = rnd.Next(0, 2);
        }
        public override void Update(LevelState ls)
        {
             verticalMove(ls);
        }
        private void verticalMove(LevelState ls)
        {
            if (moveDir == 0)
            {
                Y += speed;
            }
            if (checkCollisionBot(ls))
                moveDir = 1;
            if (moveDir == 1)
            {
                Y -= speed;
            }
            if (checkCollisionTop(ls))
                moveDir = 0;
        }
    }
}
