namespace WeatherApp.Data.Entities
{
    public class AppRequest : BaseEntity
    {
        public string Ip { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
