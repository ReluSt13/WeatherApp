namespace WeatherApp.Data.Entities
{
    public class ThirdPartyRequest : BaseEntity
    {
        public string Code { get; set; }
        public string Parameters { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
