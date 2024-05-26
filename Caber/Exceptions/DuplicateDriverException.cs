namespace Caber.Exceptions
{
    [Serializable]
    public class DuplicateDriverException : Exception
    {

        public override string Message => "Driver already exists in the system.";

    }
}