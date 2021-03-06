namespace PressAllYourButtonWebApp.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }    
        public byte[] Iv { get; set; }

        public bool Verified { get; set; }

        public IEnumerable<Device> Devices { get; set; }

    }
}
