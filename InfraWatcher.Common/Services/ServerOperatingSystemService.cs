using InfraWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using InfraWatcher.Core.Providers;

namespace InfraWatcher.Common.Services
{
    public class ServerOperatingSystemService : IServerOperatingSystemService
    {
        private readonly ICommandProvider commandProvider;

        public ServerOperatingSystemService(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public string GetKernelVersion(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetKernelVersion));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public string GetOperatingSystemName(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetOperatingSystemName));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public string GetOperatingSystemRelease(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetOperatingSystemRelease));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public string GetOperatingSystemUpdate(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetOperatingSystemUpdate));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public string GetOperatingSystemVersion(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetOperatingSystemVersion));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }
    }
}
