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
            robot.StartAt(SimpleVector());
        }

        [Test]
        public void CannotStartARobotWhenNoArenaUploaded()
        {
            var robot = new WarriorRobot();
            Assert.Throws<WarriorRobot.ArenaNotUploadedException>(() => robot.StartAt(SimpleVector()));
        }

        private static RobotVector SimpleVector()
        {
            return new RobotVector(new Position(0, 0), Heading.North);
        }

        [Test]
        public void CanProvideCommandsToRobot()
        {
            var commandList = new List<RobotCommand>();
            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1, 1));
            robot.StartAt(SimpleVector());
            robot.SetCommandList(commandList);
        }

        [Test]
        public void CannotSetCommandListUntilRobotHasStartingLocation()
        {
            var commandList = new List<RobotCommand>();
            var robot = new WarriorRobot();
            Assert.Throws<WarriorRobot.StartLocationNotSetException>(() => robot.SetCommandList(commandList));
        }

        [Test]
        public void CanReportARobotsPosition()
        {

        }
    }

    public class WarriorRobot
    {
        private Arena _arena;
        private bool _startLocationSet;
        
        public void UploadArena(Arena arena)
        {
            _arena = arena;
        }
        
        public void SetCommandList(List<RobotCommand> commandList)
        {
            if (!_startLocationSet)
                throw new StartLocationNotSetException();
        }

        public class ArenaNotUploadedException: Exception
        {
        }

        public class StartLocationNotSetException : Exception
        {
        }

        public void StartAt(RobotVector robotVector)
        {
            if (_arena == null)
                throw new ArenaNotUploadedException();

            _startLocationSet = true;
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
    }

    [TestFixture]
    public class RobotVectorTests
    {
        [Test]
        public void CanReadBackVectorValues()
        {
            var position = new Position(0, 0);
            var heading = Heading.North;
            var robotVector = new RobotVector(position, heading);

            Assert.That(robotVector.Position, Is.EqualTo(position));
            Assert.That(robotVector.Heading, Is.EqualTo(heading));
        }
    }

    public enum Heading
    {
        North
    }

    [TestFixture]
    public class PositionTests
    {
        [TestCase(0,0,1,1, false)]
        [TestCase(1,1,1,1, true)]
        [TestCase(1,2,1,2, true)]
        [TestCase(0,1,1,2, false)]
        public void EqualityTests(int positon1X, int positon1Y, int position2X, int position2Y, bool shouldBeEqual)
        {
            var positionOne = new Position(positon1X, positon1Y);
            var positionTwo = new Position(position2X, position2Y);

            Assert.AreEqual(shouldBeEqual, positionOne == positionTwo);
        }

        [TestCase(0, 0, 1, 1, true)]
        [TestCase(1, 1, 1, 1, false)]
        [TestCase(1, 2, 1, 2, false)]
        [TestCase(0, 1, 1, 2, true)]
        public void InEqualityTests(int positon1X, int positon1Y, int position2X, int position2Y, bool shouldNotBeEqual)
        {
            var positionOne = new Position(positon1X, positon1Y);
            var positionTwo = new Position(position2X, position2Y);

            Assert.AreEqual(shouldNotBeEqual, positionOne != positionTwo);
        }
    }


    public struct Position
    {
        private readonly int _xCord;

        private readonly int _yCord;

        public Position(int xCord, int yCord)
        {
            _xCord = xCord;
            _yCord = yCord;
        }

        public bool Equals(Position other)
        {
            return _xCord == other._xCord && _yCord == other._yCord;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Position && Equals((Position) obj);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        //Resharper generated
        public override int GetHashCode()
        {
            unchecked
            {
                return (_xCord*397) ^ _yCord;
            }
        }
    }
}
