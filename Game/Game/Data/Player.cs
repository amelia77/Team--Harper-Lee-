namespace Game.Data
{
    using Game.Interfaces;
    using Game.Tools;
    using System;
    using System.Collections.Generic;

    public class Player : MovingUnit, ICollidable, IMovable
    {
        //fields
        public UnitStatus status = UnitStatus.Player;
        private const int MinRow = 3;
        private int currTopLeftRow;
        private int currTopLeftCol;
        private int healthPoints;

        //constructor
        public Player(Point topLeft, char[,] image, Point speed, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, speed)
        {
            this.image = image;
            this.currTopLeftRow = topLeft.Row;
            this.currTopLeftCol = topLeft.Col;
            this.HealthPoints = 100;
            this.Weapon = new Weapon("Common weapon", ShotCoords(),
                new char[,] { { '*' } }, new Point(-1, 0), 3);
        }

        //properies
        public int HealthPoints
        {
            get
            {
                return this.healthPoints;
            }
            private set
            {
                if (value<=0)
                {
                    throw new PlayerOutOfHPException("GAME OVER");
                }
                else
                {
                    this.healthPoints = value;
                }
                
            }
        }

        public int Score { get; set; }

        public IWeapon Weapon { get; private set; }

        public Point MaxMovePoint { get; set; }

        //methods
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

        //fires weapon
        public IList<MovingUnit> Shoot()
        {
            this.Weapon.Position = ShotCoords();
            return this.Weapon.GetWeapon();
        }

        public override UnitStatus GetStatus()
        {
            return UnitStatus.Player;
        }

        //player can collide with enemies, weapons and bonuses
        public override bool CanCollideWith(UnitStatus otherStatus)
        {
            return otherStatus == UnitStatus.Weapon || otherStatus == UnitStatus.Enemy ||  
                otherStatus == UnitStatus.Bonus;
        }

        //calculates the effect of a collision on HP
        public override void RespondToCollision(CollisionData collisionData)
        {
            if (collisionData.hitObjectsCollisionGroupStrings.Contains(UnitStatus.Enemy))
            {
                this.HealthPoints -= 10 * collisionData.hitObjectsCollisionUnits.Count;
            }

            else if (collisionData.hitObjectsCollisionGroupStrings.Contains(UnitStatus.Weapon))
            {
                this.HealthPoints -= (collisionData.hitObjectsCollisionUnits[0] as Weapon).Damage;
            }

            else if (collisionData.hitObjectsCollisionGroupStrings.Contains(UnitStatus.Bonus))
            {
                Bonus bonus = collisionData.hitObjectsCollisionUnits[0] as Bonus;

                if (bonus.Type == BonusType.Health)
                {
                    this.HealthPoints += 10;
                }

                else if (bonus.Type == BonusType.MagicWeapon)
                {
                    this.Weapon = new MagicTool(ShotCoords(), new Point(-1, 0), 100);
                }

                //else if (bonus.Type == BonusType.CommonWeapon)
               //{
                //    this.Weapon = new Weapon("Common weapon", ShotCoords(),
                //                     new char[,] { { '*' } }, new Point(-1, 0), 3);
                //}
                 
            }
        }

        //restricts player movement on the console
        public override void Move()
        {
            if (this.currTopLeftRow < MinRow)
            {
                this.currTopLeftRow = MinRow + 1;
            }

            if (this.currTopLeftCol < 0)
            {
                this.currTopLeftCol = 0;
            }

            if (this.currTopLeftCol > this.MaxMovePoint.Col - this.image.GetLength(1))
            {
                this.currTopLeftCol = this.MaxMovePoint.Col - this.image.GetLength(1);
            }

            if (this.currTopLeftRow > this.MaxMovePoint.Row - this.image.GetLength(0) - 1)
            {
                this.currTopLeftRow = this.MaxMovePoint.Row - this.image.GetLength(0) - 1;
            }

            this.TopLeftCoords = new Point(currTopLeftRow, currTopLeftCol);
        }

        //returns coordinates for weapon launch
        private Point ShotCoords()
        {
            return new Point(this.currTopLeftRow, ((2 * this.currTopLeftCol + this.GetImage().GetLength(1)) / 2));
        }
    }
}
