using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    class Goblin : Monster
    {
        public Goblin() : base(15, 5, "Goblin bones")
        {
        }

        public override string GetType()
        {
            return "Goblin";
        }
    }
}
