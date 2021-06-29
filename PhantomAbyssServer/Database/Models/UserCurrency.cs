using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PhantomAbyssServer.Database.Models
{
    public class UserCurrency
    {
        [JsonIgnore]
        [Key]
        public uint Id { get; set; }
        
        [JsonIgnore]
        public uint UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public uint Essence { get; set; }

        public List<DungeonKeyCurrency> DungeonKeys { get; set; }
    }
}