using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly GlobalValuesService globalValuesService;
        private readonly IConfigurationSection securityConfig;

        public VerifyUserIdController(UserService userService, IConfiguration configuration, GlobalValuesService globalValuesService)
        {
            this.userService = userService;
            this.globalValuesService = globalValuesService;
            this.securityConfig = configuration.GetSection("Security");
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] VerifyUserIdRequest request)
        {
            bool allowAnyUserId = securityConfig.GetValue("AllowAnyUserId", defaultValue: true);
            
            if (request.UserId != null && request.UserId != 0)
            {
                User user = await userService.GetUserFromId(request.UserId.Value);

                if (user == null && allowAnyUserId)
                {
                    if (request.UserId > globalValuesService.MaxUserId)
                        return BadRequest("User id is too big");
                    
                    user = await userService.CreateUser(request.SteamId, request.CurrentUsername, request.UserId);
                }
                else if (user == null || user.SteamId != request.SteamId)
                {
                    return NotFound("There is no matching account with this steam and user id.");
                }

                await userService.ResetUserState(user);

                return Ok(user);
            }
            else
            {
                User user = await userService.GetUserFromSteamId(request.SteamId);

                if (user == null)
                {
                    user = await userService.CreateUser(request.SteamId, request.CurrentUsername);
                }
                
                await userService.ResetUserState(user);

                return Ok(user);
            }
        }
    }
}