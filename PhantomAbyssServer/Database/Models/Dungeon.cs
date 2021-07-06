using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PhantomAbyssServer.Database.Models
{
    public class Dungeon
    {
        public uint Id { get; set; }
        
        public uint? CompletedById { get; set; }
        
        [ForeignKey(nameof(CompletedById))]
        [JsonIgnore]
        public User CompletedBy { get; set; }
        
        public uint NumFloors { get; set; }
        
        public uint RouteStage { get; set; }
        
        public string RelicId { get; set; }
        
        public int Seed { get; set; }
        
        [JsonIgnore]
        public List<SavedRun> SavedRuns { get; set; }
        
        public uint RouteId { get; set; }
        
        [ForeignKey(nameof(RouteId))]
        [JsonIgnore]
        public Route Route { get; set; }
        
        #region Json properties, only used for serializing purposes
        
        [JsonProperty]
        [NotMapped]
        private uint NumGhosts => (uint) (SavedRuns?.Count ?? 0);

        [JsonProperty]
        [NotMapped]
        private string CompletedByName => CompletedBy?.Name;

        #endregion
    }
}