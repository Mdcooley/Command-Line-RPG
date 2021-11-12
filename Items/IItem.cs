using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public interface IItem
    {
        //Stores the name of the item
        public string Name { get; }

        //Stores the type of the item
        //(weapon, armor, consumable, etc..)
        public string Type { get; }
    }
}
