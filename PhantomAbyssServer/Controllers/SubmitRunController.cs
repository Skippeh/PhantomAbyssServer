using Microsoft.AspNetCore.Mvc;

namespace PhantomAbyssServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitRunController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}