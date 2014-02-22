namespace Game
{
    using System;
    public class Weapon : MovingUnit, IMovable, IWeapon, ICollidable
    {
        private string name;
        private int damage;
        private int ammo;

        public Status status = Status.Weapon;

        public Weapon(string name, Point topLeft, char[,] image, Point speed, int damage, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeft, image, speed, color)
        {
            this.Damage = damage;
            this.Ammo = ammo;
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

        public override Status GetStatus()
        {
            return this.status;
        }

        public override bool CanCollideWith(Status otherStatus)
        {
            return otherStatus == Status.Enemy || otherStatus == Status.Player;
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

        public Weapon Clone()
        {
            return new Weapon(this.name, this.TopLeftCoords, this.image, this.Speed, this.Damage, ConsoleColor.Magenta);
        }
    }
}
