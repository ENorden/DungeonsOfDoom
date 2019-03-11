using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core.Characters
{
    public abstract class Character
    {
        //Properties
        /// <summary>The character's current health</summary>
        public virtual int Health { get; set; }
        /// <summary>The character's base attack damage</summary>
        public int Damage { get; set; }
        /// <summary>If the character is currently poisoned</summary>
        public bool Poisoned { get; set; }
        /// <summary>The amount of poison damage taken from the current posion</summary>
        public int PoisonDamageTaken { get; set; }

        //Constructor
        public Character(int health, int damage)
        {
            Health = health;
            Damage = damage;
            Poisoned = false;
            PoisonDamageTaken = 0;
        }

        //Overridable method
        /// <summary>The character attacks the victim</summary>
        /// <param name="victim">The character that is attacked</param>
        public virtual void Attack(Character victim)
        {
            victim.Health -= Damage;
        }

        //Method
        /// <summary>Handles things that happen whenever a turn pass, such as poison damage</summary>
        public void TurnPassed()
        {
            if (Poisoned)
            {
                Health--;
                PoisonDamageTaken++;
                if (PoisonDamageTaken >= 5)
                {
                    Poisoned = false;
                    PoisonDamageTaken = 0;
                }
            }
        }
    }
}
