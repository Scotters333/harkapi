using Analytics.Dtos;
using Analytics.Models;
using Analytics.Repositories;
using Analytics.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Analytics.Commands
{
    public class ImportEnergyCommand : IImportEnergyCommand
    {
        private readonly IFileService _fileService;

        private readonly IEnergyRepository _repository;

        public ImportEnergyCommand(IFileService fileService, IEnergyRepository repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<IEnumerable<Energy>> ExecuteAsync(IFormFileCollection file)
        {
            var energyData = _fileService.GetFiles<EnergyDto>(file);

            var energyEntities = energyData.Select(e => new Energy(e)).ToList();

            await _repository.AddEnergyAsync(energyEntities);

            return energyEntities;
        }
    }
}
