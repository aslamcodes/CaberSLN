namespace Caber.Exceptions
{
    [Serializable]
    public class CannotCompleteRideException(string state) : Exception
    {
        public override string Message => $"Cannot complete ride in state {state}";
    }
}