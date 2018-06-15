using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace niehandlowa.net.Api.Models.JsonHackathonDataModels
{
    public class JsonGalerie
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class GalerieModel
    {
        public string json_featuretype { get; set; }
        public int FID { get; set; }
        public int ID { get; set; }
        public string KOD_POCZTO { get; set; }
        public string NAZWA { get; set; }
        public string NR_BUDYNKU { get; set; }
        public string TELEFON { get; set; }
        public string ULICA { get; set; }
        public string json_ogc_wkt_crs { get; set; }
        public JsonGeometry json_geometry { get; set; }
    }
}
