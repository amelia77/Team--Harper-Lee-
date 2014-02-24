﻿using Game.Data;
using Game.Interfaces;
using Game.Tools;
using System;
using System.Threading;
using Game.Levels;

namespace Game
{
    public class Menu
    {
        private static int choice;
        private static int x = GameProgram.WORLD_COLS / 2 - 11;
        private static int y = GameProgram.WORLD_ROWS / 3;

        private static string[] menuRows = new string[]
        {
            "Start Game",
            "Continue Game",
            "Level",
            "Exit"
        };

       /* public Menu(string[] _menu)
        {
            menuRows = _menu;
        }*/

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
                        return choice;
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
                        renderer.DrawTextBoxTopLeft(menuRows[i], x + i * (x / 3) - 1, y + rowSpace - 2, ConsoleColor.White);
                    }
                    else
                    {
                        renderer.WriteOnPosition(menuRows[i], x + i * (x / 3), y + rowSpace - 1);
                    }
                }

                Thread.Sleep(150);
            }
        }

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

        public static void InitializeLevel(IConsoleRenderer renderer, IUserInterface keyboard, Engine engine)
        {
            int choice = ChooseLevel(renderer, keyboard);
            switch (choice)
            { 
                case 1:
                    engine.Reset();
                    renderer.ClearScreen();
                    engine.Initialize(new Level_1());
                    break;
                case 2:
                    engine.Reset();
                    renderer.ClearScreen();
                    engine.Initialize(new Level_2());
                    break;
                case 3:
                    engine.Reset();
                    renderer.ClearScreen();
                    engine.Initialize(new Level_3());
                    break;
                default:
                    break;
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
                //Sounds.SFX(Sounds.SoundEffects.Shoot);
            };

            keyboard.OnEscapePressed += (sender, eventInfo) =>
            {
                gameEngine.Break();
                Sounds.SFX(Sounds.SoundEffects.GameOver);
            };
            
            var lvl = new Level_3();
            gameEngine.Initialize(lvl);

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

                    /*case 1:
                        {
                            Console.Clear();
                        }*/

                    case 2:
                        InitializeLevel(renderer, keyboard, gameEngine);
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

        /*private static void Initialize(Engine engine)
        {
            char[,] hero = ImageProducer.GetImage(@"..\..\images\pacman.txt");

            Player player = new Player(new Point(20, 10), hero, new Point(0, 0), ConsoleColor.Yellow);

            engine.AddPlayer(player);

            char[,] cat = ImageProducer.GetImage(@"..\..\images\bunny.txt");
            for (int i = 0; i < 5; i++)
            {
                Enemy enemy = new Enemy(new Point(5, 10 * (i + 1)), cat, new Point(0, -1),
                ConsoleColor.Red);
                //Thread.Sleep(50);

                engine.AddObject(enemy);
            }

        }*/
    }
}
