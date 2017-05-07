using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public static class PlayerData
    {
        public static int CurrentScore = 0;
        public static int Score = 0;
        public static int Lifes = 5;
        public static int CurrentLevel = 1;
        public static int Levels = 2;
        public static int Time = 0;
        public static int AllTime = 0;

        public static void NewGame()
        {
            Score = 0;
            Time = 0;
            CurrentScore = 0;
            AllTime = 0;
            Lifes = 5;
            CurrentLevel = 1;
        }
        public static void ResetCurrentScoreTime()
        {
            CurrentScore = 0;
            Time = 0;
        }
    }
}
