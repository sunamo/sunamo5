﻿using HtmlAgilityPack;
using sunamo.Collections;
using sunamo.Data;
using sunamo.Helpers.Number;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
public static partial class CA
{


    //public static object FirstOrNull(IEnumerable e)
    //{
    //    if (e == null)
    //    {
    //        return null;
    //    }

    //    //var tName = e.GetType().Name;
    //    //if (ThreadHelper.NeedDispatcher(tName))
    //    //{
    //    //    var result = CA.dFirstOrNull(e);
    //    //    return result;
    //    //}

    //    return e.FirstOrNull();
    //}
    public static void KeepOnlyWordsToFirstSpecialChars(List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = SH.RemoveAfterFirst(l[i], CharHelper.IsSpecial);
        }
    }

    /// <summary>
    /// jagged = zubaty
    /// Change from array where every element have two spec of location to ordinary array with inner array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    public static T[][] ToJagged<T>( T[,] value)
    {
        if (Object.ReferenceEquals(null, value))
            return null;
        // Jagged array creation
        T[][] result = new T[value.GetLength(0)][];
        for (int i = 0; i < value.GetLength(0); ++i)
            result[i] = new T[value.GetLength(1)];
        // Jagged array filling
        for (int i = 0; i < value.GetLength(0); ++i)
            for (int j = 0; j < value.GetLength(1); ++j)
                result[i][j] = value[i, j];
        return result;
    }

    public static List<string> LinesIndexes(List<string> cOnlyNamesBy10, int from, int to, bool indexedFrom1)
    {
        if (indexedFrom1)
        {
            from--;
            to--;
        }

        List<string> s = new List<string>();

        for (int i = from; i < to+1; i++)
        {
            s.Add(cOnlyNamesBy10[i]);
        }

        return s;
    }

    // In order to convert any 2d array to jagged one
    // let's use a generic implementation
    public static List<List<int>> ToJagged( bool[,] value)
    {
        List<List<int>> result = new List<List<int>>();
        for (int i = 0; i < value.GetLength(0); i++)
        {
            List<int> ca = new List<int>();
            for (int y = 0; y < value.GetLength(1); y++)
            {
                ca.Add(BTS.BoolToInt( value[i, y]));
            }
            result.Add(ca);
        }
        return result;
    }
    
    /// <summary>
    /// A1 are column names for ValuesTableGrid (not letter sorted a,b,.. but left column (Name, Rating, etc.)
    /// A2 are data
    /// </summary>
    /// <param name="captions"></param>
    /// <param name="exists"></param>
    public static string SwitchForGoogleSheets(List<string> captions, List<List<string>> exists)
    {
        ValuesTableGrid<string> vtg = new ValuesTableGrid<string>(exists);
        vtg.captions = captions;
        DataTable dt = vtg.SwitchRowsAndColumn();
        StringBuilder sb = new StringBuilder();
        foreach (DataRow item in dt.Rows)
        {
            JoinForGoogleSheetRow(sb, item.ItemArray);
        }
        string vr = sb.ToString();
        //////DebugLogger.Instance.WriteLine(vr);
        return vr;
    }
    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="input"></param>
    public static string GetNumberedList(List<string> input, int startFrom)
    {
        CA.RemoveStringsEmpty2(input);
        CA.PrependWithNumbered(input, startFrom);
        return SH.JoinNL(input);
    }
    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="input"></param>
    private static void PrependWithNumbered(List<string> input, int startFrom)
    {
        var numbered = BTS.GetNumberedListFromTo(startFrom, input.Count - 1, ") ");
        Prepend(numbered, input);
    }
    public static ABL<string, string> CompareListDifferent(List<string> c1, List<string> c2)
    {
        List<string> existsIn1 = new List<string>();
        List<string> existsIn2 = new List<string>();
        int dex = -1;
        for (int i = c2.Count - 1; i >= 0; i--)
        {
            string item = c2[i];
            dex = c1.IndexOf(item);
            if (dex == -1)
            {
                existsIn2.Add(item);
            }
        }
        for (int i = c1.Count - 1; i >= 0; i--)
        {
            string item = c1[i];
            dex = c2.IndexOf(item);
            if (dex == -1)
            {
                existsIn1.Add(item);
            }
        }
        ABL<string, string> abl = new ABL<string, string>();
        abl.a = existsIn1;
        abl.b = existsIn2;
        return abl;
    }

    /// <summary>
    /// Get every duplicated item once
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="clipboardL"></param>
    /// <returns></returns>
    public static IList<T> GetDuplicities<T>(List<T> clipboardL)
    {
        List<T> alreadyProcessed;
        return GetDuplicities<T>(clipboardL, out alreadyProcessed);
    }

    /// <summary>
    /// Get every item once
    /// A2 = more duplicities = more items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="clipboardL"></param>
    /// <param name="alreadyProcessed"></param>
    /// <returns></returns>
        public static IList<T> GetDuplicities<T>(List<T> clipboardL, out List<T> alreadyProcessed)
    {
        alreadyProcessed = new List<T>(clipboardL.Count);
        CollectionWithoutDuplicates<T> duplicated = new CollectionWithoutDuplicates<T>();
        foreach (var item in clipboardL)
        {
            if (alreadyProcessed.Contains(item))
            {
                duplicated.Add(item);
            }
            else
            {
                alreadyProcessed.Add(item);
            }
        }
        return duplicated.c;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="v"></param>
    /// <param name="l"></param>
    /// <returns></returns>
    public static List<string> StartingWith(string v, List<string> l)
    {
        for (int i = l.Count - 1; i >= 0; i--)
        {
            if (!l[i].StartsWith(v))
            {
                l.RemoveAt(i);
            }
        }
        return l;
    }

    

    /// <summary>
    /// A2,3 can be null, then no header will be append
    /// A4 nameOfSolution - main header, also can be null
    /// 
    /// </summary>
    /// <param name="alsoFileNames"></param>
    /// <param name="nameForFirstFolder"></param>
    /// <param name="nameForSecondFolder"></param>
    /// <param name="nameOfSolution"></param>
    /// <param name="files1"></param>
    /// <param name="files2"></param>
    /// <param name="inBoth"></param>
    public static string CompareListResult(bool alsoFileNames, string nameForFirstFolder, string nameForSecondFolder, string nameOfSolution, List<string> files1, List<string> files2, List<string> inBoth)
    {
        int files1Count = files1.Count;
        int files2Count = files2.Count;
        string result;
        TextOutputGenerator textOutput = new TextOutputGenerator();
        int inBothCount = inBoth.Count;
        double sumBothPlusManaged = inBothCount + files2Count;
        PercentCalculator percentCalculator = new PercentCalculator(sumBothPlusManaged);
        if (nameOfSolution != null)
        {
            textOutput.sb.AppendLine(nameOfSolution);
        }
        textOutput.sb.AppendLine("Both (" + inBothCount + AllStrings.swda + percentCalculator.PercentFor(inBothCount, false) + "%):");
        if (alsoFileNames)
        {
            textOutput.List(inBoth);
        }
        if (nameForFirstFolder != null)
        {
            textOutput.sb.AppendLine(nameForFirstFolder + AllStrings.lb + files1Count + AllStrings.swda + percentCalculator.PercentFor(files1Count, true) + "%):");
        }
        if (alsoFileNames)
        {
            textOutput.List(files1);
        }
        if (nameForSecondFolder != null)
        {
            textOutput.sb.AppendLine(nameForSecondFolder + AllStrings.lb + files2Count + AllStrings.swda + percentCalculator.PercentFor(files2Count, true) + "%):");
        }
        if (alsoFileNames)
        {
            textOutput.List(files2);
        }
        textOutput.SingleCharLine(AllChars.asterisk, 10);
        result = textOutput.ToString();
        return result;
    }

    
    public static List<string> PaddingByEmptyString(List<string> list, int columns)
    {
        for (int i = list.Count - 1; i < columns - 1; i++)
        {
            list.Add(string.Empty);
        }
        return list;
    }
    public static int CountOfEnding(List<string> winrarFiles, string v)
    {
        int count = 0;
        for (int i = 0; i < winrarFiles.Count; i++)
        {
            if (winrarFiles[i].EndsWith(v))
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="list"></param>
    public static List<string> OnlyFirstCharUpper(List<string> list)
    {
        return ChangeContent(list, SH.OnlyFirstCharUpper);
    }
    public static bool IsInRange(int od, int to, int index)
    {
        return od >= index && to <= index;
    }
    public static List<T> CreateListAndInsertElement<T>(T el)
    {
        List<T> t = new List<T>();
        t.Add(el);
        return t;
    }
    public static List<string> DummyElementsCollection(int count)
    {
        return Enumerable.Repeat<string>(string.Empty, count).ToList();
    }
    
    /// <summary>
    /// Return equal ranges of in A1  
    /// </summary>
    /// <param name="contentOneSpace"></param>
    /// <param name="r"></param>
    public static List<FromTo> EqualRanges<T>(List<T> contentOneSpace, List<T> r)
    {
        List<FromTo> result = new List<FromTo>();
        int? dx = null;
        var r_first = r[0];
        int startAt = 0;
        int valueToCompare = 0;
        for (int i = 0; i < contentOneSpace.Count; i++)
        {
            var _contentOneSpace = contentOneSpace[i];
            if (!dx.HasValue)
            {
                if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r_first))
                {
                    dx = i + 1; // +2;
                    startAt = i;
                }
            }
            else
            {
                valueToCompare = dx.Value - startAt;
                if (r.Count > valueToCompare)
                {
                    if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r[valueToCompare]))
                    {
                        dx++;
                    }
                    else
                    {
                        dx = null;
                        i--;
                    }
                }
                else
                {
                    int dx2 = (int)dx;
                    result.Add(new FromTo(dx2 - r.Count + 1, dx2, FromToUse.None));
                    dx = null;
                }
            }
        }
        foreach (var item in result)
        {
            item.from--;
            item.to--;
        }
        return result;
    }
    /// <summary>
    /// Is useful when want to wrap and also join with string. Also last element will have delimiter
    /// </summary>
    /// <param name="list"></param>
    /// <param name="wrapWith"></param>
    /// <param name="delimiter"></param>
    public static List<string> WrapWithAndJoin(IEnumerable<string> list, string wrapWith, string delimiter)
    {
        return list.Select(i => wrapWith + i + wrapWith + delimiter).ToList();
    }
    public static int PartsCount(int count, int inPart)
    {
        int celkove = count / inPart;
        if (count % inPart != 0)
        {
            celkove++;
        }
        return celkove;
    }
    public static List<string> WrapWithIf(Func<string, string, bool, bool> f, bool invert, string mustContains, string wrapWith, params string[] whereIsUsed2)
    {
        for (int i = 0; i < whereIsUsed2.Length; i++)
        {
            if (f.Invoke(whereIsUsed2[i], mustContains, invert))
            {
                whereIsUsed2[i] = wrapWith + whereIsUsed2[i] + wrapWith;
            }
        }
        return whereIsUsed2.ToList();
    }
    /// <summary>
    /// If some of A1 is match with A2
    /// </summary>
    /// <param name="list"></param>
    /// <param name="file"></param>
    public static bool MatchWildcard(List<string> list, string file)
    {
        return list.Any(d => SH.MatchWildcard(file, d));
    }
    
    public static bool HasFirstItemLength(List<string> notContains)
    {
        string t = "";
        if (notContains.Count > 0)
        {
            t = notContains[0].Trim();
        }
        return t.Length > 0;
    }
    
    public static List<string> TrimList(List<string> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            c[i] = c[i].Trim();
        }
        return c;
    }
    public static string GetTextAfterIfContainsPattern(string input, string ifNotFound, List<string> uriPatterns)
    {
        foreach (var item in uriPatterns)
        {
            int nt = input.IndexOf(item);
            if (nt != -1)
            {
                if (input.Length > item.Length + nt)
                {
                    return input.Substring(nt + item.Length);
                }
            }
        }
        return ifNotFound;
    }
    /// <summary>
    /// Direct edit 
    /// WithEndSlash - trims backslash and append new
    /// WithoutEndSlash - ony trims backslash
    /// </summary>
    /// <param name="folders"></param>
    public static List<string> WithEndSlash(List<string> folders)
    {
        List<string> list = folders as List<string>;
        if (list == null)
        {
            list = folders.ToList();
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = FS.WithEndSlash(list[i]);
        }
        return folders;
    }
    public static List<string> WithoutEndSlash(List<string> folders)
    {
        for (int i = 0; i < folders.Count; i++)
        {
            folders[i] = FS.WithoutEndSlash(folders[i]);
        }
        return folders;
    }
    
    

    public static List<string> JoinArrayAndArrayString(IEnumerable<string> a, IEnumerable<string> p)
    {
        if (a != null)
        {
            List<string> d = new List<string>(a.Length() + p.Length());
            d.AddRange(a);
            d.AddRange(p);
            return d;
        }
        return new List<string>(p);
    }

    public static List<string> JoinArrayAndArrayString(IEnumerable<string> a, params string[] p)
    {
        return JoinArrayAndArrayString(a, p.ToList());
    }
    public static void CheckExists(List<bool> photoFiles, List<string> allFilesRelative, List<string> value)
    {
        foreach (var item in allFilesRelative)
        {
            photoFiles.Add(value.Contains(item));
        }
    }
    public static bool HasOtherValueThanNull(List<string> idPhotos)
    {
        foreach (var item in idPhotos)
        {
            if (item != null)
            {
                return true;
            }
        }
        return false;
    }
    public static void AddIfNotContains<T>(List<T> founded, T e)
    {
        if (!founded.Contains(e))
        {
            founded.Add(e);
        }
    }
    public static List<string> GetRowOfTable(List<List<string>> _dataBinding, int i2)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < _dataBinding.Count; i++)
        {
            vr.Add(_dataBinding[i][i2]);
        }
        return vr;
    }
    /// <summary>
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    public static List<string> RemoveStringsByScopeKeepAtLeastOne(List<string> mySites, FromTo fromTo, int keepLines)
    {
        mySites.RemoveRange((int)fromTo.FromL, (int)fromTo.ToL - (int)fromTo.FromL + 1);
        for (long i = fromTo.FromL; i < fromTo.FromL - 1 + keepLines; i++)
        {
            mySites.Insert((int)i, "");
        }
        return mySites;
    }
    
    /// <summary>
    /// Return first A2 elements of A1 or A1 if A2 is bigger
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="p"></param>
    public static List<string> ShortCircuit(List<string> proj, int p)
    {
        List<string> vratit = new List<string>();
        if (p > proj.Count)
        {
            p = proj.Count;
        }
        for (int i = 0; i < p; i++)
        {
            vratit.Add(proj[i]);
        }
        return vratit;
    }

    public static List<string> ContainsDiacritic(IEnumerable<string> nazvyReseni)
    {
        List<string> vr = new List<string>(nazvyReseni.Count());
        foreach (var item in nazvyReseni)
        {
            if (SH.ContainsDiacritic(item))
            {
                vr.Add(item);
            }
        }
        return vr;
    }
    
    public static T[,] OneDimensionArrayToTwoDirection<T>(T[] flatArray, int width)
    {
        int height = (int)Math.Ceiling(flatArray.Length / (double)width);
        T[,] result = new T[height, width];
        int rowIndex, colIndex;
        for (int index = 0; index < flatArray.Length; index++)
        {
            rowIndex = index / width;
            colIndex = index % width;
            result[rowIndex, colIndex] = flatArray[index];
        }
        return result;
    }
    
    public static int CountOfValue<T>(T v, params T[] show)
    {
        int vr = 0;
        foreach (var item in show)
        {
            if (EqualityComparer<T>.Default.Equals(item, v))
            {
                vr++;
            }
        }
        return vr;
    }
    public static T GetElementActualOrBefore<T>(IList<T> tabItems, int indexClosedTabItem)
    {
        if (HasIndex(indexClosedTabItem, (IList)tabItems))
        {
            return tabItems[indexClosedTabItem];
        }
        indexClosedTabItem--;
        if (HasIndexWithoutException(indexClosedTabItem, (IList)tabItems))
        {
            return tabItems[indexClosedTabItem];
        }
        return default(T);
    }
    /// <summary>
    /// V prvním indexu jsou řádky, v druhém sloupce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="dex"></param>
    public static List<T> GetColumnOfTwoDimensionalArray<T>(T[,] rows, int dex)
    {
        int rowsCount = rows.GetLength(0);
        int columnsCount = rows.GetLength(1);
        List<T> vr = new List<T>(rowsCount);
        if (dex < columnsCount)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                vr.Add(rows[i, dex]);
            }
            return vr;
        }
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.InvalidRowIndexInMethodCAGetRowOfTwoDimensionalArray) + ";");
        return null;
    }
    
    

    /// <summary>
    /// V prvním indexu jsou řádky, v druhém sloupce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="dex"></param>
    public static List<T> GetRowOfTwoDimensionalArray<T>(T[,] rows, int dex)
    {
        int rowsCount = rows.GetLength(0);
        int columnsCount = rows.GetLength(1);
        List<T> vr = new List<T>(columnsCount);
        if (dex < rowsCount)
        {
            for (int i = 0; i < columnsCount; i++)
            {
                vr.Add(rows[dex, i]);
            }
            return vr;
        }
        ThrowExceptions.ArgumentOutOfRangeException(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.InvalidRowIndexInMethodCAGetRowOfTwoDimensionalArray) + ";");
        return null;
    }
    /// <summary>
    /// Change elements count in collection to A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="requiredLength"></param>
    public static List<string> ToSize(List<string> input, int requiredLength)
    {
        List<string> returnArray = null;
        int realLength = input.Count;
        if (realLength > requiredLength)
        {
            returnArray = new List<string>( requiredLength);
            CA.InitFillWith(returnArray, requiredLength);
            for (int i = 0; i < requiredLength; i++)
            {
                returnArray[i] = input[i];
            }
            return returnArray;
        }
        else if (realLength == requiredLength)
        {
            return input;
        }
        else if (realLength < requiredLength)
        {
            returnArray = new List<string>(requiredLength);
            CA.InitFillWith(returnArray, requiredLength);
            int i = 0;
            for (; i < realLength; i++)
            {
                returnArray[i] = input[i];
            }
            for (; i < requiredLength; i++)
            {
                returnArray[i] = null;
            }
        }
        return returnArray;
    }
    public static List<string> Format(string uninstallNpmPackageGlobal, List<string> globallyInstalledTsDefinitions)
    {
        for (int i = 0; i < globallyInstalledTsDefinitions.Count(); i++)
        {
            globallyInstalledTsDefinitions[i] = SH.Format2(uninstallNpmPackageGlobal, globallyInstalledTsDefinitions[i]);
        }
        return globallyInstalledTsDefinitions;
    }

    public static bool MoreOrZero(List<HtmlNode> n, out bool? zeroOrMore)
    {
        zeroOrMore = null;
        var c = n.Count;
        var b = c == 0;
        var bb = c > 1;
        if (b || bb)
        {
            if (b)
            {
                zeroOrMore = true;
            }
            else
            {
                zeroOrMore = false;
            }
            return true;
        }
        return false;
    }
}