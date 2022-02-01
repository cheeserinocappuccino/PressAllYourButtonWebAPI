using PressAllYourButtonWebApp.Models;
namespace PressAllYourButtonWebApp.DTOs
{
    public class DeviceGetDTO
    {
        public Guid Id { get; set; }
        public string DeviceTypeName { get; set; }
        public string? NicknameByUser { get; set; }
    }
}
