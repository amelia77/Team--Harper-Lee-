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

        public Bonus(BonusType type, Point topLeftCoords, char[,] image, ConsoleColor color = ConsoleColor.Magenta)
            : base(topLeftCoords, image, new Point(0,0), color)
        {
            this.Type = type;
        }

        public BonusType Type { get; set; }

        public override Status GetStatus()
        {
            return Status.Bonus;
        }

        public override bool CanCollideWith(Status otherStatus)
        {
            return otherStatus == Status.Player;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.isDestroyed = true;
        }

        public override void Move()
        {
            
        }
    }
}
