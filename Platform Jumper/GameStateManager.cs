using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class GameStateManager
    {
        public Form Form { get; set; }
        private Stack<GameState> states;
        public Screen screen { get; private set; }
        public bool Ingame { get; set; } = false;
        private int backgorundX = 0;

        public GameStateManager(Form form)
        {
            Form = form;
            states = new Stack<GameState>();
            screen = new Screen(Form1.WIDTH, Form1.HEIGHT);
            Form.Paint += Form1_Paint;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            screen.PixelsToBitmap();
            Bitmap resize = new Bitmap(screen.ScreenMap, Form1.WIDTH * Form1.SCALE, Form1.HEIGHT * Form1.SCALE);
            e.Graphics.DrawImage(resize, 0, 0);
            resize.Dispose();
        }
        public void PushState(GameState gs)
        {
            if (states.Count > 0)
                states.Peek().Cleanup();
            states.Push(gs);
            states.Peek().Init();
        }
        public void SwitchState(GameState gs)
        {
            if (states.Count > 0)
                states.Peek().Cleanup();
            PopState();
            states.Push(gs);
            states.Peek().Init();
        }
        public void PopState()
        {
            if (states.Count > 0)
                states.Peek().Cleanup();
            states.Pop();
            states.Peek().Init();

        }
        public void Update()
        {
            if (!Ingame)
            {
                backgorundX++;
                if (backgorundX >= Form1.WIDTH)
                    backgorundX = 0;
            }
            states.Peek().Update();
        }
        public void Render()
        {
            if (!Ingame)
            {         
                screen.RenderSprite(backgorundX, 0, Sprite.Background,true);
                screen.RenderSprite(backgorundX - Form1.WIDTH, 0, Sprite.Background,true);
            }
            states.Peek().Render();
            Form.Invalidate();
        }
    }
}
