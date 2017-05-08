using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public class HowTopPlayState : GameState
    {
        private ButtonMenu back = new ButtonMenu();
        public HowTopPlayState(GameStateManager gsm) : base(gsm)
        {
        }
        public override void Init()
        {
            base.Init();
            back.Click += backClick;
            controls.Add(back);
            gsm.Form.Controls.Add(back);
            back.Text = "Back";
            back.Location = new System.Drawing.Point(730,600);
        }
        public override void Cleanup()
        {
            base.Cleanup();
            back.Click -= backClick;
        }
        private void backClick(object sender, System.EventArgs e)
        {
            gsm.PopState();
        }

        public override void Render()
        {
            gsm.screen.RenderSprite(-2,-5,Sprite.HowTo,false);
        }

        public override void Update()
        {
           
        }
    }
}
