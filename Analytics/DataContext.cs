using Analytics.Models;
using Microsoft.EntityFrameworkCore;

namespace Analytics
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; } = null!;

        public DbSet<Energy> Energy { get; set; } = null!;
    }
}
