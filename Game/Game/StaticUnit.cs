using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class StaticUnit : GameUnit
    {
        public StaticUnit(Point topLeftCoords, char[,] image, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeftCoords, image, color)
        {

        }

        public override void Move()
        {
            
        }
    }
}
