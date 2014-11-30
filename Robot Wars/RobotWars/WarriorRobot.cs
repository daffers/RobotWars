using System;
using System.Collections.Generic;
using RobotWars.Commands;
using RobotWars.Positioning;

namespace RobotWars
{
    public class WarriorRobot
    {
        private Arena _arena;
        private bool _startLocationSet;
        private RobotVector _currentPosition;
        
        public void UploadArena(Arena arena)
        {
            _arena = arena;
        }
        
        public void SetCommandList(List<RobotCommand> commandList)
        {
            if (!_startLocationSet)
                throw new StartLocationNotSetException();
        }

        public class ArenaNotUploadedException: Exception
        {
        }

        public class StartLocationNotSetException : Exception
        {
        }

        public void StartAt(RobotVector staringPosition)
        {
            if (_arena == null)
                throw new ArenaNotUploadedException();

            _currentPosition = staringPosition;
            _startLocationSet = true;
        }

        public RobotVector ReportPosition()
        {
            return _currentPosition;
        }
    }
}