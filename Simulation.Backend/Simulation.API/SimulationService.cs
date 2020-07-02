using System.Collections.Generic;
using Simulation.Core;
using Simulation.Core.Models;

namespace Simulation.API
{
    public interface ISimulationService
    {
        void Init(double timeFrame, InitialConditionsDTO initConditions, Core.Simulation.VehicleType vehicleType);
        List<TimeFrame> Run();
    }
    public class SimulationService : ISimulationService
    {
        private Simulation.Core.Simulation _simulation;
        public void Init(double timeFrame, InitialConditionsDTO initConditions, Core.Simulation.VehicleType vehicleType)
        {
            _simulation = new Core.Simulation();
            _simulation.SetupEnvironment(timeFrame, initConditions, vehicleType);
        }

        public List<TimeFrame> Run()
        {
            return _simulation.Run();
        }
    }
}