using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace niehandlowa.net.Dal.Entities
{
    public class POIEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Type { get; set; }
        public virtual ICollection<OpeningHoursEntity> OpeningHours { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public byte[] Image { get; set; }
        public string Desription { get; set; }
    }
}
