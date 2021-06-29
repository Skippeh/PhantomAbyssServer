using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Models.Responses;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpendCurrencyController : SpendCurrencyControllerBase
    {
        public SpendCurrencyController(UserService userService, IConfiguration configuration) : base(userService, configuration)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SpendCurrencyRequest request)
        {
            var user = await GetUser(request);

            if (user == null)
                return NotFound("User not found");

            BuyResult buyResult = await SpendCurrencies(user, request.Keys);

            return Ok(new SpendCurrencyResponse
            {
                UserId = user.Id,
                Result = buyResult,
                Currency = user.Currency,
                PurchaseItemId = request.PurchaseItemId
            });
        }
    }
}