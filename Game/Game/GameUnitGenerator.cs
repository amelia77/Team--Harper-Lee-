namespace Game
{

    using System;
using System.Collections.Generic;
    using System.Linq;
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

        public List<MovingUnit> GenerateStaticUnit(List<GameUnit> allUnitsInTheGameSoFar)
        {
            
            List<MovingUnit> staticUnits = new List<MovingUnit>();
            BonusType type = (BonusType)randomGenerator.Next(1, 4);
            for (int i = 0; i < staticUnitCount; i++)
            {
                int randomRow = this.randomGenerator.Next(topLeft.Row, bottomRight.Row);
                int randomCol = this.randomGenerator.Next(topLeft.Col, bottomRight.Col);
                
                Point position = new Point(randomRow, randomCol);

                while (IsPointInAGameUnit(position, allUnitsInTheGameSoFar))
                {
                    //if (allUnitsInTheGameSoFar.FirstOrDefault(u => u.Equals(position)) == null)

                    randomRow = this.randomGenerator.Next(topLeft.Row, bottomRight.Row);
                    randomCol = this.randomGenerator.Next(topLeft.Col, bottomRight.Col);
                    position = new Point(randomRow, randomCol);
                }
                staticUnits.Add(new Bonus(type,position, new char[,] { { '@' } }, ConsoleColor.Green));
            }
            return staticUnits;
        }

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
