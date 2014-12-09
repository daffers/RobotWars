using NUnit.Framework;
using RobotWars.Positioning;

namespace RobotWarsTests
{
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

        [TestCase(0, 0, "0 0")]
        [TestCase(1, 0, "1 0")]
        [TestCase(0, 1, "0 1")]
        public void PositionFormatsCorrectly(int xCord, int yCord, string expected)
        {
            var position = new Position(xCord, yCord);
            Assert.That(position.ToString(), Is.EqualTo(expected));
        }
    }
}