using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowExceptions
{
    #region For easy copy from ThrowExceptionsShared64.cs - all ok 17-10-21
   

    public static void DifferentCountInLists(string stacktrace, object type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName, true), namefc, countfc, namesc, countsc));
    }

    public static void DifferentCountInLists(string stacktrace, object type, string methodName, string namefc, IEnumerable replaceFrom, string namesc, IEnumerable replaceTo)
    {
        DifferentCountInLists(stacktrace, type, methodName, namefc, replaceFrom.Count(), namesc, replaceTo.Count());
    }

    public static void Custom(string stacktrace, object type, string methodName, string message, bool reallyThrow = true)
    {
        ThrowIsNotNull( stacktrace, Exceptions.Custom(FullNameOfExecutedCode(type, methodName, true), message), reallyThrow);
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
        ThrowIsNotNull(t.Item3, exc);
    }

    public static void ThrowIsNotNullEx(Func<string, string> f)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2));
        ThrowIsNotNull(t.Item3, exc);
    }

    public static void ThrowIsNotNullEx(Func<string, string, string> f, string a1)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), a1);
        ThrowIsNotNull(t.Item3, exc);
    }

    public static void ThrowIsNotNullEx<T>(Func<string, string, T[], string> f, string a1, params T[] a2)
    {
        t = Exc.GetStackTrace2(true);

        var exc = f(FullNameOfExecutedCode(t.Item1, t.Item2), a1, a2);
        ThrowIsNotNull(t.Item3, exc);
    }

    static string lastMethod = null;

    /// <summary>
    /// true if everything is OK
    /// false if some error occured
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="exception"></param>
    public static bool ThrowIsNotNull(string stacktrace, string exception, bool reallyThrow = true)
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
                    throw new Exception(exception);
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
    public static void IsNotWindowsPathFormat(string stacktrace, object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNotWindowsPathFormat(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }

    public static void IsNullOrEmpty(string stacktrace, object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNullOrEmpty(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }

    public static void ArgumentOutOfRangeException(string stacktrace, object type, string methodName, string paramName, string message = null)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ArgumentOutOfRangeException(FullNameOfExecutedCode(type, methodName, true), paramName, message));
    }

    public static void IsNull(string stacktrace, object type, string methodName, string variableName, object variable = null)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNull(FullNameOfExecutedCode(type, methodName, true), variableName, variable));
    }

#pragma warning disable
    /// <summary>
    /// CA2211	Non-constant fields should not be visible
    /// IDE0044	Make field readonly
    /// 
    /// Must be public due to GlobalAsaxHelper
    /// </summary>
    public static Action<string, string> writeServerError;
#pragma warning enable

    public static void NotImplementedCase(string stacktrace, object type, string methodName, object niCase)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedCase(FullNameOfExecutedCode(type, methodName, true), niCase));
    }

    /// <summary>
    /// First can be Method base, then A2 can be anything
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    public static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowExceptions = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowExceptions)
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