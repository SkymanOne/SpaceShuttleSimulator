using System;
using System.Collections.Generic;
using Simulation.Core.Models;

namespace Simulation.Core
{
    public class Simulation
    {
        private const int TimeLimit = 5000;
        public double TimeInterval { get; private set; }
        public SpaceVehicle SpaceVehicle { get; private set; }
        private List<TimeFrame> _timeLine;
        public enum VehicleType
        {
            SpaceShuttle,
            SpaceCapsule
        }
        public Simulation() { }

        public void SetupEnvironment(double timeFrame, InitialConditionsDTO initConditions, VehicleType vehicleType)
        {
            TimeInterval = timeFrame;
            switch (vehicleType)
            {
                case VehicleType.SpaceShuttle:
                    SpaceVehicle = new Shuttle(initConditions);
                    break;
                case VehicleType.SpaceCapsule:
                    SpaceVehicle = new Capsule(initConditions);
                    break;
            }
            _timeLine = new List<TimeFrame>();
        }

        public List<TimeFrame> Run()
        {
            while (_timeLine.Count != TimeLimit)
            {
                SpaceVehicle.FlyNextTimeInterval(TimeInterval);
                if (SpaceVehicle.IsDestroyed || SpaceVehicle.Height < 0) break; 
                _timeLine.Add(new TimeFrame()
                {
                    Logs = new Log[0],
                    TimeElapsed = (_timeLine.Count + 1) * TimeInterval,
                    VehicleState = SpaceVehicle.GetState()
                });
            }
            return _timeLine;
        }
    }
}
