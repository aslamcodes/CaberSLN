namespace Caber.Exceptions
{
    [Serializable]
    public class CannotDeleteUser(string message) : Exception
    {
        public override string Message => message;


    }
}