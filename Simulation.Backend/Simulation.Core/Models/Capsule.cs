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

        public override void CalculateBallisticCoeff()
        {
            BallisticCoeff = Mass / (DragCoeff * CrossSectionalArea);
        }

        public override void CalculateLdCoeff()
        {
            LiftToDrag = 0.0143 * AngleOfAttack;
        }
    }
}