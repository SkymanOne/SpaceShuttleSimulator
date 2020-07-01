using System;

namespace Simulation.Core.Models
{
    /// <summary>
    /// Base class of space vehicle. 
    /// Derive it to create a specific implementation of space vehicle
    /// </summary>
    public abstract class SpaceVehicle
    {
        #region Limit Constants

        public abstract double MaxTemperature { get; }
        public abstract int ThermalConductivity { get; }
        
        public abstract double DragCoeff { get; }
        public const int DecelerationLoadingLimit = 12;
        private const double R = 6378000;
        private const double H = 6930;
        private const double InitP = 1.225;
        private const double InitGravity = 9.81;

        #endregion

        #region Inputs
        
        public double Velocity { get; set; }
        public double Height { get; protected set; }
        public int Mass { get; protected set; }
        public double CurrentTemperature { get; protected set; } = 200;
        public double Radius { get; protected set; }
        public double AngleOfAttack { get; set; }
        
        #endregion

        #region Derivations
        
        public double LiftToDrag { get; protected set; }
        public double AngleBelowHorizontal { get; protected set; }
        public double BallisticCoeff { get; protected set; }
        public double AbsoluteAcceleration { get; protected set; }
        public double Displacement { get; protected set; }
        public double Range { get; protected set; }
        public double DisplacementAroundEarth { get; protected set; }
        public int DecelerationLoad { get; protected set; }

        private double _density;
        private double _gravity;
        private double _initHeight;

        #endregion

        #region State Vars

        public bool IsDestroyed { get; set; } = false;

        #endregion

        public SpaceVehicle(InitialConditionsDTO initConditions)
        {
            Mass = initConditions.Mass;
            Velocity = initConditions.Velocity;
            Radius = initConditions.Radius;
            _initHeight = initConditions.Height;
            Height = initConditions.Height;
            AngleOfAttack = initConditions.AngleOfAttack;
            AngleBelowHorizontal = initConditions.AngleBelowHorizontal;
        }
        public abstract void CalculateBallisticCoeff();
        public abstract void CalculateLdCoeff();

        public void FlyNextTimeInterval(double timeInterval)
        {
            // Trajectory calculations
            //Height += Velocity * Math.Sin(DegreesToRadians(AngleBelowHorizontal) * timeInterval);
            _density = InitP * Math.Pow(Math.E, -(Height / H));
            _gravity = InitGravity * Math.Pow(R / (R + Height), 2);
            
            CalculateAngleBelowHorizontal(timeInterval);
            CalculateAbsoluteAcceleration();
            CalculateDisplacementAndVelocity(timeInterval);
            CalculateDisplacementComponents();

            // Calculate Deceleration Load
            DecelerationLoad = (int) Math.Round(AbsoluteAcceleration / 9.81);

            // Heating calculations
            CalculateTemperature();
        }

        private void CalculateAngleBelowHorizontal(double timeInterval)
        {
            AngleBelowHorizontal += 0 - (_density * Velocity * LiftToDrag / (2 * BallisticCoeff) +
                                    _gravity * Math.Cos(DegreesToRadians(AngleBelowHorizontal)) / R +
                                    Velocity * Math.Cos(DegreesToRadians(AngleBelowHorizontal)) / R) * timeInterval;
        }

        private void CalculateAbsoluteAcceleration()
        {
            AbsoluteAcceleration = _gravity * Math.Sin(DegreesToRadians(AngleBelowHorizontal)) - 
                                   _density * Math.Pow(Velocity, 2) / (2 * BallisticCoeff);
        }

        private void CalculateDisplacementAndVelocity(double timeInterval)
        {
            double oldVelocity = Velocity;
            Velocity = AbsoluteAcceleration * timeInterval;
            Displacement += 0.5 * (oldVelocity + Velocity) * timeInterval;
        }

        private void CalculateDisplacementComponents()
        {
            Height = _initHeight - Displacement * Math.Sin(DegreesToRadians(AngleBelowHorizontal));
            Range = Displacement * Math.Cos(DegreesToRadians(AngleBelowHorizontal));
            DisplacementAroundEarth = 2 * Math.PI * R * Range / (2 * Math.PI * (R + Height));
        }

        private void CalculateTemperature()
        {
            double heatFlux = 1.83 * Math.Pow(10, -4) * Math.Pow(Velocity, 3) * Math.Pow(_density / Radius, 0.5);
            
        }

        public bool CheckTheState()
        {
            //TODO: check properties and alert
            return true;
        }

        public double DegreesToRadians(double d)
        {
            return d * (Math.PI / 180);
        }
    }
}