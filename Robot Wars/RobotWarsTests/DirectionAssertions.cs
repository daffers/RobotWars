using NUnit.Framework;
using RobotWars.Positioning;

namespace RobotWarsTests
{
    public static class DirectionAssertions
    {
        public static void AssertHasTurnedToFaceNorth(int startXCord, int startYCord, RobotVector result)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(Heading.North));
        }

        public static void AssertHasTurnedToFaceWest(int startXCord, int startYCord, RobotVector result)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(Heading.West));
        }

        public static void AssertHasTurnedToFaceEast(int startXCord, int startYCord, RobotVector result)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(Heading.East));
        }

        public static void AssertHasTurnedToFaceSouth(int startXCord, int startYCord, RobotVector result)
        {
            Assert.That(result.Position.YCord, Is.EqualTo(startYCord));
            Assert.That(result.Position.XCord, Is.EqualTo(startXCord));
            Assert.That(result.Heading, Is.EqualTo(Heading.South));
        }
    }
}