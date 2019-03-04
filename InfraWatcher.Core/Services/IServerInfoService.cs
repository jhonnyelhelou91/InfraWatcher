using InfraWatcher.Core.Models.Connection;
using System.Net;

namespace InfraWatcher.Core.Services
{
    public interface IServerInfoService
    {
        /// <summary>
        /// Returns the IP address of the server
        /// </summary>
        /// <returns>the IP address of the server</returns>
        IPAddress GetIPAddress(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the machine model of the server
        /// </summary>
        /// <returns>the machine model of the server</returns>
        string GetMachineModel(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the serial number
        /// </summary>
        /// <returns>The serial number of the machine</returns>
        string GetSerialNumber(IServerConnectionService connectionService);

        /// <summary>
        /// Returns Whether the server is virtual or not
        /// </summary>
        /// <returns>true if the server is virtual and false otherwise</returns>
        bool GetIsVirtual(IServerConnectionService connectionService);
    }
}
