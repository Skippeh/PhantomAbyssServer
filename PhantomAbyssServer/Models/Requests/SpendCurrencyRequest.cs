using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class SpendCurrencyRequest
    {
        [Required]
        public uint UserId { get; set; }

        [Required]
        public string PurchaseItemId { get; set; }

        [Required]
        public uint Essences { get; set; }

        [Required]
        public List<KeyCurrency> Keys { get; set; }
    }
}