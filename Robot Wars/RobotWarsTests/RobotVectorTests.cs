using NUnit.Framework;
using RobotWars.Positioning;

namespace RobotWarsTests
{
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

        [Test]
        public void EqualTest()
        {
            var robotVector1 = new RobotVector(ZeroZero(), Heading.North);
            var robotVector2 = new RobotVector(ZeroZero(), Heading.North);

            Assert.IsTrue(robotVector1 == robotVector2);
        }

        [Test]
        public void NotSamePositionTest()
        {
            var robotVector1 = new RobotVector(ZeroZero(), Heading.North);
            var robotVector2 = new RobotVector(ZeroZero(), Heading.South);

            Assert.IsTrue(robotVector1 != robotVector2);
        }

        [Test]
        public void NotSameHeadingTests()
        {
            var robotVector1 = new RobotVector(ZeroZero(), Heading.North);
            var robotVector2 = new RobotVector(ZeroZero(), Heading.South);

            Assert.IsTrue(robotVector1 != robotVector2);
        }

        [TestCase(Heading.North, "0 0 N")]
        [TestCase(Heading.South, "0 0 S")]
        [TestCase(Heading.East, "0 0 E")]
        [TestCase(Heading.West, "0 0 W")]
        public void ToStringFormatsDirectionCorrectly(Heading heading, string expected)
        {
            var vector = new RobotVector(ZeroZero(), heading);
            Assert.That(vector.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void FormatsPositionCorrectly()
        {
            var vector = new RobotVector(new Position(1, 1), Heading.North);
            Assert.That(vector.ToString(), Is.EqualTo("1 1 N"));
        }

        private Position ZeroZero()
        {
            return new Position(0,0);
        }
    }
}