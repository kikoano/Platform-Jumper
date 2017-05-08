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
        private bool Escape = false;
        public Player(int x, int y) : base(x, y)
        {
            speed = 5f;
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
            fallInPit(ls);
            collisionTiles(ls);
            forceBackToMap(ls);
            if (Escape)
            {
                PlayerData.NewGame();
                ls.gsm.PopState();
            }
        }
        private void forceBackToMap(LevelState ls)
        {
            if (X < 0)
                X = 0;
            else if (X > (ls.Width * 16-16))
                X = ls.Width * 16-16;
        }
        private void fallInPit(LevelState ls)
        {
            if(Y > (ls.Height - 2) * 16)
            {
                PlayerData.Lifes--;
                Sound.Death.Play();
                ls.gsm.SwitchState(new LevelState(ls.gsm, ls.Path));
            }
        }
        private void collisionTiles(LevelState ls)
        {
            if (ls.Tiles[(int)(X/16) +(int) (Y/16) * ls.Width] == 4)
            {
                ls.gsm.SwitchState(new LevelCompleteState(ls.gsm));
            }
        }
        private void collision(LevelState ls)
        {
            foreach(Entity e in ls.ScreenEntities)
            {
                if (collisionWith(ls, e))
                {
                    if (e is Coin)
                    {
                        PlayerData.CurrentScore += 10;
                        Sound.Collect.Play();
                        e.Removed = true;
                    }
                    else if (e is Goblin)
                    {
                        if (falling)
                        {
                            e.Removed = true;
                            force = gravity;
                            jump = true;
                            Sound.Hit.Play();

                        }
                        else
                        {
                            PlayerData.Lifes--;
                            Sound.Death.Play();
                            ls.gsm.SwitchState(new LevelState(ls.gsm, ls.Path));
                        }
                    }
                    else if (e is Firehead)
                    {
                        PlayerData.Lifes--;
                        Sound.Death.Play();
                        ls.gsm.SwitchState(new LevelState(ls.gsm, ls.Path));
                    }
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
            if(e.KeyCode == Keys.Escape)
            {
                Escape = true;
                
            }
            if (!jump)
            {
                if (e.KeyCode == Keys.Space)
                {
                    force = gravity;
                    jump = true;
                    Sound.Jump.Play();
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
