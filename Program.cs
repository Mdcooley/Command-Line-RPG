using System;
using System.Collections.Generic;

namespace RPG_Project
{
    class Program
    {
        /// <summary>
        /// Entry point to the program. Resizes and repositions the console,
        /// then calls the title screen and main menu
        /// </summary>
        /// <param name="args">unused</param>
        static void Main(string[] args)
        {
            ConsolePosition.RepositionConsole();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            PrintTitleScreen();
            MainMenu();
        }

        /// <summary>
        /// Prints the title screen and awaits any input
        /// before clearing the screen
        /// </summary>
        public static void PrintTitleScreen()
        {
            Console.SetCursorPosition(Console.CursorLeft, (Console.WindowHeight) / 5);
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@"  ..          .n        ....                    ...                  ...                     ....          =u       ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(" 888B.      z8\"     .xH888888Hx.              x88\" !.              x88\" !.               .xH888888Hx.       '%b.    ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(@"48888E    x88~    .H8888888888888:           888X   8  .-=x.      888X   8  .-=x.      .H8888888888888:       88b   ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("'8888'   x888     888*\"\"\"?\"\"*88888X         X8888. X\" :'.H88L    X8888. X\" :'.H88L     888*\"\"\"?\"\"*88888X      ?88N  ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(" Y88F   :888R    'f     d8x.   ^%88k        ?88888X.  f 4888\"    ?88888X.  f 4888\"    'f     d8x.   ^%88k     4888b ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(" '88    8888R    '>    <88888X   '?8       .x*888888hX   `\"`    .x*888888hX   `\"`     '>    <88888X   '?8     48888r".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(@"  8F   :8888R     `:..:`888888>    8>     d8  `?8888888.       d8  `?8888888.          `:..:`888888>    8>    48888k".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("  4    48888R            `\"*88     X     X88L   `%888888k     X88L   `%888888k                `\"*88     X     48888R".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("  .    '8888R       .xHHhx..\"      !     8888x     ?88888>    8888x     ?88888>          .xHHhx..\"      !     48888F".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine(@" u8N.   8888R      X88888888hx. ..!      888888hx.x! ?888>    888888hx.x! ?888>         X88888888hx. ..!      48888 ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("\"*88%   '888R     !   \"*888888888\"       '*8888888\"  '888     '*8888888\"  '888         !   \"*888888888\"       4888F ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("  \"\"     \"888            ^\"***\"`           `\"\"\"\"\"   .88%        `\"\"\"\"\"   .88%                 ^\"***\"`         d88P  ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("          ^88L                                                                                               .88\"   ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("            \"Ru                                                                                             .@%     ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.WriteLine("              '\"                                                                                           \"        ".PadLeft((Console.WindowWidth / 2) + 58));
            Console.Write("Press any key to start".PadLeft((Console.WindowWidth / 2) + 11));

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Prints the main menu options
        /// </summary>
        public static void PrintMainMenu()
        {
            Console.WriteLine("1). Start game");
            Console.WriteLine("2). Credits");
            Console.WriteLine("3). Quit");
            Console.Write("Select menu option: ");
        }

        /// <summary>
        /// Prints a message telling the user that the program is awaiting input,
        /// then waits for any key to be pressed
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Handles all of the logic and input for the main menu
        /// </summary>
        public static void MainMenu()
        {
            int control = 0;

            do
            {
                control = 0;

                PrintMainMenu();

                control = CheckInput<int>();

                switch (control)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            CharacterMenu();
                            break;
                        }
                    case 2:
                        {
                            Credits();
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    default:
                        {
                            InvalidMenuOption();
                            break;
                        }
                }

                Console.Clear();
            } while (control != 3);
        }

        /// <summary>
        /// Displays the credits for the game.
        /// Credits include the creators name as well as the class it was created for
        /// </summary>
        public static void Credits()
        {
            Console.Clear();
            Console.WriteLine("Created by Matthew Cooley for CIS 142 in winter 2020");
            Pause();
        }

        /// <summary>
        /// Prints the options for the character creation menu
        /// </summary>
        public static void PrintCharacterMenu()
        {
            Console.Clear();
            Console.WriteLine("1). Warrior");
            Console.WriteLine("2). Ranger");
            Console.WriteLine("3). Wizard");
            Console.WriteLine("4). Quit");
            Console.Write("Which class would you like to play as(select for more info): ");
        }

        /// <summary>
        /// Prints details about the warrior class
        /// </summary>
        public static void PrintWarriorInfo()
        {
            Console.Clear();
            Console.WriteLine("The warrior is a physical fighter who utilizes his strength to overwhelm opponents");
            Console.WriteLine("STR\tDex\tCon\tInt\tWis\tCha\tHit Die");
            Console.WriteLine("15\t12\t14\t10\t13\t8\td10");
        }

        /// <summary>
        /// Prints details about the ranger class
        /// </summary>
        public static void PrintRangerInfo()
        {
            Console.Clear();
            Console.WriteLine("The ranger is a physical fighter who dexterously strikes at the opponents weakpoints");
            Console.WriteLine("STR\tDex\tCon\tInt\tWis\tCha\tHit Die");
            Console.WriteLine("10\t15\t13\t12\t14\t8\td8");
        }

        /// <summary>
        /// Prints details about the wizard class
        /// </summary>
        public static void PrintWizardInfo()
        {
            Console.Clear();
            Console.WriteLine("The wizard is a magical fighter who leverages his intellect to cast devastating spells");
            Console.WriteLine("STR\tDex\tCon\tInt\tWis\tCha\tHit Die");
            Console.WriteLine("8\t13\t10\t15\t12\t14\td6");
        }

        /// <summary>
        /// Handles all of the logic and inputs for the charater creation menu
        /// </summary>
        public static void CharacterMenu()
        {
            int control = 0;
            char characterChoice = 'N';
            string name = "Default";

            do
            {
                control = 0;
                PrintCharacterMenu();

                control = CheckInput<int>();

                switch (control)
                {
                    case 1:
                        {
                            PrintWarriorInfo();
                            Console.Write("Would you like to play as a warrior (Y/N)? ");

                            characterChoice = Char.ToUpper(CheckInput<char>());

                            if (characterChoice != 'Y' && characterChoice != 'N' && characterChoice != default(char))
                            {
                                InvalidMenuOption();
                            }
                            else if (characterChoice == 'Y')
                            {
                                Console.Clear();
                                Console.Write("Enter your character's name: ");
                                name = Console.ReadLine();

                                Game game = new Game(new Warrior(name));
                                game.Start();

                                control = 4;
                            }

                            break;
                        }
                    case 2:
                        {
                            PrintRangerInfo();
                            Console.Write("Would you like to play as a ranger (Y/N)? ");

                            characterChoice = Char.ToUpper(CheckInput<char>());

                            if (characterChoice != 'Y' && characterChoice != 'N' && characterChoice != default(char))
                            {
                                InvalidMenuOption();
                            }
                            else if (characterChoice == 'Y')
                            {
                                Console.Clear();
                                Console.Write("Enter your character's name: ");
                                name = Console.ReadLine();

                                Game game = new Game(new Ranger(name));
                                game.Start();

                                control = 4;
                            }

                            break;
                        }
                    case 3:
                        {
                            PrintWizardInfo();
                            Console.Write("Would you like to play as a wizard (Y/N)? ");

                            characterChoice = Char.ToUpper(CheckInput<char>());

                            if (characterChoice != 'Y' && characterChoice != 'N' && characterChoice != default(char))
                            {
                                InvalidMenuOption();
                            }
                            else if (characterChoice == 'Y')
                            {
                                Console.Clear();
                                Console.Write("Enter your character's name: ");
                                name = Console.ReadLine();

                                Game game = new Game(new Wizard(name));
                                game.Start();

                                control = 4;
                            }

                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    default:
                        {
                            InvalidMenuOption();
                            break;
                        }
                }

            } while (control != 4);
        }

        /// <summary>
        /// Prints out an error message and awaits user acknowledgement to proceed
        /// </summary>
        public static void InvalidMenuOption()
        {
            Console.Clear();
            Console.WriteLine("Please select a valid menu option");
            Pause();
        }

        /// <summary>
        /// Reads in user input and attempts to convert it to a specified type.
        /// Prints an error message if the input cannot be converted.
        /// Returns either the converted input or a default value of the specified type.
        /// </summary>
        /// <typeparam name="T">A type that implements the IConvertible interface</typeparam>
        /// <returns>A variable of type T</returns>
        public static T CheckInput<T>() where T : System.IConvertible
        {
            T input = default(T);

            try
            {
                input = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
            }
            catch(FormatException ex)
            {
                InvalidMenuOption();
            }

            return input;
        }
    }
}