﻿using Caber.Controllers;
using Caber.Exceptions;
using Caber.Extensions;
using Caber.Extensions.DtoMappers;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Models.Enums;
using Caber.Repositories.Interfaces;

namespace Caber
{
    public class CabService(IRepository<int, Cab> cabRepository,
                            IRepository<int, Ride> rideRepository,
                            IRepository<int, Driver> driverRepository) : ICabService
    {
        public async Task<BookCabResponseDto> BookCab(BookCabRequestDto request)
        {
            try
            {
                var cabDetails = await cabRepository.GetByKey(request.CabId);

                var ride = new Ride()
                {
                    CabId = request.CabId,
                    StartLocation = request.PickupLocation,
                    PassengerId = request.PassengerId,
                    EndLocation = request.DropoffLocation,
                    RideDate = DateTime.Now,
                };

                var bookedRide = await rideRepository.Add(ride);

                return bookedRide.MapToBookCabResponseDto(cabDetails);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Ride> BookAnyCab(int passengerId, string pickupLocation, string dropoffLocation, int seatingCapacity)
        {
            try
            {
                var cabs = await cabRepository.GetAll();

                var cabsByLocation = cabs.Where(c => c.Location.GetSimilarity(pickupLocation) is >= 0.40);

                if (cabsByLocation == null || cabsByLocation.Count() == 0)
                {
                    throw new NoCabsFoundException();
                }

                var cab = cabsByLocation.Where(c => c.SeatingCapacity >= seatingCapacity)
                                        .OrderByDescending(c => c.Driver.LastRide - DateTime.Now)
                                        .FirstOrDefault();

                if (cab == null)
                {
                    throw new NoCabsFoundException();
                }

                var ride = await rideRepository.Add(new Ride()
                {
                    CabId = cab.Id,
                    StartLocation = pickupLocation,
                    PassengerId = passengerId,
                    EndLocation = dropoffLocation,
                    RideDate = DateTime.Now,
                    RideStatus = RideStatusEnum.Requested,
                });

                return ride;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CabResponseDto>> GetCabsByLocation(string location, int seatingCapacity)
        {
            try
            {
                var cabs = await cabRepository.GetAll();

                var cabsByLocation = cabs
                    .Where(c => c.Location.GetSimilarity(location) is >= 0.40)
                    .Where(c => c.SeatingCapacity >= seatingCapacity)
                    .Select(c => c.MapToCabResponseDto());

                return cabsByLocation.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DriverDetailsResponseDto> GetDriverDetails(int cabId)
        {
            try
            {
                var cab = await cabRepository.GetByKey(cabId);

                var driver = cab.Driver.ToDriverDetailsResponseDto();

                return driver;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cab> RegisterCab(Cab cab)
        {
            try
            {
                var driver = await driverRepository.GetByKey(cab.DriverId) ?? throw new DriverNotFoundException(cab.DriverId);

                if (driver.IsVerified == false)
                {
                    throw new DriverIsNotVerifiedToRegister(cab.DriverId);
                }

                var registeredCab = await cabRepository.Add(cab);

                return registeredCab;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Cab> UpdateCabLocation(int cabId, string location)
        {
            try
            {
                var cab = await cabRepository.GetByKey(cabId) ?? throw new CabNotFoundException(cabId);

                cab.Location = location;

                return await cabRepository.Update(cab);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UpdateCabResponseDto> UpdateCabProfile(UpdateCabRequestDto cabDetails)
        {
            try
            {
                var cab = await cabRepository.GetByKey(cabDetails.CabId) ?? throw new CabNotFoundException(cabDetails.CabId);
                if (cabDetails.Color != string.Empty) cab.Color = cabDetails.Color;
                if (cabDetails.SeatingCapacity > 0) cab.SeatingCapacity = cabDetails.SeatingCapacity;
                if (cabDetails.Model != string.Empty) cab.Model = cabDetails.Model;
                if (cabDetails.Make != string.Empty) cab.Make = cabDetails.Make;

                var updatedCab = await cabRepository.Update(cab);

                return updatedCab.MapToUpdateCabResponseDto();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Cab>> GetCabsForDriver(int driverId)
        {
            try
            {
                var cabs = await cabRepository.GetAll();

                return cabs.Where(c => c.DriverId == driverId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}