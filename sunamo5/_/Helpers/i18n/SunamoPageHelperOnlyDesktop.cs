using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


/// <summary>
/// Tato třída je zde kvůli interoperibilitě s .web apss
/// má 3. parametr string ale ten se nevyužívá
/// </summary>
    public class SunamoPageHelper
    {
        public static string LocalizedString_String(Langs l, string key, string ms)
    {
        switch (l)
        {
            case Langs.cs:
                return RLData.cs[key];
                break;
            case Langs.en:
                return RLData.en[key];
                break;
            default:
                ThrowEx.NotImplementedCase(l); 
                break;
        }

        return null;
    }
    }

