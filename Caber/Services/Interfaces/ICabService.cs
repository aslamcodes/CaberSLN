﻿using Caber.Models;
using Caber.Models.DTOs;

namespace Caber.Controllers
{
    public interface ICabService
    {
        Task<List<CabResponseDto>> GetCabsByLocation(string location, int seatingCapacity);

        Task<BookCabResponseDto> BookCab(BookCabRequestDto request);

        Task<DriverDetailsResponseDto> GetDriverDetails(int cabId);

        Task<Cab> RegisterCab(Cab cab);

        Task<Cab> UpdateCabLocation(int cabId, string location);

        Task<UpdateCabResponseDto> UpdateCabProfile(UpdateCabRequestDto cab);

        Task<Ride> BookCabV2(int passengerId, string pickupLocation, string dropoffLocation, int seatingCapacity);
    }
}