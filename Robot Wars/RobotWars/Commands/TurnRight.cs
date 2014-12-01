using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public class TurnRight : RobotCommand
    {
        public override RobotVector GenerateNewVector(RobotVector currentPosition)
        {
            var newHeading = (currentPosition.Heading.GetHashCode() + 90) % 360;

            return new RobotVector(currentPosition.Position, (Heading)newHeading);
        }
    }
}