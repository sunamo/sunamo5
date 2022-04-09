using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunamoExceptions;

public partial class VpsHelperSunamo
{
    
    public const string ip = "46.36.38.72";
    public const string ipMyPoda = "85.135.38.18";

    public static bool IsVps => VpsHelperXlf.IsVps;
    public static string path => VpsHelperXlf.path;

    public static bool IsQ
        => Environment.MachineName == "CZOV-61TN5D3";

    public static string LocationOfSqlBackup(string s)
    {
        var p = string.Empty;
        //p = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\";
        p = @"C:\mssqllserver\";

        var r = p += @"Backup\"+s+".bak";
        return r;
    }

    public static string SunamoSln()
    {
        if (IsVps)
        {
            return @"C:\_\sunamo\";
        }
        else
        {
            return @"E:\Documents\vs\Projects\sunamo\";
        }
    }

    public static string SunamoCzSln()
    {
        if (IsVps)
        {
            return @"C:\_\sunamo.cz\";
        }
        else
        {
            return @"E:\Documents\vs\Projects\sunamo.cz\";
        }
    }

    public static string SunamoProject()
    {
        return FS.Combine(SunamoSln(), "sunamo");
    }

    
}