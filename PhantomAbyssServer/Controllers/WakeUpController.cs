using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhantomAbyssServer.Models;
using PhantomAbyssServer.Models.Requests;
using PhantomAbyssServer.Models.Responses;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WakeUpController : ControllerBase
    {
        private readonly MaintenanceService maintenanceService;
        private readonly GlobalValuesService globalValuesService;

        public WakeUpController(MaintenanceService maintenanceService, GlobalValuesService globalValuesService)
        {
            this.maintenanceService = maintenanceService;
            this.globalValuesService = globalValuesService;
        }

        [HttpPost]
        public IActionResult Index([FromBody] WakeUpRequest request)
        {
            var response = new WakeUpResponse
            {
                GlobalValues = new GlobalValues
                {
                    RelicConversionRewards = new RelicConversionRewards
                    {
                        Rewards = globalValuesService.GetRelicConversionRewards().ToList()
                    }
                },
                MaintenanceInfo = maintenanceService.GetMaintenanceInfo()
            };

            return Ok(response);
        }
    }
}