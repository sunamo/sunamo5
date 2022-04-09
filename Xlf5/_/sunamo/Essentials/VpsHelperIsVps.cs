using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace  SunamoExceptions
{
/// <summary>
/// Must be created like that
/// Is not possible via delegate, coz I could add arg to many methods and in the end to class field due to TranslateDictionary[]
/// 
/// Its not possible do it with webShared due to is importing sunamo.web and WinSec is assembly withoutDep
/// </summary>
public class VpsHelperXlf
{
    #region For easy copy
    public const string path = @"C:\_";

    public static bool IsVps
    {
        get
        {
            return Directory.Exists(path);
        }
    }
    #endregion
}
}