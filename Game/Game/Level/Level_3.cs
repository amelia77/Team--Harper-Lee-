namespace Game.Levels
{
    using System.Collections.Generic;
    using Game.Data;
    using Game.Tools;

    public class Level_3 : Level
    {
        public override Player Player
        {
            get
            {
                char[,] hero = ImageProducer.GetImage(@"..\..\images\student.txt");

                Player player = new Player(new Point(20, 10), hero, new Point(0, 0));
                return player;
            }
        }

        public override List<Enemy> Enemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                char[,] cat = ImageProducer.GetImage(@"..\..\images\C#.txt");
                for (int i = 0; i < 5; i++)
                {
                    Enemy enemy = new Enemy(new Point(5, 10 * (i + 1)), cat, new Point(0, -1));
                    //Thread.Sleep(50);

                    enemies.Add(enemy);
                }
                return enemies;
            }
        }
    }
}