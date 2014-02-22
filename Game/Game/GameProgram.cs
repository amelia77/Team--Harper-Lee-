namespace Game
{
    using System;
    using System.Threading;
    public class GameProgram
    {
        public const int WORLD_ROWS = 30;
        public const int WORLD_COLS = 60;
        //const int RacketLength = 6;

        public static void Initialize(Engine engine)
        {
            char[,] hero = ImageProducer.GetImage(@"..\..\images\pacman.txt");

            Player player = new Player(new Point(20, 10), hero,new Point (0,0), ConsoleColor.Yellow);
            player.MoveMaxRow = 0;
            player.MoveMaxCol = 0;

            engine.AddPlayer(player);

            char[,] cat = ImageProducer.GetImage(@"..\..\images\cat.txt");
            for (int i = 0; i < 5; i++)
            {
                Enemy enemy = new Enemy(new Point(5, 10*(i+1)), cat, new Point(0, -1),
                ConsoleColor.Red);
                //Thread.Sleep(50);

                engine.AddObject(enemy);
            }
            
        }

        //TEST COMMIT SASHO
        //test commit miro
        //Stef: I <3 Github
        static void Main()
        {
            IConsoleRenderer renderer = new ConsoleRenderer(GameProgram.WORLD_ROWS, GameProgram.WORLD_COLS);
            IUserInterface keyboard = new KeyboardInterface();


            Menu.EnterMenu(renderer, keyboard);

                  
        }
    }
}
