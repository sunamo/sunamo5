using System;
using System.Collections.Generic;
using System.Text;

public partial class ThrowEx
{
    public static void FirstLetterIsNotUpper(string selectedFile)
    {
        ThrowIsNotNull(Exceptions.FirstLetterIsNotUpper, selectedFile);
    }
}