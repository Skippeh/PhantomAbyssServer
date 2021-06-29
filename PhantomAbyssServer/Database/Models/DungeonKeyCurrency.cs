using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PhantomAbyssServer.Database.Models
{
    public class DungeonKeyCurrency
    {
        [JsonIgnore]
        public uint Id { get; set; }
        
        [JsonIgnore]
        public uint UserCurrencyId { get; set; }
        
        [JsonIgnore]
        [ForeignKey(nameof(UserCurrencyId))]
        public UserCurrency UserCurrency { get; set; }
        
        public uint Stage { get; set; }
        public uint NumKeys { get; set; }
    }
}