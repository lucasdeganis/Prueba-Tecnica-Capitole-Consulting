using System;

namespace GtMotive.Estimate.Microservice.Domain.Infrastructure
{
    /// <summary>
    /// indicates errors that occur during database operations.
    /// This exception is typically thrown when there are issues with database connectivity, query execution, or data integrity.
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        public DatabaseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DatabaseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
