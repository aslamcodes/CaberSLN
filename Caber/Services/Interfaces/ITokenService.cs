using Caber.Models;

namespace Caber.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateUserToken(User token);
    }
}