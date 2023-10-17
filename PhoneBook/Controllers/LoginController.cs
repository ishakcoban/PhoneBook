using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.request;
using PhoneBook.Core.response;
using PhoneBook.Services.Abstract;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            ResponseMessage response = await this.loginService.Login(loginRequest);

            return StatusCode(response.Status, response);
        }

    }
}