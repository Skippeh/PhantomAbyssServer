using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class VerifyUserIdRequest
    {
        public uint? UserId { get; set; }

        [Required]
        public string SteamId { get; set; }

        [Required]
        public string CurrentUsername { get; set; }

        [Required]
        public List<uint> LastLockedDungeons { get; set; }
    }
}