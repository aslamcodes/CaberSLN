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
    public class PassengerService(IRepository<int, Passenger> passengerRepository,
                                  IRepository<int, User> userRepository,
                                  IRepository<int, Ride> rideRepository) : IPassengerService
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

                var createdPassenger = await passengerRepository.Add(newPassenger);

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

        public async Task<RideBasicResponseDto> InitiateRide(InitiatedRideRequestDto request)
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

                return new RideBasicResponseDto()
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

        public async Task<RideCompletedResponseDto> CompleteRide(CompleteRideRequestDto request)
        {
            try
            {
                var ride = await rideRepository.GetByKey(request.RideId);
                if (ride.RideStatus != RideStatusEnum.InProgress)
                {
                    throw new CannotCompleteRideException(ride.RideStatus.ToString());
                }
                var rideCompletedTime = DateTime.Now;
                ride.RideStatus = RideStatusEnum.Completed;
                ride.EndTime = rideCompletedTime;
                ride.RideDistance = request.Distance;
                #region Fare Calculation
                var baseFare = 2.50;
                TimeSpan rideTime = rideCompletedTime - ride.StartTime;
                var timeDistanceFactor = request.Distance * rideTime.Minutes;
                double fare = baseFare + (timeDistanceFactor * 10);
                #endregion
                ride.Fare = fare;
                ride.PassengerComment = request.Comment;
                ride.PassengerRating = request.Rating;

                await rideRepository.Update(ride);

                return new RideCompletedResponseDto()
                {
                    DriverId = ride.Cab.Id,
                    PassengerId = ride.PassengerId,
                    RideId = ride.Id,
                    Status = ride.RideStatus.ToString(),
                    Fare = fare.ToString() + "$"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<RideWholeResponseDto>> GetRides(int passengerId)
        {
            try
            {
                await passengerRepository.GetByKey(passengerId);

                var rides = await rideRepository.GetAll();

                var userRides = rides.Where(r => r.PassengerId == passengerId);

                return userRides.Select(ride => new RideWholeResponseDto()
                {
                    DriverId = ride.Cab.DriverId,
                    PassengerId = ride.PassengerId,
                    Id = ride.Id,
                    Status = ride.RideStatus.ToString(),
                    Fare = ride.Fare.ToString() + "$",
                    StartLocation = ride.StartLocation,
                    EndLocation = ride.EndLocation,
                    StartTime = ride.StartTime.ToString(),
                    EndTime = ride.EndTime?.ToString()
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}