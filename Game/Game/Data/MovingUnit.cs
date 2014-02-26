namespace Game.Data
{
    using Game.Interfaces;
    using System;
    public abstract class MovingUnit: GameUnit, IMovable
    {
        public MovingUnit(Point topLeft, char[,] body, Point speed, ConsoleColor color=ConsoleColor.White)
            : base(topLeft, body, color)
        {
            this.Speed = speed;
        }
        public Point Speed { get; set; } // Moving coordinates

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
