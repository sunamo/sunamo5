using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;

public partial class ThrowEx
{
    

    #region Must be as first - newly created method fall into this
    public static void BadMappedXaml( string nameControl, string additionalInfo)
    {
        ThrowIsNotNull( stacktrace, Exceptions.BadMappedXaml(FullNameOfExecutedCode(t.Item1, t.Item2, true), nameControl, additionalInfo));
    }

    public static void CannotCreateDateTime( int year, int month, int day, int hour, int minute, int seconds, Exception ex)
    {
        ThrowIsNotNull(Exceptions.CannotCreateDateTime(FullNameOfExecutedCode(t.Item1, t.Item2, true), year, month, day, hour, minute, seconds, ex));
    }

    /// <summary>
    /// TODO: replace FileDoesntExists ->FileOrFolderDoesntExists
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="fulLPath"></param>
    public static void FileDoesntExists( string fulLPath)
    {
        ThrowIsNotNull(Exceptions.FileExists(FullNameOfExecutedCode(t.Item1, t.Item2, true), fulLPath));
    }

    public static void UseRlc()
    {
        ThrowIsNotNull(Exceptions.UseRlc(FullNameOfExecutedCode(t.Item1, t.Item2, true)));
    }
   
    public static bool OutOfRange( string colName, IEnumerable col, string indexName, int index)
    {
        return ThrowIsNotNull(Exceptions.OutOfRange(FullNameOfExecutedCode(t.Item1, t.Item2), colName, col, indexName, index));
    }

    public static void CustomWithStackTrace(Exception ex)
    {
        Custom( Exceptions.TextOfExceptions(ex));
    }

  



    /// <summary>
    /// Return & throw exception whether directory exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="photosPath"></param>
    public static bool DirectoryExists( string path)
    {
        return ThrowIsNotNull(Exceptions.DirectoryExists(FullNameOfExecutedCode(t.Item1, t.Item2, true), path));
    }
    public static void IsWhitespaceOrNull( string variable, object data)
    {
        ThrowIsNotNull(Exceptions.IsWhitespaceOrNull(FullNameOfExecutedCode(t.Item1, t.Item2, true), variable, data));
    }
    
    public static void HaveAllInnerSameCount( List<List<string>> elements)
    {
        ThrowIsNotNull(Exceptions.HaveAllInnerSameCount(FullNameOfExecutedCode(t.Item1, t.Item2, true), elements));
    }
    /// <summary>
    /// Must be string due to in sunamo is not NamespaceElement
    /// </summary>
    /// <param name="name"></param>
    public static void NameIsNotSetted( string nameControl, string nameFromProperty)
    {
        ThrowIsNotNull(Exceptions.NameIsNotSetted(FullNameOfExecutedCode(t.Item1, t.Item2, true), nameControl, nameFromProperty));
    }

    public static void HasNotKeyDictionary<Key,Value>( string nameDict, IDictionary<Key, Value> qsDict, Key remains)
    {
        ThrowIsNotNull(Exceptions.HasNotKeyDictionary<Key, Value>(FullNameOfExecutedCode(t.Item1, t.Item2), nameDict,
            qsDict, remains));
    }

    
    

    public static void DoesntHaveRequiredType( string variableName)
    {
        ThrowIsNotNull(Exceptions.DoesntHaveRequiredType(FullNameOfExecutedCode(t.Item1, t.Item2, true), variableName));
    }
    

    
    public static void MoreThanOneElement( string listName, int count, string moreInfo = Consts.se)
    {
        ThrowIsNotNull(Exceptions.MoreThanOneElement(FullNameOfExecutedCode(t.Item1, t.Item2, true), listName, count, moreInfo));
    }

    public static bool NotInt( string what, object value)
    {
        return ThrowIsNotNull(Exceptions.NotInt(FullNameOfExecutedCode(t.Item1, t.Item2, true), what, value));
    }

    

