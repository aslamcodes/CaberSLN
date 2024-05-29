using Caber.Models.DTOs;

namespace Caber.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileUpdateResponseDto> UpdateUserProfile(UserProfileUpdateRequestDto request);

        Task<UserProfileResponseDto> GetUserProfile(int userId);

        Task<DeleteUserResponseDto> DeleteUser(DeleteUserRequestDto userDetails);
    }


}
