using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhantomAbyssServer.Database.Models
{
    [Index(nameof(DataHash), IsUnique = true)]
    [Index(nameof(DungeonId), nameof(RouteId), nameof(DungeonFloorNumber))]
    public class SavedRun
    {
        [Key]
        public uint Id { get; set; }
        
        public uint UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
        public uint DungeonId { get; set; }
        
        [ForeignKey(nameof(DungeonId))]
        public Dungeon Dungeon { get; set; }
        public uint RouteId { get; set; }
        
        [ForeignKey(nameof(RouteId))]
        public Route Route { get; set; }
        public uint DungeonFloorNumber { get; set; }
        
        public string DataHash { get; set; }

        public bool RunSuccessful { get; set; }
        
        public int ServerVersion { get; set; }
        
        public DateTime RunDateTime { get; set; }
    }
}