using System.Collections.Generic;
using System.Linq;
using Simulation.Core;
using Simulation.Core.Models;

namespace Simulation.API
{
    public interface ISimulationService
    {
        void Init(double timeFrame, InitialConditionsDTO initConditions, Core.Simulation.VehicleType vehicleType);
        List<TimeFrame> Run();
        List<TimeFrame> UpdatedRun(int time);
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
            return _simulation.InternalRun();
        }

        public List<TimeFrame> UpdatedRun(int time)
        {
            return _simulation.InternalRun().Skip(time).ToList();
        }
    }
}