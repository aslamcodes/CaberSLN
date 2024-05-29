namespace Caber.Exceptions
{
    [Serializable]
    public class CannotInitiateRide(string state) : Exception
    {
        public override string Message => $"Cannot initiate ride. Ride is in {state} state.";
    }
}