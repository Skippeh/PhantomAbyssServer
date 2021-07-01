using System;

namespace PhantomAbyssServer.Exceptions
{
    /// <summary>Thrown when a run is submitted with identical run data as another existing run.</summary>
    public class RunDataExistsAlready : Exception
    {
        public RunDataExistsAlready() : base("There is already a run submitted with identical run data")
        {
        }
    }
}