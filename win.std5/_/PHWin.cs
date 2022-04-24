using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sunamo.Helpers;


    public partial class PHWin
    {


    #region Interop


















    #endregion

    private static string AddPathIfNotContains(UserFoldersWin local, string v, string codeExe)
    {
        if (!pathExe.ContainsKey(codeExe))
        {
            var fi = WindowsOSHelper.FileIn(local, v, codeExe);
            fi = FS.GetDirectoryName(fi);
            pathExe.Add(codeExe, fi);

            return fi;
        }
        return pathExe[codeExe];
    }

    public static void OpenInBrowserAutomaticallyCountOfOpened(Browsers prohlizec, string s, int waitMs = 0)
    {
        OpenInBrowser(prohlizec, s, waitMs);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<string> BrowsersWhichDontHaveExeInDefinedPath()
    {
        List<string> doesntExists = new List<string>();

        AddBrowsers();
        foreach (var item in path)
        {
            if (!FS.ExistsFile(item.Value))
            {
                doesntExists.Add(item.Value);
            }
        }

        return doesntExists;
    }





    public static void OpenInAllBrowsers(string uri)
    {
        OpenInAllBrowsers(CA.ToListString(uri));
    }

    public static void OpenInAllBrowsers(IEnumerable<string> uris)
    {
        AddBrowsers();
        foreach (var uri in uris)
        {
            foreach (var item in path)
            {
                if (item.Key == Browsers.Tor)
                {
                    continue;
                }
                OpenInBrowser(item.Key, uri);
            }
        }
    }

    public static void OpenFolder(string folder)
    {
        Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", folder);
    }



    public static void SaveAndOpenInBrowser(Browsers prohlizec, string htmlKod)
        {
            string s = Path.GetTempFileName() + ".html";
            File.WriteAllText(s, htmlKod);
            OpenInBrowser(prohlizec, s);
        }

        public static bool IsUsed(string fullPath)
        {
            return FileUtil.WhoIsLocking(fullPath).Count > 0;
        }

    



 





    }
