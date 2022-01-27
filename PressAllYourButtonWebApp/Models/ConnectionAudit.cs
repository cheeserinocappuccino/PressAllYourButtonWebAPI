namespace PressAllYourButtonWebApp.Models
{
    public class ConnectionAudit
    {
        public int Id { get; set; }
        public DateTime ActionTime { get; set; }

        public ActionTypeEnum ActionType { get; set; }


    }

    public enum ActionTypeEnum : byte
    { 
        Connected = 1,
        Disconnected = 0
    }
}
