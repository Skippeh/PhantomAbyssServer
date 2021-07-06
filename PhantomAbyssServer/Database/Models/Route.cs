using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PhantomAbyssServer.Database.Models
{
    public class Route
    {
        [Key]
        public uint Id { get; set; }
        
        public uint? CompletedById { get; set; }

        [ForeignKey(nameof(CompletedById))]
        public User CompletedBy { get; set; }

        public uint RouteAttemptCount { get; set; }
        
        public List<Dungeon> Dungeons { get; set; }
        
        /// <summary>Gets or sets the current user running this route.</summary>
        public User CurrentUser { get; set; }
    }
}