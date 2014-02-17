namespace Game
{
    using System;
    using System.Collections.Generic;
    public static class CollisionDispatcher{
    public static void HandleCollisions(List<MovingUnit> movingObjects, List<GameUnit> staticObjects)
        {
            HandleMovingWithStaticCollisions(movingObjects, staticObjects);
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
                            movingObject.GetCollisionGroupString())
                            );
                }

                if (horizontalIndex != -1)
                {
                    movingCollisionForceDirection.Col = -movingObject.Speed.Col;
                    staticObjects[horizontalIndex].RespondToCollision(
                        new CollisionData(new Point(0, movingObject.Speed.Col),
                            movingObject.GetCollisionGroupString())
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
                            movingObject.GetCollisionGroupString())
                            );
                    }
                }

                List<string> hitByMovingCollisionGroups = new List<string>();

                if(verticalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[verticalIndex].GetCollisionGroupString());
                }

                if(horizontalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[horizontalIndex].GetCollisionGroupString());
                }

                if(diagonalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[diagonalIndex].GetCollisionGroupString());
                }

                if (verticalIndex != -1 || horizontalIndex != -1 || diagonalIndex != -1)
                {
                    movingObject.RespondToCollision(
                        new CollisionData(movingCollisionForceDirection,
                            hitByMovingCollisionGroups)
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
                if (moving.CanCollideWith(obj.GetCollisionGroupString()) || obj.CanCollideWith(moving.GetCollisionGroupString()))
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
    }
}

