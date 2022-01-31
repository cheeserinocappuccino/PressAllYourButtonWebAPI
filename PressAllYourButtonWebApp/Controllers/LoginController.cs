using Microsoft.AspNetCore.Mvc;
using PressAllYourButtonWebApp.Services;
using PressAllYourButtonWebApp.DTOs;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PressAllYourButtonWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginService loginService;
        public LoginController(PressAYBDbContext p, IHttpContextAccessor h, IConfiguration config)
        {
            loginService = new LoginService(p, h, config);
        }



        
        [HttpPost]
        public async Task<ActionResult<string>> LoginAsync([FromBody]LoginInfoDTO value)
        {

            string result = await loginService.LoginAsync(value);


            return result;

        }

        [Authorize]
        [HttpPost("/logout")]
        public async Task<ActionResult<string>> LogOutAsync()
        {

            string result = await loginService.LogoutAsync();


            return result;
        }

        [HttpPost("/signup")]
        public ActionResult<string> SignUp(SignUpInfoDTO dto)
        {
            string result = loginService.SignUp(dto);

            return result;

        }

    }
}
