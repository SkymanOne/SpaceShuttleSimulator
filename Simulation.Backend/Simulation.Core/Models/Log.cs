using System;

namespace Simulation.Core.Models
{
    public class Log
    {
        public enum Level
        {
            Message,
            Warning,
            Error
        }
        public string Message { get; set; }
        public Level ContentLevel { get; set; }

        public Log(Level level, string message)
        {
            ContentLevel = level;
            Message = message;
        }
    }
}