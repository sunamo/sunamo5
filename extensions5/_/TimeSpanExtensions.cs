using System;

public static partial class TimeSpanExtensions
{
    

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