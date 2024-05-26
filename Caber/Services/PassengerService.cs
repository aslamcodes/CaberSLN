using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Repositories;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caber.Services
{
    public class PassengerService(IRepository<int, Passenger> repository) : IPassengerService
    {
        public async Task<PassengerRegisterResponseDto> RegisterPassenger(PassengerRegisterRequestDto passenger)
        {
            try
            {
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
    }
}