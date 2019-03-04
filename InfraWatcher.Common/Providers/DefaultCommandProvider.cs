using InfraWatcher.Core.Models.Command;
using Microsoft.Extensions.Options;
using System.Linq;
using InfraWatcher.Core.Models.Configuration;
using InfraWatcher.Core.Providers;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace InfraWatcher.Common.Providers
{
    public class DefaultCommandProvider : ICommandProvider
    {
        private readonly InfraWatcherOption infraWatcherOptions;

        public DefaultCommandProvider(IOptions<InfraWatcherOption> infraWatcherOptions)
        {
            this.infraWatcherOptions = infraWatcherOptions.Value;
        }

        public ServerCommand GetCommand(string name)
        {
            return this.infraWatcherOptions
                .Commands
                .FirstOrDefault(command => command.Name == name)
                .Command;
        }

        public CommandOption[] GetCommands()
        {
            return this.infraWatcherOptions.Commands;
        }
    }
}
