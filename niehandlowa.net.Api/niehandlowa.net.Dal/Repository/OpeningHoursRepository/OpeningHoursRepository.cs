using niehandlowa.net.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace niehandlowa.net.Dal.Repository.OpeningHoursRepository
{
    public class OpeningHoursRepository : BaseRepository<OpeningHoursEntity>, IOpeningHoursRepository
    {
        public OpeningHoursRepository(NonTradeContext context) : base(context)
        {
        }

    }
}
