using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerifyUserIdController : ControllerBase
    {
        private readonly UserService userService;

        public VerifyUserIdController(UserService userService)
        {
            this.userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] VerifyUserIdRequest request)
        {
            if (request.UserId != null && request.UserId != 0)
            {
                User user = await userService.GetUserFromId(request.UserId.Value);

                if (user == null || user.SteamId != request.SteamId)
                    return NotFound("There is no matching account with this steam and user id.");

                return Ok(ConvertUserToResponseObject(user));
            }
            else
            {
                User user = await userService.GetUserFromSteamId(request.SteamId);

                if (user == null)
                {
                    user = await userService.CreateUser(request.SteamId, request.CurrentUsername);
                }

                return Ok(ConvertUserToResponseObject(user));
            }
        }

        private object ConvertUserToResponseObject(User user)
        {
            return new
            {
                currency = new
                {
                    dungeonKeys = user.Currency.DungeonKeys.OrderBy(d => d.Stage).Select(d => d.NumKeys),
                    user.Currency.Essence
                },
                currentUsername = user.Name,
                user.Health,
                lockedCurrencyAndCompletedRoutes = new object[0],
                user.SharerId,
                userId = user.Id,
                //user.VictoryRoutes
                victoryRoutes = new object[0]
            };
        }
    }
}