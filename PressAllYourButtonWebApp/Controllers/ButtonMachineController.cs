using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PressAllYourButtonWebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PressAllYourButtonWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ButtonMachineController : ControllerBase
    {
        ButtonMachineService service;
        public ButtonMachineController(PressAYBDbContext db)
        {
            service = new ButtonMachineService(db);
        }


        
    }
}
