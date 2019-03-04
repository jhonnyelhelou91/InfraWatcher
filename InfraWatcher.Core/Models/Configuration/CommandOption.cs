using InfraWatcher.Core.Models.Command;

namespace InfraWatcher.Core.Models.Configuration
{
    public class CommandOption
    {
        public string Name { get; set; }
        public ServerCommand Command { get; set; }
    }
}
