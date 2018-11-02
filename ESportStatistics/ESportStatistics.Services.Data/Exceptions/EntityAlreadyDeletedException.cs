using System;

namespace ESportStatistics.Services.Data.Exceptions
{
    public class EntityAlreadyDeletedException : Exception
    {
        public EntityAlreadyDeletedException()
        {
        }

        public EntityAlreadyDeletedException(string message) : base(message)
        {
        }
    }
}
