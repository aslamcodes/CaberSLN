using Caber.Exceptions;
using Caber.Models;
using Caber.Models.DTOs;
using Caber.Models.DTOs.Mappers;
using Caber.Repositories;
using Caber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Caber.Services
{
    class AuthService(ITokenService tokenService, IRepository<int, User> userRepository) : IAuthService
    {
        public async Task<AuthResponseDto> Login(LoginRequestDto loginDto)
        {
            try
            {
                var userDB = await userRepository.GetByKey(loginDto.Id);

                HMACSHA512 hMACSHA = new(userDB.PasswordHashKey);

                var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

                if (!userDB.IsPasswordCorrect(encrypterPass))
                {
                    throw new UnauthorizedUserException();
                }

                var token = tokenService.GenerateUserToken(userDB);

                return userDB.MapToAuthResponse(token);
            }

            catch (Exception)
            {
                throw;
            }

        }

        public async Task<AuthResponseDto> Register(RegisterRequestDto registerDto)
        {
            try
            {
                HMACSHA512 hMACSHA = new();

                var user = new User()
                {
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    PasswordHashKey = hMACSHA.Key,
                    Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password))
                };

                user = await userRepository.Add(user);

                var token = tokenService.GenerateUserToken(user);

                return user.MapToAuthResponse(token);
            }
            catch (DbUpdateException)
            {
                throw new CannotRegisterUserException();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}