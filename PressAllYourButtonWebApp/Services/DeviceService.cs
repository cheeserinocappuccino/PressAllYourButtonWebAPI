using PressAllYourButtonWebApp.DTOs;
using PressAllYourButtonWebApp.Models;

namespace PressAllYourButtonWebApp.Services
{
    public class DeviceService
    {
        PressAYBDbContext dbContext;
        public DeviceService(PressAYBDbContext db)
        {
            dbContext = db;
        }

        public async Task<string> RegDevice(DeviceRegDTO value)
        {
            var device = dbContext.Devices.Where(d => d.Id == value.Id).SingleOrDefault();
            if (device == null)
                return "Device Not Exists";
            if (device.Belong_User != null)
                return "Device has been registered";

            device.Belong_User = value.userId;
            device.NicknameByUser = value.NicknameByUser;

            await dbContext.SaveChangesAsync();

            string log = string.Format("Device {0} Has been added", value.NicknameByUser);


            return log;
        }



    }
}
