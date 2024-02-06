using Analytics.Models;

namespace Analytics.Repositories
{
    public interface IWeatherRepository
    {
        Task AddWeatherAsync(IEnumerable<Weather> weatherData);
    }
}
