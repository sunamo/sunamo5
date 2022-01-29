using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class was created because is relevant just 8 letters of each word
/// </summary>
public class EventLogNames
{
    public const string AllProjectsSearchWpf="AllProjectsSearch.Wpf";
    public const string SunamoCzAdminCmd = "SunamoCzAdminCmd";
    public static string Dummy
    {
        get
        {
            ThrowEx.NotImplementedMethod();
            return null;
        }
    }
}