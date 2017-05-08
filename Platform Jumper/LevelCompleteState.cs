using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class LevelCompleteState : GameState
    {
        private ButtonMenu continueButton;
        private TextBox txtName= new TextBox();
        public LevelCompleteState(GameStateManager gsm) : base(gsm)
        {
        }
        public override void Init()
        {
            PlayerData.Score += PlayerData.CurrentScore;
            PlayerData.AllTime += PlayerData.Time;
            int witdhCenterForm = (gsm.Form.Width / 2) - ButtonMenu.w / 2;
            int heightCenterForm = (gsm.Form.Height / 2) - ButtonMenu.h / 2 - 30;
            base.Init();
            continueButton = new ButtonMenu() { Location = new System.Drawing.Point(witdhCenterForm, heightCenterForm + 290) };
            for (int i = 0; i < 5; i++)
            {
                controls.Add(new CustomLabel());
                controls[i].ForeColor = System.Drawing.Color.White;
                gsm.Form.Controls.Add(controls[i]);

            }
            continueButton.Click += optionsClick;
            controls.Add(continueButton);
            gsm.Form.Controls.Add(continueButton);
            controls.Add(txtName);
            gsm.Form.Controls.Add(txtName);

            continueButton.Text = "Continue";
            controls[0].Width = 400;
            controls[0].Text = "Level " + PlayerData.CurrentLevel;
            controls[0].Location = new System.Drawing.Point(witdhCenterForm, 50);
            controls[1].Text = PlayerData.Score.ToString();
            controls[1].Location = new System.Drawing.Point(440, 156);
            controls[2].Text = PlayerData.Lifes.ToString();
            controls[2].Location = new System.Drawing.Point(440, 255);
            controls[3].Text = PlayerData.AllTime.ToString();
            controls[3].Location = new System.Drawing.Point(440, 352);
            controls[4].Text = "Enter Name";
            controls[4].Width = 400;
            controls[4].Location = new System.Drawing.Point(350, 452);

            txtName.Location = new System.Drawing.Point(362, 520);
            txtName.Width = 245;
            txtName.Font  = new Font("Crimson Text", 26);
            txtName.Text = "Player";
            txtName.Height = 200;
            controls[4].Visible = false;
            txtName.Visible = false;

            if (PlayerData.CurrentLevel == PlayerData.Levels || PlayerData.Lifes <= 0)
            {
                controls[4].Visible = true;
                txtName.Visible = true;
            }
        }
        private void optionsClick(object sender, System.EventArgs e)
        {
            if (PlayerData.CurrentLevel == PlayerData.Levels || PlayerData.Lifes<=0)
            {
                CreateDataScore();
                gsm.PopState();
                PlayerData.NewGame();
            }
            else
                gsm.SwitchState(new LevelState(gsm,"level"+ ++PlayerData.CurrentLevel+".png"));
        }
        private void CreateDataScore()
        {
            if(txtName.Text.Trim()=="")
            {
                txtName.Text = "Player";
            }
            DataScore ds = new DataScore(txtName.Text,PlayerData.Score,PlayerData.AllTime);
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+ @"\Data";
            string serializationFile = Path.Combine(dir, "scores.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Append))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, ds);
            }
        }
        public override void Cleanup()
        {
            base.Cleanup();
            continueButton.Click -= optionsClick;
        }

        public override void Render()
        {
            gsm.screen.RenderSprite(0, 0, Sprite.BgScore, true);
            gsm.screen.RenderSprite(125, 50, Sprite.Score, true);
            gsm.screen.RenderSprite(127, 85, Sprite.Hearth, true);
            gsm.screen.RenderSprite(127, 117, Sprite.Clock, true);
        }

        public override void Update()
        {
           
        }
    }
}
