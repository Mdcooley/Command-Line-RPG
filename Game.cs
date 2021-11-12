using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Game
    {
        //Stores the map to be used in the current game
        private Map WorldMap;

        //Stores the player character for the current game
        private Character player;

        /// <summary>
        /// Constructor for the game class, copies the character created during the startup process
        /// and generates a new map for use in this game instance
        /// </summary>
        /// <param name="character">A character object with a specified name and class</param>
        public Game(Character character)
        {
            this.player = character;
            this.WorldMap = new Map();
        }

        /// <summary>
        /// Control function for the game; runs a loop that checks player inputs
        /// and performs the appropriate actions based on them
        /// </summary>
        public void Start()
        {
            ConsoleKeyInfo input = default;

            Console.Clear();
            WorldMap.PrintLegend(player);
            WorldMap.DisplayVisible(player.xCoord, player.yCoord);

            do
            {
                //ReadKey is used so that enter does not need to be pressed for each input
                //true argument prevents the key from being printed to the console
                input = Console.ReadKey(true);

                switch (input.Key.ToString())
                {
                    case "W":
                    case "UpArrow":
                        {
                            Move('U');
                            break;
                        }
                    case "A":
                    case "LeftArrow":
                        {
                            Move('L');
                            break;
                        }
                    case "S":
                    case "DownArrow":
                        {
                            Move('D');
                            break;
                        }
                    case "D":
                    case "RightArrow":
                        {
                            Move('R');
                            break;
                        }
                    case "P":
                        {
                            DropItem();
                            break;
                        }
                    case "U":
                        {
                            Unequip();
                            break;
                        }
                    case "D0":
                        {
                            if (player.GetNumItems() >= 1)
                            {
                                SmartUse(0);
                            }
                            break;
                        }
                    case "D1":
                        {
                            if (player.GetNumItems() >= 2)
                            {
                                SmartUse(1);
                            }
                            break;
                        }
                    case "D2":
                        {
                            if (player.GetNumItems() >= 3)
                            {
                                SmartUse(2);
                            }
                            break;
                        }
                    case "D3":
                        {
                            if (player.GetNumItems() >= 4)
                            {
                                SmartUse(3);
                            }
                            break;
                        }
                    case "D4":
                        {
                            if (player.GetNumItems() >= 5)
                            {
                                SmartUse(4);
                            }
                            break;
                        }
                    case "D5":
                        {
                            if (player.GetNumItems() >= 6)
                            {
                                SmartUse(5);
                            }
                            break;
                        }
                    case "D6":
                        {
                            if (player.GetNumItems() >= 7)
                            {
                                SmartUse(6);
                            }
                            break;
                        }
                    case "D7":
                        {
                            if (player.GetNumItems() >= 8)
                            {
                                SmartUse(7);
                            }
                            break;
                        }
                    case "D8":
                        {
                            if (player.GetNumItems() >= 9)
                            {
                                SmartUse(8);
                            }
                            break;
                        }
                    case "D9":
                        {
                            if (player.GetNumItems() >= 10)
                            {
                                SmartUse(9);
                            }
                            break;
                        }
                    case "Escape":
                        {
                            break;
                        }
                    default:
                        {
                            Console.Write("You chose an invalid option");
                            Console.SetCursorPosition(0, Console.CursorTop);
                            break;
                        }
                }
            } while (input.Key.ToString() != "Escape" && player.CurrentHealth > 0);   
        }

        /// <summary>
        /// Moves the character's position on the map; Accepts for arguments for direction:
        /// L, R, U, D; modifies the characters coordinates based on the direction and then
        /// calls the Maps display function(s) to update the position graphically.
        /// Checks the the desired direction of movement is valid.
        /// </summary>
        /// <param name="dir">Character representing the direction to be moved</param>
        private void Move(char dir)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            dir = Char.ToUpper(dir);

            switch(dir)
            {
                case 'U':
                    {
                        //True if the desired tile is a valid move
                        if (WorldMap.TileAt(player.xCoord, player.yCoord - 1) != 1)
                        {
                            player.yCoord -= 1;

                            if (rand.Next(1, 51) == 50)
                            {
                                WorldMap.ClearVisible();
                                Combat(GenerateBattle(WorldMap.TileAt(player.xCoord, player.yCoord)));

                                if (player.CurrentHealth > 0)
                                {
                                    WorldMap.PrintLegend(player);
                                    WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                                }
                            }
                            else
                            {
                                WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                            }

                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        else
                        {
                            Console.Write("You feel a force blocking your way!");
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        break;
                    }
                case 'L':
                    {
                        //True if the desired tile is a valid move
                        if (WorldMap.TileAt(player.xCoord - 1, player.yCoord) != 0)
                        {
                            player.xCoord -= 1;

                            if (rand.Next(1, 51) == 50)
                            {
                                WorldMap.ClearVisible();
                                Combat(GenerateBattle(WorldMap.TileAt(player.xCoord, player.yCoord)));

                                if (player.CurrentHealth > 0)
                                {
                                    WorldMap.PrintLegend(player);
                                    WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                                }
                            }
                            else
                            {
                                WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                            }

                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        else
                        {
                            Console.Write("You feel a force blocking your way!");
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        break;
                    }
                case 'D':
                    {
                        //True if the desired tile is a valid move
                        if (WorldMap.TileAt(player.xCoord, player.yCoord + 1) != 1)
                        {
                            player.yCoord += 1;

                            if (rand.Next(1, 51) == 50)
                            {
                                WorldMap.ClearVisible();
                                Combat(GenerateBattle(WorldMap.TileAt(player.xCoord, player.yCoord)));

                                if (player.CurrentHealth > 0)
                                {
                                    WorldMap.PrintLegend(player);
                                    WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                                }
                            }
                            else
                            {
                                WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                            }

                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        else
                        {
                            Console.Write("You feel a force blocking your way!");
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        break;
                    }
                case 'R':
                    {
                        //True if the desired tile is a valid move
                        if (WorldMap.TileAt(player.xCoord + 1, player.yCoord) != 0)
                        {
                            player.xCoord += 1;

                            if (rand.Next(1,51) == 50)
                            {
                                WorldMap.ClearVisible();
                                Combat(GenerateBattle(WorldMap.TileAt(player.xCoord, player.yCoord)));

                                if (player.CurrentHealth > 0)
                                {
                                    WorldMap.PrintLegend(player);
                                    WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                                }
                            }
                            else
                            {
                                WorldMap.DisplayVisible(player.xCoord, player.yCoord);
                            }

                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        else
                        {
                            Console.Write("You feel a force blocking your way!");
                            Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Generates a list of creatures that will be taking place in a battle based off of terrain type.
        /// Sorts the list based off of each creatures initiative (descending).
        /// 2 = plains
        /// 3 = forest
        /// 4 = desert
        /// 5 = water
        /// 6 = mountains
        /// </summary>
        /// <param name="terrain">Type of terrain that the battle will take place on</param>
        /// <returns>Sorted list of creatures (including the player) taking part in the battle</returns>
        private List<Creature> GenerateBattle(int terrain)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            List<Creature> creatures = new List<Creature>();

            //Random number from 1 - 3; determines how many enemies will be generated
            int numEnemies = rand.Next(1, 4);
            int enemyType = 0;

            //Sets player initiative for this battle
            player.RollInitiative();

            //Adds the player to the battle
            creatures.Add(player);

            //Switches between enemy generation probabilities based on terrain
            switch(terrain)
            {
                case 2:
                    {
                        for (int a = 1; a <= numEnemies; a++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Bandit
                            //Medium probability of generating Wolf or Mummy
                            //Low probability of generating Wyvern or Kraken
                            if(enemyType == 1)
                            {
                                creatures.Add(new Wyvern(a, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Kraken(a, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Wolf(a, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Mummy(a, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Bandit(a, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        for (int b = 1; b <= numEnemies; b++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Wolf
                            //Medium probability of generating Bandit or Mummy
                            //Low probability of generating Wyvern or Kraken
                            if (enemyType == 1)
                            {
                                creatures.Add(new Wyvern(b, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Kraken(b, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Bandit(b, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Mummy(b, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Wolf(b, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        for (int c = 1; c <= numEnemies; c++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Mummy
                            //Medium probability of generating Wolf or Bandit
                            //Low probability of generating Wyvern or Kraken
                            if (enemyType == 1)
                            {
                                creatures.Add(new Wyvern(c, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Kraken(c, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Bandit(c, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Wolf(c, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Mummy(c, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        for (int d = 1; d <= numEnemies; d++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Kraken
                            //Medium probability of generating Wolf or Bandit
                            //Low probability of generating Wyvern or Mummy
                            if (enemyType == 1)
                            {
                                creatures.Add(new Wyvern(d, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Mummy(d, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Bandit(d, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Wolf(d, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Kraken(d, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        for (int e = 1; e <= numEnemies; e++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Wyvern
                            //Medium probability of generating Wolf or Bandit
                            //Low probability of generating Mummy or Kraken
                            if (enemyType == 1)
                            {
                                creatures.Add(new Kraken(e, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Mummy(e, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Bandit(e, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Wolf(e, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Wyvern(e, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
                default:
                    {
                        for (int f = 1; f <= numEnemies; f++)
                        {
                            enemyType = rand.Next(1, 101);

                            //Highest probability of generating Bandit
                            //Medium probability of generating Wolf or Mummy
                            //Low probability of generating Wyvern or Kraken
                            if (enemyType == 1)
                            {
                                creatures.Add(new Wyvern(f, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType == 2)
                            {
                                creatures.Add(new Kraken(f, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 3 && enemyType <= 17)
                            {
                                creatures.Add(new Wolf(f, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else if (enemyType >= 18 && enemyType <= 32)
                            {
                                creatures.Add(new Mummy(f, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                            else
                            {
                                creatures.Add(new Bandit(f, rand.Next(player.GetStat("Level") - 1, player.GetStat("Level") + 2)));
                            }
                        }
                        break;
                    }
            }

            //Uses linq to sort the list of creatures based off of initiative
            var sorted = from element in creatures
                         orderby element.Initiative descending
                         select element;

            creatures = sorted.ToList<Creature>();

            return creatures;
        }

        /// <summary>
        /// Manages the logic for the battle; Loops until either the player or all enemies are dead
        /// removes enemies from the battle as they die
        /// </summary>
        /// <param name="Battle">Sorted list of creatures (including the player) taking part in the battle</param>
        private void Combat(List<Creature> Battle)
        {
            int numEnemies = Battle.Count - 1;
            int xp = Experience(Battle);
            Random rand = new Random((int)DateTime.Now.Ticks);

            //Clears input buffer
            while(Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }

            Console.Clear();
            string target = default;

            //Loops until either the player or all of the enemies are dead
            do
            {
                Console.WriteLine($"Health: {player.CurrentHealth}/{player.GetStat("HP")}\n");

                Console.Write("Combat order is: ");

                //Lists all creatures (including player) in combat
                //Lists them in the order that they will act
                foreach (Creature creature in Battle)
                {
                    Console.Write($"{creature.Name}(lvl {creature.GetStat("Level")}) ");
                }

                Console.WriteLine();

                //Loops through every creature in battle
                //Not a foreach because it is sometimes necessary to remove a creature from the list
                for (int i = 0; i < Battle.Count; i++)
                {
                    //Checks if the active creature is the player
                    if (Battle[i].IsPlayer)
                    {
                        Console.Write("Enter target (do not include lvl): ");
                        target = Console.ReadLine();

                        //Ensures that the player chooses a valid target
                        //Valid targets must be creatures that are in combat
                        //and are not the player
                        while (!IsValidTarget(target, Battle))
                        {
                            Console.Write("Please enter the name of a valid target: ");
                            target = Console.ReadLine();
                        }

                        //Selects the targeted enemy from the list
                        var enemy = from element in Battle
                                    where element.Name == target
                                    select element;

                        player.BasicAttack(enemy.First());

                        //Removes the enemy from battle if they have died
                        //Adjusts the index of the for loop if the enemy was acting before the player
                        //This is necessary so that creatures are not skipped as their index in the battle is shifted
                        if (enemy.First().CurrentHealth <= 0)
                        {
                            if (Battle.IndexOf(enemy.First()) < Battle.IndexOf(player))
                            {
                                i--;
                            }

                            Battle.Remove(enemy.First());
                        }
                    }
                    else
                    {
                        Battle[i].BasicAttack(player);

                        //Ends combat as soon as the player dies
                        if (player.CurrentHealth <= 0)
                        {
                            break;
                        }
                    }
                }

                //Pauses the screen at the end of each round of combat
                Program.Pause();

                //Clears the screen at the end of each round of combat
                Console.Clear();

            } while (Battle.Count > 1 && player.CurrentHealth > 0);

            //Experience gain and chance for loot if player is still alive
            if (player.CurrentHealth > 0)
            {
                player.GainXP(xp);
                Console.WriteLine($"You have gained {xp} xp!");

                if(player.GetStat("XP") >= ((player.GetStat("Level") + 1) * player.GetStat("Level") * 250))
                {
                    player.LevelUp();
                    Console.WriteLine("You have leveled up!");
                    player.PrintStats(Console.CursorLeft, Console.CursorTop);
                    Console.WriteLine();
                }

                for (int i = 0; i < numEnemies; i++)
                {
                    //20% chance per enemy killed of getting a potion
                    if (rand.Next(1, 6) == 5)
                    {
                        Console.WriteLine("You got a potion!");
                        player.Pickup(new Potion());
                    }

                    //2% chance per enemy killed of getting a random piece of equipment
                    if (rand.Next(1, 51) == 50)
                    {
                        switch(rand.Next(1,7))
                        {
                            case 1:
                                {
                                    IEquipment temp = new Sword();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            case 2:
                                {
                                    IEquipment temp = new Bow();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            case 3:
                                {
                                    IEquipment temp = new Staff();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            case 4:
                                {
                                    IEquipment temp = new Robes();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            case 5:
                                {
                                    IEquipment temp = new Leather();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            case 6:
                                {
                                    IEquipment temp = new Plate();
                                    Console.WriteLine($"You got a {temp.Name}");
                                    player.Pickup(temp);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }

                Program.Pause();
            }
            else
            {
                ConsoleKeyInfo selection = default;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("You Have Died, press Esc to return to the Main Menu");
                Console.ForegroundColor = ConsoleColor.Gray;

                do
                {
                    selection = Console.ReadKey(true);
                } while (selection.Key.ToString() != "Escape");
            }

            //Clears the screen after the battle ends
            Console.Clear();
        }

        /// <summary>
        /// Determines whetehr a specified name is a valid target for the player
        /// to attack in combat
        /// 
        /// Valid targets must be creatures that are in combat and are not the player
        /// </summary>
        /// <param name="name">Name of the desired target</param>
        /// <param name="combat">List of creatures in combat</param>
        /// <returns></returns>
        private bool IsValidTarget(string name, List<Creature> combat)
        {
            bool valid = false;

            //Iterates through all creatures in combat
            foreach(Creature creature in combat)
            {
                if (creature.Name == name && !creature.IsPlayer)
                {
                    valid = true;
                }
            }

            return valid;
        }


        /// <summary>
        /// Prompts the player to select an item from their inventory
        /// then removes that item from their inventory
        /// </summary>
        private void DropItem()
        {
            bool dropped = false;
            ConsoleKeyInfo selection = default;

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Select which item you would like to drop: ");
            Console.SetCursorPosition(0, Console.CursorTop);

            //Loops until the player either selects a valid item to drop or quits
            do
            {
                selection = Console.ReadKey(true);

                switch (selection.Key.ToString())
                {
                    case "D0":
                        {
                            if (player.GetNumItems() >= 1)
                            {
                                player.Drop(player.GetIndex(0));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D1":
                        {
                            if (player.GetNumItems() >= 2)
                            {
                                player.Drop(player.GetIndex(1));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D2":
                        {
                            if (player.GetNumItems() >= 3)
                            {
                                player.Drop(player.GetIndex(2));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D3":
                        {
                            if (player.GetNumItems() >= 4)
                            {
                                player.Drop(player.GetIndex(3));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D4":
                        {
                            if (player.GetNumItems() >= 5)
                            {
                                player.Drop(player.GetIndex(4));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D5":
                        {
                            if (player.GetNumItems() >= 6)
                            {
                                player.Drop(player.GetIndex(5));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D6":
                        {
                            if (player.GetNumItems() >= 7)
                            {
                                player.Drop(player.GetIndex(6));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D7":
                        {
                            if (player.GetNumItems() >= 8)
                            {
                                player.Drop(player.GetIndex(7));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D8":
                        {
                            if (player.GetNumItems() >= 9)
                            {
                                player.Drop(player.GetIndex(8));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "D9":
                        {
                            if (player.GetNumItems() >= 10)
                            {
                                player.Drop(player.GetIndex(9));
                                dropped = true;
                                WorldMap.PrintLegend(player);
                            }
                            else
                            {
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write("Please select a valid item or press Esc to quit: ");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                            break;
                        }
                    case "Escape":
                        {
                            dropped = true;
                            break;
                        }
                    default:
                        {
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write("Please select a valid item or press Esc to quit: ");
                            Console.SetCursorPosition(0, Console.CursorTop);
                            break;
                        }
                }
            } while (!dropped);

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        /// <summary>
        /// "Uses" the item at the specified index in the players inventory
        /// usage varies between item types, weapons and armor will (attempt) to be equipped
        /// potions will be consumend
        /// </summary>
        /// <param name="index"></param>
        private void SmartUse(int index)
        {
            //Determines whether the item will be consumed or equipped
            if (player.GetIndex(index) is Potion)
            {
                Potion temp = player.GetIndex(index) as Potion;
                temp.Consume(player);

                if (temp.Quantity <= 0)
                {
                    player.Drop(temp);
                }

                WorldMap.PrintLegend(player);
            }
            else if (player.GetIndex(index) is IEquipment)
            {
                IEquipment equipment = player.GetIndex(index) as IEquipment;

                player.Equip(equipment);

                WorldMap.PrintLegend(player);
            }
        }

        /// <summary>
        /// Prompts the player to select either weapon or armor and then removes
        /// the selected piece of equipment
        /// </summary>
        private void Unequip()
        {
            ConsoleKeyInfo selection = default;
            bool unequipped = false;

            //Clears the display line under the map window
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Would you like to unequip your (W)eapon or (A)rmor?");
            Console.SetCursorPosition(0, Console.CursorTop);

            //Continuously prompts the player until a valid selection is made
            do
            {
                selection = Console.ReadKey(true);

                switch (selection.Key.ToString())
                {
                    case "W":
                        {
                            player.Unequip('w');
                            unequipped = true;
                            WorldMap.PrintLegend(player);
                            break;
                        }
                    case "A":
                        {
                            player.Unequip('a');
                            unequipped = true;
                            WorldMap.PrintLegend(player);
                            break;
                        }
                    case "Escape":
                        {
                            unequipped = true;
                            break;
                        }
                    default:
                        {
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write("Please select (W)eapon, (A)rmor, or Esc to quit:");
                            Console.SetCursorPosition(0, Console.CursorTop);
                            break;
                        }
                }
            } while (!unequipped);

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        /// <summary>
        /// Calculates the amount of experience the player will gain
        /// for completing the given battle
        /// </summary>
        /// <param name="Battle">List of creatures in combat</param>
        /// <returns>The amount of experience to gain</returns>
        private int Experience(List<Creature> Battle)
        {
            int xp = 0;

            foreach (Creature enemy in Battle)
            {
                if (!enemy.IsPlayer)
                {
                    xp += enemy.GetStat("XP") * enemy.GetStat("Level");
                }
            }

            return xp;
        }
    }
}