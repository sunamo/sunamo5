using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


public partial class ThrowEx
{
    public static void NotImplementedCase(MethodBase methodBase, object niCase)
    {
        string stacktrace = Exc.GetStackTrace(true);

        Type type = methodBase.DeclaringType;
        string methodName = methodBase.Name;
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedCase(FullNameOfExecutedCode(type, methodName, true), niCase));
    }

    public static void Custom(MethodBase methodBase, string v)
    {
        string stacktrace = Exc.GetStackTrace(true);

        Type type = methodBase.DeclaringType;
        string methodName = methodBase.Name;
        ThrowIsNotNull(stacktrace, Exceptions.Custom(FullNameOfExecutedCode(type, methodName, true), v));
    }

    private static void ThrowIsNotNull(string stacktrace, string v)
    {
        ThrowExceptions.ThrowIsNotNull(stacktrace, v);
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool v)
    {
        return ThrowExceptions.FullNameOfExecutedCode(type, methodName, v);
    }

    public static void NotImplementedMethod(MethodBase methodBase)
    {
        string stacktrace = Exc.GetStackTrace(true);

        Type type = methodBase.DeclaringType;
        string methodName = methodBase.Name;
        ThrowIsNotNull(stacktrace, Exceptions.NotImplementedMethod(FullNameOfExecutedCode(type, methodName, true)));
    }

    
}
