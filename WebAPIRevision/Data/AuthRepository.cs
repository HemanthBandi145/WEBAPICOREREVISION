using Microsoft.AspNetCore.Mvc;
using WebAPIRevision.Models;


namespace WebAPIRevision.Data
{
    using BCrypt.Net;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public AuthRepository(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async Task<Users?> AuthenticateUser(string Username, string Password)
        { 
            Users users = await userRepository.GetUserbyUsernameAsync(Username);
            if (users == null || BCrypt.Verify(Password, users.Password) == false)
            {
                return null;
            }

            return users;
        }

        public string GenerateToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],null,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Users?> Register(Users user)
        {
            user.Password = BCrypt.HashPassword(user.Password);
            int? Result = await userRepository.CreateNewUser(user);
            if (Result !=0 && Result is not null )
            {
                 return user;
            }
            return null;
        }


    }
}
