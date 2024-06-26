using Caber.Models;

namespace Caber.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByEmail(string email);
    }
}
