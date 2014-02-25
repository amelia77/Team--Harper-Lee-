namespace Game.Data
{
    using Game.Interfaces;
    using System;
    using System.Collections.Generic;
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
         public override IList<MovingUnit> GetWeapon()
         {
             IList<MovingUnit> list = new List<MovingUnit>();
             list.Add(this.Clone());
             Weapon magic = this.Clone();
             magic.Speed = new Point(magic.Speed.Row, magic.Speed.Col+1);
             list.Add(magic);
             magic = this.Clone();
             magic.Speed = new Point(magic.Speed.Row, magic.Speed.Col -1);
             list.Add(magic);
             return list;
         }
    }
}
