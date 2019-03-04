using System;

namespace InfraWatcher.Core.Models.Command
{
    public class ServerCommandResult
    {
        public string Result { get; set; }
        public string Error { get; set; }
        public int ExitStatus { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }
}
