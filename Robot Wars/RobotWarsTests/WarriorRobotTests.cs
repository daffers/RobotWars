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
}