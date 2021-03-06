﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InfraWatcher.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when the command executed is not recognized
    /// </summary>
    public class CommandNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        public CommandNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public CommandNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
