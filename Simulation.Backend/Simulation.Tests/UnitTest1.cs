using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulation.Core;

namespace Simulation.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RunSimulation()
        {
            var simulation = new Core.Simulation();
            var conditions = new InitialConditionsDTO()
            {
                AngleBelowHorizontal = 6.93,
                AngleOfAttack = 20,
                Height = 80000,
                Mass = 78000,
                Radius = 6.6,
                Velocity = 11000
            };
            simulation.SetupEnvironment(5, conditions, Core.Simulation.VehicleType.SpaceShuttle);
            var output = simulation.Run(10000);
        }
    }
}