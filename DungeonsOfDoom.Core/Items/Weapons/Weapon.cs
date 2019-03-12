using DungeonsOfDoom.Core.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DungeonsOfDoom.Core.Items.Weapons
{
    public class Weapon : Item
    {
        public int Power { get; }

        public Weapon(string name, int power) : base(name)
        {
            Power = power;
        }

        /// <summary>
        /// Generates a weapon type depending on a fixed frequency
        /// </summary>
        /// <returns>A weapon type depending on a fixed frequency</returns>
        public static Weapon GenerateWeapon()
        {
            if (RandomGen.WithinPercent(3))
                return new TwoHandedAxe();
            else if (RandomGen.WithinPercent(15))
                return new Axe();
            else if (RandomGen.WithinPercent(35))
                return new Sword();
            else
                return new Spear();
        }

        /// <summary>
        /// Use a weapon from the players' backpack
        /// </summary>
        /// <param name="character">The player to use the weapon</param>
        public override void Use(Character character)
        {
            if (character is Player) // Replace this
                ((Player)character).EquippedWeapon = this;
        }
    }
}
