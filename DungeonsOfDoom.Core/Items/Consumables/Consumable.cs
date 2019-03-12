using DungeonsOfDoom.Core.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DungeonsOfDoom.Core.Items.Consumables

{
    public class Consumable : Item
    {
        public int HealthRestore { get; }

        public Consumable(string name, int healthRestore) : base(name)
        {
            HealthRestore = healthRestore;
        }

        /// <summary>
        /// Generates a consumable item depending on a fixed frequency
        /// </summary>
        /// <returns>A SuperPotion, Potion and Muffin depending on a fixed frequency</returns>
        public static Consumable GenerateConsumable()
        {           
            if (RandomGen.WithinPercent(15))
                return new SuperPotion();
            else if (RandomGen.WithinPercent(35))
                return new Potion();
            else
                return new Muffin();
        }

        /// <summary>
        /// Use a consumable item
        /// </summary>
        /// <param name="character">The character to use the consumable item</param>
        public override void Use(Character character)
        {
            character.Health += HealthRestore;
            Count--;
        }
    }
}
