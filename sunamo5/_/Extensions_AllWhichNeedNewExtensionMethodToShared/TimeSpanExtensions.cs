using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public static class TimeSpanExtensions
    {
        #region For easy copy from TimeSpanExtensionsSunamo.cs
        public static int TotalYears(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 365.2425);
        }

        public static int TotalMonths(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 30.436875);
        }

        #endregion
    }
}