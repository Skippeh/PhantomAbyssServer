using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhantomAbyssServer.Exceptions;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitRunController : ControllerBase
    {
        private readonly UserService userService;
        private readonly SavedRunsService savedRunsService;

        public SubmitRunController(UserService userService, SavedRunsService savedRunsService)
        {
            this.userService = userService;
            this.savedRunsService = savedRunsService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SubmitRunRequest request)
        {
            var user = await userService.GetUserFromId(request.PlayerUniqueId);

            if (user == null)
                return NotFound("User not found");

            // Save run data if it exists
            if (!string.IsNullOrEmpty(request.RunData?.Trim()))
            {
                // Should probably verify the data is valid but for now we'll just save it without checking as the server isn't meant to be used publicly
                try
                {
                    await savedRunsService.SaveRunData(user, request.DungeonId, request.RouteId, request.DungeonFloorNumber, request.Success, request.RunData);
                }
                catch (SaveFailedException)
                {
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Could not save run data");
                }
                catch (RunDataExistsAlready ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            if (request.DungeonFloorNumber == 3 && request.Success)
            {
                // todo: add currency in request to user
            }
            
            return Ok();
        }
    }
}