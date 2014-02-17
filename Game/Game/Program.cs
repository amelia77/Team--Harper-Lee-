namespace Game
{
    using System;
    class Program
    {
        const int WorldRows = 30;
        const int WorldCols = 60;
        //const int RacketLength = 6;

        static void Initialize(Engine engine)
        {
            char[,] hero = ImageProducer.GetImage(@"..\..\images\hero.txt");

            Player player = new Player(new Point(20, 10), hero);
            engine.AddPlayer(player);

            char[,] bunny = ImageProducer.GetImage(@"..\..\images\cat.txt");
            Enemy enemy = new Enemy(new Point(6, 3), bunny, new Point(1, 1),
                ConsoleColor.Red);

            engine.AddObject(enemy);
        }

        static void Main()
        {
            IConsoleRenderer renderer = new ConsoleRenderer(WorldRows, WorldCols); 

            IUserInterface keyboard = new KeyboardInterface();

            Engine gameEngine = new Engine(renderer, keyboard);

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

            Initialize(gameEngine);

            gameEngine.Run();
        }
    }
}
