using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class RandomGen
    {
        static Random random = new Random();
        /// <summary>
        /// Generates random number between 0 - 99, to compare with.
        /// </summary>
        /// <param name="chance">% chance</param>
        /// <returns>True if random number is under input %</returns>
        static public bool WithinPercent (int chance)
        {            
            return random.Next(0, 100) < chance;
        }
    }
}
