using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;




namespace MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _authService.UserExists(request.Username))
                return BadRequest("User already exists.");

            var newUser = await _authService.Register(request.Username, request.Email, request.Password);
            return Ok(new { newUser.Id, newUser.Username, newUser.Email });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Login(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { token });
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
