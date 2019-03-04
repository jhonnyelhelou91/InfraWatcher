using InfraWatcher.Core.Models.Command;
using Microsoft.Extensions.Options;
using System.Linq;
using InfraWatcher.Core.Models.Configuration;
using InfraWatcher.Core.Providers;
using System.IO;

namespace InfraWatcher.Common.Providers
{
    public class ScriptCommandProvider : ICommandProvider
    {
        private readonly InfraWatcherOption infraWatcherOptions;

        public ScriptCommandProvider(IOptions<InfraWatcherOption> infraWatcherOptions)
        {
            this.infraWatcherOptions = infraWatcherOptions.Value;
        }

        public ServerCommand GetCommand(string name)
        {
            var serverCommand = this.infraWatcherOptions
                .Commands
                .FirstOrDefault(command => command.Name == name)
                .Command;

            serverCommand.Text = File.ReadAllText(serverCommand.Text);

            return serverCommand;
        }

        public CommandOption[] GetCommands()
        {
            var serverCommands = this.infraWatcherOptions
                .Commands;

            foreach (var serverCommand in serverCommands)
                serverCommand.Command.Text = File.ReadAllText(serverCommand.Command.Text);

            return serverCommands;
        }
    }
}
