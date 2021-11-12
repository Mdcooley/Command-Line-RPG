using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    class Mummy : Creature
    {
        /// <summary>
        /// Constructor for the Mummy class. Sets the base stats & initiative.
        /// Sets the name to "Mummy" + number (Mummy1, Mummy2, etc..) based off of 
        /// the objects order in combat
        /// </summary>
        /// <param name="number"></param>
        public Mummy(int number, int level)
        {
            this.StatBlock["Str"] = 10;
            this.StatBlock["Dex"] = 10;
            this.StatBlock["Con"] = 10;
            this.StatBlock["Int"] = 12;
            this.StatBlock["Wis"] = 10;
            this.StatBlock["Cha"] = 10;
            this.StatBlock["HitDie"] = 6;
            this.StatBlock["HP"] = StatBlock["HitDie"];
            this.StatBlock["MP"] = 10;
            this.StatBlock["XP"] = 50;
            this.StatBlock["Level"] = 1;
            this.StatBlock["AC"] = 5;
            this.StatBlock["EV"] = 5 + CalcModifier(StatBlock["Dex"]);

            this.CurrentHealth = StatBlock["HP"];
            this.CurrentMana = StatBlock["MP"];

            this.Name = "Mummy" + number;

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
