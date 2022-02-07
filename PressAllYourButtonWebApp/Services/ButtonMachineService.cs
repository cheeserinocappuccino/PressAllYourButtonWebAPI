using PressAllYourButtonWebApp.Models;

namespace PressAllYourButtonWebApp.Services
{
    

    public class ButtonMachineService
    {
        PressAYBDbContext dbContext;
        public ButtonMachineService(PressAYBDbContext db)
        {
            dbContext = db;
        }

        public string InstantDeviceActionCommandHandler(IDeviceActionCommand command)
        {
            return "";
        }

    }
}
