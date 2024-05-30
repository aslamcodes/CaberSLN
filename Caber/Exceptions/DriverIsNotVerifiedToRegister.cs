namespace Caber.Exceptions
{
    [Serializable]
    public class DriverIsNotVerifiedToRegister(int driverId) : Exception
    {

        public override string Message => $"Driver {driverId} is not verified to register.";

    }
}