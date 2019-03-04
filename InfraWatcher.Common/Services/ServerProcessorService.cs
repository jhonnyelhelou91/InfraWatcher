using InfraWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using InfraWatcher.Core.Providers;
using System.Text.RegularExpressions;

namespace InfraWatcher.Common.Services
{
    public class ServerProcessorService : IServerProcessorService
    {
        private readonly ICommandProvider commandProvider;

        public ServerProcessorService(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public string GetArchitecture(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetArchitecture));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public int GetCoreNumber(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetCoreNumber));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return int.Parse(result.Result);
        }

        public string GetCpuClock(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetCpuClock));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public int GetCpuNumber(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetCpuNumber));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return int.Parse(result.Result);
        }

        public string GetProcessorType(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetProcessorType));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return result.Result;
        }

        public long GetProcessorL1Cache(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetProcessorL1Cache));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return long.Parse(result.Result);
        }

        public long GetProcessorL2Cache(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetProcessorL2Cache));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return long.Parse(result.Result);
        }

        public long GetProcessorL3Cache(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetProcessorL3Cache));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return long.Parse(result.Result);
        }

        public long GetProcessorL4Cache(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetProcessorL4Cache));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return long.Parse(result.Result);
        }
    }
}
