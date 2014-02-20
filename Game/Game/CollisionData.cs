namespace Game
{
    using System.Collections.Generic;
    public class CollisionData
    {
        public readonly Point CollisionForceDirection; // direction of the current game unit
        public readonly List<string> hitObjectsCollisionGroupStrings;

        public CollisionData(Point collisionForceDirection, string objectCollisionGroupString)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<string>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
        }

        public CollisionData(Point collisionForceDirection, List<string> hitObjectsCollisionGroupStrings)
        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<string>();

            foreach (var str in hitObjectsCollisionGroupStrings)
            {
                this.hitObjectsCollisionGroupStrings.Add(str);
            }
        }
    }
}
