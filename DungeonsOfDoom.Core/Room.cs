using DungeonsOfDoom.Core.Characters.Monsters;
using DungeonsOfDoom.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core
{
    public class Room
    {
        public Monster Monster { get; set; }
        public Item Item { get; set; }
        public bool IsBlocked { get; set; }
    }
}
