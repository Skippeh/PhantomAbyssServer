using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace PhantomAbyssServer.Database.Models
{
    [Index(nameof(SteamId), IsUnique = true)]
    [Index(nameof(SharerId), IsUnique = true)]
    public class User
    {
        [Key]
        public uint Id { get; set; }
        
        public string Name { get; set; }
        public string SteamId { get; set; }
        public string SharerId { get; set; }
        
        public uint CurrencyId { get; set; }
        
        [ForeignKey(nameof(CurrencyId))]
        public UserCurrency Currency { get; set; }
        
        public uint HealthId { get; set; }
        
        [ForeignKey(nameof(HealthId))]
        public UserHealth Health { get; set; }
        
        // todo: create model for routes and dungeons
        [NotMapped]
        public List<object> VictoryRoutes { get; set; }
    }
}