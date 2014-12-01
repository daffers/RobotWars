using System;
using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public class TurnLeft : RobotCommand
    {
        public override RobotVector GenerateNewVector(RobotVector currentPosition)
        {
            var newHeading = (currentPosition.Heading.GetHashCode() + 270) % 360;

            return new RobotVector(currentPosition.Position, (Heading)newHeading);
        }
    }
}