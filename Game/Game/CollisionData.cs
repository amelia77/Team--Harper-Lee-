namespace Game
{
    //blalatest
    using System.Collections.Generic;

    public class CollisionData
    {
        public readonly Point CollisionForceDirection; // direction of the current game unit
        public readonly List<Status> hitObjectsCollisionGroupStrings;

        public CollisionData(Point collisionForceDirection, Status objectCollisionGroupString)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<Status>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
        }

        public CollisionData(Point collisionForceDirection, List<Status> hitObjectsCollisionGroupStrings)
        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<Status>();

            foreach (Status status in hitObjectsCollisionGroupStrings)
            {
                this.hitObjectsCollisionGroupStrings.Add(status);
            }
        }
    }
}
