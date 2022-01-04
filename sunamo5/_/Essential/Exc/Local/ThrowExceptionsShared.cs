﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public partial class ThrowExceptions
{
    /// <summary>
    /// Return false in case of exception, otherwise true
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="v"></param>
    private static bool ThrowIsNotNull(string stacktrace, object v)
    {
        if (v != null)
        {
            ThrowIsNotNull(stacktrace, v.ToString());
            return false;
        }
        return true;
    }
}