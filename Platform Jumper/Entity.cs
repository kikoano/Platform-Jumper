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
        protected bool collisionWith(LevelState ls,Entity e)
        {
            if(e is Goblin)
            {
                if((Y + 8) > e.Y && (Y - 8) < e.Y && (X + 10) > e.X && (X - 10) < e.X)
                    return true;
                return false;
            }
            else if ((Y + 8) > e.Y && (Y - 8) < e.Y && (X + 12) > e.X && (X - 12) < e.X)
            {
                return true;
            }
            return false;
        }
    }
}
