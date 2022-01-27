using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public partial class ThrowEx
{
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