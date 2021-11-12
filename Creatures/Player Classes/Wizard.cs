using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Wizard : Character
    {
        /// <summary>
        /// Constructor for a wizard object, sets the name to the given string
        /// and sets the base stats.
        /// </summary>
        /// <param name="name">The desired name of the character</param>
        public Wizard(string name)
        {
            this.Name = name;
            this.xCoord = 1;
            this.yCoord = 1;

            this.StatBlock["Str"] = 8;
            this.StatBlock["Dex"] = 13;
            this.StatBlock["Con"] = 10;
            this.StatBlock["Int"] = 15;
            this.StatBlock["Wis"] = 12;
            this.StatBlock["Cha"] = 14;
            this.StatBlock["HitDie"] = 6;
            this.StatBlock["HP"] = 10 + this.CalcModifier(StatBlock["Str"]) + this.CalcModifier(StatBlock["Con"]);
            this.StatBlock["MP"] = 10 + this.CalcModifier(StatBlock["Int"]) + this.CalcModifier(StatBlock["Wis"]);
            this.StatBlock["XP"] = 0;
            this.StatBlock["Level"] = 1;
            this.StatBlock["AC"] = 10;
            this.StatBlock["EV"] = 10;

            Class = "Wizard";

            this.CurrentHealth = StatBlock["HP"];
            this.CurrentMana = StatBlock["MP"];

            this.IsPlayer = true;

            Pickup(new Potion());
            Pickup(new Potion());
        }

        /// <summary>
        /// Attacks a single target, damage is based on int
        /// </summary>
        /// <param name="target">Enemy to be targeted</param>
        public override void BasicAttack(Creature target)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int accuracy = rand.Next(1, 21) + CalcModifier(GetStat("Wis"));
            int power = rand.Next(1, 21) + CalcModifier(GetStat("Int"));
            int damage = rand.Next(1, 7) + CalcModifier(GetStat("Int"));

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
