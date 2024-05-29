using System.Runtime.Serialization;

namespace Caber.Services
{
    [Serializable]
    internal class CannotDeleteUser : Exception
    {
        public CannotDeleteUser()
        {
        }

        public CannotDeleteUser(string? message) : base(message)
        {
        }

        public CannotDeleteUser(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CannotDeleteUser(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}