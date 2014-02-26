namespace Game.Data
{
    using System.Collections.Generic;
    using System;
    using Game.Interfaces;
    using Game.Tools;

    public abstract class GameUnit : IObjectRenderable, IMovable, IObjectProducer, ICollidable
    {
        protected Point topLeftCoords; //top left object point
        protected char[,] image; //image of the object 
        protected bool isDestroyed; // is object already destroyed

        protected GameUnit(Point topLeftCoords, char[,] image, ConsoleColor color = ConsoleColor.Magenta)
        {
            this.TopLeftCoords = topLeftCoords;
            this.image = CopyImageMatrix(image);
            this.IsDestroyed = false;
            this.ImageColor = color;
        }

        public ConsoleColor ImageColor { get; set; }

        public Point TopLeftCoords
        {
            get
            {
                return new Point(this.topLeftCoords.Row, this.topLeftCoords.Col);
            }
            protected set
            {
                this.topLeftCoords = new Point(value.Row, value.Col);
            }
        }

        public bool IsDestroyed
        {
            get
            {
                return this.isDestroyed;
            }
            protected set
            {
                this.isDestroyed = value;
            }
        }

        public virtual char[,] GetImage()
        {
            return this.CopyImageMatrix(this.image);
        }

        public Point GetTopLeftCoords() //Return Top left Point(X,Y) of current image
        {
            return this.topLeftCoords;
        }

        public virtual IEnumerable<GameUnit> ProduceObjects()
        {
            return new List<GameUnit>();
        }

        public abstract void Move();

        public virtual void RespondToCollision(CollisionData collisionData)
        {
        }

        public virtual List<Point> GetCollisionProfile()
        {
            List<Point> profile = new List<Point>();

            int bodyRows = this.image.GetLength(0);
            int bodyCols = this.image.GetLength(1);

            for (int row = 0; row < bodyRows; row++)
            {
                for (int col = 0; col < bodyCols; col++)
                {
                    profile.Add(new Point(row + this.TopLeftCoords.Row, col + this.TopLeftCoords.Col));
                }
            }

            return profile;
        }


        public virtual bool CanCollideWith(UnitStatus otherStatus)
        {
            return false;
        }

        public virtual UnitStatus GetStatus()
        {
            return UnitStatus.EmptyUnit;
        }

        private char[,] CopyImageMatrix(char[,] matrixToCopy)
        {
            int rows = matrixToCopy.GetLength(0);
            int cols = matrixToCopy.GetLength(1);

            char[,] result = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = matrixToCopy[row, col];
                }
            }

            return result;
        }
    }
}
