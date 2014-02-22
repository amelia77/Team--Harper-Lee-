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
            "Continue Game",
            "Level",
            "Exit"
        };

        private static int ChooseFromMenu(IConsoleRenderer renderer)
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

                renderer.ClearScreen();

                for (int i = 0, rowSpace = 0; i < menu.Length; i++, rowSpace += 3)
                {
                    if (i == choice)
                    {
                        renderer.DrawTextBoxTopLeft(menu[i], x + i * (x / 3) - 1, y + rowSpace - 2, ConsoleColor.White);
                    }
                    else
                    {
                        renderer.WriteOnPosition(menu[i], x + i * (x / 3), y + rowSpace - 1);
                    }
                }

                Thread.Sleep(150);
            }
        }
        

        public static void EnterMenu(IConsoleRenderer renderer, IUserInterface keyboard)
        {
            bool IN_LOOP = true;

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

            while (IN_LOOP)
            {
                int choice = ChooseFromMenu(renderer);

                switch (choice)
                {
                    case 0:
                        {
                            renderer.ClearScreen();
                            gameEngine.inLoop = true;
                            gameEngine.Run();
                        }
                        break;

                    //case 1:
                      //  {
                        //    Console.Clear();
                        //}

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
