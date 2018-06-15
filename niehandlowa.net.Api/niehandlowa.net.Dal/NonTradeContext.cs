using Microsoft.EntityFrameworkCore;

namespace niehandlowa.net.Dal
{
    public class NonTradeContext : DbContext
    {
        public NonTradeContext(DbContextOptions options) : base(options)
        {
        }


    }
}