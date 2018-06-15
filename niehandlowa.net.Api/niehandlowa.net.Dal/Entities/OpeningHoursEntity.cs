using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace niehandlowa.net.Dal.Entities
{
    public class OpeningHoursEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int DayOfWeek { get; set; } // 1 - poniedziałek, 2 - wtorek ... 6 - sobota, 7 - niedziela handlowa, 8 - niedziela niehandlowa
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int POIId { get; set; }
        public virtual POIEntity POI { get; set; }
}
}
