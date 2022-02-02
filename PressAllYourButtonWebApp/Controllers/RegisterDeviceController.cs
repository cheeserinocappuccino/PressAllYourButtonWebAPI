using Microsoft.AspNetCore.Mvc;
using PressAllYourButtonWebApp.DTOs;
using PressAllYourButtonWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PressAllYourButtonWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterDeviceController : ControllerBase
    {
        DeviceRegisterService service;

        public RegisterDeviceController(PressAYBDbContext db, IHttpContextAccessor ac)
        {
            service = new DeviceRegisterService(db, ac);
        }

        [Authorize]
        [HttpGet("~/GetMyDevices")]
        public ActionResult<List<DeviceGetDTO>> GetMyDevices() 
        {
            var result = service.GetOwnDevices();
            

            return result;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterDevices(DeviceRegDTO deviceDto)
        {

            string result = await service.RegDeviceAsync(deviceDto);

            return result;
        }

        [Authorize]
        [HttpPut("~/DisownDevice")]
        public async Task<ActionResult<string>> DisownDevice([FromBody]Guid id)
        {

            string result = await service.DisownDevice(id);

            return result;
        }

    }
}
