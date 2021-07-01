using System;
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
    public class SubmitDungeonLayoutController : ControllerBase
    {
        private readonly DungeonService dungeonService;

        public SubmitDungeonLayoutController(DungeonService dungeonService)
        {
            this.dungeonService = dungeonService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SubmitDungeonLayoutRequest request)
        {
            try
            {
                await dungeonService.SaveDungeonLayout(
                    request.DungeonId,
                    request.DungeonFloorNumber,
                    request.DungeonVersion,
                    request.RelicId,
                    request.DungeonFloorCount,
                    request.DungeonFloorType,
                    request.DungeonLayoutType,
                    request.PermanentSettingsData,
                    request.SavedLayoutData
                );
            }
            catch (SaveFailedException)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, "Could not save layout data");
            }
            catch (DataExistsAlready ex)
            {
                return Ok("OK");
            }
            
            return Ok("OK");
        }
    }
}