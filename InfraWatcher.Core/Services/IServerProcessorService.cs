using InfraWatcher.Core.Models.Connection;

namespace InfraWatcher.Core.Services
{
    public interface IServerProcessorService
    {
        /// <summary>
        /// Get the architecture of the OS
        /// </summary>
        /// <returns>The architecture of the processor</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        string GetArchitecture(IServerConnectionService connectionService);

        /// <summary>
        /// Get the number of cpus in the system
        /// </summary>
        /// <returns>The cpu number</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        int GetCpuNumber(IServerConnectionService connectionService);

        /// <summary>
        /// Get the number of cores in the system
        /// </summary>
        /// <returns>The core number</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        int GetCoreNumber(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the processor type of the server
        /// </summary>
        /// <returns>the processor type of the server</returns>
        string GetProcessorType(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the Central Processing Unit clock of the server
        /// </summary>
        /// <returns>the Central Processing Unit clock of the server</returns>
        string GetCpuClock(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the L1 processor cache of the server
        /// </summary>
        long GetProcessorL1Cache(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the L2 processor cache of the server
        /// </summary>
        long GetProcessorL2Cache(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the L3 processor cache of the server
        /// </summary>
        long GetProcessorL3Cache(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the L4 processor cache of the server
        /// </summary>
        long GetProcessorL4Cache(IServerConnectionService connectionService);
    }
}
