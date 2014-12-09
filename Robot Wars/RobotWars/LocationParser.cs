using System;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using RobotWars.Positioning;

namespace RobotWars
{
    public class LocationParser
    {
        public RobotVector Parse(string startingLocation)
        {
            const string matchPattern = @"(?<xCord>\d+) (?<yCord>\d+) (?<direction>N|E|S|W)";
            var regex = new Regex(matchPattern);
            Match matches = regex.Match(startingLocation);
            string xCord = matches.Groups["xCord"].Value;
            string yCord = matches.Groups["yCord"].Value;
            string direction = matches.Groups["direction"].Value;
            
            return new RobotVector(new Position(int.Parse(xCord), int.Parse(yCord)), ParseDirection(direction));
        }

        private Heading ParseDirection(string direction)
        {
            switch (direction)
            {
                case "N":
                    return Heading.North;
                case "E":
                    return Heading.East;
                case "S":
                    return Heading.South;
                case "W":
                    return Heading.West;
                default:
                    throw new ArgumentException("Not a valid direction");
            }
        }
    }
}