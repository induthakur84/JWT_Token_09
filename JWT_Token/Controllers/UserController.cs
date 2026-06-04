using JWT_Token.DTO;
using JWT_Token.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _userInterface;
        public UserController(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [Authorize("Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userInterface.Register(userRegisterDto);
            return Ok(result);

        }

        //httpget

        //api/user/login/username:user/password:

        //http post
        //api/user/login


        [HttpPost("login")]


        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var result = await _userInterface.Login(loginRequestDto);
                return Ok(new
                {
                    message = "Login successful",
                    Token = result.Token,
                    User = result.User
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Login failed",
                    error = ex.Message
                });
            }
        }
    }
}
