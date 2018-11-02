using System;

namespace ESportStatistics.Services.Data.Exceptions
{
    public class EntityAlreadyActiveException : Exception
    {
        public EntityAlreadyActiveException()
        {
        }

        public EntityAlreadyActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
