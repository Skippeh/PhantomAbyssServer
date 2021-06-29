using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models
{
    public class KeyCurrency
    {
        [Required]
        public uint KeyIndex { get; set; }

        [Required]
        public uint Amount { get; set; }
    }
}