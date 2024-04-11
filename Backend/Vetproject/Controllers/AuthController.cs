using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Vetproject.Contracts;
using Vetproject.Data.Repository;
using Vetproject.Model;
using Vetproject.Service.Authentication;

namespace Vetproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IConfiguration configuration, IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userRole = _configuration["RoleSettings:UserRole"];
                var result = await _authService.RegisterAsync(request.Email, request.UserName, request.Password,
                    request.BirthDate, request.Address, userRole!);

                if (!result.Success)
                    return BadRequest(result.ErrorMessages);

                return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration.");
                return StatusCode(500, "An unexpected error occurred during registration.");
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);

            return Ok(new AuthResponse(result.Email, result.UserName, result.Token));
        }

        [Authorize]
        [HttpGet("GetUserByEmail/{userEmail}")]
        public async Task<ActionResult<ApplicationUser>> GetUserByEmail(string userEmail)
        {
            Console.WriteLine("im backend");
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(userEmail);
                if (user == null)
                    return NotFound("No user found in the database");

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error with finding user by email");
                return BadRequest("Error with finding user by email");
            }
        }

        [HttpDelete("DeleteUserByEmail")]
        public async Task<IActionResult> DeleteUserByEmailAsync(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email is required.");
            }

            try
            {
                var userToDelete = await _userRepository.GetUserByEmailAsync(userEmail);

                if (userToDelete == null)
                {
                    _logger.LogError("User with email {UserEmail} not found in the database", userEmail);
                    return NotFound($"User with email {userEmail} not found in the database");
                }

                await _userRepository.DeleteUserAsync(userToDelete);
                return Ok($"User with email {userEmail} successfully deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error with deleting user with email {UserEmail}", userEmail);
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }


        [Authorize(Roles = "User, Admin")]
        [HttpPatch("DeactivateAccount")]
        public async Task<ActionResult<DeactivationResponse>> Deactivate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.DeactivateAsync(request.Email, request.Password);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);

            return Ok(new DeactivationResponse(result.UserName));
        }
    }
}
