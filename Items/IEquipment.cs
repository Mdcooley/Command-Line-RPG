using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public interface IEquipment : IItem
    {
        //Stores the class that the equipment is for
        public string Class { get; }

        //Stores the stat modifiers of the equipment
        public Dictionary<string, int> StatBlock { get; }
    }
}
