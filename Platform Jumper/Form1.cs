using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        private DateTime currentTime;
        private DateTime lastTime;
        private GameStateManager gsm;
        public static int WIDTH = 320;
        public static int HEIGHT = 240;
        public static int SCALE = 3;

        public Form1()
        {
            InitializeComponent();
            Width = WIDTH * SCALE;
            Height = HEIGHT * SCALE;

            DoubleBuffered = true;
            KeyPreview = true;
            MaximizeBox = false;
            gsm = new GameStateManager(this);
            gsm.PushState(new MenuState(gsm));
            timer.Interval = 1000 / 60;
            timer.Tick += timerTick;
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {
            currentTime = DateTime.Now;
            gsm.Update((float)(currentTime - lastTime).TotalSeconds);
            gsm.Render();
            lastTime = currentTime;
        }
    }
}
