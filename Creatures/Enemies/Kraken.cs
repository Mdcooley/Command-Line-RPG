using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    class Kraken : Creature
    {
        /// <summary>
        /// Constructor for the Kraken class. Sets the base stats & initiative.
        /// Sets the name to "Kraken" + number (Kraken1, Kraken2, etc..) based off of 
        /// the objects order in combat
        /// </summary>
        /// <param name="number"></param>
        public Kraken(int number, int level)
        {
            this.StatBlock["Str"] = 10;
            this.StatBlock["Dex"] = 10;
            this.StatBlock["Con"] = 10;
            this.StatBlock["Int"] = 12;
            this.StatBlock["Wis"] = 12;
            this.StatBlock["Cha"] = 10;
            this.StatBlock["HitDie"] = 10;
            this.StatBlock["HP"] = StatBlock["HitDie"];
            this.StatBlock["MP"] = 10;
            this.StatBlock["XP"] = 125;
            this.StatBlock["Level"] = 1;
            this.StatBlock["AC"] = 5;
            this.StatBlock["EV"] = 5 + CalcModifier(StatBlock["Dex"]);

            this.CurrentHealth = StatBlock["HP"];
            this.CurrentMana = StatBlock["MP"];

            this.Name = "Kraken" + number;

            for (int i = 0; i < level - 1; i++)
            {
                LevelUp();
            }

            this.RollInitiative();

            this.IsPlayer = false;
        }

        /// <summary>
        /// Attacks a single target, damage is based on int
        /// </summary>
        /// <param name="target">Enemy to be targeted</param>
        public override void BasicAttack(Creature target)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int accuracy = rand.Next(1, 21) + CalcModifier(StatBlock["Wis"]);
            int power = rand.Next(1, 21) + CalcModifier(StatBlock["Int"]);
            int damage = rand.Next(0, 2) + CalcModifier(StatBlock["Int"]);

            if (accuracy > target.GetStat("EV") || accuracy == 20)
            {
                if (power > target.GetStat("AC") || power == 20)
                {
                    target.CurrentHealth -= damage;
                    Console.WriteLine($"{this.Name} attacks {target.Name} and deals {damage} damage!");

                    if (target.CurrentHealth <= 0)
                    {
                        Console.WriteLine("You have died");
                    }
                }
                else
                {
                    Console.WriteLine($"{this.Name} attacks {target.Name} but does no damage!");
                }
            }
            else
            {
                Console.WriteLine($"{this.Name} attacks {target.Name} but it misses!");
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
