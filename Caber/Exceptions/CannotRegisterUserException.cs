namespace Caber.Exceptions
{
    [Serializable]
    internal class CannotRegisterUserException : Exception
    {
        private readonly string message;
        public CannotRegisterUserException()
        {
            message = "Cannot register user";
        }

        public override string Message => message;

    }
}