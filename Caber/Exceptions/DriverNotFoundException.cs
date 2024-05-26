namespace Caber.Exceptions
{
    [Serializable]
    public class DriverNotFoundException : Exception
    {
        private readonly string message;
        public DriverNotFoundException(int key)
        {
            this.message = $"Driver with key {key} not found";
        }

        public override string Message => message;

    }
}