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
        public Player(int x, int y) : base(x, y)
        {
            sprite = Sprite.PlayerRight;
        }
        public override void Update(LevelState ls)
        {
            base.Update(ls);
            if (movement[0] && !checkCollisionLeft(ls))
            {
                sprite = Sprite.PlayerLeft;
                X -= speed;
            }
            if (movement[1])
                Y -= speed;
            if (movement[2] && !checkCollisionRight(ls))
            {
                sprite = Sprite.PlayerRight;
                X += speed;
            }
            if (movement[3])
                Y += speed;
            collision(ls);
        }
        private void collision(LevelState ls)
        {
            foreach(Entity e in ls.ScreenEntities)
            {
                if (collisionWith(ls, e))
                {
                    if(e is Coin){
                        ls.PlayerData.Score += 10;
                        e.Removed = true;
                    }
                    else if(e is Goblin)
                    {
                        if (falling)
                        {
                            e.Removed = true;
                            force = gravity;
                            jump = true;

                        }
                        //else
                           // ls.gsm.PopState();
                    }
                    else if(e is Firehead)
                        ls.gsm.PopState();
                }
            }
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
