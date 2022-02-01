namespace PressAllYourButtonWebApp.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public UserInfo userinfo { get; set; }
        public int? Belong_User { get; set;}
        public DateTime? Manufacture_Date { get; set; }

        public DeviceType? DeviceType { get; set; }

        public int? DeviceType_id { get; set; }

        public string? NicknameByUser { get; set; }

    }
}
