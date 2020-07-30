using System;

namespace Simulation.Core.Models
{
    public class Capsule : SpaceVehicle
    {

        public override double MaxTemperature => 2400;

        public override int ThermalConductivity => 80;

        public override double DragCoeff => 1.14;
        
        public double CrossSectionalArea { get; private set; }
        
        public Capsule(InitialConditionsDTO initConditions) 
            : base(initConditions)
        {
            CrossSectionalArea = Math.PI * Math.Pow(Radius, 2);
            CalculateBallisticCoeff();
            CalculateLdCoeff();
        }

        public sealed override void CalculateBallisticCoeff()
        {
            BallisticCoeff = Mass / (DragCoeff * CrossSectionalArea);
        }

        public sealed override void CalculateLdCoeff()
        {
            LiftToDrag = 0.0143 * AngleOfAttack;
        }

        protected override void CalculateTemperature(double timeInterval)
        {
            //_heatTransfer = 10.45 - Math.Abs(Velocity) + 10 * Math.Pow(Math.Abs(Velocity), 0.5) * 1.163;
            HeatFlux = 1.83 * Math.Pow(10, -4) * Math.Pow(Velocity, 3) * Math.Pow(_density / Radius, 0.5);
        }
    }
}