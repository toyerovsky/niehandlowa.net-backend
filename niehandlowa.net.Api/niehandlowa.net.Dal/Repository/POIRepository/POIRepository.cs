using niehandlowa.net.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace niehandlowa.net.Dal.Repository.POIRepository
{
    public class POIRepository : BaseRepository<POIEntity>, IPOIRepository
    {
        public POIRepository(NonTradeContext context) : base(context)
        {
        }
    }
}
