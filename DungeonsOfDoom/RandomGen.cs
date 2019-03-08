using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    static class RandomGen
    {
        static Random random = new Random();
        static public bool WithinPercent (int chance)
        {            
            return random.Next(0, 100) < chance;
        }
    }
}
