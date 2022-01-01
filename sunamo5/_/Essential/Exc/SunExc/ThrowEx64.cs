using System;
using System.Collections.Generic;
using System.Text;


    public partial class ThrowEx
    {
        #region For easy copy from ThrowEx64.cs
        public static void NotImplementedCase(object niCase)
        {
            //Type type = methodBase.DeclaringType;
            //string methodName = methodBase.Name;
            ThrowIsNotNull(t.Item3, Exceptions.NotImplementedCase(FullNameOfExecutedCode(), niCase));
        }

        static Tuple<string, string, string> t = null;

        public static void Custom(string v)
        {
            //Type type = methodBase.DeclaringType;
            //string methodName = methodBase.Name;
            ThrowIsNotNull(t.Item3, Exceptions.Custom(FullNameOfExecutedCode(), v));
        }

        private static void ThrowIsNotNull(string stacktrace, string v)
        {
            ThrowExceptions.ThrowIsNotNull(stacktrace, v);
        }

        private static string FullNameOfExecutedCode()
        {
            return ThrowExceptions.FullNameOfExecutedCode(t.Item1, t.Item2, true);
        }

        public static void NotImplementedMethod()
        {
            string stacktrace = Exc.GetStackTrace(true);

            ThrowIsNotNull(stacktrace, Exceptions.NotImplementedMethod(FullNameOfExecutedCode()));
        }
        #endregion
    }
