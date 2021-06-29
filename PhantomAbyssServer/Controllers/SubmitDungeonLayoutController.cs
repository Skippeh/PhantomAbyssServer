using Microsoft.AspNetCore.Mvc;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitDungeonLayoutController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}