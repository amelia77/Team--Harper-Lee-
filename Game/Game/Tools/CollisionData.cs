namespace Game.Tools
{
    using Game.Data;
    using System.Collections.Generic;

    public class CollisionData
    {
        public readonly Point CollisionForceDirection; // direction of the current game unit
        public readonly List<UnitStatus> hitObjectsCollisionGroupStrings;
        public readonly List<GameUnit> hitObjectsCollisionUnits;

        public CollisionData(Point collisionForceDirection, UnitStatus objectCollisionGroupString, GameUnit hitObjectsCollisionUnits)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<UnitStatus>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
            this.hitObjectsCollisionUnits = new List<GameUnit>();
            this.hitObjectsCollisionUnits.Add(hitObjectsCollisionUnits);
        }

        public CollisionData(Point collisionForceDirection, List<UnitStatus> hitObjectsCollisionGroupStrings, List<GameUnit> hitObjectsCollisionUnits)
        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<UnitStatus>();

            foreach (UnitStatus status in hitObjectsCollisionGroupStrings)
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
