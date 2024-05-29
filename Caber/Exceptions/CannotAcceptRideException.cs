namespace Caber.Exceptions
{
    [Serializable]
    public class CannotAcceptRideException(string state) : Exception
    {
        public override string Message => $"Cannot accept ride in state {state}";
    }
}