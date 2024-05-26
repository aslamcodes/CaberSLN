using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(RegisterRequestDto registerDto);

        Task<AuthResponseDto> Login(LoginRequestDto loginDto);
    }
}