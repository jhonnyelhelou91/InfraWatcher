using InfraWatcher.Core.Models.Connection;

namespace InfraWatcher.Core.Providers
{
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
