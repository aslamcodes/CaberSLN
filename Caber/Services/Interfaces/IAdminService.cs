using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IAdminService
    {
        Task<VerifyDriverResponseDto> VerifyDriver(VerifyDriverRequestDto request);
    }
}