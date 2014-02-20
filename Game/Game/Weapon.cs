namespace Game
{
    using System;
    public class Weapon : MovingUnit, IMovable, IWeapon, ICollidable
    {
        private int damage;

        public const string CollisionGroupString = "weapon";

        public Weapon(Point topLeft, char[,] image, Point speed, int damage, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, speed, color)
        {
            this.Damage = damage;
        }
        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Damage value can't be negative!");
                }
                this.damage = value;
            }
        }

        public override string GetCollisionGroupString()
        {
            return Player.CollisionGroupString;
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "enemy" || otherCollisionGroupString == "player";
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.CollisionForceDirection.Row * this.TopLeftCoords.Row < 0)
            {
                this.isDestroyed = true;
            }
            if (collisionData.CollisionForceDirection.Col * this.TopLeftCoords.Col < 0)
            {
                this.isDestroyed = true;
            }
        }

        public override void Move()
        {
            if (this.topLeftCoords.Row <= 0)
            {
                this.isDestroyed = true;
            }
            else
            {
                base.Move();
            }
        }
    }
}
