using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulation.Core;

namespace Simulation.Tests
{
    [TestClass]
    public class BaseTests
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
            var output = Simulation.InternalRun();
            foreach (var o in output)
            {
                Assert.IsTrue(o.VehicleState.Height > 0);
                Assert.IsTrue(o.VehicleState.Velocity > 0);
            }
        }

        [TestMethod]
        public void TestValues()
        {
            Simulation.SpaceVehicle.FlyNextTimeInterval(10);
            Assert.IsTrue(Simulation.SpaceVehicle.AngleBelowHorizontal > 0);
        }
        
        [TestMethod]
        public void ChangeAngleOfAttack() 
        {
            for (int i = 1; i <= 5; i++)
            {
                Simulation.SpaceVehicle.AngleOfAttack += i;
                RunSimulation();
            }
        }

        [TestMethod]
        public void TestMass()
        {
            int mass = Simulation.SpaceVehicle.Mass;
            while (Simulation.SpaceVehicle.Mass > 10000)
            {
                mass -= 1000;
                var conditions = new InitialConditionsDTO()
                {
                    AngleBelowHorizontal = 6.93,
                    AngleOfAttack = 20,
                    Height = 80000,
                    Mass = mass,
                    Radius = 6.6,
                    Velocity = 8000
                };
                Simulation.SetupEnvironment(5, conditions, Core.Simulation.VehicleType.SpaceShuttle);
                RunSimulation();
            }
        }
    }
}