using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using NUnit.Framework;
using RobotWars;

namespace Scratch
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        [Ignore("Awaiting implementation")]
        public void AcceptanceCriteriaPassesThroughConsole()
        {
            const string gridSize = "5 5";
            const string rover1StartingVector = "1 2 N";
            const string rover1MovementSequence = "LMLMLMLMM";
            const string rover2StartingVector = "3 3 E";
            const string rover2MovementSequence = "MMRMMRMRRM";

            var controlConsole = new ControlConsole();

            controlConsole.SetGrid(gridSize);
            controlConsole.SetRoverPositionAndCommands(rover1StartingVector, rover1MovementSequence);
            controlConsole.SetRoverPositionAndCommands(rover2StartingVector, rover2MovementSequence);

            List<string> finalPositions = controlConsole.GetFinalPositions();

            const string rover1ExpectedFinalPositon = "1 3 N";
            const string rover2ExpectedFinalPositon = "5 1 E";

            Assert.That(finalPositions.Count, Is.EqualTo(2));
            Assert.That(finalPositions[0], Is.EqualTo(rover1ExpectedFinalPositon));
            Assert.That(finalPositions[0], Is.EqualTo(rover2ExpectedFinalPositon));
        }
    }

    [TestFixture]
    public class WarriorRobotTests
    {
        [Test]
        public void CanCreateAWarriorRobot()
        {
            var robot = new WarriorRobot();
        }

        [Test]
        public void CanUploadArenaToRobot()
        {
            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1, 1));
        }

        [Test]
        public void CanStartARobotOnAnArenaPosition()
        {
            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1,1));
            robot.StartAt(0, 0, "N");
        }

        [Test]
        public void CannotStartARobotWhenNoArenaUploaded()
        {
            var robot = new WarriorRobot();
            Assert.Throws<WarriorRobot.ArenaNotUploadedException>(() => robot.StartAt(0, 0, "N"));
        }

        [Test]
        public void CanProvideCommandsToRobot()
        {

        }
    }

    public class WarriorRobot
    {
        private Arena _arena;

        public void StartOn(Arena arena)
        {
        }

        public void UploadArena(Arena arena)
        {
            _arena = arena;
        }

        public void StartAt(int xCord, int yCord, string direction)
        {
            if (_arena == null)
                throw new ArenaNotUploadedException();
        }

        public class ArenaNotUploadedException: Exception
        {
        }
    }

    [TestFixture]
    public class CommandParsingTests
    {
        [Test]
        public void CanCreateACommanParser()
        {
            var commandParser = new CommandParser();
        }

        [Test]
        public void CanParseRobotMovementCommandsIntoAListOfCommands()
        {
            const string commandString = "LRM";
            var commandParser = new CommandParser();
            IEnumerable<RobotCommand> commands = commandParser.ParseRoverMovements(commandString);

            Assert.That(commands.Count(), Is.EqualTo(commandString.Length));
        }

        [Test]
        public void LIsParsedToATurnLeftCommand()
        {
            const string commandString = "L";
            var commandParser = new CommandParser();
            IEnumerable<RobotCommand> commands = commandParser.ParseRoverMovements(commandString);

            Assert.That(commands.First(), Is.TypeOf<TurnLeft>());
        }

        [Test]
        public void RIsParsedToATurnRightCommand()
        {
            const string commandString = "R";
            var commandParser = new CommandParser();
            IEnumerable<RobotCommand> commands = commandParser.ParseRoverMovements(commandString);

            Assert.That(commands.First(), Is.TypeOf<TurnRight>());
        }

        [Test]
        public void MIsParsedToAMoveForwardCommand()
        {
            const string commandString = "M";
            var commandParser = new CommandParser();
            IEnumerable<RobotCommand> commands = commandParser.ParseRoverMovements(commandString);

            Assert.That(commands.First(), Is.TypeOf<MoveForwards>());
        }

        [Test]
        public void MultiCommandParseTest()
        {
            const string commandString = "MRRMLLRM";
            var commandParser = new CommandParser();
            IList<RobotCommand> commands = commandParser.ParseRoverMovements(commandString).ToList();

            Assert.That(commands.Count(), Is.EqualTo(commandString.Length));
            Assert.That(commands[0], Is.TypeOf<MoveForwards>());
            Assert.That(commands[1], Is.TypeOf<TurnRight>());
            Assert.That(commands[2], Is.TypeOf<TurnRight>());
            Assert.That(commands[3], Is.TypeOf<MoveForwards>());
            Assert.That(commands[4], Is.TypeOf<TurnLeft>());
            Assert.That(commands[5], Is.TypeOf<TurnLeft>());
            Assert.That(commands[6], Is.TypeOf<TurnRight>());
            Assert.That(commands[7], Is.TypeOf<MoveForwards>());
        }
    }

    public class MoveForwards : RobotCommand
    {
    }

    public class TurnRight : RobotCommand
    {
    }

    public class TurnLeft : RobotCommand
    {
    }

    public class RobotCommand
    {
    }

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
