using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Warrior : Character
    {
        /// <summary>
        /// Constructor for a warrior object, sets the name to the given string
        /// and sets the base stats.
        /// </summary>
        /// <param name="name">The desired name of the character</param>
        public Warrior(string name)
        {
            this.Name = name;
            this.xCoord = 1;
            this.yCoord = 1;

            this.StatBlock["Str"] = 15;
            this.StatBlock["Dex"] = 12;
            this.StatBlock["Con"] = 14;
            this.StatBlock["Int"] = 10;
            this.StatBlock["Wis"] = 13;
            this.StatBlock["Cha"] = 8;
            this.StatBlock["HitDie"] = 10;
            this.StatBlock["HP"] = 10 + this.CalcModifier(StatBlock["Str"]) + this.CalcModifier(StatBlock["Con"]);
            this.StatBlock["MP"] = 10 + this.CalcModifier(StatBlock["Int"]) + this.CalcModifier(StatBlock["Wis"]);
            this.StatBlock["XP"] = 0;
            this.StatBlock["Level"] = 1;
            this.StatBlock["AC"] = 10;
            this.StatBlock["EV"] = 10;

            Class = "Warrior";

            this.CurrentHealth = StatBlock["HP"];
            this.CurrentMana = StatBlock["MP"];

            this.IsPlayer = true;

            Pickup(new Potion());
            Pickup(new Potion());
        }

        /// <summary>
        /// Attacks a single target, damage is based on str
        /// </summary>
        /// <param name="target">Enemy to be targeted</param>
        public override void BasicAttack(Creature target)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int accuracy = rand.Next(1, 21) + CalcModifier(GetStat("Dex"));
            int power = rand.Next(1, 21) + CalcModifier(GetStat("Str"));
            int damage = rand.Next(1, 7) + CalcModifier(GetStat("Str"));

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
        /// Behavior not determined yet
        /// </summary>
        /// <param name="target">Unit to be targeted</param>
        public override void SpecialAttack(Creature target)
        {
            throw new NotImplementedException();
        }
    }
}
