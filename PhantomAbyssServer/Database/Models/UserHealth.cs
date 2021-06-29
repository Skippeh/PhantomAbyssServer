using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhantomAbyssServer.Database.Models
{
    public class UserHealth
    {
        [JsonIgnore]
        [Key]
        public uint Id { get; set; }
        
        [JsonIgnore]
        public uint UserId { get; set; }
        
        [JsonIgnore]
        public User User { get; set; }
        
        public uint BaseHealth { get; set; }
        public uint BonusHealth { get; set; }
        public uint MaxBonusHealth { get; set; }
    }
}