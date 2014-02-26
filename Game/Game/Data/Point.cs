namespace Game.Data
{
    //Summary:
    //struct Point represents two-dimentional point with integer coordinates
    //Overloads operators + and -
    public struct Point
    {
        
        public int Row { get; set; }
        public int Col { get; set; }

        
        public Point(int row, int col) 
            : this()
        {
            this.Row = row;
            this.Col = col;
        }

        //methods
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.Row + b.Row, a.Col + b.Col);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.Row - b.Row, a.Col - b.Col);
        }

        public override bool Equals(object obj)
        {
            Point objAsMatrixCoords = (Point)obj;

            return objAsMatrixCoords.Row == this.Row && objAsMatrixCoords.Col == this.Col;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
