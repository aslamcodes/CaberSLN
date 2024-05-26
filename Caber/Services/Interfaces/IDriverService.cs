using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IDriverService
    {
        Task<DriverRegisterResponseDto> RegisterDriver(DriverRegisterRequestDto driver);
    }
}