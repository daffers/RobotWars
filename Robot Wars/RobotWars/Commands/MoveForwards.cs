using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public class MoveForwards : RobotCommand
    {
        public override RobotVector GenerateNewVector(RobotVector currentPosition)
        {
            if (currentPosition.Heading == Heading.North)
                return new RobotVector(currentPosition.Position + new YDimensionUnit(1), Heading.North);
            if (currentPosition.Heading == Heading.East)
                return new RobotVector(currentPosition.Position + new XDimensionUnit(1), Heading.East);
            if (currentPosition.Heading == Heading.West)
                return new RobotVector(currentPosition.Position - new XDimensionUnit(1), Heading.West);

            return new RobotVector(currentPosition.Position - new YDimensionUnit(1), Heading.South);
        }
    }
}