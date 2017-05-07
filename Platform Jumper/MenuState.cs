using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class MenuState : GameState
    {

        public MenuState(GameStateManager gsm) : base(gsm)
        {

        }
        private void optionsClick(object sender, System.EventArgs e)
        {
            gsm.PushState(new OptionsState(gsm));
        }
        private void exitClick(object sender, System.EventArgs e)
        {
            gsm.Form.Close();
        }
        private void playClick(object sender, System.EventArgs e)
        {
            gsm.PushState(new LevelState(gsm, "level"+PlayerData.CurrentLevel+".png"));
        }
        public override void Init()
        {
            base.Init();

            gsm.Ingame = false;

            int witdhCenterForm = (gsm.Form.Width / 2) - ButtonMenu.w / 2;
            int heightCenterForm = (gsm.Form.Height / 2) - ButtonMenu.h / 2 - 30;

            for (int i = 0; i < 4; i++)
            {
                controls.Add(new ButtonMenu() { Location = new System.Drawing.Point(witdhCenterForm, heightCenterForm + (i * 80)) });
                gsm.Form.Controls.Add(controls[i]);
            }
            controls[0].Text = "Play";
            controls[0].Click += playClick;
            controls[1].Text = "High Scores";

            controls[2].Text = "Options";
            controls[2].Click += optionsClick;

            controls[3].Text = "Exit";
            controls[3].Click += exitClick;
        }
        public override void Cleanup()
        {
            // removes Handler, Form references and dispose all
            controls[0].Click -= playClick;
            controls[2].Click -= optionsClick;
            controls[3].Click -= exitClick;
            base.Cleanup();
        }

        public override void Render()
        {
            
        }

        public override void Update()
        {
            
        }
        
    }
}
