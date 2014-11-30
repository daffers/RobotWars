using NUnit.Framework;
using RobotWars;

namespace RobotWarsTests
{
    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void CanSupplyDimensionsToAnArena()
        {
            new Arena(1, 1);
        }

        [TestCase(0,1)]
        [TestCase(1,0)]
        [TestCase(0,0)]
        public void CannotCreateArenaWithOneZeroDimension(int maxX, int maxY)
        {
            Assert.Throws<Arena.CannotCreateArenaWithZeroDimensionException>(() => new Arena(maxX, maxY));
        }

        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        public void CannotCreateArenaWithNegativeDimension(int maxX, int maxY)
        {
            Assert.Throws<Arena.CannotCreateArenaWithNegativeDimensionException>(() => new Arena(maxX, maxY));
        }
    }
}