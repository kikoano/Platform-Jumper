using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public static class PlayerData
    {
        public static int Score = 0;
        public static int Lifes = 3;

        public static void ResetScore()
        {
            Score = 0;
        }
    }
}
