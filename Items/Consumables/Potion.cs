using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Potion : IItem
    {
        //Stores the name of the item
        public string Name { get; private set; }

        //Stores the type of the item
        //(weapon, armor, consumable, etc..)
        public string Type { get; private set; }

        //Stores the strength (amount of healing) of the potion
        public int Strength { get; private set; }

        //Stores how many potions are in the stack
        public int Quantity { get; private set; }

        /// <summary>
        /// Potion constructor; sets the quantity, strength, type, and name of
        /// a potion object
        /// </summary>
        public Potion()
        {
            Quantity = 1;

            Strength = 5;

            Type = "Potion";

            Name = $"Potion [{this.Quantity}]";
        }

        /// <summary>
        /// Adds a potion to the stack, updating the quantity and name
        /// in the process
        /// </summary>
        public void AddToStack()
        {
            Quantity += 1;
            Name = $"Potion [{this.Quantity}]";
        }

        /// <summary>
        /// Makes the targeted character consume the potion, restoring their health
        /// </summary>
        /// <param name="player">Player to have consume the potion</param>
        public void Consume(Character player)
        {
            //Makes sure a player with full HP can't consume the potion
            if (player.CurrentHealth < player.GetStat("HP"))
            {
                //Makes sure the player cannot heal past their maximum health
                if (player.CurrentHealth + Strength <= player.GetStat("HP"))
                {
                    player.CurrentHealth += Strength;
                }
                else
                {
                    player.CurrentHealth = player.GetStat("HP");
                }

                Quantity -= 1;
                Name = $"Potion [{this.Quantity}]";

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"You have regained {Strength} HP!");
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Your health is already full!");
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
    }
}
