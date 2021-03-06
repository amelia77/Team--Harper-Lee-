﻿namespace Game.Tools
{
    using Game.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    //Summary:
    //Generates random bonuses
    public class GameUnitGenerator
    {
        private const int staticUnitCount = 1;
        private Random randomGenerator;
        private Point topLeft;
        private Point bottomRight;

        public GameUnitGenerator(Random randomGenerator, Point topLeft, Point bottomRight)
        {
            this.randomGenerator = randomGenerator;
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public Random RandomGenerator
        {
            get { return this.randomGenerator; }
            private set
            {
                this.randomGenerator = value;
            }
        }

        //generates static bonuses randomly
        public List<MovingUnit> GenerateStaticUnit(List<GameUnit> allUnitsInTheGameSoFar)
        {
            
            List<MovingUnit> staticUnits = new List<MovingUnit>();
            BonusType type = (BonusType)randomGenerator.Next(1, 4);
            for (int i = 0; i < staticUnitCount; i++)
            {
                int randomRow = this.randomGenerator.Next(topLeft.Row, bottomRight.Row);
                int randomCol = this.randomGenerator.Next(topLeft.Col, bottomRight.Col);
                
                Point position = new Point(randomRow, randomCol);
                Point speed = new Point(randomGenerator.Next(0, 2), randomGenerator.Next(-1, 2));

                while (IsPointInAGameUnit(position, allUnitsInTheGameSoFar))
                {
                    //if (allUnitsInTheGameSoFar.FirstOrDefault(u => u.Equals(position)) == null)

                    randomRow = this.randomGenerator.Next(topLeft.Row, bottomRight.Row);
                    randomCol = this.randomGenerator.Next(topLeft.Col, bottomRight.Col);
                    position = new Point(randomRow, randomCol);
                }
                staticUnits.Add(new Bonus(type,position, new char[,] { { (char) 2}},speed, ConsoleColor.Green));
            }
            return staticUnits;
        }

        //makes sure that on creation randomly generated bonuses don't overlap with a unit
        private bool IsPointInAGameUnit(Point position, List<GameUnit> allUnitsInTheGameSoFar)
        {
            foreach (var unit in allUnitsInTheGameSoFar)
            {
                Point currUnitLeftTopPoint = unit.TopLeftCoords;
                if (position.Equals(currUnitLeftTopPoint))
                {
                    return true;
                }
                else if (position.Row>= currUnitLeftTopPoint.Row && 
                    position.Row <= currUnitLeftTopPoint.Row + unit.GetImage().GetLength(0) &&
                    position.Col >= currUnitLeftTopPoint.Col &&
                    position.Col <= currUnitLeftTopPoint.Col + unit.GetImage().GetLength(1))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
