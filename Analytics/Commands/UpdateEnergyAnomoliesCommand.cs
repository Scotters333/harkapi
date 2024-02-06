using Analytics.Dtos;
using Analytics.Models;
using Analytics.Repositories;
using Analytics.Services;

namespace Analytics.Commands
{
    public class UpdateEnergyAnomoliesCommand : IUpdateEnergyAnomoliesCommand
    {
        private readonly IFileService _fileService;

        private readonly IEnergyRepository _repository;

        public UpdateEnergyAnomoliesCommand(IFileService fileService, IEnergyRepository repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<IEnumerable<DateTime>> ExecuteAsync(IFormFileCollection file)
        {
            var energyData = _fileService.GetFiles<EnergyDto>(file);

            var anomolyData = energyData.Select(e => e.Timestamp).ToList();

            await _repository.SetAnomoliesAsync(anomolyData);

            return anomolyData;
        }
    }
}
