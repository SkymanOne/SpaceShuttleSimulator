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
        private int _shc = 760;
        private double _materialDesity = 1700;
        private double _noseMass = 1697;

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

        public Shuttle(InitialConditionsDTO initConditions) 
            : base(initConditions)
        {
            CrossSectionalArea = Math.PI * Math.Pow(Radius, 2);
            CalculateBallisticCoeff();
            CalculateLdCoeff();
        }
        
        public override void CalculateBallisticCoeff()
        {
            BallisticCoeff = Mass / (CrossSectionalArea * DragCoeff);
        }

        public override void CalculateLdCoeff()
        {
            double angleAttackRadians = DegreesToRadians(AngleOfAttack);
            LiftToDrag = Math.Sin(DegreesToRadians(AngleOfAttack)) * Math.Sin(angleAttackRadians * 2) /
                         (2 * Math.Pow(Math.Sin(angleAttackRadians), 3) + SkinFriction);
        }

        protected override void CalculateTemperature(double timeInterval)
        {
            HeatFlux = 1.83 * Math.Pow(10, -4) * Math.Pow(Velocity, 3) * Math.Pow(_density / Radius, 0.5);
            var heat = HeatFlux * timeInterval * Math.PI * Math.Pow(Radius, 2);
            var deltaT = heat / (_noseMass * _shc) - 273.15; //in Celsius
            CurrentTemperature -= deltaT;
        }
    }
}