using InfraWatcher.Core.Models.Command;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraWatcher.Core.Services
{
    public interface IServerConnectionService
    {
        /// <summary>
        /// Server connection details
        /// </summary>
        ServerConnection ServerConnection { get; }

        /// <summary>
        /// Flag to check if the connection has been established
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Date when the connection was established
        /// </summary>
        DateTime? ConnectionDate { get; }

        /// <summary>
        /// Duration to establish the connection
        /// </summary>
        TimeSpan? ConnectionDuration { get; }

        ///<summary>
        ///Connects to a server
        ///</summary>
        ///<param name="serverConnection">The server credentials when the connection is to be made</param>
        /// <exception cref="ArgumentNullException">Is trown if serverCredential == null or serverCredential.Host == null or serverCredential.Credential.Username == null</exception>
        bool Connect(ServerConnection serverConnection);

        /// <summary>
        /// Executes a command on the server 
        /// </summary>
        /// <param name="serverCommand">The command to be executed.</param>
        /// <returns>The command result or the error</returns>
        /// <exception cref="ArgumentException">serverCommand.Command property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.Command"/> is null.</exception>
        ServerCommandResult ExecuteCommand(ServerCommand serverCommand);

        ///<summary>
        ///Disconnects from the connected server
        ///</summary>
        void Disconnect();
    }
}
