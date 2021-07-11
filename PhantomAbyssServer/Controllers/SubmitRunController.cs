using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhantomAbyssServer.Database.Models;
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
        private readonly DungeonService dungeons;
        private readonly MaintenanceService maintenanceService;

        public SubmitRunController(UserService userService, SavedRunsService savedRunsService, DungeonService dungeons, MaintenanceService maintenanceService)
        {
            this.userService = userService;
            this.savedRunsService = savedRunsService;
            this.dungeons = dungeons;
            this.maintenanceService = maintenanceService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SubmitRunRequest request)
        {
            var user = await userService.GetUserFromId(request.PlayerUniqueId);

            if (user == null)
                return NotFound("User not found");

            Route route = await dungeons.GetRouteById(request.RouteId);
            Dungeon dungeon = await dungeons.GetDungeonById(request.DungeonId);

            if (route == null)
                return BadRequest("Route not found");

            if (dungeon == null)
                return BadRequest("Dungeon not found");

            if (dungeon.RouteId != route.Id)
                return BadRequest("Route and dungeon are not related");

            if (route.CurrentUser?.Id != user.Id)
                return BadRequest("The user is not the current runner of this route");
            
            if (request.DungeonFloorNumber == 3 && request.Success && !string.IsNullOrEmpty(request.RunData?.Trim()))
            {
                await userService.GiveCurrency(user, request.Currency.Essence, request.Currency.DungeonKeys);
                await dungeons.FinishRoute(user, route, dungeon);
            }
            else if (!request.Success)
            {
                await userService.ResetUserState(user);
            }

            // Should probably verify the data is valid but for now we'll just save it without checking as the server isn't meant to be used publicly
            try
            {
                await savedRunsService.SaveRunData(user, dungeon.Id, route.Id, request.DungeonFloorNumber, request.Success, request.RunData);
            }
            catch (SaveFailedException)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, "Could not save run data");
            }
            catch (DataExistsAlready ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new
            {
                currency = user.Currency,
                dungeonFloorNumber = request.DungeonFloorNumber,
                dungeonId = dungeon.Id,
                routeId = route.Id,
                success = request.Success,
                userId = user.Id,
                health = user.Health,
                lockedCurrencyAndCompletedRoutes = new object[0],
                maintenanceInfo = maintenanceService.GetMaintenanceInfo()
            });
        }
    }
}