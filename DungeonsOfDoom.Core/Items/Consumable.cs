using DungeonsOfDoom.Core.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DungeonsOfDoom.Core.Items

{
    public class Consumable : Item
    {
        public int HealthRestore { get; }

        public Consumable(string name, int healthRestore) : base(name)
        {
            HealthRestore = healthRestore;
        }

        public static Consumable GenerateConsumable()
        {           
            if (RandomGen.WithinPercent(15))
                return new SuperPotion();
            else if (RandomGen.WithinPercent(35))
                return new Potion();
            else
                return new Muffin();
        }

        public override void Use(Character character)
        {
            character.Health += HealthRestore;
            Count--;
        }
    }
}
