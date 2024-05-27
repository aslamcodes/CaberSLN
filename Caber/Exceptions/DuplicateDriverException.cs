namespace Caber.Services
{
    [Serializable]
    public class DuplicateDriverException : Exception
    {
        public override string Message => "Passenger already exists in the system.";

    }
}