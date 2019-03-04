using InfraWatcher.Core.Models.Connection;

namespace InfraWatcher.Core.Providers
{
    /// <summary>
    /// Interface responsible of detecting the operating system of the server connection
    /// </summary>
    public interface IOperatingSystemProvider
    {
        /// <summary>
        /// Detect Operating System
        /// </summary>
        /// <param name="serverConnection"></param>
        /// <returns></returns>
        ServerOperatingSystem Detect(ServerConnection serverConnection);
    }
}
