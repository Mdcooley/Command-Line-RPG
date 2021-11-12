using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    class Robes : IEquipment
    {
        //Stores the name of the item
        public string Name { get; private set; }

        //Stores the type of the item
        //(weapon, armor, consumable, etc..)
        public string Type { get; private set; }

        //Stores the class that the equipment is for
        public string Class { get; private set; }

        //Stores the stat modifiers on the equipment
        public Dictionary<string, int> StatBlock { get; private set; }

        /// <summary>
        /// Constructor for the robes armor class, sets the name, type, class, and stat modifiers
        /// </summary>
        public Robes()
        {
            Type = "Armor";

            Class = "Wizard";

            StatBlock = new Dictionary<string, int> { { "Wis", 2 }, { "Int", 2 } };

            Name = GenerateName();
        }

        /// <summary>
        /// Generates a name for the item that factors it both the type
        /// and all of the stat bonuses it includes
        /// </summary>
        /// <returns>The full name as a string</returns>
        public string GenerateName()
        {
            string name = "Robes [";

            foreach (KeyValuePair<string, int> entry in StatBlock)
            {
                name += $" {entry.Key}+{entry.Value}";
            }

            name += " ]";

            return name;
        }
    }
}
