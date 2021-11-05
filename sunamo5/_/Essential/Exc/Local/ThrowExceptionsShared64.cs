using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowExceptions
{
    public static void IsNotAllowed(string stacktrace, object type, string methodName, string what)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNotAllowed(FullNameOfExecutedCode(type, methodName, true), what));
    }

    public static void BadFormatOfElementInList(string stacktrace, object type, string methodName, object elVal, string listName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.BadFormatOfElementInList(FullNameOfExecutedCode(type, methodName), elVal, listName));
    }

    /// <summary>
    /// return true if exception was thrown
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="colName"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static bool IsOdd(string stacktrace, object type, string methodName, string colName, IEnumerable col)
    {
        return ThrowIsNotNull(stacktrace, Exceptions.IsOdd(FullNameOfExecutedCode(type, methodName), colName, col));
    }



    public static readonly Type type = typeof(ThrowExceptions);
    
    static readonly string dot = ".";

    public static void IsTheSame(string stacktrace, object type, string methodName, string fst, string sec)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsTheSame(FullNameOfExecutedCode(type, methodName), fst, sec));
    }

    

    public static void WrongNumberOfElements(string stacktrace, Type type, string methodName, int requireElements, string nameCount, IEnumerable<string> ele)
    {
        ThrowIsNotNull(stacktrace, Exceptions.WrongNumberOfElements(FullNameOfExecutedCode(type, methodName), requireElements, nameCount, ele));
    }

    

    

    

    

    /// <summary>
    /// Return & throw exception whether
    /// directory NOT exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="folder1"></param>
    public static void DirectoryWasntFound(string stacktrace, object type, string methodName, string folder1)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DirectoryWasntFound(FullNameOfExecutedCode(type, methodName, true), folder1));
    }



    public static void DivideByZero(string stacktrace, object type, string methodName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DivideByZero(FullNameOfExecutedCode(type, methodName, true)));
    }
    
    

    

    public static void ViolationSqlIndex(string stacktrace, object type, string methodName, string tableName, ABC columnsInIndex)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ViolationSqlIndex(FullNameOfExecutedCode(type, methodName, true), tableName, columnsInIndex));
    }

    public static void Custom(string stacktrace, object type, string methodName, Exception message, bool reallyThrow = true)
    {
        Custom(stacktrace, type, methodName, Exceptions.TextOfExceptions(message), reallyThrow);
    }

    

    public static bool WrongExtension(string stacktrace, Type type, string methodName, string path, string ext)
    {
        return ThrowIsNotNull(stacktrace, Exceptions.WrongExtension(FullNameOfExecutedCode(type, methodName, true), path, ext));
    }

    
}
