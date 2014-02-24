namespace Game.Interfaces
{
    using Game.Data;
    using Game.Tools;
    using System.Collections.Generic;
    public interface ICollidable
    {
        bool CanCollideWith(UnitStatus objectStatus);

        List<Point> GetCollisionProfile();

        void RespondToCollision(CollisionData collisionData);

        UnitStatus GetStatus();
    }
}
