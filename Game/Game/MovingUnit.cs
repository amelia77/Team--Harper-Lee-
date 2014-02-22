
using System;
namespace Game
{
    public class MovingUnit: GameUnit
    {
        public Point Speed { get; set; } // Moving coordinates

        public MovingUnit(Point topLeft, char[,] body, Point speed, ConsoleColor color=ConsoleColor.White)
            : base(topLeft, body, color)
        {
            this.Speed = speed;
        }

        protected virtual void UpdatePosition()
        {
            this.TopLeftCoords += this.Speed;
        }

        public override void Move()
        {
            this.UpdatePosition();
        }
    }
}
