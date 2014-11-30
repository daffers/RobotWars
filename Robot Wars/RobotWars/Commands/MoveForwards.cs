using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public class MoveForwards : RobotCommand
    {
        public override RobotVector GenerateNewVector(RobotVector currentPosition)
        {
            return new RobotVector(new Position(0,1), Heading.North);
        }
    }
}