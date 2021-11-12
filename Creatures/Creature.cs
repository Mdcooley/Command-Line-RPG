using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public abstract class Creature
    {
        //Data structure containing all information about a creatures' stats
        //Stats to be included are: Str, Dex, Con, Int, Wis, Cha, HitDie, Hp,
        //Mp, Xp, and Level
        protected Dictionary<string, int> StatBlock = new Dictionary<string, int>
        { { "Str", 0 },
          { "Dex", 0 },
          { "Con", 0 },
          { "Int", 0 },
          { "Wis", 0 },
          { "Cha", 0 },
          { "HitDie", 0 },
          { "HP", 0 },
          { "MP", 0 },
          { "XP", 0 },
          { "Level", 0 },
          { "AC", 0 },
          { "EV", 0} };

        //Integer that stores the creatures current health (distinct from HP which is maximum health)
        public int CurrentHealth { get; set; }

        //Integer that stores the creatures current mana (distinct from MP which is maximum mana)
        public int CurrentMana { get; set; }

        //String that stores the creatures name
        public string Name { get; set; }

        //Int that stores the creatures' initiative
        public int Initiative { get; private set; }

        //Tracks whether any given creature is the player; true if player, false otherwise
        public bool IsPlayer { get; set; }

        /// <summary>
        /// Increases the creatures level by 1 and increases their stats accordingly
        /// HP increases by 1dHidDie + Con
        /// Str/Dex/Con/Int/Wis/Cha have a 40 + 10*modifier % chance to increase
        /// Level is increased by 1
        /// </summary>
        public void LevelUp()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            if (rand.Next(1,101) <= (40 + (CalcModifier(StatBlock["Str"]) * 10)))
            {
                StatBlock["Str"] += 1;
            }

            if (rand.Next(1, 101) <= (40 + (CalcModifier(StatBlock["Dex"]) * 10)))
            {
                StatBlock["Dex"] += 1;
            }

            if (rand.Next(1, 101) <= (40 + (CalcModifier(StatBlock["Con"]) * 10)))
            {
                StatBlock["Con"] += 1;
            }

            if (rand.Next(1, 101) <= (40 + (CalcModifier(StatBlock["Int"]) * 10)))
            {
                StatBlock["Int"] += 1;
            }

            if (rand.Next(1, 101) <= (40 + (CalcModifier(StatBlock["Wis"]) * 10)))
            {
                StatBlock["Wis"] += 1;
            }

            if (rand.Next(1, 101) <= (40 + (CalcModifier(StatBlock["Cha"]) * 10)))
            {
                StatBlock["Cha"] += 1;
            }

            StatBlock["HP"] += (rand.Next(1, StatBlock["HitDie"] + 1) + CalcModifier(StatBlock["Con"]));
            CurrentHealth = StatBlock["HP"];

            StatBlock["Level"] += 1;
        }

        /// <summary>
        /// Calculates the modifier for an ability score based off of d&d 5e calculations
        /// modifier = (ability score - 10) / 2; rounded down
        /// </summary>
        /// <param name="stat">The character stat to have the modifier calculated for</param>
        /// <returns>The modifier for the given stat</returns>
        public int CalcModifier(int stat)
        {
            return (int)Math.Floor((stat - 10) / 2f);
        }

        /// <summary>
        /// Calculates a random initiative for the creature, modified by their dex and wis scores.
        /// Initiative is used to determine combat order
        /// </summary>
        public void RollInitiative()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            this.Initiative = (rand.Next(1, 21) + CalcModifier(StatBlock["Dex"]) + CalcModifier(StatBlock["Wis"]));
        }

        /// <summary>
        /// Accesses the creatures StatBlock and return a chosen stat
        /// </summary>
        /// <param name="stat">Stat to be returned</param>
        /// <returns>The value of the stat</returns>
        public int GetStat(string stat)
        {
            return StatBlock[stat];
        }

        //Basic attack function, deals damage to an enemy of type Creature
        //Which stat the attack is based off of varies between classes.
        public abstract void BasicAttack(Creature target);

        //Special attack funtion, effects vary between classes.
        public abstract void SpecialAttack(Creature target);
    }
}
