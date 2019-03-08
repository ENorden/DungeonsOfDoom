using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom
{
    abstract class Character
    {
        //Properties
        public virtual int Health { get; set; }
        public int Damage { get; set; }
        public bool Poisoned { get; set; }
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
        public virtual void Attack(Character victim)
        {
            victim.Health -= Damage;
        }

        //Method
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
