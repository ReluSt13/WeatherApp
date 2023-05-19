namespace WeatherApp.Data.DataTransferObjects
{
    public class WeatherForecastParameters
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int ForecastDays { get; set; }
    }
}
