using System;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the execution result contains an error
    /// </summary>
    public class OperatingSystemProviderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemProviderException"/> class.
        /// </summary>
        public OperatingSystemProviderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemProviderException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OperatingSystemProviderException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemProviderException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public OperatingSystemProviderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
