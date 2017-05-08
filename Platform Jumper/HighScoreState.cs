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
    public class HighScoreState : GameState
    {
        private List<DataScore> ds = new List<DataScore>();
        private CustomListBox listNames = new CustomListBox();
        private CustomListBox listScores = new CustomListBox();
        private CustomListBox listTime = new CustomListBox();
        private CustomLabel lblNames = new CustomLabel();
        private CustomLabel lblScores = new CustomLabel();
        private CustomLabel lblTime = new CustomLabel();
        private ButtonMenu backButton;
        public HighScoreState(GameStateManager gsm) : base(gsm)
        {

        }
        public override void Init()
        {
            base.Init();
            int witdhCenterForm = (gsm.Form.Width / 2) - ButtonMenu.w / 2;
            int heightCenterForm = (gsm.Form.Height / 2) - ButtonMenu.h / 2 - 30;
            backButton=new ButtonMenu() { Location = new System.Drawing.Point(witdhCenterForm, heightCenterForm + 290) };
            backButton.Text = "Back";
            loadDataScore();
            listNames.Location = new System.Drawing.Point(250, 200);
            listScores.Location = new System.Drawing.Point(520, 200);
            listScores.Width = 100;
            listTime.Location = new System.Drawing.Point(620, 200);
            listTime.Width = 100;
            controls.Add(listNames);
            gsm.Form.Controls.Add(listNames);
            controls.Add(listScores);
            gsm.Form.Controls.Add(listScores);
            controls.Add(listTime);
            gsm.Form.Controls.Add(listTime);
            controls.Add(backButton);
            gsm.Form.Controls.Add(backButton);
            backButton.Click += back;

            lblNames.Text = "Name";
            lblNames.Font= new Font("Crimson Text", 24);
            lblNames.Location = new System.Drawing.Point(245, 160);


            lblScores.Text = "Score";
            lblScores.Font = new Font("Crimson Text", 24);
            lblScores.Location = new System.Drawing.Point(515, 160);
            lblScores.Width = 100;

            lblTime.Text = "Time";
            lblTime.Font = new Font("Crimson Text", 24);
            lblTime.Location = new System.Drawing.Point(615, 160);

            controls.Add(lblNames);
            gsm.Form.Controls.Add(lblNames);
            controls.Add(lblScores);
            gsm.Form.Controls.Add(lblScores);
            controls.Add(lblTime);
            gsm.Form.Controls.Add(lblTime);

        }
        public override void Cleanup()
        {
            base.Cleanup();
            backButton.Click -= back;
        }

        public override void Render()
        {

        }

        public override void Update(float delta)
        {

        }
        private void back(object sender, System.EventArgs e)
        {
            gsm.PopState();
        }
        private void loadDataScore()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Data";
            string serializationFile = Path.Combine(dir, "scores.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                while (stream.Position < stream.Length)
                    ds.Add((DataScore)bformatter.Deserialize(stream));
            }
            //sorting
            ds.Sort();
            ds.Reverse();
            //
            foreach (DataScore d in ds)
            {
                listNames.Items.Add(d.Name);
                listScores.Items.Add(d.Score);
                listTime.Items.Add(d.Time);
            }
        }
    }
}
