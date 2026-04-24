namespace HotelAPI.Helpers
{
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }
        public string Host { get; set; }   // ✅ ADD THIS
        public int Port { get; set; }      // ✅ ADD THIS
    }
}