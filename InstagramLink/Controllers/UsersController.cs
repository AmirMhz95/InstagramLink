using Microsoft.AspNetCore.Mvc;
using InstagramLink.Models; // Adjust the namespace as needed
using InstagramLink.Services; // Adjust the namespace as needed

namespace InstagramLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.Register(user);
            if (result)
            {
                return Ok("User registered successfully");
            }
            return BadRequest("User registration failed");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.Login(loginRequest.Username, loginRequest.Password);
            if (result)
            {
                return Ok("Login successful");
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}