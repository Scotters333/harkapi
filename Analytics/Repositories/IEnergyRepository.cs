using Analytics.Models;

namespace Analytics.Repositories
{
    public interface IEnergyRepository
    {
        Task AddEnergyAsync(IEnumerable<Energy> energyData);

        Task SetAnomoliesAsync(IEnumerable<DateTime> dates);
    }
}
