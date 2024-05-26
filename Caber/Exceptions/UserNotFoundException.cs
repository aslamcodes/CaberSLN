namespace Caber.Exceptions
{
    [Serializable]
    public class UserNotFoundException(int key) : Exception
    {
        public int Key { get; } = key;

        public override string Message => $"User with key {Key} not found";

    }
}