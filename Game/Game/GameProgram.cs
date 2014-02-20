namespace Game
{
    using System;
    using System.Threading;
    class GameProgram
    {
        const int WorldRows = 30;
        const int WorldCols = 60;
        //const int RacketLength = 6;

        static void Initialize(Engine engine)
        {
            char[,] hero = ImageProducer.GetImage(@"..\..\images\pacman.txt");

            Player player = new Player(new Point(20, 10), hero, ConsoleColor.Yellow);
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
        static void Main()
        {
            IConsoleRenderer renderer = new ConsoleRenderer(WorldRows, WorldCols); 

            IUserInterface keyboard = new KeyboardInterface();
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

            Initialize(gameEngine);

            gameEngine.Run();
        }
    }
}
