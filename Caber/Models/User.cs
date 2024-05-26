﻿using Caber.Models.Enums;

namespace Caber.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public required string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public UserTypeEnum UserType { get; set; }

        public Driver Driver { get; set; }

        public Passenger Passenger { get; set; }
    }
}
