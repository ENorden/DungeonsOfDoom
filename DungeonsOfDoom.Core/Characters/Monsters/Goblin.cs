using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core.Characters.Monsters
{
    public class Goblin : Monster
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
