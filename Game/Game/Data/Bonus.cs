namespace Game.Data
{
    using Game.Tools;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Bonus : MovingUnit
    {
        
        public Bonus(BonusType type, Point topLeftCoords, char[,] image, Point speed, 
            ConsoleColor color = ConsoleColor.Green)
            : base(topLeftCoords, image, speed, color)
        {
            this.Type = type;
        }

        //enum BonusType consists of three different bonuses: Health ; Magic weapon; Common weapon
        public BonusType Type { get; set; }

        public override UnitStatus GetStatus()
        {
            return UnitStatus.Bonus;
        }

        //bonuses can collide with enemies, weapons and the player
        public override bool CanCollideWith(UnitStatus otherStatus)
        {
            return otherStatus == UnitStatus.Player || otherStatus == UnitStatus.Enemy || otherStatus == UnitStatus.Weapon;
        }

        //bonus gets destroyed on impact
        public override void RespondToCollision(CollisionData collisionData)
        {
            this.isDestroyed = true;
        }

        //the bonuses' initial direction is calculated based on the player's last move
        //bounces off left wall if movemet is to the left
        public override void Move()
        {
            if (this.topLeftCoords.Col < 1)
            {
                this.Speed = new Point(this.Speed.Row, this.Speed.Col * (-1));
            }
            this.UpdatePosition();
        }

    }
}
