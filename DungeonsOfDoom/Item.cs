using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : IPickUpable
    {
       public Item(string name)
        {
            Name = name;
            Count = 1;
        }

        public string Name { get; }
        public int Count { get; set; }

        public abstract void Use(Character character);
    }
}
