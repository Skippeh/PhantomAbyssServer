using System.ComponentModel.DataAnnotations;

namespace PhantomAbyssServer.Models.Requests
{
    public class PurchaseRouteRequest : SpendCurrencyRequest
    {
        [Required]
        public uint RouteId { get; set; }
    }
}