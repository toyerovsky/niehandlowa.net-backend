using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using niehandlowa.net.Dal.Repository.OpeningHoursRepository;
using niehandlowa.net.Dal.Repository.POIRepository;

namespace niehandlowa.net.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NonTradeContext _dbContext;

        private readonly IOpeningHoursRepository _openingHoursRepository;
        private readonly IPOIRepository _POIRepository;

        public UnitOfWork(NonTradeContext dbContext)
        {
            _dbContext = dbContext;
            _openingHoursRepository = new OpeningHoursRepository(_dbContext);
            _POIRepository = new POIRepository(_dbContext);
        }

        public IOpeningHoursRepository OpeningHoursRepository
        {
            get
            {
                return _openingHoursRepository;
            }
        }

        public IPOIRepository POIRepository
        {
            get
            {
                return _POIRepository;
            }
        }

        public int Comit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> ComitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
