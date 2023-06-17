using Microsoft.AspNetCore.Mvc;
using Roy.Enterprise.API.Authorization;
using Roy.Enterprise.API.Models;
using Roy.Enterprise.API.Services;

namespace Roy.Enterprise.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtUtils _jwtUtils;

        public UsersController(IUserService userService, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var user = _userService.Authenticate(model);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });


            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(user);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("/Register")]
        public IActionResult Register([FromBody] UserModel model)
        {
            var user = _userService.RegisterUser(model);
            return Ok(user);
        }
    }
}
