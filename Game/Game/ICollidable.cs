using System.Collections.Generic;
namespace Game
{
    public interface ICollidable
    {
        bool CanCollideWith(UnitStatus objectStatus);

        List<Point> GetCollisionProfile();

        void RespondToCollision(CollisionData collisionData);

        UnitStatus GetStatus();
    }
}
