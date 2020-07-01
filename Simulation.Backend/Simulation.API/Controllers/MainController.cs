using Microsoft.AspNetCore.Mvc;

namespace Simulation.API.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}