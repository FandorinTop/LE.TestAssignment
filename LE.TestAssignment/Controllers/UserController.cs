using LE.Common.Api.Users;
using LE.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LE.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var id = await _userService.CreateAsync(userDto);

            return Ok(id);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var res = await _userService.Login(request);

            return Ok(res);
        }
    }
}
