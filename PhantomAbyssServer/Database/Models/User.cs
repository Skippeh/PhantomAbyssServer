using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PhantomAbyssServer.Database.Models
{
    [Index(nameof(SteamId), IsUnique = true)]
    [Index(nameof(SharerId), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("userId")]
        public uint Id { get; set; }
        
        [JsonProperty("currentUsername")]
        public string Name { get; set; }
        public string SteamId { get; set; }
        public string SharerId { get; set; }
        
        [JsonIgnore]
        public uint CurrencyId { get; set; }
        
        [ForeignKey(nameof(CurrencyId))]
        public UserCurrency Currency { get; set; }
        
        public uint HealthId { get; set; }
        
        [ForeignKey(nameof(HealthId))]
        public UserHealth Health { get; set; }
        
        [JsonIgnore]
        public uint? CurrentRouteId { get; set; }
        
        [JsonIgnore]
        [ForeignKey(nameof(CurrentRouteId))]
        public Route CurrentRoute { get; set; }
        
        // todo: create model for routes and dungeons
        [NotMapped]
        public List<object> VictoryRoutes { get; set; } = new();

        [NotMapped]
        public List<object> LockedCurrencyAndCompletedRoutes = new();
    }
}