namespace WeatherApp.Data.DataTransferObjects
{
    public class LocationDTO
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public bool IsCapital { get; set; }
    }
}
