using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.Models.Session;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User(model.Username, model.Email, model.Sub);

            var result = await _userService.RegisterUserAsync(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.User);
        }
    }
}
