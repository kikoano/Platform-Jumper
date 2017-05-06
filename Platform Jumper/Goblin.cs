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
        private float movePosition = 0;

        public Goblin(int x, int y) : base(x, y)
        {

            sprite = Sprite.GoblinLeft;
            speed = 1.6f;
            moveDir = rnd.Next(0, 2);
        }
        public override void Update(LevelState ls)
        {
            base.Update(ls);
            horizontalMove(ls);
            maxMovePosition(ls);
            if (moveDir == 0)
                sprite = Sprite.GoblinRight;
            else
                sprite = Sprite.GoblinLeft;
        }
        private void maxMovePosition(LevelState ls)
        {
            movePosition++;
            if (movePosition > 9*(int)speed*10)
            {
                if (moveDir == 0)
                    moveDir = 1;
                else
                    moveDir = 0;
                movePosition = -movePosition;
            }
        }
        private void horizontalMove(LevelState ls)
        {
            // max move 18!
            if (moveDir == 0)
            {
                X += speed;
            }
            if (checkCollisionRight(ls) || !checkCollisionBot(ls))
                moveDir = 1;
            if (moveDir == 1)
            {
                X -= speed;
            }
            if (checkCollisionLeft(ls) || !checkCollisionBot(ls))
                moveDir = 0;
        }
    }
}
