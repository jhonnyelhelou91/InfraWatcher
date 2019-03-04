using System;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the execution result contains an error
    /// </summary>
    public class CommandTimeoutException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTimeoutException"/> class.
        /// </summary>
        public CommandTimeoutException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTimeoutException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandTimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTimeoutException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public CommandTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
