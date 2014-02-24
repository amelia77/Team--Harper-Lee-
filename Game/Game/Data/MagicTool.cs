namespace Game.Data
{
    using System;
    public class MagicTool : Weapon
    {
        public MagicTool(Point topLeft, Point speed, int damage, ConsoleColor color = ConsoleColor.Magenta)
            : base("Magic Tool", topLeft, new char[,] { { (char)3 } }, speed, damage, color)
        {
        }

         public override void Move()
         {
             base.Move();
         }
    }
}
