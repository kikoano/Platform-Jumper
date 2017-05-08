using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            Width = 270;
            Height = 400;
            Font = new Font("Crimson Text", 20);
            SelectionMode = SelectionMode.None;
        }
    }
}
