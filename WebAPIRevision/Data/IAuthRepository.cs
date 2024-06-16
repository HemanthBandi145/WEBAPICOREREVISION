using Microsoft.AspNetCore.Mvc;
using WebAPIRevision.Models;

namespace WebAPIRevision.Data
{
    public interface IAuthRepository
    {
        Task<Users?> AuthenticateUser(string Username, string Password);

        string GenerateToken(Users users);

        Task<Users?> Register(Users user);

    }
}
