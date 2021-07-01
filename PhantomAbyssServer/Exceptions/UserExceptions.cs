using System;

namespace PhantomAbyssServer.Exceptions
{
    /// <summary>Thrown when a new user account has a steam id from an existing account.</summary>
    public class UserExistsAlreadyException : DataExistsAlready
    {
        public UserExistsAlreadyException() : base("There is already a user with the same steam id")
        {
        }
    }
}