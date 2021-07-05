using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Models;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Models.Responses;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetDungeonController : ControllerBase
    {
        private readonly MaintenanceService maintenanceService;
        private readonly SavedRunsService savedRuns;
        private readonly DungeonService dungeons;
        private readonly UserService userService;

        public GetDungeonController(MaintenanceService maintenanceService, SavedRunsService savedRuns, DungeonService dungeons, UserService userService)
        {
            this.maintenanceService = maintenanceService;
            this.savedRuns = savedRuns;
            this.dungeons = dungeons;
            this.userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] GetDungeonRequest request)
        {
            User user = await userService.GetUserFromId(request.UserId);

            if (user == null)
                return BadRequest("User not found");
            
            // todo: get 'real' ids.
            uint dungeonId = request.DungeonId > 0 ? request.DungeonId : 1;
            uint routeId = request.RouteId > 0 ? request.RouteId : 1;

            var ghostRuns = await savedRuns.GetSavedRuns(dungeonId, routeId, request.DungeonFloorNumber);
            
            string relicId = null; // null makes the game pick a relic
            uint floorNumber = request.DungeonFloorNumber;
            uint floorType = 0;
            uint layoutType = 0;
            uint floorCount = 4;
            string layoutDownloadUrl = null;
            uint version = request.ClientDungeonVersion;
            
            var layoutData = await dungeons.GetDungeonLayout(dungeonId, floorNumber, version);

            if (layoutData != null)
            {
                relicId = layoutData.RelicId;
                floorType = layoutData.DungeonFloorType;
                layoutType = layoutData.DungeonLayoutType;
                floorCount = layoutData.DungeonFloorCount;

                layoutDownloadUrl = Url.ActionLink("DownloadLayout", values: new
                {
                    dungeonId,
                    floorNumber,
                    version = version
                });
            }

            var addedUserIds = new HashSet<uint>();

            return Ok(new
            {
                dungeonFloorNumber = request.DungeonFloorNumber,
                dungeonFloorTotalCount = floorCount,
                dungeonFloorType = floorType,
                dungeonId = dungeonId,
                routeId = routeId,
                routeStage = request.RouteStage,
                dungeonLayoutType = layoutType,
                dungeonSeed = 1000,
                dungeonSettingsIndex = request.DungeonSettingsIndex,
                dungeonVersion = maintenanceService.GetServerVersion(),
                ghostRuns = ghostRuns.Select(run =>
                {
                    // If a user runs the same dungeon more than once the game will crash if two ghosts have the same user id.
                    // Work around this by replacing duplicate ids with an unused one
                    uint userId = run.User.Id;
                    uint offset = 0;

                    while (addedUserIds.Contains(userId))
                    {
                        userId = uint.MaxValue - offset;
                        offset += 1;
                    }

                    addedUserIds.Add(userId);

                    return new GhostRun
                    {
                        Success = run.RunSuccessful ? 1 : 0,
                        DownloadUrl = Url.ActionLink("DownloadRun", values: new {hash = run.DataHash}),
                        UserId = userId,
                        UserName = run.User.Name
                    };
                }),
                health = user.Health,
                layoutDownloadUrl = layoutDownloadUrl,
                lockedRoutes = new object[0],
                persistentChanges = (object) null,
                precalculatedRooms = (object) null,
                relicId = relicId,
                serverStatus = maintenanceService.GetMaintenanceInfo(),
                totalDungeonAttemptsAllFloors = 0,
                userId = request.UserId
            });
        }
        
        [HttpGet]
        [Route("DownloadRun/{hash}")]
        public async Task<IActionResult> DownloadRun(string hash, bool deserialize = false)
        {
            var savedRun = await savedRuns.GetSavedRunFromHash(hash);

            if (savedRun == null)
                return NotFound("A run with the specified hash was not found");

            var runData = await savedRuns.GetRunDataFromSavedRun(savedRun);

            if (runData == null)
                return StatusCode((int) HttpStatusCode.InternalServerError, "Failed to fetch the run data");

            if (!deserialize)
                return Ok(runData);

            byte[] decodedBytes = Convert.FromBase64String(runData);
            runData = Encoding.UTF8.GetString(decodedBytes);

            return Content(runData, "application/json", Encoding.UTF8);
        }

        [HttpGet]
        [Route("DownloadLayout/{version}/{dungeonId}/{floorNumber}")]
        public async Task<IActionResult> DownloadLayout(uint dungeonId, uint floorNumber, uint version, bool deserialize = false)
        {
            var layoutMetaData = await dungeons.GetDungeonLayout(dungeonId, floorNumber, version);

            if (layoutMetaData == null)
                return NotFound("No layout data found for this dungeon");

            var layoutData = await dungeons.GetLayoutData(layoutMetaData);

            if (layoutData == null)
                return StatusCode((int) HttpStatusCode.InternalServerError, "Failed to fetch the layout data");

            if (!deserialize)
                return Ok(layoutData);

            byte[] decodedBytes = Convert.FromBase64String(layoutData);
            layoutData = Encoding.UTF8.GetString(decodedBytes);

            return Content(layoutData, "application/json", Encoding.UTF8);
        }
    }
}