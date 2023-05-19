namespace WeatherApp.Data.DataTransferObjects
{
    public class WeatherForecastDTO
    {
        public List<double> TemperatureC { get; set; }
        public List<double> ApparentTemperatureC { get; set; }
        public List<int> PrecipitationProbability { get; set; }
        public List<double> WindSpeed { get; set; }
        public List<int> WindDirection { get; set; }
        public List<double> RelativeHumidity { get; set; }
        public List<double> SurfacePressure { get; set; }

    }
}
