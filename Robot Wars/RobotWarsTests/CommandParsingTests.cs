using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RobotWars;
using RobotWars.Commands;

namespace RobotWarsTests
{
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
}