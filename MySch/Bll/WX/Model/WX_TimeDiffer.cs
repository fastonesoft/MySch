using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Time
    {
        public static double TimeDiffer(DateTime begin, DateTime end)
        {
            var tb = new TimeSpan(begin.Ticks);
            var te = new TimeSpan(end.Ticks);
            return tb.Subtract(te).Duration().TotalSeconds;
        }

    }
}