namespace Game
{
    using System;
    using System.Collections.Generic;

    public class Player : MovingUnit, ICollidable, IMovable
    {
        public Status status = Status.Player;

        private int currTopLeftRow;
        private int currTopLeftCol; 

        public Player(Point topLeft, char[,] image, Point speed,ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, speed)
        {
            this.image = image;
            currTopLeftRow = topLeft.Row;
            currTopLeftCol = topLeft.Col;
            this.HealthPoints = 100;
            this.Weapon = new Weapon("weapon", new Point(this.currTopLeftRow, ((2 * this.currTopLeftCol + this.GetImage().GetLength(1)) / 2)),
                new char[,] { { '*' } }, new Point(-1, 0), 3);
        }

        public int HealthPoints { get; private set; }
        public Weapon Weapon { get; private set; }

        public override char[,] GetImage()
        {
            char[,] newImage = new char[this.image.GetLength(0), this.image.GetLength(1)];
            for (int row = 0; row < newImage.GetLength(0); row++)
            {
                for (int col = 0; col < newImage.GetLength(1); col++)
                {
                    newImage[row, col] = this.image[row, col];

                }
            }
            return this.image;
        }

        public void MoveLeft()
        {
            currTopLeftCol--;
        }

        public void MoveRight()
        {
            currTopLeftCol++;
        }

        public void MoveUp()
        {
            currTopLeftRow--;
        }

        public void MoveDown()
        {
            currTopLeftRow++;
        }

        public MovingUnit Shoot()
        {
            this.Weapon.TopLeftCoords = new Point(this.currTopLeftRow, ((2 * this.currTopLeftCol + this.GetImage().GetLength(1)) / 2));
            return this.Weapon.Clone();
        }

        public override Status GetStatus()
        {
            return Status.Player;
        }

        public override bool CanCollideWith(Status otherStatus)
        {
            return otherStatus == Status.Weapon || otherStatus == this.status ||
                otherStatus == Status.Weapon;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {

            if (collisionData.CollisionForceDirection.Row * this.currTopLeftRow < 0)
            {
                // ....
            }
            if (collisionData.CollisionForceDirection.Col * this.currTopLeftCol < 0)
            {
                //......
            }
            if (collisionData.hitObjectsCollisionGroupStrings.Contains(Status.Enemy))
            {
                this.HealthPoints -= 10;
            }
        }

        public override void Move()
        {
            if (this.currTopLeftRow < 0)
            {
                this.currTopLeftRow = 0;
            }

            if (this.currTopLeftCol < 0)
            {
                this.currTopLeftCol = 0;
            }

            if (this.currTopLeftCol > GameProgram.WORLD_COLS - this.image.GetLength(1))
            {
                this.currTopLeftCol = GameProgram.WORLD_COLS - this.image.GetLength(1);
            }

            if (this.currTopLeftRow > GameProgram.WORLD_ROWS - this.image.GetLength(0) - 1)
            {
                this.currTopLeftRow = GameProgram.WORLD_ROWS - this.image.GetLength(0) - 1;
            }

            this.TopLeftCoords = new Point(currTopLeftRow, currTopLeftCol);
        }
    }
}
