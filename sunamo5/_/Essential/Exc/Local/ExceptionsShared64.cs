using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Exceptions
{
    public static string IsNull(string before, string variableName, object variable)
    {
        if (variable == null)
        {
            return CheckBefore(before) + variable + " is null.";
        }

        return null;
    }

    public static object IsNotAllowed(string before, string what)
    {
        return CheckBefore(before) + what + " is not allowed.";
    }

    /// <summary>
    /// Je lichý
    /// </summary>
    /// <param name="before"></param>
    /// <param name="colName"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static string IsOdd(string before, string colName, IEnumerable col)
    {
        if (col.Count() % 2 == 1)
        {
            return CheckBefore(before) + colName + " has odd number of elements " + col.Count();
        }
        return null;
    }

    public static string BadFormatOfElementInList(string before, object elVal, string listName)
    {
        return before + SunamoPageHelperSunamo.i18n(XlfKeys.BadFormatOfElement) + " " + SH.NullToStringOrDefault(elVal) + " in list " + listName;
    }

    public static string IsTheSame(string before, string fst, string sec)
    {
        return before + $"{fst} and {sec} has the same value";
    }

    public static string WrongExtension(string before, string path, string ext)
    {
        if (FS.GetExtension(path) != ext)
        {
            return before + path + "don't have " + ext + " extension";
        }
        return null;
    }



    public static string WrongNumberOfElements(string before, int requireElements, string nameCount, IEnumerable<string> ele)
    {
        var c = ele.Count();
        if (c != requireElements)
        {
            return before + $" {nameCount} has {c}, it's required {requireElements}";
        }
        return null;
    }











    public static string DirectoryWasntFound(string before, string directory)
    {
        if (!FS.ExistsDirectory(directory))
        {
            return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.Directory) + " " + directory + " wasn't found.";
        }

        return null;
    }


    public static string DivideByZero(string before)
    {
        return CheckBefore(before) + " is dividing by zero.";
    }





    public static string AnyElementIsNullOrEmpty(string before, string nameOfCollection, List<int> nulled)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + SH.Join(AllChars.comma, nulled) + " with null value";
    }



    #region Called from TemplateLoggerBase
    public static string NotEvenNumberOfElements(string before, string nameOfCollection)
    {
        return CheckBefore(before) + nameOfCollection + " have odd elements count";
    }
    #endregion
}
