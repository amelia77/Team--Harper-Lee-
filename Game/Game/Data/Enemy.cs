namespace Game.Data
{
    using Game.Interfaces;
    using Game.Tools;
    using System;

    public class Enemy : MovingUnit, ICollidable, IMovable
    {
        
        public UnitStatus status = UnitStatus.Enemy;

        
        public Enemy(Point topLeft, char[,] image, Point speed, ConsoleColor color = ConsoleColor.Green)
            : base(topLeft, image, speed, color)
        {
        }

        //enemies can collide with other enemies and the player
        public override bool CanCollideWith(UnitStatus otherStatus)
        {
            return otherStatus == UnitStatus.Player || otherStatus == UnitStatus.Enemy;
        }

        public override UnitStatus GetStatus()
        {
            return this.status;
        }

        //describes how the enemy reacts upon collision
        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.CollisionForceDirection.Row * this.Speed.Row < 0)
            {
                this.Speed = new Point(1, this.Speed.Col);
            }
            else if (collisionData.CollisionForceDirection.Col * this.Speed.Col < 0)
            {
                this.Speed = new Point(this.Speed.Row, 0);
            }
            else if (collisionData.hitObjectsCollisionGroupStrings.Contains(UnitStatus.Enemy))
            {
                
            }
            else if (collisionData.hitObjectsCollisionGroupStrings.Contains(UnitStatus.Bonus))
            {       
            }
            else            
            {
                this.isDestroyed = true;   //enemy gets destroyed on impact with player
            }
        }

        //enemies move from the right-hand side of the screen and stop at the left wall
        public override void Move()
        {
            if (this.topLeftCoords.Row < 1)
            {
                this.Speed = new Point(1, this.Speed.Col);
            }
            if (this.topLeftCoords.Col < 1)
            {
                this.Speed = new Point(this.Speed.Row, 0);
            }
            this.UpdatePosition();
        }

        //Enemy can shoot straight down
        public MovingUnit Shoot()
        {
            Point shootingSpeed = new Point(1, 0);
            int topLeftRow = this.TopLeftCoords.Row + this.image.GetLength(0);
            int topLeftCol = (2 * this.TopLeftCoords.Col + this.GetImage().GetLength(1)) / 2;
            Point topLeftCoords = new Point(topLeftRow, topLeftCol);
            return new Weapon("weapon", topLeftCoords, new char[,] { { 'C', '#' } }, shootingSpeed, 10, ConsoleColor.Blue);
        }

    }
}
