using System;

namespace PhantomAbyssServer.Exceptions
{
    public class SaveFailedException : Exception
    {
        public SaveFailedException() : this(null)
        {
        }

        public SaveFailedException(string message) : base(message)
        {
        }
    }
}