using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class OptionsState : GameState
    {
        public OptionsState(GameStateManager gsm) : base(gsm)
        {
          
        }
        private void backClick(object sender, System.EventArgs e)
        {
            gsm.PopState();
        }
        public override void Init()
        {
            base.Init();
            int witdhCenterForm = (gsm.Form.Width / 2) - ButtonMenu.w / 2;
            int heightCenterForm = (gsm.Form.Height / 2) - ButtonMenu.h / 2 + 30;

            for (int i = 0; i < 2; i++)
            {
                controls.Add(new ButtonMenu() { Location = new System.Drawing.Point(witdhCenterForm, heightCenterForm + (i * 80)) });
                gsm.Form.Controls.Add(controls[i]);
            }
            controls[0].Click += muteSound;
            controls[1].Text = "Back";
            controls[1].Click += backClick;
        }
        public override void Cleanup()
        {
            base.Cleanup();
            controls[1].Click -= backClick;
            controls[0].Click -= muteSound;
        }

        public override void Render()
        {
        }
        private void muteSound(object sender, System.EventArgs e)
        {
            Sound.Mute = !Sound.Mute;
        }
        public override void Update()
        {
            string mute = "on";
            if (Sound.Mute)
            {
                mute = "off";
            }
            controls[0].Text = "Sound " + mute;
        }
    }
}
