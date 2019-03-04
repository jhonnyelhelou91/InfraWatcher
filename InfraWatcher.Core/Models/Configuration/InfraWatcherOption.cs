namespace InfraWatcher.Core.Models.Configuration
{
    public class InfraWatcherOption
    {
        public ServerTTLOption TTLOption { get; set; }
        public CommandOption[] Commands { get; set; }
    }
}