    /// <summary>
    /// Should always check for null before because otherwise stacktrace and methodName is computed uselessly
    /// must be in code coz Invoke in ThrowIsNotNull should add more lines
    /// NOT checking whether variable is null, but whether is not null! 
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="variableName"></param>
    /// <param name="variable"></param>
    public static void IsNotNull( string variableName, object variable)
    {
        ThrowIsNotNull(Exceptions.IsNotNull(FullNameOfExecutedCode(t.Item1, t.Item2, true), variableName, variable));
    }
    public static void ArrayElementContainsUnallowedStrings( string arrayName, int dex, string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(t.Item1, t.Item2, true), arrayName, dex, valueElement, unallowedStrings));
    }
    public static void OnlyOneElement( string colName, IEnumerable list)
    {
        ThrowIsNotNull(Exceptions.OnlyOneElement(FullNameOfExecutedCode(t.Item1, t.Item2, true), colName, list));
    }
    public static void StringContainsUnallowedSubstrings( string input, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.StringContainsUnallowedSubstrings(FullNameOfExecutedCode(t.Item1, t.Item2, true), input, unallowedStrings));
    }
    /// <summary>
    /// Is used when single (not list etc) bad arg is entered to method
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="valueVar"></param>
    /// <param name="nameVar"></param>
    public static void InvalidParameter( string valueVar, string nameVar)
    {
        ThrowIsNotNull(Exceptions.InvalidParameter(FullNameOfExecutedCode(t.Item1, t.Item2, true), valueVar, nameVar));
    }
    public static void ElementCantBeFound( string nameCollection, string element)
    {
        ThrowIsNotNull(Exceptions.ElementCantBeFound(FullNameOfExecutedCode(t.Item1, t.Item2, true), nameCollection, element));
    }
    //IsNotWindowsPathFormat
    

    

    #endregion
    #region Without parameters
   
    public static void NotSupported()
    {
        ThrowIsNotNull(Exceptions.NotSupported(FullNameOfExecutedCode(t.Item1, t.Item2, true)));
    }
    #endregion
    #region Without locating executing code
    public static void CheckBackslashEnd(string stacktrace, string r)
    {
        ThrowIsNotNull(Exceptions.CheckBackslashEnd("", r));
    }
    #endregion

    public static void WasNotKeysHandler( string name, object keysHandler)
    {
        ThrowIsNotNull(Exceptions.WasNotKeysHandler(FullNameOfExecutedCode(t.Item1, t.Item2), name, keysHandler));
    }

    

    #region Helpers

    public static void IsEmpty( IEnumerable folders, string colName, string additionalMessage)
    {
        ThrowIsNotNull(Exceptions.IsEmpty(FullNameOfExecutedCode(t.Item1, t.Item2, true), folders, colName, additionalMessage));
    }

    public static void NoPassedFolders( IEnumerable folders)
    {
        ThrowIsNotNull(Exceptions.NoPassedFolders(FullNameOfExecutedCode(t.Item1, t.Item2, true), folders));
    }

    public static void RepeatAfterTimeXTimesFailed( int times, int timeoutInMs, string address)
    {
        ThrowIsNotNull(Exceptions.RepeatAfterTimeXTimesFailed(FullNameOfExecutedCode(t.Item1, t.Item2), times, timeoutInMs, address));
    }
    
    /// <summary>
    /// Throw exc A4,5 is same count of elements
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="detailLocation"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static void ElementWasntRemoved( string detailLocation, int before, int after)
    {
        ThrowIsNotNull(Exceptions.ElementWasntRemoved(FullNameOfExecutedCode(t.Item1, t.Item2, true), detailLocation, before, after));
    }
   
    public static void FolderCantBeRemoved( string folder)
    {
        ThrowIsNotNull(Exceptions.FolderCantBeRemoved(FullNameOfExecutedCode(t.Item1, t.Item2, true), folder));
    }
    public static void FileHasExtensionNotParseableToImageFormat( string fnOri)
    {
        ThrowIsNotNull(Exceptions.FileHasExtensionNotParseableToImageFormat(FullNameOfExecutedCode(t.Item1, t.Item2), fnOri));
    }
    public static void FileSystemException( Exception ex)
    {
        ThrowIsNotNull(Exceptions.FileSystemException(FullNameOfExecutedCode(t.Item1, t.Item2), ex));
    }
    public static void FuncionalityDenied( string description)
    {
        ThrowIsNotNull(Exceptions.FuncionalityDenied(FullNameOfExecutedCode(t.Item1, t.Item2), description));
    }

    

    public static void CannotMoveFolder( string item, string nova, Exception ex)
    {
        ThrowIsNotNull(Exceptions.CannotMoveFolder(FullNameOfExecutedCode(t.Item1, t.Item2), item, nova, ex));

    }

    public static void ExcAsArg(Exception ex, string p = Consts.se)
    {
        var mth = new StackTrace().GetFrame(1).GetMethod();
        var cls = mth.ReflectedType.Name;
        ThrowIsNotNull(Exc.GetStackTrace(), Exceptions.ExcAsArg(FullNameOfExecutedCode(cls, mth.Name), ex, p));
    }


    #endregion
}