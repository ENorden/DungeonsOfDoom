using DungeonsOfDoom.Core;
using DungeonsOfDoom.Core.Characters;
using DungeonsOfDoom.Core.Characters.Monsters;
using DungeonsOfDoom.Core.Items;
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
        List<string> messagesToPlayer = new List<string>();
        int previousMessageCount = 0;
        const int WorldHeight = 5;
        const int WorldWidth = 20;

        enum StatToDisplay
        {
            Name, Level, Exp, Health, Attack, Equipped, Monster, Messages
        };

        public void Play()
        {
            CreatePlayer();
            CreateWorld();
            Console.CursorVisible = false;
            Console.Clear();

            do
            {
                DisplayWorld();
                DisplayStats();
                DisplayInventory();
                AskForAction();
                PickUpItem();
            } while (player.Health > 0 && Monster.MonsterCount > 0);

            Console.CursorVisible = true;
            GameOver();
        }

        private void DisplayInventory()
        {
            Console.SetCursorPosition(WorldWidth + 2, 0);
            Console.Write("Backpack contains: ");
            int i = 0;
            for (; i < player.Inventory.Count; i++)
            {
                Console.SetCursorPosition(WorldWidth + 2, i + 1);
                IPickUpable pickUp = player.Inventory[i];
                if (pickUp.Count == 1)
                {
                    Console.Write($"{i + 1}. {pickUp.Name}");
                }
                else if (pickUp.Count > 1)
                {
                    Console.Write($"{i + 1}. {pickUp.Name} x{pickUp.Count}");
                }
                ClearLine();
            }
            Console.SetCursorPosition(WorldWidth + 2, i + 1);
            ClearLine();
        }

        private void PickUpItem()
        {
            if (world[player.X, player.Y].Monster == null && world[player.X, player.Y].Item != null)
            {
                player.AddItem(world[player.X, player.Y].Item);
                    
                AddMessage($"You have picked up item: {world[player.X, player.Y].Item.Name}");
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
            Console.SetCursorPosition(0, 0);
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

        /// <summary>Clears the rest of the console line from the current cursor position</summary>
        private void ClearLine()
        {
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
        }

        private void DisplayStats()
        {
            foreach (StatToDisplay stat in Enum.GetValues(typeof(StatToDisplay)))
            {
                Console.SetCursorPosition(0, WorldHeight + (int)stat);
                switch (stat)
                {
                    case StatToDisplay.Name:
                        Console.Write(player.Name);
                        break;
                    case StatToDisplay.Level:
                        //Console.Write($"Level: {player.Level}");
                        break;
                    case StatToDisplay.Exp:
                        //Console.Write($"Experience: {player.Exp}");
                        break;
                    case StatToDisplay.Health:
                        Console.Write($"Health: {player.Health}");
                        break;
                    case StatToDisplay.Attack:
                        if (player.EquippedWeapon != null)
                            Console.Write($"Attack Damage: {player.Damage + player.EquippedWeapon.Power}");
                        else
                            Console.Write($"Attack Damage: {player.Damage}");
                        break;
                    case StatToDisplay.Equipped:
                        if (player.EquippedWeapon != null)
                            Console.Write($"Equipped Weapon: {player.EquippedWeapon.Name}");
                        break;
                    case StatToDisplay.Monster:
                        if (world[player.X, player.Y].Monster != null)
                            Console.Write($"Monster: {world[player.X, player.Y].Monster.GetType()}");
                        break;
                    case StatToDisplay.Messages:
                        DisplayMessages();
                        break;
                }
                ClearLine();
            }
        }

        private void DisplayMessages()
        {
            int count = messagesToPlayer.Count;
            int i = 0;
            for (; i < count; i++)
            {
                Console.Write(messagesToPlayer[i]);
                ClearLine();
                Console.WriteLine();
            }
            for (; i < previousMessageCount; i++)
            {
                ClearLine();
                Console.WriteLine();
            }
            previousMessageCount = count;
            messagesToPlayer.Clear();
        }

        private void AskForAction()
        {
            int newX = player.X;
            int newY = player.Y;
            bool isValidKey = true;

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
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
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;
            }

            if (isValidKey)
            {
                if (player.Poisoned)
                {
                    AddMessage("You took poison damage!");
                }
                player.TurnPassed();
            }
        }

        private void AddMessage(string msg)
        {
            messagesToPlayer.Add(msg);
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
                        AddMessage($"You attacked {enemy.GetType()} with your fist");
                    }
                    else
                    {
                        AddMessage($"You attacked {enemy.GetType()} with your {player.EquippedWeapon.Name}");
                    }
                    enemy.Attack(player);
                    AddMessage($"{enemy.GetType()} attacked for {enemy.Damage} damage");
                }
                else
                {
                    AddMessage($"You killed {enemy.GetType()} and picked up {enemy.Name}");
                    player.AddItem(enemy);
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
