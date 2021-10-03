﻿using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
namespace sunamo.Essential
{ }
public partial class ThrowExceptions
{
    #region For easy copy from ThrowExceptions.cs
    #region DifferentCountInLists
    public static void DifferentCountInLists(string stacktrace, object type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName, true), namefc, countfc, namesc, countsc));
    }

    public static void DifferentCountInLists(string stacktrace, object type, string methodName, string namefc, IEnumerable replaceFrom, string namesc, IEnumerable replaceTo)
    {
        DifferentCountInLists(stacktrace, type, methodName, namefc, replaceFrom.Count(), namesc, replaceTo.Count());
    }
    #endregion 
    #endregion

    #region Must be as first - newly created method fall into this
    public static void BadMappedXaml(string stacktrace, object type, string methodName, string nameControl, string additionalInfo)
    {
        ThrowIsNotNull(stacktrace, Exceptions.BadMappedXaml(FullNameOfExecutedCode(type, methodName, true), nameControl, additionalInfo));
    }
    public static void FileDoesntExists(string stacktrace, object type, string methodName, string fulLPath)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FileExists(FullNameOfExecutedCode(type, methodName, true), fulLPath));
    }

    public static void UseRlc(string stacktrace, object type, string methodName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.UseRlc(FullNameOfExecutedCode(type, methodName, true)));
    }
   
    public static bool OutOfRange(string stacktrace, object type, string methodName, string colName, IEnumerable col, string indexName, int index)
    {
        return ThrowIsNotNull(stacktrace, Exceptions.OutOfRange(FullNameOfExecutedCode(type, methodName), colName, col, indexName, index));
    }

    public static void CustomWithStackTrace(Exception ex)
    {
        Custom( Exc.GetStackTrace(), type, Exc.CallingMethod(), Exceptions.TextOfExceptions(ex));
    }

  



    /// <summary>
    /// Return & throw exception whether directory exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="photosPath"></param>
    public static bool DirectoryExists(string stacktrace, object type, string methodName, string path)
    {
        return ThrowIsNotNull(stacktrace, Exceptions.DirectoryExists(FullNameOfExecutedCode(type, methodName, true), path));
    }
    public static void IsWhitespaceOrNull(string stacktrace, object type, string methodName, string variable, object data)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsWhitespaceOrNull(FullNameOfExecutedCode(type, methodName, true), variable, data));
    }
    
    public static void HaveAllInnerSameCount(string stacktrace, object type, string methodName, List<List<string>> elements)
    {
        ThrowIsNotNull(stacktrace, Exceptions.HaveAllInnerSameCount(FullNameOfExecutedCode(type, methodName, true), elements));
    }
    /// <summary>
    /// Must be string due to in sunamo is not NamespaceElement
    /// </summary>
    /// <param name="name"></param>
    public static void NameIsNotSetted(string stacktrace, object type, string methodName, string nameControl, string nameFromProperty)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NameIsNotSetted(FullNameOfExecutedCode(type, methodName, true), nameControl, nameFromProperty));
    }

    public static void HasNotKeyDictionary<Key,Value>(string stacktrace, object type, string methodName, string nameDict, IDictionary<Key, Value> qsDict, Key remains)
    {
        ThrowIsNotNull(stacktrace, Exceptions.HasNotKeyDictionary<Key, Value>(FullNameOfExecutedCode(type, methodName), nameDict,
            qsDict, remains));
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
       return  ThrowIsNotNull(stacktrace, Exceptions.IsOdd(FullNameOfExecutedCode(type, methodName), colName, col));
    }
    

    public static void DoesntHaveRequiredType(string stacktrace, object type, string methodName, string variableName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.DoesntHaveRequiredType(FullNameOfExecutedCode(type, methodName, true), variableName));
    }
    

    public static void IsNotAllowed(string stacktrace, object type, string methodName, string what)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNotAllowed(FullNameOfExecutedCode(type, methodName, true), what));
    }
    public static void MoreThanOneElement(string stacktrace, object type, string methodName, string listName, int count)
    {
        ThrowIsNotNull(stacktrace, Exceptions.MoreThanOneElement(FullNameOfExecutedCode(type, methodName, true), listName, count));
    }

    public static bool NotInt(string stacktrace, object type, string methodName, string what, object value)
    {
        return ThrowIsNotNull(stacktrace, Exceptions.NotInt(FullNameOfExecutedCode(type, methodName, true), what, value));
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
    public static void IsNotNull(string stacktrace, object type, string methodName, string variableName, object variable)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsNotNull(FullNameOfExecutedCode(type, methodName, true), variableName, variable));
    }
    public static void ArrayElementContainsUnallowedStrings(string stacktrace, object type, string methodName, string arrayName, int dex, string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(type, methodName, true), arrayName, dex, valueElement, unallowedStrings));
    }
    public static void OnlyOneElement(string stacktrace, object type, string methodName, string colName, IEnumerable list)
    {
        ThrowIsNotNull(stacktrace, Exceptions.OnlyOneElement(FullNameOfExecutedCode(type, methodName, true), colName, list));
    }
    public static void StringContainsUnallowedSubstrings(string stacktrace, object type, string methodName, string input, params string[] unallowedStrings)
    {
        ThrowIsNotNull(stacktrace, Exceptions.StringContainsUnallowedSubstrings(FullNameOfExecutedCode(type, methodName, true), input, unallowedStrings));
    }
    /// <summary>
    /// Is used when single (not list etc) bad arg is entered to method
    /// </summary>
    /// <param name="stacktrace"></param>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="valueVar"></param>
    /// <param name="nameVar"></param>
    public static void InvalidParameter(string stacktrace, object type, string methodName, string valueVar, string nameVar)
    {
        ThrowIsNotNull(stacktrace, Exceptions.InvalidParameter(FullNameOfExecutedCode(type, methodName, true), valueVar, nameVar));
    }
    public static void ElementCantBeFound(string stacktrace, object type, string methodName, string nameCollection, string element)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ElementCantBeFound(FullNameOfExecutedCode(type, methodName, true), nameCollection, element));
    }
    //IsNotWindowsPathFormat
    

    

    #endregion
    #region Without parameters
   
    public static void NotSupported(string stacktrace, Type type, string methodName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NotSupported(FullNameOfExecutedCode(type, methodName, true)));
    }
    #endregion
    #region Without locating executing code
    public static void CheckBackslashEnd(string stacktrace, string r)
    {
        ThrowIsNotNull(stacktrace, Exceptions.CheckBackslashEnd("", r));
    }
    #endregion

    public static void WasNotKeysHandler(string stacktrace, object type, string methodName, string name, object keysHandler)
    {
        ThrowIsNotNull(stacktrace, Exceptions.WasNotKeysHandler(FullNameOfExecutedCode(type, methodName), name, keysHandler));
    }

    
    #region Helpers

    public static void IsEmpty(string stacktrace, object type, string methodName, IEnumerable folders, string colName, string additionalMessage)
    {
        ThrowIsNotNull(stacktrace, Exceptions.IsEmpty(FullNameOfExecutedCode(type, methodName, true), folders, colName, additionalMessage));
    }

    public static void NoPassedFolders(string stacktrace, object type, string methodName, IEnumerable folders)
    {
        ThrowIsNotNull(stacktrace, Exceptions.NoPassedFolders(FullNameOfExecutedCode(type, methodName, true), folders));
    }

    public static void RepeatAfterTimeXTimesFailed(string stacktrace, object type, string methodName, int times, int timeoutInMs, string address)
    {
        ThrowIsNotNull(stacktrace, Exceptions.RepeatAfterTimeXTimesFailed(FullNameOfExecutedCode(type, methodName), times, timeoutInMs, address));
    }
    
    /// <summary>
    /// Throw exc A4,5 is same count of elements
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="detailLocation"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static void ElementWasntRemoved(string stacktrace, object type, string methodName, string detailLocation, int before, int after)
    {
        ThrowIsNotNull(stacktrace, Exceptions.ElementWasntRemoved(FullNameOfExecutedCode(type, methodName, true), detailLocation, before, after));
    }
   
    public static void FolderCantBeRemoved(string stacktrace, object type, string methodName, string folder)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FolderCantBeRemoved(FullNameOfExecutedCode(type, methodName, true), folder));
    }
    public static void FileHasExtensionNotParseableToImageFormat(string stacktrace, object type, string methodName, string fnOri)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FileHasExtensionNotParseableToImageFormat(FullNameOfExecutedCode(type, methodName), fnOri));
    }
    public static void FileSystemException(string stacktrace, object type, string methodName, Exception ex)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FileSystemException(FullNameOfExecutedCode(type, methodName), ex));
    }
    public static void FuncionalityDenied(string stacktrace, object type, string methodName, string description)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FuncionalityDenied(FullNameOfExecutedCode(type, methodName), description));
    }

    public static void BadFormatOfElementInList(string stacktrace, object type, string methodName, object elVal, string listName)
    {
        ThrowIsNotNull(stacktrace, Exceptions.BadFormatOfElementInList(FullNameOfExecutedCode(type, methodName), elVal, listName));
    }
    #endregion
}