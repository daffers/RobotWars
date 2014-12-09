using System.Collections.Generic;

namespace RobotWars
{
    public class ControlConsole
    {
        private readonly List<string> _finalPositions;

        public ControlConsole()
        {
            _finalPositions = new List<string>();
        }

        public void SetGrid(string gridSize)
        {
        }

        public void SetRoverPositionAndCommands(string roverStartingVector, string roverMovementSequence)
        {
            var locationParser = new LocationParser();
            var commandParser = new CommandParser();
            var startingPostion = locationParser.Parse(roverStartingVector);
            var roverCommands = commandParser.ParseRoverMovements(roverMovementSequence);

            var warrior = new WarriorRobot();
            warrior.UploadArena(new Arena(1,1));
            warrior.StartAt(startingPostion);
            warrior.ExecuteCommandList(roverCommands);
            _finalPositions.Add(warrior.ReportPosition().ToString());
        }

        public List<string> GetFinalPositions()
        {
            return _finalPositions;
        }
    }
}