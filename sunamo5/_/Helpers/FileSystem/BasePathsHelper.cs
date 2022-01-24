using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BasePathsHelper
{
    static Dictionary<string, bool> exists = new Dictionary<string, bool>();
    static string actualPlatform = null;

    static string bpMb => DefaultPaths.bpMb;
    static string bpQ => DefaultPaths.bpQ;
    static string bpVps => DefaultPaths.bpVps;

    static string bpBb => DefaultPaths.bpBb;

    static BasePathsHelper()
    {
        Add(bpMb);
        Add(bpQ);
        Add(bpVps);

        var where = exists.Where(d => d.Value);
        if (where.Count() > 1)
        {
            ThrowEx.Custom("Can't identify platform on which app run");
        }
        else
        {
            actualPlatform = where.First().Key;
        }
    }

    public static bool IsIgnored(string p)
    {
        if (p.StartsWith(bpBb))
        {
            return true;
        }
        return false;
    }

    public static string ConvertToActualPlatform(string s)
    {
        if (s.StartsWith(actualPlatform))
        {
            return s;
        }

        if (s.StartsWith(bpMb))
        {
            return s.Replace(bpMb, actualPlatform);
        }
        else if (s.StartsWith(bpQ))
        {
            return s.Replace(bpMb, actualPlatform);
        }
        else if (s.StartsWith(bpVps))
        {
            return s.Replace(bpVps, actualPlatform);
        }
        else
        {
            ThrowEx.NotImplementedCase(s);
        }
        return null;
    }

    private static void Add(string bpMb)
    {
        exists.Add(bpMb, FS.ExistsDirectoryWorker(bpMb));
    }
}