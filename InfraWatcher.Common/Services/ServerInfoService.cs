using InfraWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using InfraWatcher.Core.Providers;

namespace InfraWatcher.Common.Services
{
    public class ServerInfoService : IServerInfoService
    {
        private readonly ICommandProvider commandProvider;

        public ServerInfoService(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public IPAddress GetIPAddress(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetIPAddress));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return IPAddress.Parse(result.Result);
        }

        public bool GetIsVirtual(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetIsVirtual));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return !string.IsNullOrEmpty(result.Result);
        }

        public string GetMachineModel(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetMachineModel));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public string GetSerialNumber(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetSerialNumber));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }
    }
}
