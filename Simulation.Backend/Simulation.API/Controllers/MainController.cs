using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Simulation.Core;
using Simulation.Core.Models;

namespace Simulation.API.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly ISimulationService _simulationService;

        public MainController(ISimulationService simulation)
        {
            _simulationService = simulation;
        }
        // GET
        [Route("/")]
        public IActionResult Index()
        {
            return Ok();
        }
        
        [HttpPost]
        [Route("/run")]
        public List<TimeFrame> Run(SetupDTO setupDTO)
        {
            _simulationService.Init(setupDTO.TimeFrame, new InitialConditionsDTO
            {
                AngleBelowHorizontal = setupDTO.AngleBelowHorizontal,
                AngleOfAttack = setupDTO.AngleOfAttack,
                Height = setupDTO.Height,
                Mass = setupDTO.Mass,
                Radius = setupDTO.Radius,
                Velocity = setupDTO.Velocity
            }, setupDTO.VehicleType);
            var output = _simulationService.Run();
            return output;
        }
    }
}