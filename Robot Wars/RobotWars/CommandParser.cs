using System;
using System.Collections.Generic;
using RobotWars.Commands;

namespace RobotWars
{
    public class CommandParser
    {
        public IEnumerable<RobotCommand> ParseRoverMovements(string commandString)
        {
            foreach (char command in commandString)
            {
                yield return ParseCommand(command);
            }
        }

        private RobotCommand ParseCommand(char command)
        {
            if (command == 'L')
                return new TurnLeft();
            if (command == 'R')
                return new TurnRight();
            if (command == 'M')
                return new MoveForwards();
            throw new ArgumentException();
        }
    }
}