using Analytics.Models;

namespace Analytics.Commands
{
    public interface IImportEnergyCommand
    {
        public Task<IEnumerable<Energy>> ExecuteAsync(IFormFileCollection file);
    }
}
