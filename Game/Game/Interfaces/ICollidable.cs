namespace Game.Interfaces
{
    using Game.Data;
    using Game.Tools;
    using System.Collections.Generic;

    public interface ICollidable
    {
        //checks for collision depending on the status of the unit(weapon, enemy, player, bonus)
        bool CanCollideWith(UnitStatus objectStatus);

        List<Point> GetCollisionProfile();

        //what to do on impact
        void RespondToCollision(CollisionData collisionData);

        UnitStatus GetStatus();
    }
}
