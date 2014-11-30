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

        private Position ZeroZero()
        {
            return new Position(0,0);
        }
    }
}