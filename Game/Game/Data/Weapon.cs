namespace Game.Data
{
    using Game.Interfaces;
    using Game.Tools;
    using System;
    using System.Collections.Generic;
    public class Weapon : MovingUnit, IWeapon
    {
        private const int MinMoveRow = 3;

        public UnitStatus status = UnitStatus.Weapon;
        private string name;
        private int damage;
        private int ammo;       

        public Weapon(string name, Point topLeft, char[,] image, Point speed, int damage, 
            ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, speed, color)
        {
            this.Name = name;
            this.Damage = damage;
            this.Ammo = ammo;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Incorrect name!");
                }
                this.name = value;
            }
        }
        public Point Position
        {
            get
            {
                return this.topLeftCoords;
            }
            set
            {
                this.topLeftCoords = value;
            }
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

        public int Ammo
        {
            get
            {
                return this.ammo;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ammo value can't be negative!");
                }
                this.ammo = value;
            }
        }

        public override UnitStatus GetStatus()
        {
            return this.status;
        }

        public override bool CanCollideWith(UnitStatus otherStatus)
        {
            return otherStatus == UnitStatus.Enemy || otherStatus == UnitStatus.Player || otherStatus == UnitStatus.Bonus;
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
            if (this.topLeftCoords.Row <= MinMoveRow || this.topLeftCoords.Col <= 0)
            {
                this.isDestroyed = true;
            }
            else
            {
                base.Move();
            }
        }

        public Weapon Clone()
        {
            return new Weapon(this.name, this.TopLeftCoords, this.image, this.Speed, this.Damage, ConsoleColor.Magenta);
        }

        public virtual IList<MovingUnit> GetWeapon()
        {
            IList<MovingUnit> list = new List<MovingUnit>();
            list.Add(this.Clone());
            return list;
        }
    }
}
