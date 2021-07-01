using System;

namespace PhantomAbyssServer.Exceptions
{
    public class SaveFailedException : Exception
    {
        public SaveFailedException() : this(null, null)
        {
        }

        public SaveFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DataExistsAlready : Exception
    {
        public DataExistsAlready(string message) : base(message)
        {
        }
    }
}