namespace Game
{
    using Game.Data;
    using Game.Interfaces;
    using Game.Tools;
    using System;
    using System.Threading;
    public class GameProgram
    {
        public const int WORLD_ROWS = 30;
        public const int WORLD_COLS = 60;

        static void Main()
        {
            IConsoleRenderer renderer = new ConsoleRenderer(GameProgram.WORLD_ROWS, GameProgram.WORLD_COLS);
            IUserInterface keyboard = new KeyboardProvessor();

            Menu.EnterMenu(renderer, keyboard);
        }
    }
}
