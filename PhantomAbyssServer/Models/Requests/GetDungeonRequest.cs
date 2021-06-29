using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class GetDungeonRequest
    {
        [Required]
        public uint UserId { get; set; }

        [Required]
        public uint DungeonSettingsIndex { get; set; }

        [Required]
        public uint ClientDungeonVersion { get; set; }

        [Required]
        public uint DungeonId { get; set; }

        [Required]
        public uint RouteId { get; set; }

        [Required]
        public uint RouteStage { get; set; }

        [Required]
        public uint DungeonFloorNumber { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string ShareCode { get; set; }

        [Required]
        public List<uint> KnownDungeons { get; set; }

        [Required]
        public string WhipWagered { get; set; }
    }
}