using InfraWatcher.Core.Services;
using System;
using InfraWatcher.Core.Models.Command;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using System.Management;
using InfraWatcher.Core.Exceptions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InfraWatcher.Windows
{
    public class ManagementScopeConnectionService : IServerConnectionService
    {
        private ManagementScope ManagementScope;

        public ServerConnection ServerConnection { get; private set; }
        public bool IsConnected { get; private set; }
        public DateTime? ConnectionDate { get; private set; }
        public TimeSpan? ConnectionDuration { get; private set; }

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

            ServerConnection = serverConnection;

            var connectionOptions = new ConnectionOptions
            {
                EnablePrivileges = true,
                Username = serverConnection.Credential.Username,
                Password = serverConnection.Credential.Password
            };

            if (serverConnection.Timeout.HasValue)
                connectionOptions.Timeout = serverConnection.Timeout.Value;
            if (!string.IsNullOrEmpty(serverConnection.Credential.Domain))
                connectionOptions.Authority = "ntlmdomain:" + serverConnection.Credential.Domain;

            ManagementScope = new ManagementScope($"\\\\{serverConnection.Host}\\root\\cimv2", connectionOptions);

            try
            {
                var now = DateTime.UtcNow;
                ManagementScope.Connect();

                ConnectionDate = DateTime.UtcNow;
                ConnectionDuration = ConnectionDate.Value.Subtract(now);

                IsConnected = true;
            }
            catch (Exception exception)
            {
                throw new ServerConnectionException(exception.Message);
            }

            return IsConnected;
        }

        public void Disconnect()
        {
            if (ManagementScope != null || ManagementScope.IsConnected)
            {
                ManagementScope = null;
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

            var result = new ServerCommandResult();
            ObjectQuery query;

            try
            {
                try
                {
                    query = new ObjectQuery(serverCommand.Text);
                }
                catch (Exception exception)
                {
                    if (serverCommand.ThrowError)
                        throw new CommandNotFoundException($"Command {serverCommand.Text} is not found", exception);
                    else
                    {
                        result.ExitStatus = -1;
                        result.Error = exception.Message;
                        return result;
                    }
                }

                var commandOptions = new EnumerationOptions();
                if (serverCommand.Timeout.HasValue)
                    commandOptions.Timeout = serverCommand.Timeout.Value;

                var searcher = new ManagementObjectSearcher(ManagementScope, query, commandOptions);

                using (var managementObjectCollection = searcher.Get())
                {
                    var list = new List<Dictionary<string, object>>();

                    foreach (var managementObject in managementObjectCollection)
                    {
                        var dictionary = new Dictionary<string, object>();
                        foreach (var property in managementObject.Properties)
                        {
                            if (dictionary.TryGetValue(property.Name, out object value))
                                dictionary.Add(property.Name, value);
                        }

                        list.Add(dictionary);
                    }

                    result.Result = JsonConvert.SerializeObject(list);

                    return result;
                }
            }
            catch (TimeoutException exception)
            {
                throw new CommandTimeoutException(exception.Message);
            }
            catch (Exception exception)
            {
                if (serverCommand.ThrowError)
                    throw new CommandResultException(
                        $"Command {serverCommand.Text} returned the following error: {exception.Message}");
                else
                {
                    result.ExitStatus = -1;
                    result.Error = exception.Message;
                    return result;
                }
            }
        }
    }
}
