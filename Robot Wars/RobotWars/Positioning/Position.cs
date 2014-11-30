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
}