using System;

namespace RobotWars
{
    public class Arena
    {
        public Arena(int maxX, int maxY)
        {
            if (maxX == 0 || maxY == 0)
                throw new CannotCreateArenaWithZeroDimensionException();

            if (maxX < 0 || maxY < 0)
                throw new CannotCreateArenaWithNegativeDimensionException();
        }

        public class CannotCreateArenaWithZeroDimensionException : Exception
        {
        }

        public class CannotCreateArenaWithNegativeDimensionException : Exception
        {
        }
    }
}