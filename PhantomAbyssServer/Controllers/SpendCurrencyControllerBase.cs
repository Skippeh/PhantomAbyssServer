using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Models;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Models.Responses;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    public abstract class SpendCurrencyControllerBase : ControllerBase
    {
        private readonly UserService userService;
        private readonly IConfiguration cheatConfig;

        public SpendCurrencyControllerBase(UserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            cheatConfig = configuration.GetSection("Cheats");
        }

        protected async Task<User> GetUser(SpendCurrencyRequest request)
        {
            return await userService.GetUserFromId(request.UserId);
        }

        protected async Task<BuyResult> SpendCurrencies(User user, ICollection<KeyCurrency> keys)
        {
            if (cheatConfig.GetValue<bool>("FreePurchases", defaultValue: false))
                return BuyResult.Success;
            
            // Check if user has enough currency and subtract it
            foreach (KeyCurrency currency in keys)
            {
                var dbCurrency = user.Currency.DungeonKeys.FirstOrDefault(c => c.Stage == currency.KeyIndex);

                if (dbCurrency == null)
                    return BuyResult.NotEnoughCurrency;

                if (dbCurrency.NumKeys < currency.Amount)
                    return BuyResult.NotEnoughCurrency;

                dbCurrency.NumKeys -= currency.Amount;
            }

            await userService.SaveChanges();
            return BuyResult.Success;
        }
    }
}