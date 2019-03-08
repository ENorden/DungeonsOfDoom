using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    class GiantRat : Monster
    {
        public GiantRat() : base(5, 1, "Rat bones")
        {
        }
        public override void Attack(Character victim)
        {
            victim.Poisoned = true;
            base.Attack(victim);
        }
        public override string GetType()
        {
            return "Giant Rat";
        }
    }
}
