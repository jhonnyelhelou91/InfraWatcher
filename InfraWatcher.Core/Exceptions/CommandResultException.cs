using System;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the execution result contains an error
    /// </summary>
    public class CommandResultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResultException"/> class.
        /// </summary>
        public CommandResultException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandResultException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public CommandResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
