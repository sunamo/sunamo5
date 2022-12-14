using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowEx
{
    /// <summary>
    /// A1 have to be Dictionary<T,U>, not IDictionary without generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="en"></param>
    /// <param name="dictName"></param>
    /// <param name="key"></param>
    public static void KeyNotFound<T, U>(string stacktrace, object type, string v, IDictionary<T, U> en, string dictName, T key)
    {
        ThrowIsNotNull(Exceptions.KeyNotFound(FullNameOfExecutedCode(type, v), en, dictName, key));
    }

    public static void NotValidXml( string path, Exception ex)
    {
        bool v = ThrowIsNotNull(Exceptions.NotValidXml(FullNameOfExecutedCode(t.Item1, t.Item2), path, ex));
    }

    #region For easy copy in SunamoException project
    //[SuppressMessage(type, "IDE0060")]
#pragma warning disable
    public static void DummyNotThrow(Exception ex)
    {

    }
#pragma warning enable

    public static void NotImplementedMethod()
    {
        ThrowIsNotNull(Exceptions.NotImplementedMethod(FullNameOfExecutedCode(t.Item1, t.Item2)));
    }

   

    
    /// <summary>
    /// Verify whether A3 contains A4
    /// true if everything is OK
    /// false if some error occured
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="p"></param>
    /// <param name="after"></param>
    public static bool NotContains(string stacktrace, object type, string v, string p, params string[] after)
    {
        return ThrowIsNotNull(Exceptions.NotContains(FullNameOfExecutedCode(type, v, true), p, after));
    }

    

    /// <summary>
    /// Default use here method with one argument
    /// Return false in case of exception, otherwise true
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="exception"></param>
    //public static bool ThrowIsNotNull( string exception)
    //{
    //    if (exception != null)
    //    {
            
    //        ThrowEx.Custom(exception, cm);
    //        return false;
    //    }
    //    return true;
    //}
    #endregion
}