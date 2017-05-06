using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public abstract class Entity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public bool Removed = false;
        public abstract void Update(LevelState ls);
        public abstract void Render(Screen screen);
        public Entity(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
