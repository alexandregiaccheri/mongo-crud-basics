using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Services;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            return Ok(await _userService.CreateUserAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var result = await _userService.GetUserAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _userService.DeleteUserAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var result = await _userService.GetUserAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] User user)
        {
            var result = await _userService.GetUserAsync(id);
            if (result == null)
                return NotFound();
            return await _userService.UpdateUserAsync(id, user);
        }
    }
}
