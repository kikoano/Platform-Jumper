using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class Hud
    {
        private List<Control> controls;
        private CustomLabel score=new CustomLabel();
        private CustomLabel life = new CustomLabel();
        private CustomLabel time = new CustomLabel();
        private Timer timer = new Timer();
        private int t = 0;
        private LevelState ls;
        public Hud(List<Control> controls,LevelState ls)
        {
            this.ls = ls;
            this.controls = controls;
            timer.Interval = 1000;
            timer.Tick += tick;
            timer.Start();

            score.Location = new System.Drawing.Point(55, 7);
            score.BackColor = Color.LightGray;
            score.Width = 153;
            life.Location = new System.Drawing.Point(270, 7);
            life.BackColor = Color.LightGray;
            life.Width = 40;
            time.Location = new System.Drawing.Point(370, 7);
            time.BackColor= Color.LightGray;

            controls.Add(score);
            controls.Add(life);
            controls.Add(time);
            foreach (Control c in controls)
            {
                ls.gsm.Form.Controls.Add(c);
            }
        }
        public void CleanUp()
        {
            timer.Tick -= tick;
        }
        public void Render(Screen screen)
        {
            screen.RenderSprite(-2, 0, Sprite.Score,true);
            screen.RenderSprite(70, 3, Sprite.Hearth, true);
            screen.RenderSprite(105, 2, Sprite.Clock, true);
        }
        private void tick(object sender, EventArgs e)
        {
            t++;
            PlayerData.Time = t;
        }
        public void Update()
        {
            score.Text = PlayerData.CurrentScore.ToString();
            life.Text = PlayerData.Lifes.ToString();
            time.Text = t.ToString();
        }

    }
}
