using System;
namespace Game
{
    public class Enemy : MovingUnit
    {
        public new const string CollisionGroupString = "enemy";

        public Enemy(Point topLeft, char[,] image, Point speed, ConsoleColor color)
            : base(topLeft, image, speed, color)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "player";
        }

        public override string GetCollisionGroupString()
        {
            return Enemy.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.CollisionForceDirection.Row * this.Speed.Row < 0)
            {
                this.Speed.Row *= -1;
            }
            if (collisionData.CollisionForceDirection.Col * this.Speed.Col < 0)
            {
                this.Speed.Col *= -1;
            }
            this.isDestroyed = true;
        }
    }
}
