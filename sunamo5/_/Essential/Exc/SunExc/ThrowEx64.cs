using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public partial class ThrowEx
{
    #region For easy copy from ThrowEx64.cs
    public static void NotImplementedCase(object niCase)
    {
        ThrowIsNotNull(Exceptions.NotImplementedCase, niCase);
    }

    public static Tuple<string, string, string> t = null;

    public static void Custom(string v)
    {
        ThrowIsNotNull(Exceptions.Custom, v);
    }



    private static void ThrowIsNotNull(Func<string, string> f)
    {
        ThrowExceptions.ThrowIsNotNullEx(f);
    }

    private static void ThrowIsNotNull(Func<string, object, string> f, object o)
    {
        ThrowExceptions.ThrowIsNotNullEx(f, o);
    }

    private static void ThrowIsNotNull(Func<string, string, string> f, string a1)
    {
        ThrowExceptions.ThrowIsNotNullEx(f, a1);
    }

    /// <summary>
    /// true if everything is OK
    /// false if some error occured
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <param name="a1"></param>
    /// <param name="a2"></param>
    /// <returns></returns>
    private static bool ThrowIsNotNull(Func<string, string, IEnumerable, string> f, string a1, IEnumerable a2)
    {
        return ThrowExceptions.ThrowIsNotNullEx(f, a1, a2);
    }

    private static void ThrowIsNotNull<T>(Func<string, string, T[], string> f, string a1, params T[] a2)
    {
        ThrowExceptions.ThrowIsNotNullEx(f, a1, a2);
    }

    private static string FullNameOfExecutedCode()
    {
        return ThrowExceptions.FullNameOfExecutedCode(t.Item1, t.Item2, true);
    }

    public static void NotImplementedMethod()
    {
        ThrowIsNotNull(Exceptions.NotImplementedMethod);
    }
    #endregion
}
