using System;

namespace Simulation.Core.Models
{
    public class Shuttle : SpaceVehicle
    {
        //TODO: to compute automatically
        public const double SkinFriction = 0.045;

        public override double MaxTemperature => 2400;
        public override int ThermalConductivity => 40;
        public override double DragCoeff => 0.666;

        private double _crossSectionalArea;
        private double _referenceArea;

        public double CrossSectionalArea
        {
            get => _crossSectionalArea;
            set => _crossSectionalArea = value;
        }

        public double ReferenceArea
        {
            get => _referenceArea;
            set => _referenceArea = value;
        }

        public Shuttle(int mass, double velocity, int height, double angleOfAttack, double angleBelowHorizontal, double radius) 
            : base(mass, velocity, height, angleOfAttack, angleBelowHorizontal, radius)
        {
            CrossSectionalArea = Math.PI * Math.Pow(Radius, 2);
        }
        
        public override void CalculateBallisticCoeff()
        {
            BallisticCoeff = Mass / (DragCoeff * CrossSectionalArea);
        }

        public override void CalculateLdCoeff()
        {
            double angleAttackRadians = DegreesToRadians(AngleOfAttack);
            LiftToDrag = Math.Sin(DegreesToRadians(AngleOfAttack)) * Math.Sin(angleAttackRadians * 2) /
                         (2 * Math.Pow(Math.Sin(angleAttackRadians), 3) + SkinFriction);
        }
    }
}