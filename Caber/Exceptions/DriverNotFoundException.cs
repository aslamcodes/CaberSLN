namespace Caber.Exceptions
{
    [Serializable]
    public class DriverNotFoundException(int key) : Exception
    {
        public override string Message => $"Driver with key {key} not found";


    }
}