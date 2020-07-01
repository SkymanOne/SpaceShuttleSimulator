using System;
using System.Collections.Generic;
using Simulation.Core.Models;

namespace Simulation.Core
{
    public class Simulation
    {
        public double TimeInterval { get; private set; }
        private SpaceVehicle _spaceVehicle;
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
                    _spaceVehicle = new Shuttle(initConditions);
                    break;
                case VehicleType.SpaceCapsule:
                    _spaceVehicle = new Capsule(initConditions);
                    break;
            }
            _timeLine = new List<TimeFrame>();
        }

        public List<TimeFrame> Run(int numberOfTimeFrames)
        {
            while (!_spaceVehicle.IsDestroyed && _spaceVehicle.Height > 100 && _timeLine.Count != numberOfTimeFrames)
            {
                _spaceVehicle.FlyNextTimeInterval(TimeInterval);
                _timeLine.Add(new TimeFrame()
                {
                    Logs = new Log[0],
                    TimeElapsed = (_timeLine.Count + 1) * TimeInterval,
                    VehicleState = _spaceVehicle
                });
            }
            return _timeLine;
        }

    }
}
