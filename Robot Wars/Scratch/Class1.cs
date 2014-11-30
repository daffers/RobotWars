using System.Collections.Generic;
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
}
