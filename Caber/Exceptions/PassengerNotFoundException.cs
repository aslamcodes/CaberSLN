namespace Caber.Exceptions
{
    [Serializable]
    public class PassengerNotFoundException : Exception
    {
        private readonly string message;

        public PassengerNotFoundException(int key)
        {
            message = $"Passenger with key {key} not found";
        }

        public override string Message => message;
    }
}