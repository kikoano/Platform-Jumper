using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public abstract class GameState
    {
        public GameStateManager gsm { get; protected set; }
        protected List<Control> controls;
        public GameState(GameStateManager gsm)
        {
            this.gsm = gsm;
        }
        public virtual void Init()
        {
            controls = new List<Control>();
        }
        public virtual void Cleanup()
        {
            // removes Handler with override and from base removes Form references and dispose all
            foreach (Control c in controls)
            {
                gsm.Form.Controls.Remove(c);
                c.Dispose();
            }
        }
        public abstract void Update();
        public abstract void Render();
    }
}
