using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    class Goblin : Mob
    {
        private int moveDir;
        private static Random rnd = new Random();

        public Goblin(int x, int y) : base(x, y)
        {

            sprite = Sprite.GoblinLeft;
            speed = 30f;
            moveDir = rnd.Next(0, 2);
        }
        public override void Update(LevelState ls,float delta)
        {
            horizontalMove(ls, delta);
            if (moveDir == 0)
                sprite = Sprite.GoblinRight;
            else
                sprite = Sprite.GoblinLeft;
        }
        private void horizontalMove(LevelState ls,float delta)
        {
            // max move 18!
            if (moveDir == 0)
            {
                X += speed*delta;
            }
            if (checkCollisionRight(ls) || !checkCollisionBot(ls))
                moveDir = 1;
            if (moveDir == 1)
            {
                X -= speed* delta;
            }
            if (checkCollisionLeft(ls) || !checkCollisionBot(ls))
                moveDir = 0;
        }
    }
}
