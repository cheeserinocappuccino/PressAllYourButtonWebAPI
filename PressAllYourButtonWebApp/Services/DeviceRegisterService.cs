using PressAllYourButtonWebApp.DTOs;
using PressAllYourButtonWebApp.Models;
using System.Security.Claims;
namespace PressAllYourButtonWebApp.Services
{
    public class DeviceRegisterService
    {
        PressAYBDbContext dbContext;
        IHttpContextAccessor httpContextAccessor; // To access HttpContext.User
        public DeviceRegisterService(PressAYBDbContext db, IHttpContextAccessor ac)
        {
            dbContext = db;
            httpContextAccessor = ac;
        }

        public async Task<string> RegDevice(DeviceRegDTO value)
        {
            var device = dbContext.Devices.Where(d => d.Id == value.Id).SingleOrDefault();
            if (device == null)
                return "Device Not Exists";
            if (device.Belong_User != null)
                return "Device has been registered";

            // Get Claim value from user's identity
            int userid =int.Parse(httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

            device.Belong_User = userid;
            device.NicknameByUser = value.NicknameByUser;

            await dbContext.SaveChangesAsync();

            string log = string.Format("Device {0} Has been added", value.NicknameByUser);
            

            return log;
        }



    }
}
