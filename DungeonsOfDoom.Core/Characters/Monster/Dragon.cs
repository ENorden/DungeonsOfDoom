using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core.Characters.Monster
{
    public class Dragon : Monster
    {
        public Dragon() : base(60, 15, "Dragon bones")
        {
        }

        public override string GetType()
        {
            return "Dragon";
        }
    }
}
