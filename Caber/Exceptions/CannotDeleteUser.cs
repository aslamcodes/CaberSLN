namespace Caber.Exceptions
{
    [Serializable]
    public class CannotDeleteUser(int userId) : Exception
    {
        public override string Message => $"Cannot delete user {userId}.";


    }
}