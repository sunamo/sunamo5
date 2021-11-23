using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


public class ThrowEx
{
    public static void NotImplementedCase(MethodBase mi, object niCase)
    {
        string stacktrace = Exc.GetStackTrace(true);

        Type type = mi.DeclaringType;
        string methodName = mi.Name;
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedCase(FullNameOfExecutedCode(type, methodName, true), niCase));
    }

    private static void ThrowIsNotNull(string stacktrace, string v)
    {
        ThrowExceptions.ThrowIsNotNull(stacktrace, v);
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool v)
    {
        return ThrowExceptions.FullNameOfExecutedCode(type, methodName, v);
    }
}
