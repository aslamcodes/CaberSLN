using Caber.Models;
using Caber.Models.DTOs;
using Caber.Repositories.Interfaces;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class UserService(IRepository<int, User> userRepository,
                             IRepository<int, Passenger> passengerRepository,
                             IRepository<int, Driver> driverRepository) : IUserService
    {
        //public async Task<DeleteUserResponseDto> DeleteUser(DeleteUserRequestDto userDetails)
        //{
        //    try
        //    {
        //        var user = await userRepository.GetByKey(userDetails.Id);

        //        switch (user.UserType)
        //        {
        //            case UserTypeEnum.Admin:
        //                throw new CannotDeleteUser("Admin user cannot be deleted");

        //            case UserTypeEnum.Driver:
        //                await driverRepository.Delete(userDetails.Id);
        //                break;

        //            case UserTypeEnum.Passenger:
        //                await passengerRepository.Delete(userDetails.Id);
        //                break;
        //            default:
        //                throw new Exception("Invalid user type");
        //        }

        //        await userRepository.Delete(userDetails.Id);

        //        return new DeleteUserResponseDto()
        //        {
        //            Id = userDetails.Id,
        //            Message = "User deleted successfully"
        //        };

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<UserProfileResponseDto> GetUserProfile(int userId)
        {
            try
            {
                var user = await userRepository.GetByKey(userId);

                return new UserProfileResponseDto()
                {
                    Address = user.Address,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserProfileUpdateResponseDto> UpdateUserProfile(UserProfileUpdateRequestDto request)
        {
            try
            {
                var user = await userRepository.GetByKey(request.Id);

                if (request.Address != null) user.Address = request.Address;
                if (request.FirstName != null) user.FirstName = request.FirstName;
                if (request.LastName != null) user.LastName = request.LastName;
                if (request.Phone != null) user.Phone = request.Phone;

                var updatedUser = await userRepository.Update(user);

                return new UserProfileUpdateResponseDto()
                {
                    Address = updatedUser.Address,
                    FirstName = updatedUser.FirstName,
                    Id = updatedUser.Id,
                    LastName = updatedUser.LastName,
                    Phone = updatedUser.Phone
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
