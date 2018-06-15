using Microsoft.EntityFrameworkCore;
using niehandlowa.net.Dal.Entities;

namespace niehandlowa.net.Dal
{
    public class NonTradeContext : DbContext
    {
        public NonTradeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<POIEntity> POIEntities { get; set; }
        public DbSet<OpeningHoursEntity> OpeningHours { get; set; }
    }
}