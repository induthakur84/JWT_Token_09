using JWT_Token.DTO;
using JWT_Token.Services.IServices;
using Microsoft.AspNetCore.Http;
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


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userInterface.Register(userRegisterDto);
            return Ok(result);

        }
    }
}
