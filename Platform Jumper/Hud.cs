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
        private LevelState ls;
        public Hud(List<Control> controls,LevelState ls)
        {
            this.ls = ls;
            this.controls = controls;
            score.Location = new System.Drawing.Point(65, 5);
            controls.Add(score);
            foreach (Control c in controls)
            {
                ls.gsm.Form.Controls.Add(c);
            }
        }
        public void Render(Screen screen)
        {
            screen.RenderSprite(0, 0, Sprite.Score,true);
        }
        public void Update()
        {
            score.Text = ls.Player.Score.ToString();
        }

    }
}
