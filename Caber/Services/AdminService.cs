using Caber.Controllers;
using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services.Interfaces;

namespace Caber.Services
{
    public class AdminService(IRepository<int, Driver> driverRepository,
                              IRepository<int, Passenger> passengerRepository,
                              IRepository<int, User> userRepository,
                              IRepository<int, Cab> cabRepository,
                              IRepository<int, Ride> rideRepository) : IAdminService
    {
        public async Task<List<UserProfileResponseDto>> GetAllUsers()
        {
            try
            {
                var users = (await userRepository.GetAll()).Select(user => new UserProfileResponseDto()
                {
                    FirstName = user.FirstName,
                    Address = user.Address,
                    Email = user.Email,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Id = user.Id
                }).ToList();

                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CabResponseDto>> GetCabs()
        {
            try
            {
                var cabs = (await cabRepository.GetAll()).Select(cab => new CabResponseDto()
                {
                    CabId = cab.Id,
                    DriverId = cab.DriverId,
                    Location = cab.Location,
                    Status = cab.Status,
                }).ToList();

                return cabs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<DriverDetailsResponseDto>> GetDrivers()
        {
            try
            {
                var drivers = (await driverRepository.GetAll())
                                .Select(driver => new DriverDetailsResponseDto()
                                {
                                    DriverId = driver.Id,
                                    LicenseExpiryDate = driver.LicenseExpiryDate,
                                    LicenseNumber = driver.LicenseNumber
                                }).ToList();

                return drivers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PassengerResponseDto>> GetPassengers()
        {
            try
            {
                var passengers = (await passengerRepository.GetAll())
                                .Select(passenger => new PassengerResponseDto() { Id = passenger.Id, UserId = passenger.Id })
                                .ToList();

                return passengers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<VerifyDriverResponseDto> VerifyDriver(VerifyDriverRequestDto request)
        {
            try
            {
                var driver = await driverRepository.GetByKey(request.DriverId);

                driver.IsVerified = true;

                await driverRepository.Update(driver);

                return new VerifyDriverResponseDto
                {
                    DriverId = driver.Id,
                    IsVerified = driver.IsVerified
                };
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<RideWholeResponseDto>> GetRidesByStatus(RideStatusEnum status)
        {
            try
            {
                var rides = (await rideRepository.GetAll())
                                                 .Where(ride => ride.RideStatus == status)
                                                 .Select(ride => ride.MapToRideWholeResponseDto())
                                                 .ToList();

                return rides;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<VerifyCabResponseDto> VerifyCab(VerifyCabRequestDto request)
        {
            try
            {
                var cab = await cabRepository.GetByKey(request.CabId);

                cab.IsVerified = true;

                return new VerifyCabResponseDto()
                {
                    CabId = cab.Id,
                    CabStatus = "Idle"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
