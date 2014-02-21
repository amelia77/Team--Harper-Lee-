using System.Collections.Generic;
namespace Game
{
    public interface ICollidable
    {
        bool CanCollideWith(Status objectStatus);

        List<Point> GetCollisionProfile();

        void RespondToCollision(CollisionData collisionData);

        Status GetStatus();
    }
}
