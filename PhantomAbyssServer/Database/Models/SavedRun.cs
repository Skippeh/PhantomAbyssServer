using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhantomAbyssServer.Database.Models
{
    [Index(nameof(DataHash), IsUnique = true)]
    public class SavedRun
    {
        [Key]
        public uint Id { get; set; }
        
        public uint UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
        public uint DungeonId { get; set; }
        public uint RouteId { get; set; }
        public uint DungeonFloorNumber { get; set; }
        
        [Required]
        public string DataHash { get; set; }

        public bool RunSuccessful { get; set; }
    }
}