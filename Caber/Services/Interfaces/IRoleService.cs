using Caber.Models;
using Caber.Models.Enums;

namespace Caber.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CanAccessCab(int userId, int cabId);
        Task<bool> CanAccessRide(int userId, UserTypeEnum userType, int rideId);

        Task<Driver?> GetDriverForUser(int userId);

        Task<Passenger?> GetPassengerForUser(int userId);

    }
}
