namespace Caber.Exceptions
{
    [Serializable]
    public class DuplicatePassengerException : Exception
    {

        public override string Message => "Passenger already exists in the system.";

    }
}