using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    class Coin : Item
    {
        public Coin(int x, int y) : base(x, y)
        {
            sprite = Sprite.Coin;
        }
        public override void Update(LevelState ls,float delta)
        {
            base.Update(ls,delta);

        }
        public override void Render(Screen screen)
        {
            base.Render(screen);

        }
    }
}
