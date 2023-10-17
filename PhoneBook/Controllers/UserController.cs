using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.request;
using PhoneBook.Core.response;
using PhoneBook.Services.Abstract;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    [Route("api/v1/users")]

    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> addUser([FromBody] AddUserRequest addUserRequest)
        {
            ResponseMessage response = await this.userService.AddUser(addUserRequest);
            return StatusCode(response.Status, response);
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> getAllUsers()
        {
            ResponseMessage response = await this.userService.GetAllUsers();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUserById([FromRoute] int id)
        {
            ResponseMessage response = await this.userService.GetUserById(id);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser([FromRoute] int id)
        {
            ResponseMessage response = await this.userService.DeleteUser(id);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateUser([FromBody] AddUserRequest addUserRequest, [FromRoute] int id)
        {
            ResponseMessage response = await this.userService.UpdateUser(addUserRequest, id);
            return StatusCode(response.Status, response);
        }

    }
}






