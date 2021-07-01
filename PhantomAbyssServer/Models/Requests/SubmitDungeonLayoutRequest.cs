using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class SubmitDungeonLayoutRequest
    {
        public uint DungeonId { get; set; }
        public uint DungeonFloorNumber { get; set; }
        public uint DungeonVersion { get; set; }
        
        public uint DungeonFloorCount { get; set; }
        public uint DungeonFloorType { get; set; }
        public uint DungeonLayoutType { get; set; }
        
        [Required]
        public string PermanentSettingsData { get; set; }
        
        [Required]
        public string RelicId { get; set; }
        
        [Required]
        public string SavedLayoutData { get; set; }
    }
}