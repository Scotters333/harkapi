using Analytics.Models;

namespace Analytics.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly DataContext _context;

        public WeatherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddWeatherAsync(IEnumerable<Weather> weatherData)
        {
            await _context.Weather.AddRangeAsync(weatherData);
            await _context.SaveChangesAsync();
        }
    }
}
