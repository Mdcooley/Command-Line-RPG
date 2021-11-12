using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public abstract class Character : Creature
    {
        //Integer that stores the characters x coordinate on the map
        public int xCoord;

        //Integer that stores the characters y coordinate on the map
        public int yCoord;

        //Data structure that stores all items possessed (but not equipped) by the player
        private List<IItem> Inventory = new List<IItem>(10);

        //Stores the player's equipped weapon
        private IEquipment Weapon = default;

        //Stores the player's equipped armor
        private IEquipment Armor = default;

        //Stores which class the player belongs to
        public string Class;

        /// <summary>
        /// Attempts to equip the player with a specified weapon or piece of armor
        /// will verify that the specified equipment is able to be wielded by the player's class
        /// </summary>
        /// <param name="equipment">Weapon/Armor to be equipped</param>
        public void Equip(IEquipment equipment)
        {
            IEquipment temp = default;

            //Seperate logic for weapons and armor
            //the only real difference is what slot is used
            if(equipment.Type == "Weapon")
            {
                if (equipment.Class == Class)
                {
                    temp = Weapon;
                    Weapon = equipment;
                    Drop(equipment);

                    if (temp != default)
                    {
                        Pickup(temp);
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"A {Class} cannot equip this kind of weapon!");
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }
            else if (equipment.Type == "Armor")
            {
                if (equipment.Class == Class)
                {
                    temp = Armor;
                    Armor = equipment;
                    Drop(equipment);

                    if (temp != default)
                    {
                        Pickup(temp);
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"A {Class} cannot equip this kind of armor!");
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }
            else
            {
                Console.Write("Error: Invalid equipment");
            }
        }

        /// <summary>
        /// Unequips either the player's weapon or armor based off the char passed in
        /// </summary>
        /// <param name="which">represents either weapons or armor</param>
        public void Unequip(char which)
        {
            IEquipment temp = default;

            if (which == 'w')
            {
                temp = Weapon;
                Weapon = default;

                if (temp != default)
                {
                    Pickup(temp);
                }
            }
            else if (which == 'a')
            {
                temp = Armor;
                Armor = default;

                if (temp != default)
                {
                    Pickup(temp);
                }
            }
        }

        /// <summary>
        /// Attempts to add a specified item to the player's inventory
        /// will prompt the player to drop an item if their inventory is full
        /// </summary>
        /// <param name="item">The item to be added to the inventory</param>
        public void Pickup(IItem item)
        {
            bool added = false;
            ConsoleKeyInfo input = default;

            //Only allows items to be added if the inventory is not full
            //Potions can stack to 5, so they are checked seperately from the rest of the inventory
            if (Inventory.Count < 10 || (item is Potion && HasOpenPotionStack()))
            {
                //Checks if the item is a potion
                if (item.Type == "Potion")
                {
                    //Looks for the first potion stack with an empty space
                    for (int i = 0; i < Inventory.Count; i++)
                    {
                        if (Inventory[i] is Potion)
                        {
                            Potion temp = Inventory[i] as Potion;

                            if (temp.Quantity < 5)
                            {
                                temp.AddToStack();
                                added = true;
                                break;
                            }
                        }
                    }

                    //Creates a new potion stack if there were none available
                    if (!added)
                    {
                        Inventory.Add(item);
                    }
                }
                else
                {
                    Inventory.Add(item);
                }
            }
            else
            {
                Console.WriteLine("You have too many items in your inventory! Please select one to drop: ");

                DisplayInventory(0, Console.CursorTop);
                Console.WriteLine($"\na) {item.Name}");

                //Loops until the player selects a valid item to drop
                do
                {
                    input = Console.ReadKey(true);

                    switch(input.Key.ToString())
                    {
                        case "D0":
                            {
                                Inventory.Remove(GetIndex(0));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D1":
                            {
                                Inventory.Remove(GetIndex(1));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D2":
                            {
                                Inventory.Remove(GetIndex(2));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D3":
                            {
                                Inventory.Remove(GetIndex(3));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D4":
                            {
                                Inventory.Remove(GetIndex(4));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D5":
                            {
                                Inventory.Remove(GetIndex(5));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D6":
                            {
                                Inventory.Remove(GetIndex(6));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D7":
                            {
                                Inventory.Remove(GetIndex(7));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D8":
                            {
                                Inventory.Remove(GetIndex(8));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "D9":
                            {
                                Inventory.Remove(GetIndex(9));
                                Inventory.Add(item);
                                added = true;
                                break;
                            }
                        case "A":
                            {
                                added = true;
                                break;
                            }
                        default:
                            {
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item!");
                                break;
                            }
                    }

                } while (!added);

                //Clears the displayed inventory from the screen
                Console.SetCursorPosition(0, Console.CursorTop - 12);
                for (int i = 0; i < 13; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, Console.CursorTop - 13);
            }
        }

        /// <summary>
        /// Removes a specified item from the player's inventory
        /// </summary>
        /// <param name="item">Item to be removed</param>
        public void Drop(IItem item)
        {
            //Ensures you don't try to remove an item that doesn't exist
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
            }
        }

        /// <summary>
        /// Displays the player's inventory as a list at the specified coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void DisplayInventory(int x, int y)
        {
            //Loops through the maximum size of the inventory
            for (int i = 0; i < Inventory.Capacity; i++)
            {
                Console.SetCursorPosition(x, y + i);

                //Displays the item if there is one
                //Clears the line if there is not
                if (i < Inventory.Count)
                {
                    Console.Write($"{i}) {Inventory[i].Name}");
                }
                else
                {
                    Console.Write(new string(' ', Console.WindowWidth - x));
                }
            }
        }

        /// <summary>
        /// Returns the item at a specified index in the inventory
        /// </summary>
        /// <param name="x">Index to be accessed</param>
        /// <returns>The item at the indexed location</returns>
        public IItem GetIndex(int x)
        {
            return Inventory[x];
        }

        /// <summary>
        /// Returns the number of items currently in the player's inventory
        /// A stack of potions is considered only one item
        /// </summary>
        /// <returns>Number of items in the inventory</returns>
        public int GetNumItems()
        {
            return Inventory.Count;
        }

        /// <summary>
        /// Returns the specified stat factoring in equipment boosts
        /// </summary>
        /// <param name="stat">Key of the stat to get</param>
        /// <returns>Player's stat + equipment stats</returns>
        public new int GetStat(string stat)
        {
            int val = StatBlock[stat];

            if (Weapon != default)
            {
                if (Weapon.StatBlock.ContainsKey(stat))
                {
                    val += Weapon.StatBlock[stat];
                }
            }

            if (Armor != default)
            {
                if (Armor.StatBlock.ContainsKey(stat))
                {
                    val += Armor.StatBlock[stat];
                }
            }

            if (stat == "EV")
            {
                val += CalcModifier(GetStat("Dex"));
            }

            return val;
        }

        /// <summary>
        /// Returns the player's equipped weapon
        /// </summary>
        /// <returns>weapon the player has equipped</returns>
        public IEquipment GetWeapon()
        {
            return Weapon;
        }

        /// <summary>
        /// Returns the player's equipped armor
        /// </summary>
        /// <returns>armor the player has equipped</returns>
        public IEquipment GetArmor()
        {
            return Armor;
        }

        /// <summary>
        /// Determines if the player has a stack of potions in their inventory that is not full
        /// </summary>
        /// <returns>true if their is an open spot in a potion stack</returns>
        private bool HasOpenPotionStack()
        {
            bool temp = false;

            foreach(IItem item in Inventory)
            {
                if (item is Potion)
                {
                    Potion pot = item as Potion;

                    if (pot.Quantity < 5)
                    {
                        temp = true;
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// Prints the character page (name stats and equipment) as a list 
        /// at the specified coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void PrintStats(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"{Name}");

            Console.SetCursorPosition(x, y + 1);
            Console.Write($"Health: {CurrentHealth}/{GetStat("HP")}");

            Console.SetCursorPosition(x, y + 2);
            Console.Write($"Str: {GetStat("Str")}");

            Console.SetCursorPosition(x, y + 3);
            Console.Write($"Dex: {GetStat("Dex")}");

            Console.SetCursorPosition(x, y + 4);
            Console.Write($"Con: {GetStat("Con")}");

            Console.SetCursorPosition(x, y + 5);
            Console.Write($"Int: {GetStat("Int")}");

            Console.SetCursorPosition(x, y + 6);
            Console.Write($"Wis: {GetStat("Wis")}");

            Console.SetCursorPosition(x, y + 7);
            Console.Write($"Cha: {GetStat("Cha")}");

            Console.SetCursorPosition(x, y + 8);
            Console.Write($"Level: {GetStat("Level")}");

            Console.SetCursorPosition(x, y + 9);
            Console.Write($"XP: {GetStat("XP")}");

            Console.SetCursorPosition(x, y + 10);
            Console.Write($"AC: {GetStat("AC")}");

            Console.SetCursorPosition(x, y + 11);
            Console.Write($"EV: {GetStat("EV")}");

            Console.SetCursorPosition(x, y + 13);
            if (Weapon != default)
            {
                Console.Write($"Weapon: {Weapon.Name}");
            }
            else
            {
                //Clears the weapon field if weapon has been unequipped
                Console.Write(new string(' ', Console.WindowWidth - x));
                Console.SetCursorPosition(x, y + 13);
                Console.Write("Weapon: ");
            }

            Console.SetCursorPosition(x, y + 14);
            if (Armor != default)
            {
                Console.Write($"Armor: {Armor.Name}");
            }
            else
            {
                //Clears the armor field if armor has been unequipped
                Console.Write(new string(' ', Console.WindowWidth - x));
                Console.SetCursorPosition(x, y + 14);
                Console.Write("Armor: ");
            }
        }

        /// <summary>
        /// Increases the players experience (XP) stat
        /// by a given amount
        /// </summary>
        /// <param name="xp">Amount of xp to gain</param>
        public void GainXP(int xp)
        {
            StatBlock["XP"] += xp;
        }
    }
}
