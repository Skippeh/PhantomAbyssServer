using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.Models.Responses
{
    public class SpendCurrencyResponse
    {
        public uint UserId { get; set; }
        public UserCurrency Currency { get; set; }
        public string PurchaseItemId { get; set; }
        public BuyResult Result { get; set; }
    }
}