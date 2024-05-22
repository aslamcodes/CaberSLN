using Caber.Models.Enums;

namespace Caber.Models
{
    public class Ride
    {
        public int Id { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public RideStatusEnum RideStatus { get; set; }

        public DateTime RideDate { get; set; }

        public float RideDistance { get; set; }

        public double Fare { get; set; }

        public int PassengerRating { get; set; }

        public string PassengerComment { get; set; }

        public int PassengerId { get; set; }

        public Passenger Passenger { get; set; }

        public int CabId { get; set; }

        public Cab Cab { get; set; }

        //public int DriverId { get; set; }

        //public Driver Driver { get; set; }


    }
}