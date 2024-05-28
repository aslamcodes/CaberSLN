namespace Caber.Services
{
    [Serializable]
    public class DuplicateDriverException : Exception
    {
        public override string Message => "Drviver already exists in the system.";

    }
}