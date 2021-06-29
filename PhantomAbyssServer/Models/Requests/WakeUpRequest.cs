using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class WakeUpRequest
    {
        [Required]
        public int ClientVersion { get; set; }
    }
}