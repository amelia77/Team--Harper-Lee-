﻿using System;
namespace Game
{
    public class Enemy : MovingUnit
    {
        public Status status = Status.Enemy;

        public Enemy(Point topLeft, char[,] image, Point speed, ConsoleColor color)
            : base(topLeft, image, speed, color)
        {
        }

        public override bool CanCollideWith(Status otherStatus)
        {
            return otherStatus == Status.Player || otherStatus == Status.Enemy;
        }

        public override Status GetStatus()
        {
            return this.status;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.CollisionForceDirection.Row * this.Speed.Row < 0)
            {
                this.Speed.Row = 1;
            }
            if (collisionData.CollisionForceDirection.Col * this.Speed.Col < 0)
            {
                this.Speed.Col = 0;
            }
            if (collisionData.hitObjectsCollisionGroupStrings.Contains(Status.Enemy))
            {
                //this.Speed.Row = 1;
                //this.Speed.Col = 0;
            }
            else
            {
                this.isDestroyed = true;
            }
        }

        public override void Move()
        {
            if (this.topLeftCoords.Row < 1)
            {
                this.Speed.Row = 1;
            }
            if (this.topLeftCoords.Col < 1)
            {
                this.Speed.Col = 0;
            }
            this.UpdatePosition();
        }
    }
}
