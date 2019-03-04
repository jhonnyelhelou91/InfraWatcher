using System;
using System.Collections.Generic;
using System.Text;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the command executed is not recognized
    /// </summary>
    public class ServerConnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionException"/> class.
        /// </summary>
        public ServerConnectionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServerConnectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public ServerConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
