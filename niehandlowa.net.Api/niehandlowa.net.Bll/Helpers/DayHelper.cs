using niehandlowa.net.Bll.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace niehandlowa.net.Bll.Helpers
{
    public static class DayHelper
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool IsSundayTrading(DateTime sunday)
        {
            if (sunday.DayOfWeek != DayOfWeek.Sunday)
            {
                throw new Exception($"{sunday.DayOfWeek} is not a sunday!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }

            if (NonTradingSundaysStaticList.NonTradeSundaysList.Contains(sunday.DayOfYear))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNextSundayTrading()
        {
            var nextSunday = StartOfWeek(DateTime.Now, DayOfWeek.Monday).AddDays(6);
            return IsSundayTrading(nextSunday);
        }
    }
}
