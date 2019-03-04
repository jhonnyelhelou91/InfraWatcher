using System;
using System.Collections.Generic;
using System.Text;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the command executed has no message
    /// </summary>
    public class CommandNotSpecifiedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotSpecifiedException"/> class.
        /// </summary>
        public CommandNotSpecifiedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotSpecifiedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandNotSpecifiedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotSpecifiedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public CommandNotSpecifiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
