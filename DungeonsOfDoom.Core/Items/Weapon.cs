using DungeonsOfDoom.Core.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DungeonsOfDoom.Core.Items
{
    public class Weapon : Item
    {
        public int Power { get; }

        public Weapon(string name, int power) : base(name)
        {
            Power = power;
        }

        public static Weapon GenerateWeapon()
        {

            if (RandomGen.WithinPercent(15))
                return new Weapon("Axe", 5);
            else if (RandomGen.WithinPercent(35))
                return new Weapon("Sword", 3);
            else
                return new Weapon("Spear", 2);
        }

        public override void Use(Character character)
        {
            if (character is Player) // Replace this
                ((Player)character).EquippedWeapon = this;
        }
    }
}
