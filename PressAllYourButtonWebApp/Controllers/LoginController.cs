using Microsoft.AspNetCore.Mvc;
using PressAllYourButtonWebApp.Services;
using PressAllYourButtonWebApp.DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PressAllYourButtonWebApp.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginService loginService;
        public LoginController(PressAYBDbContext p, IHttpContextAccessor h)
        {
            loginService = new LoginService(p, h);
        }



        // POST api/<LoginController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody]LoginInfoDTO value)
        {

            string result = await loginService.Login(value);


            return result;
        }


        
    }
}
