using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Characters;
using DungeonsOfDoom.Core.Characters.Monsters;
using DungeonsOfDoom.Core.Items;
using DungeonsOfDoom.Core.Items.Consumables;
using DungeonsOfDoom.Core.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DungeonsOfDoom
{
    class ConsoleGame
    {
        Player player;
        Room[,] world;
        string messageToPlayer;
        const int WorldHeight = 5;
        const int WorldWidth = 20;

        

        public void Play()
        {
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                DisplayInventory();
                AskForAction();
                PickUpItem();
            } while (player.Health > 0 && Monster.MonsterCount > 0);

            GameOver();
        }

        private void DisplayInventory()
        {
            Console.WriteLine("Backpack contains: ");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                IPickUpable pickUp = player.Inventory[i];
                if (pickUp.Count == 1)
                {
                    Console.WriteLine($"{i + 1}. {pickUp.Name}");
                }
                else if (pickUp.Count > 1)
                {
                    Console.WriteLine($"{i + 1}. {pickUp.Name} x{pickUp.Count}");
                }
            }
        }

        private void PickUpItem()
        {
            if (world[player.X, player.Y].Monster == null && world[player.X, player.Y].Item != null)
            {
                player.AddItem(world[player.X, player.Y].Item);
                    
                messageToPlayer += $"You have picked up item: {world[player.X, player.Y].Item.Name}\n";
                world[player.X, player.Y].Item = null;

            }
        }

        private void CreatePlayer()
        {
            Console.Write("Choose a name: ");
            string name = Console.ReadLine();
            player = new Player(name, 30, 0, 0);
        }

        private void CreateWorld()
        {
            world = new Room[WorldWidth, WorldHeight];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    if (RandomGen.WithinPercent(3))
                        world[x, y].IsBlocked = true;

                    if (RandomGen.WithinPercent(10))
                        world[x, y].Monster = Monster.GenerateMonster();

                    if (RandomGen.WithinPercent(7))
                        world[x, y].Item = Weapon.GenerateWeapon();
                    else if (RandomGen.WithinPercent(13))
                        world[x, y].Item = Consumable.GenerateConsumable();

                }
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    //Console.BackgroundColor = ConsoleColor.DarkGray;
                    if (player.X == x && player.Y == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("P");
                        Console.ResetColor();
                    }
                    else if (world[x, y].IsBlocked == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("O");
                        Console.ResetColor();
                    }
                    else if (world[x, y].Monster != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("M");
                        Console.ResetColor();
                    }
                    else if (world[x, y].Item != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("I");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(".");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine(player.Name);
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"XP: {player.Exp}");
            Console.WriteLine($"Level: {player.Level}");

            if (player.EquippedWeapon != null) 
                Console.WriteLine($"Attack Damage: {player.Damage+player.EquippedWeapon.Power}");
            else
                Console.WriteLine($"Attack Damage: {player.Damage}");
            Console.WriteLine();

            if (world[player.X, player.Y].Monster != null)
            {
                Console.WriteLine($"Monster: {world[player.X, player.Y].Monster.GetType()}");
            }
            else
            {
                Console.WriteLine();
            }
            Console.WriteLine(messageToPlayer);
            messageToPlayer = string.Empty;
        }

        private void AskForAction()
        {
            int newX = player.X;
            int newY = player.Y;
            bool isValidKey = true;

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.Spacebar: Fight(); break;
                case ConsoleKey.D1:
                    player.UseInventoryItem(0); break;
                case ConsoleKey.D2:
                    player.UseInventoryItem(1); break;
                case ConsoleKey.D3:
                    player.UseInventoryItem(2); break;
                case ConsoleKey.D4:
                    player.UseInventoryItem(3); break;
                case ConsoleKey.D5:
                    player.UseInventoryItem(4); break;
                case ConsoleKey.D6:
                    player.UseInventoryItem(5); break;
                case ConsoleKey.D7:
                    player.UseInventoryItem(6); break;
                case ConsoleKey.D8:
                    player.UseInventoryItem(7); break;
                case ConsoleKey.D9:
                    player.UseInventoryItem(8); break;
                default: isValidKey = false; break;
            }

            if (isValidKey &&
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1) && world[newX, newY].IsBlocked == false)
            {
                player.X = newX;
                player.Y = newY;
            }

            if (isValidKey)
            {
                if (player.Poisoned)
                {
                    messageToPlayer += "You took poison damage!\n";
                }
                player.TurnPassed();
            }
        }

        private void Fight()
        {
            Monster enemy = world[player.X, player.Y].Monster;
            if (enemy != null)
            {
                player.Attack(enemy);
                if (enemy.Health > 0)
                {
                    if (player.EquippedWeapon == null)
                    {
                        messageToPlayer += $"You attacked {enemy.GetType()} with your fist\n";
                    }
                    else
                    {
                        messageToPlayer += $"You attacked {enemy.GetType()} with your {player.EquippedWeapon.Name}\n";
                    }
                    enemy.Attack(player);
                    messageToPlayer += $"{enemy.GetType()} attacked for {enemy.Damage} damage\n";
                }
                else
                {
                    messageToPlayer += $"You killed {enemy.GetType()} and picked up {enemy.Name}\n";
                    player.AddItem(enemy);
                    player.Exp += enemy.Exp;
                    world[player.X, player.Y].Monster = null;
                }

            }
        }



        private void GameOver()
        {
            Console.Clear();
            if (player.Health <= 0)
            {
                Console.WriteLine("Game over... You died...");
            }
            else
            {
                Console.WriteLine("Game over, you won!!! (du kloppa alla monster)");
            }
            Console.ReadKey();
            Play();
        }
    }
}
