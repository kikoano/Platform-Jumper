using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    [Serializable]
    public class DataScore: IComparable
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public DataScore(string name,int score,int time)
        {
            Name = name;
            Score = score;
            Time = time;
        }
        public override string ToString()
        {
            return string.Format("{0:20}{1:5}{2:5}",Name,Score,Time);
        }

        public int CompareTo(object obj)
        {
            DataScore d = (DataScore)obj;
            int cmp= Score.CompareTo(d.Score);
            if (cmp == 0)
                cmp = Time.CompareTo(d.Time);
            return cmp;
        }
    }
}
