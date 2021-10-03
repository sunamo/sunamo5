﻿using System;

public static class TimeSpanExtensions
{
    public static int TotalYears(this TimeSpan timespan)
    {
        return (int)((double)timespan.Days / 365.2425);
    }
    public static int TotalMonths(this TimeSpan timespan)
    {
        return (int)((double)timespan.Days / 30.436875);
    }

    public static string ToNiceString(this TimeSpan timeSpan)
    {
        string ret = timeSpan.ToString();
        string secondPostfix = ":00";
        if (ret.EndsWith(secondPostfix))
        {
            ret = ret.Substring(0, ret.Length - secondPostfix.Length);
        }
        return ret;
    }
}