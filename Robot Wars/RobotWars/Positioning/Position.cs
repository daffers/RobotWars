namespace RobotWars.Positioning
{
    public struct Position
    {
        private readonly int _xCord;

        private readonly int _yCord;

        public Position(int xCord, int yCord)
        {
            _xCord = xCord;
            _yCord = yCord;
        }

        public int XCord { get { return _xCord; } }
        public int YCord { get { return _yCord; } }

        public bool Equals(Position other)
        {
            return _xCord == other._xCord && _yCord == other._yCord;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Position && Equals((Position) obj);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        //Resharper generated
        public override int GetHashCode()
        {
            unchecked
            {
                return (_xCord*397) ^ _yCord;
            }
        }
    }

    public struct XDimensionUnit
    {
        private readonly int _units;

        public XDimensionUnit(int units)
        {
            _units = units;
        }

        public static Position operator +(Position startingPoint, XDimensionUnit movement)
        {
            return new Position(startingPoint.XCord + movement._units, startingPoint.YCord);
        }

        public static Position operator -(Position startingPoint, XDimensionUnit movement)
        {
            return new Position(startingPoint.XCord - movement._units, startingPoint.YCord);
        }
    }

    public struct YDimensionUnit
    {
        private readonly int _units;

        public YDimensionUnit(int units)
        {
            _units = units;
        }

        public static Position operator +(Position startingPoint, YDimensionUnit movement)
        {
            return new Position(startingPoint.XCord, startingPoint.YCord + movement._units);
        }

        public static Position operator -(Position startingPoint, YDimensionUnit movement)
        {
            return new Position(startingPoint.XCord, startingPoint.YCord - movement._units);
        }
    }
}