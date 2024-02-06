using Analytics.Dtos;
using Analytics.Models;
using Analytics.Repositories;
using Analytics.Services;

namespace Analytics.Commands
{
    public class ImportWeatherCommand : IImportWeatherCommand
    {
        private readonly IFileService _fileService;

        private readonly IWeatherRepository _repository;

        public ImportWeatherCommand(IFileService fileService, IWeatherRepository repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<IEnumerable<Weather>> ExecuteAsync(IFormFileCollection file)
        {
            var weatherData = _fileService.GetFiles<WeatherDto>(file);

            var weatherEntities = weatherData.Select(w => new Weather(w)).ToList();

            await _repository.AddWeatherAsync(weatherEntities);

            return weatherEntities;
        }
    }
}
