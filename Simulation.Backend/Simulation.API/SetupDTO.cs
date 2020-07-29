namespace Simulation.API
{
    public class SetupDTO
    {
        public int Mass { get; set; }
        public double Velocity { get; set; }
        public int Height { get; set; }
        public double AngleOfAttack { get; set; }
        public double AngleBelowHorizontal { get; set; }
        public double Radius { get; set; }
        public double TimeFrame { get; set; }
        public int Time { get; set; }
        public Core.Simulation.VehicleType VehicleType { get; set; }
    }
}