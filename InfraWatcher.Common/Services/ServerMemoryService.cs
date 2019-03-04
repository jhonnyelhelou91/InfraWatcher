using InfraWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using InfraWatcher.Core.Providers;

namespace InfraWatcher.Common.Services
{
    public class ServerMemoryService : IServerMemoryService
    {
        private readonly ICommandProvider commandProvider;

        public ServerMemoryService(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public ulong GetActiveMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetActiveMemory));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetBufferMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetBufferMemory));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetCacheSwap(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetCacheSwap));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetFreeMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetFreeMemory));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetFreeSwap(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetFreeSwap));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetInactiveMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetInactiveMemory));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetTotalMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetTotalMemory));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetTotalSwap(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetTotalSwap));
            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetUsedMemory(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetUsedMemory));
            if (string.IsNullOrEmpty(command.Text))
            {
                return GetTotalMemory(connectionService) - GetFreeMemory(connectionService);
            }

            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }

        public ulong GetUsedSwap(IServerConnectionService connectionService)
        {
            var command = commandProvider.GetCommand(nameof(GetUsedSwap));
            if (string.IsNullOrEmpty(command.Text))
            {
                return GetTotalSwap(connectionService) - GetFreeSwap(connectionService);
            }

            command.ThrowError = true;
            var result = connectionService.ExecuteCommand(command);

            return ulong.Parse(result.Result);
        }
    }
}
