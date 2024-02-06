using Analytics.Dtos;

namespace Analytics.Models
{
    public class Weather : IEntity
    {
        public Weather() {}

        public Weather(WeatherDto weather)
        {
            Date = weather.Date;
            Temperature = weather.AverageTemperature;
            Humidity = weather.AverageHumidity;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }
    }
}
