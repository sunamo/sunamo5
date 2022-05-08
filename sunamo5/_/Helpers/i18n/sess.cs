﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

public static class sess
{
    static Type type = typeof(sess);

    public static string i18n(string key)
    {
        if (Exc.aspnet)
        {
            //ThrowExceptions.IsNotAllowed("SunamoPageHelperSunamo.i18n in asp.net due to use global ThisApp.l");
        }       

        switch (ThisApp.l)
        {
            case Langs.cs:
                return RLData.cs[key];
            case Langs.en:
                return RLData.en[key];
            default:
                ThrowExceptions.NotImplementedCase(ThisApp.l);
                break;
        }
        return null;
    }
}