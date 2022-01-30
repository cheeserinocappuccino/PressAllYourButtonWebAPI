using Microsoft.AspNetCore.Mvc;
using PressAllYourButtonWebApp.Services;
using PressAllYourButtonWebApp.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PressAllYourButtonWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {

        SignUpService signUpService;
        public SignUpController(PressAYBDbContext db, IConfiguration config)
        {
            signUpService = new SignUpService(db, config);
        }



        [HttpPost]
        public ActionResult<string> SignUp(SignUpInfoDTO dto)
        {
            string result = signUpService.SignUp(dto);

            return result;

        }
    }
}
