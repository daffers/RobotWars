using System.Collections.Generic;
using NUnit.Framework;
using RobotWars;
using RobotWars.Commands;
using RobotWars.Positioning;

namespace RobotWarsTests
{
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
            return new RobotVector(new Position(1, 1), Heading.West);
        }

        [Test]
        public void CanProvideCommandsToRobot()
        {
            var commandList = new List<RobotCommand>();
            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1, 1));
            robot.StartAt(SimpleVector());
            robot.ExecuteCommandList(commandList);
        }

        [Test]
        public void CannotSetCommandListUntilRobotHasStartingLocation()
        {
            var commandList = new List<RobotCommand>();
            var robot = new WarriorRobot();
            Assert.Throws<WarriorRobot.StartLocationNotSetException>(() => robot.ExecuteCommandList(commandList));
        }

        [Test]
        public void CanReportARobotsPosition()
        {
            var startPosition = SimpleVector();

            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1, 1));
            robot.StartAt(startPosition);

            RobotVector reportedPosition = robot.ReportPosition();

            Assert.That(reportedPosition, Is.EqualTo(startPosition));
        }

        [Test]
        public void ExecutingASingleCommandUpdatesThePositionAsExpected()
        {
            var startPosition = new RobotVector(new Position(0, 0), Heading.North);

            var robot = new WarriorRobot();
            robot.UploadArena(new Arena(1, 1));
            robot.StartAt(startPosition);
            
            var commandList = new List<RobotCommand>();
            commandList.Add(new MoveForwards());
            robot.ExecuteCommandList(commandList);

            RobotVector reportedPosition = robot.ReportPosition();

            Assert.That(startPosition, Is.Not.EqualTo(reportedPosition));
        }
    }

    [TestFixture]
    public class MovementTests
    {
        [Test]
        public void MoveForwardWhenFacingNorth()
        {
            const int startXCord = 0;
            const int startYCord = 0;
            const Heading startingHeading = Heading.North;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord,startYCord), startingHeading));

            Assert.That(result.Position.YCord, Is.EqualTo(startYCord + 1));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }

        [Test]
        public void MoveForwardWhenFacingSouth()
        {
            const int startXCord = 0;
            const int startYCord = 0;
            const Heading startingHeading = Heading.South;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            Assert.That(result.Position.YCord, Is.EqualTo(startYCord - 1));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }
        
        [Test]
        public void MoveForwardWhenFacingEast()
        {
            const int startXCord = 0;
            const int startYCord = 0;
            const Heading startingHeading = Heading.East;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord + 1));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }
        
        [Test]
        public void MoveForwardWhenFacingWest()
        {
            const int startXCord = 0;
            const int startYCord = 0;
            const Heading startingHeading = Heading.West;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord - 1));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }


    }
}