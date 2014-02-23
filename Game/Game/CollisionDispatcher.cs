namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollisionDispatcher
    {
        public static void HandleCollisions(List<MovingUnit> movingObjects, List<GameUnit> staticObjects)
        {
            HandleMovingWithStaticCollisions(movingObjects, staticObjects);
            HandleMovingWithMovingCollisions(movingObjects);
        }

        private static void HandleMovingWithStaticCollisions(List<MovingUnit> movingObjects, List<GameUnit> staticObjects)
        {
            foreach (var movingObject in movingObjects)
            {
                int verticalIndex = VerticalCollisionIndex(movingObject, staticObjects);
                int horizontalIndex = HorizontalCollisionIndex(movingObject, staticObjects);

                Point movingCollisionForceDirection = new Point(0, 0);

                if (verticalIndex != -1)
                {
                    movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                    staticObjects[verticalIndex].RespondToCollision(
                        new CollisionData(new Point(movingObject.Speed.Row, 0),
                            movingObject.GetStatus(), movingObject)
                            );
                    
                }

                if (horizontalIndex != -1)
                {
                    movingCollisionForceDirection.Col = -movingObject.Speed.Col;
                    staticObjects[horizontalIndex].RespondToCollision(
                        new CollisionData(new Point(0, movingObject.Speed.Col),
                            movingObject.GetStatus(), movingObject)
                            );
                }

                int diagonalIndex = -1;
                if (horizontalIndex == -1 && verticalIndex == -1)
                {
                    diagonalIndex = DiagonalCollisionIndex(movingObject, staticObjects);
                    if (diagonalIndex != -1)
                    {
                        movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                        movingCollisionForceDirection.Col = -movingObject.Speed.Col;

                        staticObjects[diagonalIndex].RespondToCollision(
                        new CollisionData(new Point(movingObject.Speed.Row, 0),
                            movingObject.GetStatus(), movingObject)
                            );
                    }
                }

                List<UnitStatus> hitByMovingCollisionGroups = new List<UnitStatus>();
                List<GameUnit> hitByMovingCollisionGroupsObj = new List<GameUnit>();

                if (verticalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[verticalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(staticObjects[verticalIndex]);
                }

                if (horizontalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[horizontalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(staticObjects[horizontalIndex]);
                }

                if (diagonalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[diagonalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(staticObjects[diagonalIndex]);
                }

                if (verticalIndex != -1 || horizontalIndex != -1 || diagonalIndex != -1)
                {
                    movingObject.RespondToCollision(
                        new CollisionData(movingCollisionForceDirection,
                            hitByMovingCollisionGroups, hitByMovingCollisionGroupsObj)
                            );
                }
            }
        }

        public static int VerticalCollisionIndex(MovingUnit moving, List<GameUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> verticalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                verticalProfile.Add(new Point(coord.Row + moving.Speed.Row, coord.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, verticalProfile);

            return collisionIndex;
        }

        public static int HorizontalCollisionIndex(MovingUnit moving, List<GameUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> horizontalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new Point(coord.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        public static int DiagonalCollisionIndex(MovingUnit moving, List<GameUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> horizontalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new Point(coord.Row + moving.Speed.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        private static int GetCollisionIndex(MovingUnit moving, ICollection<GameUnit> objects, List<Point> movingProfile)
        {
            int collisionIndex = 0;

            foreach (var obj in objects)
            {
                if (moving.CanCollideWith(obj.GetStatus()) || obj.CanCollideWith(moving.GetStatus()))
                {
                    List<Point> objProfile = obj.GetCollisionProfile();

                    if (ProfilesIntersect(movingProfile, objProfile))
                    {
                        return collisionIndex;
                    }
                }

                collisionIndex++;
            }

            return -1;
        }

        private static bool ProfilesIntersect(List<Point> firstProfile, List<Point> secondProfile)
        {
            foreach (var firstCoord in firstProfile)
            {
                foreach (var secondCoord in secondProfile)
                {
                    if (firstCoord.Equals(secondCoord))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void HandleMovingWithMovingCollisions(List<MovingUnit> movingObjects)
        {
            foreach (var movingObject in movingObjects)
            {
                List<MovingUnit> moving = new List<MovingUnit>(movingObjects);
                moving.Remove(movingObject);

                int verticalIndex = VerticalCollisionIndex(movingObject, moving);
                int horizontalIndex = HorizontalCollisionIndex(movingObject, moving);

                Point movingCollisionForceDirection = new Point(0, 0);

                if (verticalIndex != -1)
                {

                    movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                    movingObjects[verticalIndex].RespondToCollision(
                        new CollisionData(new Point(movingObject.Speed.Row, 0),
                            movingObject.GetStatus(), movingObject)
                            );
                }

                if (horizontalIndex != -1)
                {
                    movingCollisionForceDirection.Col = -movingObject.Speed.Col;
                    movingObjects[horizontalIndex].RespondToCollision(
                        new CollisionData(new Point(0, movingObject.Speed.Col),
                            movingObject.GetStatus(), movingObject)
                            );
                }

                int diagonalIndex = -1;
                if (horizontalIndex == -1 && verticalIndex == -1)
                {

                    diagonalIndex = DiagonalCollisionIndex(movingObject, movingObjects);
                    if (diagonalIndex != -1)
                    {
                        //movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                        //movingCollisionForceDirection.Col = -movingObject.Speed.Col;

                        movingObjects[diagonalIndex].RespondToCollision(
                        new CollisionData(new Point(movingObject.Speed.Row, 0),
                            movingObject.GetStatus(), movingObject)
                            );
                    }
                }

                List<UnitStatus> hitByMovingCollisionGroups = new List<UnitStatus>();
                List<GameUnit> hitByMovingCollisionGroupsObj = new List<GameUnit>();

                if (verticalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(movingObjects[verticalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(movingObjects[verticalIndex]);
                }

                if (horizontalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(movingObjects[horizontalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(movingObjects[horizontalIndex]);
                }

                if (diagonalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(movingObjects[diagonalIndex].GetStatus());
                    hitByMovingCollisionGroupsObj.Add(movingObjects[diagonalIndex]);
                }

                if (verticalIndex != -1 || horizontalIndex != -1 || diagonalIndex != -1)
                {
                    movingObject.RespondToCollision(
                        new CollisionData(movingCollisionForceDirection,
                            hitByMovingCollisionGroups, hitByMovingCollisionGroupsObj)
                            );
                }
            }
        }


        public static int VerticalCollisionIndex(MovingUnit moving, List<MovingUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> verticalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                verticalProfile.Add(new Point(coord.Row + moving.Speed.Row, coord.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, verticalProfile);

            return collisionIndex;
        }

        public static int HorizontalCollisionIndex(MovingUnit moving, List<MovingUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> horizontalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new Point(coord.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        public static int DiagonalCollisionIndex(MovingUnit moving, List<MovingUnit> objects)
        {
            List<Point> profile = moving.GetCollisionProfile();

            List<Point> horizontalProfile = new List<Point>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new Point(coord.Row + moving.Speed.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        private static int GetCollisionIndex(MovingUnit moving, ICollection<MovingUnit> objects, List<Point> movingProfile)
        {
            int collisionIndex = 0;

            foreach (var obj in objects)
            {
                if (moving.CanCollideWith(obj.GetStatus()) || obj.CanCollideWith(moving.GetStatus()))
                {
                    List<Point> objProfile = obj.GetCollisionProfile();

                    if (ProfilesIntersect(movingProfile, objProfile))
                    {
                        return collisionIndex;
                    }
                }

                collisionIndex++;
            }

            return -1;
        }

    }
}

