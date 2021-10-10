﻿using System;
using System.Collections.Generic;
using System.Diagnostics;


/// <summary>
/// Je to všechno v jedné třídě, aby se mi změny udělané zde projevili ve všech třídách.
/// </summary>
public static partial class AllChars
{
    private static Type type = typeof(AllChars);

    // my extension
    public static readonly List<char> generalChars = null;
    // my extension
    public static readonly List<int> specialKeyCodes = new List<int>(new int[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });

    public static readonly List<char> specialCharsWhite = new List<char>(new char[] { space });
    public static readonly List<char> specialCharsNotEnigma = new List<char>(new char[] { nbsp, space160, copy });
    /// <summary>
    /// Used in enigma
    /// </summary>
    public static readonly List<char> specialCharsAll = null;

    /*
equal->equals

     * 
     * Ascii - 128 chars - 7B
     * ASCII + code page (e.g. ISO 8859-1) - 256 chars - 8B
     * 
     * Unicode 11.0:
     * * Basic Multilingual Plane - 65536 letters - table 16*16, in every cell 256 letters, all others (SMP, SIP, TIP) are part of this
     * Supplementary Multilingual Plane (SMP) - Doplňková multilinguální rovina - 14000 chars - 
     * Supplementary Ideographic Plane (SIP) - 53424 chars - 
     * Tertiary Ideographic Plane (TIP) - 16672 chars
     * Supplementary Special-purpose Plane (SSP) - 368 chars 
     * 15 PUA-A 65536
     * 16 PUA-B 65536
     * 
     * =  overall 264256 letters
     * 
     * Surrogates - replacement, by two surrogates is coded one letter above BMP (surrogate pair). Is in UTF16. Benefit is less space (some letters have two bytes, others 4B). In UTF32 is every char 4bytes.
     * char.IsSurrogatePair(low, right) - pair is formed by low and high
     * * 
     */

    // IsControl
    //public const List<int> controlKeyCodes = 
    // IsDigit - IsNumber + superset atd.
    public static readonly List<int> numericKeyCodes = new List<int>(new int[] { 49, 50, 51, 52, 53, 54, 55, 56, 57, 48 });
    public static readonly List<char> numericChars = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });

    // IsHighSurrogate -  

    // IsLower
    public static readonly List<int> lowerKeyCodes = new List<int>(new int[] { 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 });
    public static readonly List<char> lowerChars = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });

    // IsLowSurrogate

    // IsNumber - 0..9 
    // IsPunctuation
    // IsSeparator
    // IsSurrogate
    // IsSurrogatePair
    // IsSymbol

    // IsUpper
    public static readonly List<int> upperKeyCodes = new List<int>(new int[] { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 });
    public static readonly List<char> upperChars = new List<char>(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });

    // IsWhiteSpace
    // , 55296 mi taky vrátila metoda IsWhiteSpace vrátila, ale při znovu vytvoření pomocí tohoto kódu to vyhazovalo výjimku
    /// <summary>
    /// 160 is space, is used for example in Uctenkovka
    /// </summary>
    public static readonly List<int> whiteSpacesCodes = new List<int>(new int[] { 9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8232, 8233, 8239, 8287, 12288 });
    public static List<char> whiteSpacesChars = null;
    

    static AllChars()
    {
        ConvertWhiteSpaceCodesToChars();
        notNumber = (char)whiteSpacesCodes[0];
        generalChars = new List<char>(new char[] { notNumber });

        specialCharsAll = new List<char>(specialChars.Count + specialChars2.Count + specialCharsWhite.Count);
        specialCharsAll.AddRange(specialChars);
        specialCharsAll.AddRange(specialChars2);
        specialCharsAll.AddRange(specialCharsWhite);
    }

    public static Predicate<char> ReturnRightPredicate(char genericChar)
    {
        Predicate<char> predicate = null;
        if (genericChar == AllChars.notNumber)
        {
            predicate = char.IsNumber;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, "ReturnRightPredicate", generalChars);
        }

        return predicate;
    }

    public static void ConvertWhiteSpaceCodesToChars()
    {
        whiteSpacesChars = new List<char>();
        foreach (int item in whiteSpacesCodes)
        {
            string s = "";
            s = char.ConvertFromUtf32(item);
            whiteSpacesChars.Add(Convert.ToChar(s));
        }
    }

    
}