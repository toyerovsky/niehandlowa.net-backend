using niehandlowa.net.Dal.Repository.OpeningHoursRepository;
using niehandlowa.net.Dal.Repository.POIRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace niehandlowa.net.Dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IOpeningHoursRepository OpeningHoursRepository { get; }
        IPOIRepository POIRepository { get; }
        int Comit();
        Task<int> ComitAsync();
    }
}
