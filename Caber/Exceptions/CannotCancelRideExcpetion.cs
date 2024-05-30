namespace Caber.Exceptions
{
    [Serializable]
    public class CannotCancelRideExcpetion(string message) : Exception
    {

        public override string Message => message;

    }
}