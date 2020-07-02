using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulation.Core;

namespace Simulation.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Core.Simulation Simulation { get; set; }

        [TestInitialize]
        public void Init()
        {
            Simulation = new Core.Simulation();
            var conditions = new InitialConditionsDTO()
            {
                AngleBelowHorizontal = 6.93,
                AngleOfAttack = 20,
                Height = 80000,
                Mass = 78000,
                Radius = 6.6,
                Velocity = 8000
            };
            Simulation.SetupEnvironment(5, conditions, Core.Simulation.VehicleType.SpaceShuttle);
        }

        [TestMethod]
        public void RunSimulation()
        {
            var output = Simulation.Run();
            foreach (var o in output)
            {
                Assert.IsTrue(o.VehicleState.Height > 100);
            }
        }

        [TestMethod]
        public void TestValues()
        {
            Simulation.SpaceVehicle.FlyNextTimeInterval(10);
            Assert.IsTrue(Simulation.SpaceVehicle.AngleBelowHorizontal > 0);
        }
    }
}