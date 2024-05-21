using Caber.Models.Enums;

namespace Caber.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public required string Email { get; set; }

        //public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public UserTypeEnum UserType { get; set; }
    }
}
