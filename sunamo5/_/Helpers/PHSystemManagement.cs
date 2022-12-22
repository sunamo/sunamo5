using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Not include in standard
/// </summary>
public partial class PH
{
    private static string GetMainModuleFilepath(int processId)
    {
        string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
        using (var searcher = new ManagementObjectSearcher(wmiQueryString))
        {
            using (var results = searcher.Get())
            {
                ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                if (mo != null)
                {
                    return (string)mo["ExecutablePath"];
                }
            }
        }
        return null;
    }
}
