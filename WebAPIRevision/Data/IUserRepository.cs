using WebAPIRevision.Models;

namespace WebAPIRevision.Data
{
    public interface IUserRepository
    {
        Task<Users> GetUserbyUsernameAsync(string Username);
        Task<int> CreateNewUser(Users users);
    }
}
