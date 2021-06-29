using System;

namespace PhantomAbyssServer.Models
{
    public class MaintenanceInfo
    {
        public int Mode { get; set; }
        public bool NewGamesLockedOut { get; set; }
        public DateTime MaintenanceTimeUtc { get; set; }
        public int ServerVersion { get; set; }
    }
}