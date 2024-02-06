using Analytics.Models;

namespace Analytics.Commands
{
    public interface IImportWeatherCommand
    {
        public Task<IEnumerable<Weather>> ExecuteAsync(IFormFileCollection file);
    }
}
