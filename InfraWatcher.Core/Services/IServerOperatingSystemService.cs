using InfraWatcher.Core.Models.Connection;
using System;
using System.Text;

namespace InfraWatcher.Core.Services
{
    public interface IServerOperatingSystemService
    {
        /// <summary>
        /// Get the name of the operating system
        /// </summary>
        /// <returns>OS Name</returns>
        /// <exception cref="ArgumentException">serverCommand.Command property is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serverCommand.Command"/> is null.</exception>
        string GetOperatingSystemName(IServerConnectionService connectionService);

        /// <summary>
        /// Returns The version of the kernel
        /// </summary>
        /// <returns>The kernel version of the server</returns>
        string GetKernelVersion(IServerConnectionService connectionService);

        /// <summary>
        /// Returns The version of the Operating System running on the server
        /// </summary>
        /// <returns>The OS version of the server</returns>
        string GetOperatingSystemVersion(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the operating system update of the server
        /// </summary>
        /// <returns>the operating system update of the server</returns>
        string GetOperatingSystemUpdate(IServerConnectionService connectionService);

        /// <summary>
        /// Returns the Operating System release of the server
        /// </summary>
        /// <returns>the Operating System release of the server</returns>
        string GetOperatingSystemRelease(IServerConnectionService connectionService);
    }
}
