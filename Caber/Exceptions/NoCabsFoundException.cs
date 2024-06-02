namespace Caber.Exceptions
{
    [Serializable]
    public class NoCabsFoundException : Exception
    {
        public override string Message => "No cabs found for the given location and seating";

    }
}