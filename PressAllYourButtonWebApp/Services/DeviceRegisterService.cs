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

        public async Task<string> RegDeviceAsync(DeviceRegDTO value)
        {
            var device = dbContext.Devices.Where(d => d.Id == value.Id).SingleOrDefault();
            if (device == null)
                return "Device Not Exists";
            if (device.Belong_User != null)
                return "Device has been registered";

            // Get Claim value from current user's identity
            int userid =int.Parse(httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

            device.Belong_User = userid;
            device.NicknameByUser = value.NicknameByUser;

            await dbContext.SaveChangesAsync();

            string log = string.Format("Device {0} Has been added", value.NicknameByUser);
            

            return log;
        }

        public List<DeviceGetDTO> GetOwnDevices()
        {

            int userid = int.Parse(httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());

            // Select device model to a simplified version (DEviceGetDTO) to filter out excessive info 
            var devices = dbContext.Devices.Where(d => d.Belong_User == userid)
                .Select(o => new DeviceGetDTO()
                {
                    Id = o.Id,
                    DeviceTypeName = o.DeviceType.Name,
                    NicknameByUser = o.NicknameByUser
                }).ToList() ;


            return devices;
        }

    }
}
