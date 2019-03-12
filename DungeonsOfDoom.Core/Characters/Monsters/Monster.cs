using DungeonsOfDoom.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DungeonsOfDoom.Core.Characters.Monsters
{
    public abstract class Monster : Character, IPickUpable
    {
        /// <summary>Number of Monster on a given IPickUpable space</summary>
        public int Count { get; set; }
        /// <summary>The total number of Monster instances</summary>
        public static int MonsterCount { get; private set; } = 0;
        /// <summary>The name for the Monster in an inventory</summary>
        public string Name { get; }

        public override int Health
        {
            get { return base.Health; }
            set
            {
                base.Health = value;
                if (value <= 0)
                    MonsterCount--;
            }
        }


        public Monster(int health, int damage, string name, int exp) : base(health, damage, exp)
        {
            MonsterCount++;
            Name = name;
            Count = 1;
        }

        public void Use(Character character)
        {
            Count--;
        }

        public static Monster GenerateMonster()
        {
            if (RandomGen.WithinPercent(15))
                return new Dragon();
            
            else if (RandomGen.WithinPercent(35))
                return new Goblin();
            else
                return new GiantRat();
        }

        new abstract public string GetType();
    }
}
