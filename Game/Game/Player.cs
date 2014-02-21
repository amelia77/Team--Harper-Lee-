namespace Game
{
    using System;

    public class Player : GameUnit, ICollidable
    {
        public Status status = Status.Player;

        public int HealthPoints { get; set; }
        private int currTopLeftRow;
        private int currTopLeftCol;
        public int MoveMaxRow { get; set; }
        public int MoveMaxCol { get; set; }

        public Player(Point topLeft, char[,] image, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, color)
        {
            this.image = image;
            currTopLeftRow = topLeft.Row;
            currTopLeftCol = topLeft.Col;
            this.HealthPoints = 100;
        }

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
            MovingUnit weapon = new Weapon(new Point(this.currTopLeftRow - 1, this.currTopLeftCol + (this.currTopLeftCol / 2) - 1),
                new char[,] { { '*' } }, new Point(-1, 0), 3); // Create a common weapon
            return weapon;
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
            this.TopLeftCoords = new Point(currTopLeftRow, currTopLeftCol);
        }
    }
}
