namespace Caber
{
    [Serializable]
    public class CabNotFoundException(int key) : Exception
    {
        public override string Message => $"Cab with key {key} not found";

    }
}