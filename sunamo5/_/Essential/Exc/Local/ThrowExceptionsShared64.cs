using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowEx
{
    public static void IsNotAllowed( string what)
    {
        ThrowIsNotNull(Exceptions.IsNotAllowed(FullNameOfExecutedCode(t.Item1, t.Item2, true), what));
    }

    public static void BadFormatOfElementInList( object elVal, string listName)
    {
        ThrowIsNotNull(Exceptions.BadFormatOfElementInList(FullNameOfExecutedCode(t.Item1, t.Item2), elVal, listName));
    }

    /// <summary>
    /// return true if exception was thrown
    /// odd - lichý, even - sudý
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="colName"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static bool IsOdd( string colName, IEnumerable col)
    {
        return ThrowIsNotNull(Exceptions.IsOdd(FullNameOfExecutedCode(t.Item1, t.Item2), colName, col));
    }



    public static readonly Type type = typeof(ThrowEx);
    
    static readonly string dot = ".";

    public static void IsTheSame( string fst, string sec)
    {
        ThrowIsNotNull(Exceptions.IsTheSame(FullNameOfExecutedCode(t.Item1, t.Item2), fst, sec));
    }

    

    public static void WrongNumberOfElements(string stacktrace, Type type, string methodName, int requireElements, string nameCount, IEnumerable<string> ele)
    {
        ThrowIsNotNull(Exceptions.WrongNumberOfElements(FullNameOfExecutedCode(t.Item1, t.Item2), requireElements, nameCount, ele));
    }

    

    

    

    

    /// <summary>
    /// Return & throw exception whether
    /// directory NOT exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="folder1"></param>
    public static void DirectoryWasntFound( string folder1)
    {
        ThrowIsNotNull(Exceptions.DirectoryWasntFound(FullNameOfExecutedCode(t.Item1, t.Item2, true), folder1));
    }



    public static void DivideByZero()
    {
        ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode(t.Item1, t.Item2, true)));
    }
    
    

    

    public static void ViolationSqlIndex( string tableName, ABC columnsInIndex)
    {
        ThrowIsNotNull(Exceptions.ViolationSqlIndex(FullNameOfExecutedCode(t.Item1, t.Item2, true), tableName, columnsInIndex));
    }

    public static void Custom( Exception message, bool reallyThrow = true)
    {
        Custom(ThrowIsNotNull( Exceptions.TextOfExceptions(message), reallyThrow);
    }

    

    public static bool WrongExtension(string stacktrace, Type type, string methodName, string path, string ext)
    {
        return ThrowIsNotNull(Exceptions.WrongExtension(FullNameOfExecutedCode(t.Item1, t.Item2, true), path, ext));
    }

    
}
