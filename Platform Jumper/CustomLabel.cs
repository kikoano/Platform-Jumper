using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class CustomLabel: Label
    {
        public CustomLabel()
        {
            BackColor = Color.Transparent;
            Font = new Font("Georgia", 34);
            Width = 150;
            Height = 50;
        }
    }
}
