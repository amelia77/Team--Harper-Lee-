using System;
using System.Threading;

namespace Game
{
    public static class Menu
    {
        private static int choice;
        private static int x = GameProgram.WORLD_COLS / 2 - 11;
        private static int y = GameProgram.WORLD_ROWS / 3;

        private static string[] menu = new string[]
        {
            "Start Game",
            "Level",
            "Exit"
        };

        static void DrawText(int x, int y, string text, ConsoleColor textColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        private static int ChooseFromMenu()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        if (choice > 0)
                        {
                            choice--;
                        }
                        else
                        {
                            choice = menu.Length - 1;
                        }
                    }

                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (choice < menu.Length - 1)
                        {
                            choice++;
                        }
                        else
                        {
                            choice = 0;
                        }
                    }

                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        return choice;
                    }
                }

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                Console.Clear();

                for (int i = 0, rowSpace = 0; i < menu.Length; i++, rowSpace += 2)
                {
                    if (i == choice)
                    {
                        DrawText(x + i * 8 - 1, y + rowSpace, "[" + menu[i] + "]", ConsoleColor.White, ConsoleColor.Red);
                    }
                    else
                    {
                        DrawText(x + i * 8, y + rowSpace, menu[i]);
                    }
                }

                Thread.Sleep(100);
            }
        }
        

        public static void EnterMenu(IConsoleRenderer renderer, IUserInterface keyboard)
        {
            bool IN_LOOP = true;

            while (IN_LOOP)
            {
                int choice = ChooseFromMenu();

                switch (choice)
                {
                    case 0:
                        {
                            // To be done in a method
                            Console.Clear();
                            Random randomGenerator = new Random();
                            GameUnitGenerator unitGenerator = new GameUnitGenerator(randomGenerator, new Point(5, 5), new Point(30, 30));

                            Engine gameEngine = new Engine(renderer, keyboard, unitGenerator);

                            keyboard.OnLeftPressed += (sender, eventInfo) =>
                            {
                                gameEngine.MovePlayerLeft();
                            };

                            keyboard.OnRightPressed += (sender, eventInfo) =>
                            {
                                gameEngine.MovePlayerRight();
                            };

                            keyboard.OnUpPressed += (sender, eventInfo) =>
                            {
                                gameEngine.MovePlayerUp();
                            };

                            keyboard.OnDownPressed += (sender, eventInfo) =>
                            {
                                gameEngine.MovePlayerDown();
                            };

                            keyboard.OnActionPressed += (sender, eventInfo) =>
                            {
                                gameEngine.PlayerShoot();
                            };

                            keyboard.OnEscapePressed += (sender, eventInfo) =>
                            {
                                gameEngine.Break();
                            };

                            GameProgram.Initialize(gameEngine);
                            gameEngine.Run();
                        }
                        break;

                    case 3:
                        IN_LOOP = false;
                        break;

                    default:
                        IN_LOOP = false;
                        break;
                }
            }
        }
    }
}
