using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class Player : Mob
    {
        public int Score { get; private set; } = 0;
        public Player(int x, int y) : base(x, y)
        {
            sprite = Sprite.Player;
        }
        public override void Update(LevelState ls)
        {
            base.Update(ls);
            if (movement[0] && !checkCollisionLeft(ls))
                X -= speed;
            if (movement[1])
                Y -= speed;
            if (movement[2] && !checkCollisionRight(ls))
                X += speed;
            if (movement[3])
                Y += speed;
            collision(ls);
            enemyCollision(ls);
        }
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                movement[0] = true;
            }
            /* if (e.KeyCode == Keys.W)
            {
                movement[1] = true;
            }*/
            if (e.KeyCode == Keys.D)
            {
                movement[2] = true;
            }
            /* if(e.KeyCode == Keys.S)
            {
                movement[3] = true;
            }*/
            if (!jump)
            {
                if (e.KeyCode == Keys.Space)
                {
                    force = gravity;
                    jump = true;
                }
            }
        }
        private void collision(LevelState ls)
        {
            // scan in area -160 to 160
            for (int x = (int)X - 160; x < (int)X + 160; x++)
            {
                Entity e;
                if (ls.Entities.TryGetValue(x + (int)Y * ls.Width, out e))
                {
                    if ((X+8) + Y * ls.Width > e.X + e.Y * ls.Width && (X - 8) + Y * ls.Width < e.X + e.Y * ls.Width)
                    {
                        if (e is Coin)
                        {
                            ls.Entities.Remove(x + (int)Y * ls.Width);

                            Score++;
                        }
                        else if (e is Goblin)
                        {
                            if (falling)
                                ls.Entities.Remove(x + (int)Y * ls.Width);
                            else ls.gsm.PopState();
                        }
                    }
                }
            }
        }
        private void enemyCollision(LevelState ls)
        {

        }
        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                movement[0] = false;
            }
            /*if (e.KeyCode == Keys.W)
            {
                movement[1] = false;
            }*/
            if (e.KeyCode == Keys.D)
            {
                movement[2] = false;
            }
            /* if (e.KeyCode == Keys.S)
             {
                 movement[3] = false;
             }   */
        }
        public override void Render(Screen screen)
        {
            base.Render(screen);
        }
    }
}
