namespace Game.Levels
{
    using System;
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

                int width = GameProgram.WORLD_COLS - 7;

                char[,] CSharp= ImageProducer.GetImage(@"..\..\images\C#.txt");
                char[,] HTML = ImageProducer.GetImage(@"..\..\images\HTML.txt");

                enemies.Add(new Enemy(new Point(3, 1), CSharp, new Point(0, 1)));
                enemies.Add(new Enemy(new Point(3, width - 7), CSharp, new Point(0, -1)));

                enemies.Add(new Enemy(new Point(8, 1), CSharp, new Point(0, 1)));
                enemies.Add(new Enemy(new Point(8, width - 7), CSharp, new Point(0, -1)));

                enemies.Add(new Enemy(new Point(3, width/2), CSharp, new Point(1, 0)));

                enemies.Add(new Enemy(new Point(3, 8), CSharp, new Point(0, 1)));
                enemies.Add(new Enemy(new Point(3, width), CSharp, new Point(0, -1)));

                enemies.Add(new Enemy(new Point(8, 8), CSharp, new Point(0, 1)));
                enemies.Add(new Enemy(new Point(8, width), CSharp, new Point(0, -1)));
                return enemies;
            }
        }
    }
}
