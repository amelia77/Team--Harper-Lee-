namespace Game.Data
{
    using Game.Interfaces;
    using System;

    //Inherited by all units that can move
    public abstract class MovingUnit: GameUnit, IMovable
    {
        
        public MovingUnit(Point topLeft, char[,] body, Point speed, ConsoleColor color=ConsoleColor.White)
            : base(topLeft, body, color)
        {
            this.Speed = speed;
        }

        // Moving coordinates
        public Point Speed { get; set; } 

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
