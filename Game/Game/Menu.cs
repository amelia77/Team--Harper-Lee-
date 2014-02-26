namespace Game
{
    using Game.Data;
    using Game.Interfaces;
    using Game.Tools;
    using System;
    using System.Threading;
    using Game.Levels;

    //Loads Start Menu
    public class Menu
    {
        private static int choice;
        private static int x = GameProgram.WORLD_COLS / 2 - 11;
        private static int y = GameProgram.WORLD_ROWS / 3;
        private static Level level = new Level_1();
        
        private static char[,] hero = ImageProducer.GetImage(@"..\..\images\spaceship.txt");
        private static Player player = new Player(new Point(20, x + 7), hero, new Point(0, 0));

        private static string[] menuRows = new string[]
        {
            "New Game",
            "Continue Game",
            "Player",
            "Level",
            "Exit"
        };

        //navigates menu with arrow keys
        private static string ChooseFromMenu(IConsoleRenderer renderer)
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
                            choice = menuRows.Length - 1;
                        }
                    }

                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (choice < menuRows.Length - 1)
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
                        return menuRows[choice];
                    }
                }

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                renderer.ClearScreen();

                for (int i = 0, rowSpace = 0; i < menuRows.Length; i++, rowSpace += 3)
                {
                    if (i == choice)
                    {
                        renderer.DrawTextBoxTopLeft(menuRows[i], x + i * (x / 3) - 5, y + rowSpace - 2, ConsoleColor.White);
                    }
                    else
                    {
                        renderer.WriteOnPosition(menuRows[i], x + i * (x / 3) - 4, y + rowSpace - 1);
                    }
                }

                Thread.Sleep(150);
            }
        }

        //chooses level with user input
        public static int ChooseLevel(IConsoleRenderer renderer, IUserInterface keyboard)
        {
            bool in_loop = true;
            int choice = 0;

            while (in_loop)
            {
                renderer.ClearScreen();
                renderer.DrawTextBoxTopLeft("Enter level between 1 and 3", GameProgram.WORLD_COLS / 2 - 15, GameProgram.WORLD_ROWS / 2 - 7);

                renderer.DrawTextBoxTopLeft("            ", GameProgram.WORLD_COLS / 2 - 7, GameProgram.WORLD_ROWS / 2 - 2);

                string choiceStr = keyboard.EnterText(GameProgram.WORLD_COLS / 2 - 5, GameProgram.WORLD_ROWS / 2 - 1);
                in_loop = !int.TryParse(choiceStr, out choice);

                if (choice < 1 || choice > 3)
                {
                    in_loop = true;
                }

                Thread.Sleep(150);
            }
            return choice;
        }

        //initializes level
        public static void InitializeLevel(IConsoleRenderer renderer, IUserInterface keyboard, Engine engine)
        {
            int choice = ChooseLevel(renderer, keyboard);

            switch (choice)
            { 
                case 1:
                    engine.Reset();
                    level = new Level_1();
                    engine.Initialize(level);
                    engine.SetPlayer(player);
                    break;
                case 2:
                    engine.Reset();
                    level = new Level_2();
                    engine.Initialize(level);
                    engine.SetPlayer(player);
                    break;
                case 3:
                    engine.Reset();
                    level = new Level_3();
                    engine.Initialize(level);
                    engine.SetPlayer(player);
                    break;
                default:
                    break;
            }
        }

        //navigates player choice menu
        public static void InitializePlayer(IConsoleRenderer renderer, IUserInterface keyboard, Engine engine)
        {
            bool inLoop = true;
            int choice = 0;

            while (inLoop)
            {
                const int MAX_CHOICES = 2;

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
                            choice = MAX_CHOICES;
                        }
                    }

                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (choice < MAX_CHOICES)
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
                        switch (choice)
                        { 
                            case 0:
                                char[,] hero = ImageProducer.GetImage(@"..\..\images\pacman.txt");
                                player = new Player(new Point(20, x + 7), hero, new Point(0, 0));
                                engine.SetPlayer(player);
                                inLoop = false;
                                break;
                            case 1:
                                hero = ImageProducer.GetImage(@"..\..\images\student.txt");
                                player = new Player(new Point(20, x + 7), hero, new Point(0, 0));
                                engine.SetPlayer(player);
                                inLoop = false;
                                break;
                            case 2:
                                hero = ImageProducer.GetImage(@"..\..\images\spaceship.txt");
                                player = new Player(new Point(20, x + 7), hero, new Point(0, 0));
                                engine.SetPlayer(player);
                                inLoop = false;
                                break;
                        }
                    }
                }

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                //renderer.ClearScreen();

                const int height = 8;
                switch (choice)
                { 
                    case 0:
                        renderer.ClearScreen();
                        char[,] hero = ImageProducer.GetImage(@"..\..\images\pacman.txt");
                        renderer.DrawImage(hero, 10, height);
                        renderer.DrawTextBoxTopLeft("Packman in the wrong game", 20, height);
                        break;
                    case 1:
                        renderer.ClearScreen();
                        hero = ImageProducer.GetImage(@"..\..\images\student.txt");
                        renderer.DrawImage(hero, 10, height);
                        renderer.DrawTextBoxTopLeft("Confused student", 20, height);
                        break;
                    case 2:
                        renderer.ClearScreen();
                        hero = ImageProducer.GetImage(@"..\..\images\spaceship.txt");
                        renderer.DrawImage(hero, 10, height);
                        renderer.DrawTextBoxTopLeft("Nasa powerful spaceship", 20, height);
                        break;
                    default:
                        break;
                }

                Thread.Sleep(200);
            }
        }

        public static void EnterMenu(IConsoleRenderer renderer, IUserInterface keyboard)
        {
            bool IN_LOOP = true;

            Random randomGenerator = new Random();
            GameUnitGenerator unitGenerator = new GameUnitGenerator(randomGenerator, new Point(5, 5), new Point(25, 30));


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
                //Sounds.SFX(Sounds.SoundEffects.Shoot);
            };

            keyboard.OnEscapePressed += (sender, eventInfo) =>
            {
                gameEngine.Break();
                Sounds.SFX(Sounds.SoundEffects.GameOver);
            };
            
            gameEngine.Initialize(level);
            gameEngine.SetPlayer(player);

            while (IN_LOOP)
            {
                string choice = ChooseFromMenu(renderer);

                switch (choice)
                {
                    case "New Game":
                        {
                            renderer.ClearScreen();
                            gameEngine.Reset();
                            gameEngine.inLoop = true;
                            gameEngine.Initialize(level);
                            gameEngine.SetPlayer(player);
                            gameEngine.Run();
                            break;
                        }

                    case "Continue Game":
                        {
                            renderer.ClearScreen();
                            gameEngine.inLoop = true;
                            gameEngine.Run();
                            break;
                        }

                    case "Player":
                        {
                            renderer.ClearScreen();
                            InitializePlayer(renderer, keyboard, gameEngine);
                            break;
                        }

                    case "Level":
                        {
                            renderer.ClearScreen();
                            InitializeLevel(renderer, keyboard, gameEngine);
                            break;
                        }

                    case "Exit":
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
