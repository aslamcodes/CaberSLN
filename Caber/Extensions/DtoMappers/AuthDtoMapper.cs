namespace Caber.Models.DTOs.Mappers
{
    public static class AuthDtoMapper
    {
        public static AuthResponseDto MapToAuthResponse(this User registerDto, string token)
        {
            return new AuthResponseDto
            {
                Token = token
            };
        }
    }
}
