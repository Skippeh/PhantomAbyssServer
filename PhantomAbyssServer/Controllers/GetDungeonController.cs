using Microsoft.AspNetCore.Mvc;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Models;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetDungeonController : ControllerBase
    {
        private readonly MaintenanceService maintenanceService;

        public GetDungeonController(MaintenanceService maintenanceService)
        {
            this.maintenanceService = maintenanceService;
        }
        
        [HttpPost]
        public IActionResult Index([FromBody] GetDungeonRequest request)
        {
            // todo: get 'real' ids.
            uint dungeonId = request.DungeonId > 0 ? request.DungeonId : 1;
            uint routeId = request.RouteId > 0 ? request.RouteId : 1;
            
            return Ok(new
            {
                dungeonFloorNumber = request.DungeonFloorNumber,
                dungeonFloorTotalCount = 0,
                dungeonFloorType = 0,
                dungeonId = dungeonId,
                routeId = routeId,
                routeStage = request.RouteStage,
                dungeonLayoutType = 0,
                dungeonSeed = 1000,
                dungeonSettingsIndex = request.DungeonSettingsIndex,
                dungeonVersion = maintenanceService.GetServerVersion(),
                ghostRuns = new object[0],
                health = new UserHealth
                {
                    BaseHealth = 3,
                    BonusHealth = 3,
                    MaxBonusHealth = 3
                },
                layoutDownloadUrl = (string) null,
                lockedRoutes = new object[0],
                persistentChanges = (object) null,
                precalculatedRooms = (object) null,
                relicId = (string) null, // null makes the game pick a relic
                serverStatus = maintenanceService.GetMaintenanceInfo(),
                totalDungeonAttemptsAllFloors = 0,
                userId = request.UserId
            });
        }
    }
}