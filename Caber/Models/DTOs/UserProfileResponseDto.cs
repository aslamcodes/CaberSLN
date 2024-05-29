namespace Caber.Models.DTOs
{
    public class UserProfileResponseDto
    {
        public string? Address { get; set; }
        public required string FirstName { get; set; }
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }

        public string Email { get; set; }
    }
}