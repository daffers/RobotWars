namespace RobotWars.Positioning
{
    public struct RobotVector
    {
        private readonly Position _position;
        private readonly Heading _heading;

        public RobotVector(Position position, Heading heading)
        {
            _position = position;
            _heading = heading;
        }

        public Position Position { get { return _position; } }
        public Heading Heading { get { return _heading; } }


        public override bool Equals(object obj)
        {
            //Resharper generated
            if (ReferenceEquals(null, obj)) return false;
            return obj is RobotVector && Equals((RobotVector) obj);
        }

        public static bool operator ==(RobotVector left, RobotVector right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RobotVector left, RobotVector right)
        {
            return !(left == right);
        }

        //Resharper generated
        public bool Equals(RobotVector other)
        {
            return _position.Equals(other._position) && _heading == other._heading;
        }

        //Resharper generated
        public override int GetHashCode()
        {
            unchecked
            {
                return (_position.GetHashCode()*397) ^ (int) _heading;
            }
        }
    }
}