namespace Game
{
    
    using System.Collections.Generic;

    public class CollisionData
    {
        public readonly Point CollisionForceDirection; // direction of the current game unit
        public readonly List<Status> hitObjectsCollisionGroupStrings;
        public readonly List<GameUnit> hitObjectsCollisionUnits;

        public CollisionData(Point collisionForceDirection, Status objectCollisionGroupString, GameUnit hitObjectsCollisionUnits)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<Status>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
            this.hitObjectsCollisionUnits = new List<GameUnit>();
            this.hitObjectsCollisionUnits.Add(hitObjectsCollisionUnits);
        }

        public CollisionData(Point collisionForceDirection, List<Status> hitObjectsCollisionGroupStrings, List<GameUnit> hitObjectsCollisionUnits)
        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<Status>();

            foreach (Status status in hitObjectsCollisionGroupStrings)
            {
                this.hitObjectsCollisionGroupStrings.Add(status);
            }
            this.hitObjectsCollisionUnits = new List<GameUnit>();

            foreach (var obj in hitObjectsCollisionUnits)
            {
                this.hitObjectsCollisionUnits.Add(obj);
            }
        }
    }
}
