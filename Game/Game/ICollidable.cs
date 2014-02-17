using System.Collections.Generic;
namespace Game
{
    public interface ICollidable
    {
        bool CanCollideWith(string objectType);

        List<Point> GetCollisionProfile();

        void RespondToCollision(CollisionData collisionData);

        string GetCollisionGroupString();
    }
}
