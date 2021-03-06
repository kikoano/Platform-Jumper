﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Jumper
{
    public class ButtonMenu : Button
    {
        public static int w = 200;
        public static int h = 60;

        public ButtonMenu()
        {
            Width = w;
            Height = h;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Crimson Text", 24);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Sound.Click.Play();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Sound.Select.Play();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Sound.Select.Play();
        }
    }
}
