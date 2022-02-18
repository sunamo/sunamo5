using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SunamoExceptions
{
    public class Exc
    {
        public static bool aspnet = false;
    }
}

public class Exc
{
    public static bool aspnet
    {
        get => SunamoExceptions.Exc.aspnet;
        set => SunamoExceptions.Exc.aspnet = value;
    }

    #region For easy copy in SunamoException project
    static bool first = true;
    static StringBuilder sb = new StringBuilder();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stopAtFirstSystem"></param>
    /// <returns></returns>
    public static string GetStackTrace(bool stopAtFirstSystem = false)
    {
        var r = GetStackTrace2(false);
        return r.Item3;
    }

    /// <summary>
    /// type, methodName, stacktrace
    /// Remove GetStackTrace (first line
    /// </summary>
    /// <returns></returns>
    public static Tuple<string, string, string> GetStackTrace2(bool fillAlsoFirstTwo, bool stopAtFirstSystem = false)
    {
        if (stopAtFirstSystem)
        {
            if (first)
            {
                first = false;
            }
        }

        StackTrace st = new StackTrace();

        var v = st.ToString();
        var l = GetLines(v);
        Trim(l);
        l.RemoveAt(0);

        int i = 0;

        string type = null;
        string methodName = null;

        for (; i < l.Count; i++)
        {
            var item = l[i];

            if (fillAlsoFirstTwo)
            {
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            }

            if (item.StartsWith(CsConsts.atSystemDot))
            {
                //Tohle nevím k čemu to je, vypadá to jako kdyby mě to dělalo duplikát již existujícího
                //l = l.Take(i).ToList();
                l.Add(string.Empty);
                l.Add(string.Empty);

                break;
            }

        }

        return new Tuple<string, string, string>(type, methodName, JoinNL(l));
    }

    public static void TypeAndMethodName(string l, out string type, out string methodName)
    {
        var s = SH.TrimStart(l, "   at ");
        s = SH.RemoveAfterFirst(s, AllChars.lb);
        var p = SH.Split(s, AllChars.dot);

        methodName = p[p.Count - 1];
        p.RemoveAt(p.Count - 1);
        type = SH.Join(AllStrings.dot, p);
    }

    public static bool _trimTestOnEnd = true;

    /// <summary>
    /// Print name of calling method, not GetCurrentMethod
    /// If is on end Test, will trim
    /// </summary>
    public static string CallingMethod(int v = 1)
    {
        StackTrace stackTrace = new StackTrace();
        MethodBase methodBase = stackTrace.GetFrame(v).GetMethod();
        var methodName = methodBase.Name;
        if (_trimTestOnEnd)
        {
            methodName = TrimEnd(methodName, XlfKeys.Test);
        }
        return methodName;
    }

    #region MyRegion


    public static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext))
        {
            return name.Substring(0, name.Length - ext.Length);
        }
        return name;
    }

    public static object lockObject = new object();

    private static string JoinNL(List<string> l)
    {
        sb.Clear();
        foreach (var item in l)
        {
            sb.AppendLine(item);
        }
        var r = string.Empty;
        lock (lockObject)
        {
            ;
            r = sb.ToString();
        }
        return r;
    }

    public static List<string> Trim(List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = l[i].Trim();
        }
        return l;
    }

    public static string MethodOfOccuredFromStackTrace(string exc)
    {
        var st = SH.FirstLine(exc);
        var dx = st.IndexOf(" in ");
        if (dx != -1)
        {
            st = SH.SubstringIfAvailable(st, dx);
        }
        return st;
    }

    private static List<string> GetLines(string v)
    {
        var l = v.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        return l;
    }
    #endregion 
    #endregion
}