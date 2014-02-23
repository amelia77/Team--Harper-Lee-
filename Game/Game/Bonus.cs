using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum BonusType
    {
        Health = 1,
        MagicWeapon = 2,
        CommonWeapon = 3
    }
    public class Bonus : MovingUnit
    {

        public Bonus(BonusType type, Point topLeftCoords, char[,] image, Point speed, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeftCoords, image, speed, color)
        {
            this.Type = type;
        }

        public BonusType Type { get; set; }

        public override UnitStatus GetStatus()
        {
            return UnitStatus.Bonus;
        }

        public override bool CanCollideWith(UnitStatus otherStatus)
        {
            return otherStatus == UnitStatus.Player;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.isDestroyed = true;
        }

        public override void Move()
        {
            if (this.topLeftCoords.Col < 1)
            {
                this.Speed = new Point(this.Speed.Row, this.Speed.Col *(-1));
            }
            this.UpdatePosition();
        }

    }
}
