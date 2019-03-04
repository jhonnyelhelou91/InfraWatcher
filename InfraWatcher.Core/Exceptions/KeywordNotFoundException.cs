using System;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the keyword is not found
    /// </summary>
    public class KeywordNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordNotFoundException"/> class.
        /// </summary>
        public KeywordNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public KeywordNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public KeywordNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
