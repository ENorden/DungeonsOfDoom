using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    class Dragon : Monster
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
