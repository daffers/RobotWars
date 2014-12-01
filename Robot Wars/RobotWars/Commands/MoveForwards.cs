using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public class MoveForwards : RobotCommand
    {
        public override RobotVector GenerateNewVector(RobotVector currentPosition)
        {
            if (currentPosition.Heading == Heading.North)
                return new RobotVector(new Position(0,1), Heading.North);
            if (currentPosition.Heading == Heading.East)
                return new RobotVector(new Position(1, 0), Heading.East);
            if (currentPosition.Heading == Heading.West)
                return new RobotVector(new Position(-1, 0), Heading.West);
            return new RobotVector(new Position(0, -1), Heading.South);
        }
    }
}