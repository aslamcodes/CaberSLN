namespace Caber.Models.DTOs.Mappers
{
    public static class PassengerDtoMappers
    {
        public static PassengerRegisterResponseDto ToPassengerRegisterResponseDto(this Passenger passenger)
        {
            return new PassengerRegisterResponseDto
            {
                PassengerId = passenger.Id,
                UserId = passenger.UserId
            };
        }
    }
}
