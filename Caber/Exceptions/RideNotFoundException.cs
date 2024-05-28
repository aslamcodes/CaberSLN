namespace Caber.Services
{
    [Serializable]
    public class RideNotFoundException(int rideId) : Exception
    {

        public override string Message => $"Ride with id {rideId} not found";
    }
}