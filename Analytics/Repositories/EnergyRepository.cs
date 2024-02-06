using Analytics.Models;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Repositories
{
    public class EnergyRepository : IEnergyRepository
    {
        private readonly DataContext _context;

        public EnergyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Energy>> GetEnergyByDates(IEnumerable<DateTime> dates)
        {
            return await _context.Energy.Where(e => dates.Contains(e.Date)).ToListAsync();
        }

        public async Task AddEnergyAsync(IEnumerable<Energy> energyData)
        {
            await _context.Energy.AddRangeAsync(energyData);
            await _context.SaveChangesAsync();
        }

        public async Task SetAnomoliesAsync(IEnumerable<DateTime> dates)
        {
            var energyData = (await GetEnergyByDates(dates)).ToList();

            if (energyData.Count != 0)
            {
                foreach (var val in energyData)
                {
                    val.IsAnomaly = true;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
