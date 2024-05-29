using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Models.Enums;
using Caber.Repositories;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caber.Services
{
    public class PassengerService(IRepository<int, Passenger> repository, IRepository<int, User> userRepository, IRepository<int, Ride> rideRepository) : IPassengerService
    {
        public async Task<PassengerRegisterResponseDto> RegisterPassenger(PassengerRegisterRequestDto passenger)
        {
            try
            {
                var existingUser = await userRepository.GetByKey(passenger.UserId);

                existingUser.UserType = UserTypeEnum.Passenger;

                await userRepository.Update(existingUser);

                var newPassenger = new Passenger()
                {
                    UserId = passenger.UserId,
                };

                var createdPassenger = await repository.Add(newPassenger);

                return createdPassenger.ToPassengerRegisterResponseDto();
            }
            catch (DbUpdateException)
            {
                throw new DuplicatePassengerException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RideResponseDto> InitiateRide(InitiatedRideRequestDto request)
        {
            try
            {
                var ride = await rideRepository.GetByKey(request.RideId);
                if (ride.RideStatus != RideStatusEnum.Accepted)
                {
                    throw new CannotInitiateRide(ride.RideStatus.ToString());
                }
                ride.RideStatus = RideStatusEnum.InProgress;
                ride.StartTime = DateTime.Now;
                await rideRepository.Update(ride);

                return new RideResponseDto()
                {
                    DriverId = ride.Cab.Id,
                    PassengerId = ride.PassengerId,
                    RideId = ride.Id,
                    Status = ride.RideStatus.ToString()
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}