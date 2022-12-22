using sunamo;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

public partial class ThrowEx
{
    #region For easy copy from ThrowExShared64.cs - all ok 17-10-21


    public static void DifferentCountInLists( string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(Exceptions.DifferentCountInLists(FullNameOfExecutedCode(t.Item1, t.Item2, true), namefc, countfc, namesc, countsc));
    }

    public static void DifferentCountInLists( string namefc, IEnumerable replaceFrom, string namesc, IEnumerable replaceTo)
    {
        DifferentCountInLists(ThrowIsNotNull( namefc, replaceFrom.Count(), namesc, replaceTo.Count());
    }

    public static void Custom( string message, bool reallyThrow = true)
    {
        ThrowIsNotNull(Exceptions.Custom(FullNameOfExecutedCode(t.Item1, t.Item2, true), message), reallyThrow);
    }

    public static bool reallyThrow2 = true;

#if MB
    static void ShowMb(string s)
    {
        
        TranslateDictionary.ShowMb(s);
    }
#endif

    public static Tuple<string, string, string> t
    {
        get
        {
            return ThrowEx.t;
        }
        set
        {
            ThrowEx.t = value;
        }
    }

    public static void ThrowIsNotNullEx(Func<string, object, string> f, object o)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), o);
        ThrowIsNotNull( exc);
    }

    public static void ThrowIsNotNullEx(Func<string, string> f)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2));
        ThrowIsNotNull( exc);
    }

    public static void ThrowIsNotNullEx(Func<string, string, string> f, string a1)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), a1);
        ThrowIsNotNull( exc);
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
    public static bool ThrowIsNotNullEx(Func<string, string, IEnumerable, string> f, string a1, IEnumerable a2)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), a1, a2);
        return ThrowIsNotNull( exc);
    }

    public static void ThrowIsNotNullEx<T>(Func<string, string, T[], string> f, string a1, params T[] a2)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), a1, a2);
        ThrowIsNotNull( exc);
    }

    static string lastMethod = null;

    /// <summary>
    /// true if everything is OK
    /// false if some error occured
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="exception"></param>
    public static bool ThrowIsNotNull( string exception, bool reallyThrow = true)
    {
        // Výjimky se tak často nevyhazují. Tohle je daň za to že jsem tu měl arch
        // jež nebyla dobře navržená. V ThrowEx se to již podruhé volat nebude.

        if (exception != null)
        {
            t = Exc.GetStackTrace2(true);
            var cm = t.Item2;
            if (lastMethod == cm)
            {
#if MB
                ShowMb("lastMethod == cm");
#endif
                return false;
            }
            else
            {
#if MB
                if (lastMethod == null)
                {
                    ShowMb("lastMethod = " + Consts.nulled);
                }
                else
                {
                    ShowMb("lastMethod = " + lastMethod.ToString());
                }
#endif
                lastMethod = cm;
            }

            if (Exc.aspnet)
            {
                exception = exception.Replace("Violation of PRIMARY KEY constraint", ShortenedExceptions.ViolationOfPK);

                // Will be written in globalasax error
                writeServerError(stacktrace, exception);

                /*
reallyThrow - method arg
reallyThrow2 - is setted in ShowMb and all excs handlers in WpfAppShared.cs
                 */

                if (reallyThrow && reallyThrow2)
                {
                    throw new Exception(exception);
                }
            }
            else
            {
#if MB
                //ShowMb($"reallyThrow = {reallyThrow} && reallyThrow2 = {reallyThrow2}");
#endif

                if (reallyThrow && reallyThrow2)
                {
#if MB
                    ShowMb("Throw exc");
#endif
                    if (showExceptionWindow != null)
                    {
                        var nl = Environment.NewLine;

                        showExceptionWindow(stacktrace + nl + nl + exception);
                    }
                    else
                    {
                        throw new Exception(exception);
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// !FS.IsWindowsPathFormat
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="argName"></param>
    /// <param name="argValue"></param>
    public static void IsNotWindowsPathFormat( string argName, string argValue)
    {
        ThrowIsNotNull(Exceptions.IsNotWindowsPathFormat(FullNameOfExecutedCode(t.Item1, t.Item2, true), argName, argValue));
    }

    public static void IsNullOrEmpty( string argName, string argValue)
    {
        ThrowIsNotNull(Exceptions.IsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2, true), argName, argValue));
    }

    public static void ArgumentOutOfRangeException( string paramName, string message = null)
    {
        ThrowIsNotNull(Exceptions.ArgumentOutOfRangeException(FullNameOfExecutedCode(t.Item1, t.Item2, true), paramName, message));
    }

    public static void IsNull( string variableName, object variable = null)
    {
        ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(t.Item1, t.Item2, true), variableName, variable));
    }

#pragma warning disable
    /// <summary>
    /// CA2211	Non-constant fields should not be visible
    /// IDE0044	Make field readonly
    /// 
    /// Must be public due to GlobalAsaxHelper
    /// </summary>
    public static Action<string, string> writeServerError;
    static Action<object> _showExceptionWindow = null;
    public static Action<object> showExceptionWindow
    { 
        get
        {
            return _showExceptionWindow;
        }
        set
        { 
            #if DEBUG
            _showExceptionWindow = null;
            #else
            _showExceptionWindow = value;
            #endif
        }
    }
#pragma warning enable

    public static void NotImplementedCase( object niCase)
    {
        ThrowIsNotNull(Exceptions.NotImplementedCase(FullNameOfExecutedCode(t.Item1, t.Item2, true), niCase));
    }

    /// <summary>
    /// First can be Method base, then A2 can be anything
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    public static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowEx)
            {
                depth++;
            }
            methodName = Exc.CallingMethod(depth);
        }
        string typeFullName = string.Empty;
        if (type is Type)
        {
            var type2 = ((Type)type);
            typeFullName = type2.FullName;
        }
        else if (type is MethodBase)
        {
            MethodBase method = (MethodBase)type;
            typeFullName = method.ReflectedType.FullName;
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString();
        }
        else
        {
            Type t = type.GetType();
            typeFullName = t.FullName;
        }
        return string.Concat(typeFullName, dot, methodName);
    }
    #endregion
}