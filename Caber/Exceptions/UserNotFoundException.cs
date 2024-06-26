namespace Caber.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string _message { get; set; }

        public UserNotFoundException(int key)
        {
            _message = $"User with key {key} not found";
        }

        public UserNotFoundException()
        {
            _message = $"User not found";

        }
        public override string Message => _message;

    }
}