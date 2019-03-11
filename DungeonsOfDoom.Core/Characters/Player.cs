using DungeonsOfDoom.Core.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core.Characters
{
    public class Player : Character
    {
        //Properties
        public string Name { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public override int Health
        {
            get { return base.Health; }
            set
            {
                if (value > maxHealth)
                    base.Health = maxHealth;
                else
                    base.Health = value;
            }
        }
        private const int BaseDamage = 3;
        private readonly int maxHealth = 30;

        public List<IPickUpable> Inventory { get; }
        public Weapon EquippedWeapon { get; set; }

        //Constructor
        public Player(string name, int health, int x, int y) : base(health, BaseDamage)
        {
            Name = name;
            X = x;
            Y = y;
            Inventory = new List<IPickUpable>();
            Poisoned = false;
            maxHealth = health;
            Health = health;
        }
        
        //Methods
        public void AddItem(IPickUpable itemToAdd)
        {
            bool exists = false;
            int insertIndex = 0;
            foreach (IPickUpable pickUp in Inventory)
            {
                if (pickUp.Name == itemToAdd.Name)
                {
                    exists = true;
                    pickUp.Count++;                    
                }
                if (string.Compare(itemToAdd.Name, pickUp.Name, true) > 0)
                {
                    insertIndex++;
                }                               
            }

            if (!exists)
            { Inventory.Insert(insertIndex, itemToAdd); }

        }

        public override void Attack(Character victim)
        {
            if (EquippedWeapon == null)
                base.Attack(victim);
            else
                victim.Health -= Damage + EquippedWeapon.Power;
        }

        internal void UseInventoryItem(int inventoryIndex)
        {
            IPickUpable item = null;
            if (inventoryIndex < Inventory.Count)
            {
                item = Inventory[inventoryIndex];
            }
            if (item != null)
            {
                item.Use(this);
                if (item.Count <= 0)
                {
                    Inventory.Remove(item);
                }
            }
        }
    }
}
