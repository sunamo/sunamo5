using System;
using System.Collections;

public partial class ThrowEx
{
    public static string lastError;

    public static void FirstLetterIsNotUpper(string selectedFile)
    {
        ThrowIsNotNull(Exceptions.FirstLetterIsNotUpper, selectedFile);
    }

    public static bool IsOdd(string colName, IEnumerable e)
    {
        Func<string, string, IEnumerable, string> f = Exceptions.IsOdd;
        return ThrowIsNotNull(f, colName, e);
    }


}