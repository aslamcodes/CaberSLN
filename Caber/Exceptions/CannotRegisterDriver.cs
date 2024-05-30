namespace Caber.Exceptions
{
    [Serializable]
    public class CannotRegisterDriver : Exception
    {
        public override string Message => "Cannot register driver. Please try again later.";
    }
}