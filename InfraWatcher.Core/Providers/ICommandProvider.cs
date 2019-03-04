using InfraWatcher.Core.Models.Command;
using InfraWatcher.Core.Models.Configuration;
using System;

namespace InfraWatcher.Core.Providers
{
    /// <summary>
    /// Interface responsible of retreiving the list of commands or a single command by name
    /// </summary>
    public interface ICommandProvider
    {
        /// <summary>
        /// Get all commands
        /// </summary>
        /// <returns>All commands</returns>
        CommandOption[] GetCommands();

        /// <summary>
        /// Get first command with specific name
        /// </summary>
        /// <param name="name">Command name</param>
        /// <returns></returns>
        ServerCommand GetCommand(string name);
    }
}
