using DungeonsOfDoom.Core.Items;
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

        /// <summary> Describes the player's X position </summary>
        public int X { get; set; }

        /// <summary> Describes the player's Y position </summary>
        public int Y { get; set; }

        /// <summary>The player's current health, cannot exceed the maximum value</summary>
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

        /// <summary>Contains every IPickUpable in the player's inventory</summary>
        public List<IPickUpable> Inventory { get; }
        /// <summary>The weapon currently equipped by the player</summary>
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
        /// <summary>Adds an IPickUpable to the player's inventory</summary>
        /// <param name="itemToAdd">The IPickUpable to add</param>
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

        /// <summary>The player attacks the victim using their equipped weapon</summary>
        /// <param name="victim">The character that is attacked</param>
        public override void Attack(Character victim)
        {
            if (EquippedWeapon == null)
                base.Attack(victim);
            else
                victim.Health -= Damage + EquippedWeapon.Power;
        }

        /// <summary>The player uses the specified IPickUpable</summary>
        /// <param name="inventoryIndex">The index in inventory of the IPickUpable to use</param>
        public void UseInventoryItem(int inventoryIndex)
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
