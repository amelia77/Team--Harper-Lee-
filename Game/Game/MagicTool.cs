namespace Game
{
    using System;
    public class MagicTool : Weapon
    {
        public MagicTool(Point topLeft, char[,] image, Point speed, int damage, ConsoleColor color = ConsoleColor.Magenta)
            : base("Ivailo Kenov", topLeft, new char[,] { { (char)244 } }, speed, damage, color)
        {
        }

         public override void Move()
         {
             /*if (this.topLeftCoords)
             {

             }*/
             base.Move();
         }
    }
}
