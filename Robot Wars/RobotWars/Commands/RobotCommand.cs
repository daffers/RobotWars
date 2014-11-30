using RobotWars.Positioning;

namespace RobotWars.Commands
{
    public abstract class RobotCommand
    {
        public abstract RobotVector GenerateNewVector(RobotVector currentPosition);
    }
}