namespace WeatherApp.Data.DataTransferObjects.OpenMeteoDTOs
{
    public class HourlyWeatherForecastDTO
    {
        public List<double> temperature_2m { get; set; }
        public List<double> apparent_temperature { get; set; }
        public List<int> precipitation_probability { get; set; }
        public List<double> windspeed_10m { get; set; }
        public List<int> winddirection_10m { get; set; }
        public List<double> relativehumidity_2m { get; set; }
        public List<double> surface_pressure { get; set; }
        public List<int> weathercode { get; set; }
    }
}
