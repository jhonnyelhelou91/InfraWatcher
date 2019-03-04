using InfraWatcher.Core.Models.Connection;

namespace InfraWatcher.Core.Services
{
    public interface IServerMemoryService
    {
        /// <summary>
        /// Returns the amount of cache swap memory in kilobytes
        /// </summary>
        /// <returns>Swap memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetCacheSwap(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of used swap memory in kilobytes
        /// </summary>
        /// <returns>Swap memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetUsedSwap(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of free swap memory in kilobytes
        /// </summary>
        /// <returns>Swap memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetFreeSwap(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of swap memory in kilobytes
        /// </summary>
        /// <returns>Swap memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetTotalSwap(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of total memory in kilobytes
        /// </summary>
        /// <returns>Total memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetTotalMemory(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of used memory in kilobytes
        /// </summary>
        /// <returns>Used memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetUsedMemory(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of active memory in kilobytes
        /// </summary>
        /// <returns>Active memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetActiveMemory(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of inactive memory in kilobytes
        /// </summary>
        /// <returns>Inactive memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetInactiveMemory(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of free memory in kilobytes
        /// </summary>
        /// <returns>Free memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetFreeMemory(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the amount of buffer memory in kilobytes
        /// </summary>
        /// <returns>Buffer memory in kilobytes</returns>
        /// <exception cref="ArgumentException">serverCommand.CommandText property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.CommandText"/> is null.</exception>
        ulong GetBufferMemory(IServerConnectionService connectionService);
    }
}
