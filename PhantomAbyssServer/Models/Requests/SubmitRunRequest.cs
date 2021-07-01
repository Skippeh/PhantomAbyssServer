using System.Collections.Generic;
using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.Models.Requests
{
    public class SubmitRunRequest
    {
        public UserCurrency Currency { get; set; }
        public uint DungeonFloorNumber { get; set; }
        public uint DungeonId { get; set; }
        public uint RouteId { get; set; }
        public List<uint> LastLockedDungeonList { get; set; }
        public string PermanentSettingsData { get; set; }
        public uint PlayerUniqueId { get; set; }
        public string RunData { get; set; }
        public bool Success { get; set; }
    }
}