using BudgetTracker.Api.DTO;
using BudgetTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Api.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ApiVersion("1.0")]

    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<UsersDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        [ProducesResponseType(typeof(UsersDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto user)
        {
            var result = await _usersService.AddUserAsync(user);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user, int id)
        {
            if (user == null)
                return BadRequest("Request body is missing or malformed.");
            user.Id = id;
            var updatedUser = await _usersService.UpdateUserAsync(user);
            if (updatedUser == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _usersService.DeleteUserAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("UserLogin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLogin)
        {
            if (userLogin == null || string.IsNullOrWhiteSpace(userLogin.Email) || string.IsNullOrWhiteSpace(userLogin.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var result = await _usersService.UserLoginAsync(userLogin);
            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(result);
        }
    }
}
