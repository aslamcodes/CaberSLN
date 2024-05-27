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
    public class PassengerService(IRepository<int, Passenger> repository, IRepository<int, User> userRepository) : IPassengerService
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
    }
}