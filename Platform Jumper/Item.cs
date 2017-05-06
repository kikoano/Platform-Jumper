using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public class Item : Entity
    {
        protected Sprite sprite;

        public Item(int x, int y) : base(x, y)
        {
        }

        public override void Render(Screen screen)
        {
            screen.RenderSprite((int)X, (int)Y, sprite,false);
        }

        public override void Update(LevelState ls)
        {
            
        }
    }
}
