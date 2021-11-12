using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Project
{
    public class Map
    {
        //2d array that stores map data
        int[,] map;

        //2d array that stores the visible portion of the map
        int[,] visible;

        //Determines the width of the viewable area of the map
        const int VISIBLE_AREA_X = 83;

        //Determines the height of the viewable area of the map
        const int VISIBLE_AREA_Y = 41;

        //Determines the width of the map
        const int PLAYABLE_AREA_X = 1000;

        //Determines the height of the map
        const int PLAYABLE_AREA_Y = 1000;

        /// <summary>
        /// Default constructor for the Map class
        /// Randomly generates a map
        /// 0 - Horizontal border
        /// 1 - Vertical border
        /// 2 - Plains
        /// 3 - Forest
        /// 4 - Desert
        /// 5 - Water
        /// 6 - Mountains
        /// </summary>
        public Map()
        { 
            map = new int[PLAYABLE_AREA_Y, PLAYABLE_AREA_X];

            for (int a = 0; a < PLAYABLE_AREA_Y; a++)
            {
                for (int b = 0; b < PLAYABLE_AREA_X; b++)
                {
                    map[a, b] = -1;
                }
            }
            GenerateTerrain();

            visible = new int[VISIBLE_AREA_Y, VISIBLE_AREA_X];

            for (int i = 0; i < VISIBLE_AREA_Y; i++)
            {
                for (int j = 0; j < VISIBLE_AREA_X; j++)
                {
                    visible[i, j] = -1;
                }
            }
        }
        
        /// <summary>
        /// Returns the type of terrain at a specified point in the map
        /// </summary>
        /// <param name="x">x coordinate of tile</param>
        /// <param name="y">y coordinate of tile</param>
        /// <returns></returns>
        public int TileAt(int x, int y)
        {
            return map[y, x];
        }

        /// <summary>
        /// Prints the correct terrain tile to a specified set of
        /// map coordinates
        /// </summary>
        /// <param name="x">x coordinate to be printed at</param>
        /// <param name="y">y coordinate to be printed at</param>
        public void PrintTile(int x, int y)
        {
            //Moves cursor to the proper coordinates
            Console.SetCursorPosition(x, y);

            //Prints the appropriate terrain tile
            switch (visible[y, x])
            {
                case 0:
                    {
                        Console.Write("|");
                        break;
                    }
                case 1:
                    {
                        Console.Write("-");
                        break;
                    }
                case 2:
                    {
                        PrintPlainsTile();
                        break;
                    }
                case 3:
                    {
                        PrintForestTile();
                        break;
                    }
                case 4:
                    {
                        PrintDesertTile();
                        break;
                    }
                case 5:
                    {
                        PrintWaterTile();
                        break;
                    }
                case 6:
                    {
                        PrintMountainTile();
                        break;
                    }
                case 7:
                    {
                        PrintCharacterTile();
                        break;
                    }
                default:
                    {
                        Console.Write(" ");
                        break;
                    }
            }
        }

        /// <summary>
        /// Prints the controls on the righthand side of the play area
        /// </summary>
        public void PrintLegend(Character player)
        {
            //Current x coordinate of the cursor
            int oldX = Console.CursorLeft;

            //Current y coordinate of the cursor
            int oldY = Console.CursorTop;

            Console.SetCursorPosition(visible.GetLength(1) + 1, 0);
            player.PrintStats(visible.GetLength(1) + 1, 0);

            Console.SetCursorPosition(visible.GetLength(1) + 1, 16);
            Console.Write("Inventory: ");
            player.DisplayInventory(visible.GetLength(1) + 1, 17);

            //Moves the cursor to the right side of the map, 12 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 12);
            Console.Write("Controls:");

            //Moves the cursor to the right side of the map, 11 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 11);
            Console.Write("Movement: Arrow Keys/WASD");

            //Moves the cursor to the right side of the map, 10 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 10);
            Console.Write("Use item: 0 - 9");

            //Moves the cursor to the right side of the map, 9 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 9);
            Console.Write("Drop item: P");

            //Moves the cursor to the right side of the map, 8 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 8);
            Console.Write("Unequip item: U");

            //Moves the cursor to the right side of the map, 7 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 7);
            Console.Write("Exit to main menu: ESC");

            //Moves the cursor to the right side of the map, 5 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 5);
            Console.Write("Legend:");

            //Moves the cursor to the right side of the map, 4 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 4);
            Console.Write("- and |, World Border");

            //Moves the cursor to the right side of the map, 3 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 3);
            PrintCharacterTile();
            Console.Write(" - Player\t");
            PrintPlainsTile();
            Console.Write(" - Plains");

            //Moves the cursor to the right side of the map, 2 rows from the bottom
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 2);
            PrintForestTile();
            Console.Write(" - Forest\t");
            PrintDesertTile();
            Console.Write(" - Desert");

            //Moves the cursor to the right side of the map, bottom row
            Console.SetCursorPosition(visible.GetLength(1) + 1, visible.GetLength(0) - 1);
            PrintWaterTile();
            Console.Write(" - Water\t");
            PrintMountainTile();
            Console.Write(" - Mountain");

            //Moves the cursor back to previous position
            Console.SetCursorPosition(oldX, oldY);
        }

        /// <summary>
        /// Prints the tile that represents the player's character
        /// </summary>
        private void PrintCharacterTile()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("@");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Prints the tile that represents the plains terrain
        /// </summary>
        private void PrintPlainsTile()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(",");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Prints the tile that represents the forest terrain
        /// </summary>
        private void PrintForestTile()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\u00a5");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Prints the tile that represents the desert terrain
        /// </summary>
        private void PrintDesertTile()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Prints the tile that represents the water terrain
        /// </summary>
        private void PrintWaterTile()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("~");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Prints the tile that represents the mountain terrin
        /// </summary>
        private void PrintMountainTile()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("^");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Determines which portion of the map is visible based on the players position
        /// Tracks which tiles of the map have changed since the last time it was called.
        /// </summary>
        /// <param name="x">X coordinate of the player</param>
        /// <param name="y">Y coordinate of the player</param>
        /// <returns>A list of tiles that have been changed since the update was made</returns>
        private List<Coordinate> DetermineVisible(int x, int y)
        {
            //List of tiles that will need to be updated
            List<Coordinate> toUpdate = new List<Coordinate>();

            //Determines the left boundary of the visible area
            //VISIBLE_AREA_X is the width of the visible area, map.GetLength(1) is the total width of the map
            //The nested ternary determines if the visible area would exceed the map boundary; if it would a fixed minimum is used (which minimum depends on which edge they are at)
            //If the visible area would not exceed the boundary, then the minimum is set to the player's x coordinate minus half of the VISIBLE_AREA_X
            int xMin = (x + ((VISIBLE_AREA_X - 1) / 2) >= map.GetLength(1)) ? (map.GetLength(1) - VISIBLE_AREA_X) : (x - ((VISIBLE_AREA_X - 1) / 2) >= 0) ? (x - ((VISIBLE_AREA_X - 1) / 2)) : 0;

            //Determines the right boundary of the visible area
            //VISIBLE_AREA_X is the width of the visible area, map.GetLength(1) is the total width of the map
            //The nested ternary determines if the visible area would exceed the map boundary; if it would a fixed maximum is used (which maximum depends on which edge they are at)
            //If the visible area would not exceed the boundary, then the maximum is set to the player's x coordinate plus half of the VISIBLE_AREA_X
            int xMax = (xMin == 0) ? VISIBLE_AREA_X : (x + (((VISIBLE_AREA_X - 1) / 2) + 1) <= map.GetLength(1)) ? (x + (((VISIBLE_AREA_X - 1) / 2) + 1)) : map.GetLength(1);

            //Determines the top boundary of the visible area
            //VISIBLE_AREA_Y is the height of the visible area, map.GetLength(0) is the total height of the map
            //The nested ternary determines if the visible area would exceed the map boundary; if it would a fixed minimum is used (which minimum depends on which edge they are at)
            //If the visible area would not exceed the boundary, then the minimum is set to the player's y coordinate minus half of the VISIBLE_AREA_Y
            int yMin = (y + ((VISIBLE_AREA_Y - 1) / 2) >= map.GetLength(0)) ? (map.GetLength(0) - VISIBLE_AREA_Y) : (y - ((VISIBLE_AREA_Y - 1) / 2) >= 0) ? (y - ((VISIBLE_AREA_Y - 1) / 2)) : 0;

            //Determines the bottom boundary of the visible area
            //VISIBLE_AREA_Y is the height of the visible area, map.GetLength(0) is the total height of the map
            //The nested ternary determines if the visible area would exceed the map boundary; if it would a fixed maximum is used (which maximum depends on which edge they are at)
            //If the visible area would not exceed the boundary, then the maximum is set to the player's y coordinate plus half of the VISIBLE_AREA_Y
            int yMax = (yMin == 0) ? VISIBLE_AREA_Y : (y + (((VISIBLE_AREA_Y - 1) / 2) + 1) <= map.GetLength(0)) ? (y + (((VISIBLE_AREA_Y - 1) / 2) + 1)) : map.GetLength(0);


            //Nested for loops through a subset of the map
            //Size of the subset is equal to the size of the visible area
            for (int i = yMin, a = 0; i < yMax; i++, a++)
            {
                for (int j = xMin, b = 0; j < xMax; j++, b++)
                {
                    //Checks if any given tile in visible is changing
                    //if it is; adds the coordinates to a list of tiles
                    //that need to be updated
                    if (map[i, j] != visible[a, b] || (j == x && i == y))
                    {
                        toUpdate.Add(new Coordinate(b, a));
                    }

                    //Inserts the player tile into the visible area
                    if (j == x && i == y)
                    {
                        visible[a, b] = 7;
                    }
                    else
                    {
                        visible[a, b] = map[i, j];
                    }
                    
                }
            }

            //Returns the list of tiles that need to be updated
            return toUpdate;
        }

        /// <summary>
        /// Prints the visible portion of the map (including the player tile) to the screen
        /// Only prints the tiles that have changed since the last call to increase performance
        /// </summary>
        /// <param name="x">X coordinate of the player</param>
        /// <param name="y">Y coordinate of the player</param>
        public void DisplayVisible(int x, int y)
        {
            //List containing the coordinates of the tiles that have changed
            List<Coordinate> UpdatedTiles = DetermineVisible(x, y);

            //Iterates through the list and prints the appropriate tile
            foreach (Coordinate position in UpdatedTiles)
            {
                PrintTile(position.x, position.y);
            }

            //Sets the cursor to below the bottom of the map
            Console.SetCursorPosition(0, visible.GetLength(0));
        }

        /// <summary>
        /// Randomly generates terrain for the entire map,
        /// also adds map borders.
        /// </summary>
        private void GenerateTerrain()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int control = 0;

            //Iterates through the entire map
            for (int i = 0; i < PLAYABLE_AREA_Y; i++)
            {
                for (int j = 0; j < PLAYABLE_AREA_X; j++)
                {
                    if (j == 0 || j == (PLAYABLE_AREA_X - 1))
                    {
                        //Left/Right border
                        map[i, j] = 0;
                    }
                    else if (i == 0 || i == (PLAYABLE_AREA_Y - 1))
                    {
                        //Top/Bottom border
                        map[i, j] = 1;
                    }
                    else
                    {
                        //Each tile will have a 1/1500 chance of becoming a new chunk
                        //The remaining tiles will become plains
                        control = rand.Next(1, 1501);

                        switch(control)
                        {
                            case 1:
                                {
                                    //Makes sure the tile is valid to generate terrain on
                                    //-1 == unassigned, 2 == plains, 3 == Forest
                                    if (map[i, j] == -1 || map[i, j] == 2 || map[i, j] == 3)
                                    {
                                        GenerateChunkRecursive(j, i, 3, -2);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    //Makes sure the tile is valid to generate terrain on
                                    //-1 == unassigned, 2 == plains, 4 == Desert
                                    if (map[i, j] == -1 || map[i, j] == 2 || map[i, j] == 4)
                                    {
                                        GenerateChunkRecursive(j, i, 4, -2);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    //Makes sure the tile is valid to generate terrain on
                                    //-1 == unassigned, 2 == plains, 5 == Water
                                    if (map[i, j] == -1 || map[i, j] == 2 || map[i, j] == 5)
                                    {
                                        GenerateChunkRecursive(j, i, 5, -2);
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    //Makes sure the tile is valid to generate terrain on
                                    //-1 == unassigned, 2 == plains, 6 == Mountains
                                    if (map[i, j] == -1 || map[i, j] == 2 || map[i, j] == 6)
                                    {
                                        GenerateChunkRecursive(j, i, 6, -2);
                                    }
                                    break;
                                }
                            default:
                                {
                                    if (map[i, j] == -1 || map[i, j] == 2)
                                    {
                                        map[i, j] = 2;
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recursively generates a chunk of one terrain type.
        /// uses a + shape as the basic unit to build the chunk with; may randomly choose to leave one leg off the +
        /// </summary>
        /// <param name="x">X coordinate of the center of the +</param>
        /// <param name="y">Y coordinate of the center of the +</param>
        /// <param name="terrain">Type of terrain to be generated</param>
        /// <param name="control">Modifies probability of the recursion continuing</param>
        private void GenerateChunkRecursive(int x, int y, int terrain, int control)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            //Determines if the selected tile is valid to generate terrain on
            if (map[y, x] == 2 || map[y, x] == -1 || map[y, x] == terrain)
            {
                map[y, x] = terrain;

                /*
                 *The next four if statements collectively for a + around the center tile.
                 *They check if those four tiles are within the bounds of the map and then
                 *have a 75% chance of flipping the terrain on that tile. This can overwrite
                 *other types of terrain
                 */

                //Upper arm of the +
                if((y - 1) > 0 && rand.Next(1, 5) != 4)
                {
                    map[y - 1, x] = terrain;
                }

                //Left arm of the +
                if((x - 1) > 0 && rand.Next(1, 5) != 4)
                {
                    map[y, x - 1] = terrain;
                }

                //Bottom arm of the +
                if((y + 1) < (map.GetLength(0) - 1) && rand.Next(1, 5) != 4)
                {
                    map[y + 1, x] = terrain;
                }

                //Right arm of the +
                if((x + 1) < (map.GetLength(1) - 1) && rand.Next(1, 5) != 4)
                {
                    map[y, x + 1] = terrain;
                }

                /*
                 * The next four if statements handle the recursive calls for the four corners of the +
                 * They check if the tiles are within the bounds of the array and run a random check for if the recursion will continue
                 * The check resets in between each if statement
                 * The check for whether or not the tile is valid will occur within the recursive call
                 */

                //Top left corner
                if ((y - 1) > 0 && (x - 1) > 0 && rand.Next(1, 6) > control)
                {
                    GenerateChunkRecursive(x - 1, y - 1, terrain, ++control);
                }

                //Top right corner
                if ((y - 1) > 0 && (x + 1) < (map.GetLength(1) - 1) && rand.Next(1, 6) > control)
                {
                    GenerateChunkRecursive(x + 1, y - 1, terrain, ++control);
                }

                //Bottom left corner
                if ((y + 1) < (map.GetLength(0) - 1) && (x - 1) > 0 && rand.Next(1, 6) > control)
                {
                    GenerateChunkRecursive(x - 1, y + 1, terrain, ++control);
                }

                //Bottom right corner
                if ((y + 1) < (map.GetLength(0) - 1) && (x + 1) < (map.GetLength(1) - 1) && rand.Next(1, 6) > control)
                {
                    GenerateChunkRecursive(x + 1, y + 1, terrain, ++control);
                }

            }
        }

        /// <summary>
        /// Sets the visible area of the map to -1; used to make sure that the display
        /// updates properly after the screen has been cleared
        /// </summary>
        public void ClearVisible()
        {
            for (int i = 0; i < VISIBLE_AREA_Y; i++)
            {
                for (int j = 0; j < VISIBLE_AREA_X; j++)
                {
                    visible[i, j] = -1;
                }
            }
        }
    }
}
