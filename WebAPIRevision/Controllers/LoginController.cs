using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIRevision.Data;
using WebAPIRevision.Models;

namespace WebAPIRevision.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public LoginController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] Users users)
        {
            UserModel userModel = new UserModel(); 
            if (users is null || String.IsNullOrEmpty(users.UserName) || String.IsNullOrEmpty(users.Password))
            {
                return BadRequest(new { message = "Invalid Credentials !!!" });
            }
            Users? AuthenticatedUser =  await authRepository.AuthenticateUser(users.UserName,users.Password);

            if (AuthenticatedUser is not null)
            {
                var token = authRepository.GenerateToken(AuthenticatedUser);
                userModel.AccessToken = token;
                userModel.users = AuthenticatedUser;
                return Ok(userModel);
            }
            
            return BadRequest(new { message = "User not Authenticated !!!" });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] Users user) 
        {
            if (String.IsNullOrEmpty(user.UserName))
            {
                return BadRequest(new { message = "Username should not be empty" });
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Password should not be empty" });
            }
            else if (String.IsNullOrEmpty(user.Role))
            {
                return BadRequest(new { message = "Role should not be empty"});
            }

            Users? userFromDB = await authRepository.Register(user);
            Users? loggedInUser = await authRepository.AuthenticateUser(userFromDB.UserName, userFromDB.Password);
            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { message = "User Registration Successful"});
        }


    }
}
