using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Ranger : Character
    {
        /// <summary>
        /// Constructor for a ranger object, sets the name to the given string
        /// and sets the base stats.
        /// </summary>
        /// <param name="name">The desired name of the character</param>
        public Ranger(string name)
        {
            this.Name = name;
            this.xCoord = 1;
            this.yCoord = 1;

            this.StatBlock["Str"] = 10;
            this.StatBlock["Dex"] = 15;
            this.StatBlock["Con"] = 13;
            this.StatBlock["Int"] = 12;
            this.StatBlock["Wis"] = 14;
            this.StatBlock["Cha"] = 8;
            this.StatBlock["HitDie"] = 8;
            this.StatBlock["HP"] = 10 + this.CalcModifier(StatBlock["Str"]) + this.CalcModifier(StatBlock["Con"]);
            this.StatBlock["MP"] = 10 + this.CalcModifier(StatBlock["Int"]) + this.CalcModifier(StatBlock["Wis"]);
            this.StatBlock["XP"] = 0;
            this.StatBlock["Level"] = 1;
            this.StatBlock["AC"] = 10;
            this.StatBlock["EV"] = 10;

            Class = "Ranger";

            this.CurrentHealth = StatBlock["HP"];
            this.CurrentMana = StatBlock["MP"];

            this.IsPlayer = true;

            Pickup(new Potion());
            Pickup(new Potion());
        }

        /// <summary>
        /// Attacks a single target, damage is based on dex
        /// </summary>
        /// <param name="target">Enemy to be targeted</param>
        public override void BasicAttack(Creature target)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int accuracy = rand.Next(1, 21) + CalcModifier(GetStat("Dex"));
            int power = rand.Next(1, 21) + CalcModifier(GetStat("Str"));
            int damage = rand.Next(1,7) + CalcModifier(GetStat("Dex"));

            if (accuracy > target.GetStat("EV") || accuracy == 20)
            {
                if (power > target.GetStat("AC") || power == 20)
                {
                    target.CurrentHealth -= damage;
                    Console.WriteLine($"You attack {target.Name} and deal {damage} damage!");

                    if (target.CurrentHealth <= 0)
                    {
                        Console.WriteLine($"{target.Name} has died");
                    }
                }
                else
                {
                    Console.WriteLine($"You attack {target.Name} but your attack does no damage!");
                }
            }
            else
            {
                Console.WriteLine($"You attack {target.Name} but your attack misses!");
            }
        }

        /// <summary>
        /// Behavior not yet determined
        /// </summary>
        /// <param name="target">Unit to be targeted</param>
        public override void SpecialAttack(Creature target)
        {
            throw new NotImplementedException();
        }
    }
}
