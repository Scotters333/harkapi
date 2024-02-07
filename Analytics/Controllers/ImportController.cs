using Analytics.Dtos;
using Analytics.Models;
using Analytics.Repositories;
using Analytics.Services;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IFileService _fileService;

        private readonly IEnergyRepository _energyRepo;

        private readonly IWeatherRepository _weatherRepo;

        public ImportController(IFileService fileService, IEnergyRepository energyRepo, IWeatherRepository weatherRepo)
        {
            _fileService = fileService;
            _energyRepo = energyRepo;
            _weatherRepo = weatherRepo;
        }

        // POST: api/weather/importall
        [HttpPost]
        public async Task<ActionResult<Weather>> ImportAll()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var root = Directory.GetCurrentDirectory();

            var energyData = _fileService.GetFile<EnergyDto>($"{root}/Files/HalfHourlyEnergyData.csv");

            var energyEntities = energyData.Select(e => new Energy(e)).ToList();

            await _energyRepo.AddEnergyAsync(energyEntities);

            var anomolies = _fileService.GetFile<EnergyDto>($"{root}/Files/HalfHourlyEnergyDataAnomalies.csv");

            var anomolyData = anomolies.Select(e => e.Timestamp).ToList();

            await _energyRepo.SetAnomoliesAsync(anomolyData);

            var weatherData = _fileService.GetFile<WeatherDto>($"{root}/Files/Weather.csv");

            var weatherEntities = weatherData.Select(w => new Weather(w)).ToList();

            await _weatherRepo.AddWeatherAsync(weatherEntities);

            return Ok();
        }
    }
}