using System;

namespace InfraWatcher.Core.Models.Command
{
    public class ServerCommand
    {
        public string Text { get; set; }
        public TimeSpan? Timeout { get; set; }
        public bool ThrowError { get; set; }
    }
}
