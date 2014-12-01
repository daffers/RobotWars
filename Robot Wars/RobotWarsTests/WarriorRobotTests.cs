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
    public class ForwardMovementTests
    {
        [TestCase(0,0)]
        [TestCase(1,1)]
        [TestCase(5,6)]
        public void MoveForwardWhenFacingNorth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.North;

            var command = new MoveForwards();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord,startYCord), startingHeading));

            AssertHasOnlyMovedNorth(startXCord, startYCord, result, startingHeading);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 2)]
        public void MoveForwardWhenFacingSouth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.South;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            AssertHasOnlyMovedSouth(startXCord, startYCord, result, startingHeading);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(7, 3)]
        public void MoveForwardWhenFacingEast(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.East;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            AssertHasOnlyMovedEast(startXCord, startYCord, result, startingHeading);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 9)]
        public void MoveForwardWhenFacingWest(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.West;

            var command = new MoveForwards();

            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            AssertHasOnlyMovedWest(startXCord, startYCord, result, startingHeading);
        }

        private static void AssertHasOnlyMovedNorth(int startXCord, int startYCord, RobotVector result, Heading startingHeading)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord + 1));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }

        private static void AssertHasOnlyMovedSouth(int startXCord, int startYCord, RobotVector result, Heading startingHeading)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord - 1));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }

        private static void AssertHasOnlyMovedEast(int startXCord, int startYCord, RobotVector result, Heading startingHeading)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord + 1));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }

        private static void AssertHasOnlyMovedWest(int startXCord, int startYCord, RobotVector result, Heading startingHeading)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord - 1));
            Assert.That(result.Heading, Is.EqualTo(startingHeading));
        }
    }

    [TestFixture]
    public class TurnRightTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnRightWhenFacingNorth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.North;

            var command = new TurnRight();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceEast(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnRightWhenFacingEast(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.East;

            var command = new TurnRight();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceSouth(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnRightWhenFacingSouth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.South;

            var command = new TurnRight();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceWest(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnRightWhenFacingWest(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.West;

            var command = new TurnRight();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceNorth(startXCord, startYCord, result);
        }
    }

    [TestFixture]
    public class TurnLeftTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnLeftWhenFacingNorth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.North;

            var command = new TurnLeft();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceWest(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnLeftWhenFacingEast(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.East;

            var command = new TurnLeft();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceNorth(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnLeftWhenFacingSouth(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.South;

            var command = new TurnLeft();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceEast(startXCord, startYCord, result);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 6)]
        public void TurnLeftWhenFacingWest(int startXCord, int startYCord)
        {
            const Heading startingHeading = Heading.West;

            var command = new TurnLeft();
            RobotVector result = command.GenerateNewVector(new RobotVector(new Position(startXCord, startYCord), startingHeading));

            DirectionAssertions.AssertHasTurnedToFaceSouth(startXCord, startYCord, result);
        }
    }
}