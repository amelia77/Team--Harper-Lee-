namespace Game
{
    using System;
    public class MagicTool : Weapon
    {
        public MagicTool(Point topLeft, Point speed, int damage, ConsoleColor color = ConsoleColor.Magenta)
            : base("Ivailo Kenov", topLeft, new char[,] { { (char)3 } }, speed, damage, color)
        {
        }

         public override void Move()
         {
             //if (this.topLeftCoords)
             {

             }
             base.Move();
         }
    }
}
