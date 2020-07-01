namespace Simulation.Core.Models
{
    public class TimeFrame
    {
        public SpaceVehicle VehicleState { get; set; }
        public Log[] Logs { get; set; }
        public double TimeElapsed { get; set; }
    }
}