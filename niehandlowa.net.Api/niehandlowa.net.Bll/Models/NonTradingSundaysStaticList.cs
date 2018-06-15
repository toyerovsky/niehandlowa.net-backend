using System;
using System.Collections.Generic;
using System.Text;

namespace niehandlowa.net.Bll.Models
{
    public static class NonTradingSundaysStaticList
    {
        public static List<int> NonTradeSundaysList { get; set; }

        static NonTradingSundaysStaticList()
        {
            NonTradeSundaysList = new List<int>()
            {
                70, 77, 91, 98, 105, 112, 133, 140, 161, 168, 189, 196, 203, 224, 231, 252, 259, 266, 287, 294, 315, 322, 343
            };
        }
    }
}
