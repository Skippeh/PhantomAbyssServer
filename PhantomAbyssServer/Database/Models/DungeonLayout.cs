using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PhantomAbyssServer.Database.Models
{
    [Index(
        nameof(DungeonId),
        nameof(DungeonVersion),
        nameof(DungeonFloorNumber),
        IsUnique = true
    )]
    [Index(nameof(LayoutDataHash), IsUnique = true)]
    public class DungeonLayout
    {
        [Key]
        public uint Id { get; set; }

        public uint DungeonId { get; set; }
        public uint DungeonVersion { get; set; }
        public uint DungeonFloorNumber { get; set; }
        
        public uint DungeonFloorCount { get; set; }
        public uint DungeonFloorType { get; set; }
        public uint DungeonLayoutType { get; set; }

        [Required]
        public string PermanentSettingsData { get; set; }

        [Required]
        public string RelicId { get; set; }

        [Required]
        public string LayoutDataHash { get; set; }
    }
}