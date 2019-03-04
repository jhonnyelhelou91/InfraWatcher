using InfraWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using InfraWatcher.Core.Models.Command;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using Renci.SshNet;
using InfraWatcher.Core.Exceptions;
using Renci.SshNet.Common;

namespace InfraWatcher.Ssh
{
    public class SshConnectionService : IServerConnectionService, IDisposable
    {
        private SshClient Client;

        public ServerConnection ServerConnection { get; private set; }
        public DateTime? ConnectionDate { get; private set; }
        public TimeSpan? ConnectionDuration { get; private set; }

        public bool IsConnected
        {
            get
            {
                return Client?.IsConnected ?? false;
            }
        }

        public bool Connect(ServerConnection serverConnection)
        {
            if (serverConnection == null)
                throw new ArgumentNullException(nameof(serverConnection));

            if (serverConnection.Credential == null)
                throw new ArgumentNullException(nameof(serverConnection.Credential));

            if (string.IsNullOrEmpty(serverConnection.Host))
                throw new ArgumentNullException(nameof(serverConnection.Host));

            if (string.IsNullOrEmpty(serverConnection.Credential.Username))
                throw new ArgumentNullException(nameof(serverConnection.Credential.Username));

            //SSH default port is 22
            serverConnection.Port = serverConnection.Port ?? 22;
            ServerConnection = serverConnection;

            Client = new SshClient(serverConnection.Host, serverConnection.Port.Value,
                serverConnection.Credential.Username, serverConnection.Credential.Password);
            try
            {
                var now = DateTime.UtcNow;
                Client.Connect();

                ConnectionDate = DateTime.UtcNow;
                ConnectionDuration = ConnectionDate.Value.Subtract(now);
            }
            catch (Exception exception)
            {
                throw new ServerConnectionException(exception.Message);
            }

            return IsConnected;
        }

        public void Disconnect()
        {
            if (Client != null && Client.IsConnected)
            {
                Client.Disconnect();
                Client.Dispose();
                Client = null;
            }
        }

        public ServerCommandResult ExecuteCommand(ServerCommand serverCommand)
        {
            if (!IsConnected)
            {
                throw new ServerConnectionException("Server connection is not established");
            }
            if (string.IsNullOrEmpty(serverCommand.Text))
                throw new CommandNotSpecifiedException($"Command was not defined");

            using (var command = Client.CreateCommand(serverCommand.Text))
            {
                if (serverCommand.Timeout.HasValue)
                    command.CommandTimeout = serverCommand.Timeout.Value;

                try
                {
                    command.Execute();
                }
                catch (SshOperationTimeoutException exception)
                {
                    throw new CommandTimeoutException(exception.Message);
                }
                catch (SshConnectionException exception)
                {
                    throw new ServerConnectionException(exception.Message);
                }

                var result = new ServerCommandResult
                {
                    Result = command.Result,
                    Error = command.Error,
                    ExitStatus = command.ExitStatus
                };

                if (serverCommand.ThrowError && !string.IsNullOrEmpty(command.Error))
                {
                    if (result.Error.Contains("command not found"))
                        throw new CommandNotFoundException($"Command {command.CommandText} is not found");

                    throw new CommandResultException($"Command {command.CommandText} returned the following error: {command.Error}");
                }

                return result;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
