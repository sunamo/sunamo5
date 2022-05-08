using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

    public partial class PHWin //: IPHWin
    {
        public static Type type = typeof(PHWin);
    
        const string CodeExe = "Code.exe";

        public static void Code(string defFile)
        {
            if (string.IsNullOrWhiteSpace(defFile))
            {
                ThrowEx.InvalidParameter(defFile, "defFile");
            }

            //var v = AddPathIfNotContains( UserFoldersWin.Local, @"Programs\Microsoft VS Code", CodeExe);

            PH.RunFromPath(CodeExe, defFile, false);
        }
    


        
        static Browsers defBr = Browsers.Chrome;
        public static int opened = 0;
        /// <summary>
        /// Not contains Other
        /// </summary>
        public static Dictionary<Browsers, string> path = new Dictionary<Browsers, string>();
        public static Dictionary<string, string> pathExe = new Dictionary<string, string>();

        public static void AddBrowsers()
        {
            if (path.Count == 0)
            {
                var all = EnumHelper.GetValues<Browsers>();
                foreach (var item in all)
                {
                    if (item != Browsers.None && item != Browsers.EdgeDev && item != Browsers.EdgeCanary && item != Browsers.EdgeStable)
                    {
                        AddBrowser(item);
                    }
                }
            }
        }

        public static void OpenInBrowser(Browsers prohlizec, string s, int waitMs = 0)
        {
            opened++;
            string b = path[prohlizec];
            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
            s = PH.NormalizeUri(s);

            if (!UH.HasHttpProtocol(s))
            {
                s = SH.WrapWithQm(s);
            }

            if (prohlizec == Browsers.Chrome)
            {
                s = "/new-tab " + s;
            }



            Process.Start(new ProcessStartInfo(b, s));

            if (waitMs > 0)
            {
                Thread.Sleep(waitMs);
            }
        }

        public static void OpenInBrowser(string uri)
        {
            OpenInBrowser(defBr, uri);
        }

        public static void AddBrowser()
        {
            AddBrowser(defBr);
        }

        static int countOfBrowsers = 0;
        static PHWin()
        {
            var brs = EnumHelper.GetValues<Browsers>();
            countOfBrowsers = brs.Count;
            // None is deleting automatically
            //countOfBrowsers--;
        }

        public static string AddBrowser(Browsers prohlizec)
        {
            if (path.Count != countOfBrowsers)
            {
                if (path.ContainsKey(prohlizec))
                {
                    return path[prohlizec];
                }

                string b = string.Empty;

                switch (prohlizec)
                {
                    case Browsers.Chrome:
                        b = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.Firefox:
                        b = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                        if (!FS.ExistsFile(b))
                        {
                            b = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        }
                        NullIfNotExists(ref b);
                        break;
                    case Browsers.EdgeBeta:
                        
                            //C:\Users\Administrator\AppData\Local\Microsoft\WindowsApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe
                            b = @"C:\Program Files (x86)\Microsoft\Edge Beta\Application\msedge.exe";//WindowsOSHelper.FileIn(UserFoldersWin.Local, @"microsoft\edge beta\application", "msedge.exe");
                        

                        break;
                    case Browsers.Opera:
                        // Opera has version also when is installing to PF, it cant be changed
                        //b = @"C:\Program Files\Opera\65.0.3467.78\opera.exe";
                        b = WindowsOSHelper.FileIn(@"C:\Program Files\Opera\", "opera.exe");
                        if (!FS.ExistsFile(b))
                        {
                            b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Programs\Opera", "opera.exe");
                        }
                        NullIfNotExists(ref b);
                        break;
                    case Browsers.Vivaldi:
                        b = @"C:\Program Files\Vivaldi\Application\vivaldi.exe";
                        if (!FS.ExistsFile(b))
                        {
                            b = WindowsOSHelper.FileIn(UserFoldersWin.Local, XlfKeys.Vivaldi, "vivaldi.exe");
                        }
                        NullIfNotExists(ref b);
                        break;
                    //case Browsers.InternetExplorer:
                    //    b = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
                    //    break;
                    case Browsers.Maxthon:
                        b = @"C:\Program Files (x86)\Maxthon5\Bin\Maxthon.exe";
                        if (!FS.ExistsFile(b))
                        {
                            b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Maxthon\Application", "Maxthon.exe");
                        }
                        NullIfNotExists(ref b);
                        break;
                    case Browsers.Seznam:
                        b = WindowsOSHelper.FileIn(UserFoldersWin.Roaming, @"Seznam Browser", "Seznam.cz.exe");
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.Chromium:
                        b = @"D:\paSync\_browsers\Chromium\chrome.exe";
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.ChromeCanary:
                        b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Google\Chrome SxS", "chrome.exe");
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.Tor:
                        b = @"D:\Desktop\Tor Browser\Browser\firefox.exe";
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.Bravebrowser:
                        b = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                    NullIfNotExists(ref b);
                        break;
                    case Browsers.PaleMoon:
                        b = @"C:\Program Files\Pale Moon\palemoon.exe";
                        NullIfNotExists(ref b);
                        break;

                    case Browsers.EdgeDev:
                        
                            //C:\Users\Administrator\AppData\Local\Microsoft\WindowsApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe
                            b = @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe";
                    NullIfNotExists(ref b);
                        break;

                    case Browsers.EdgeCanary:
                        b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"microsoft\edge sxs\application", "msedge.exe");
                        NullIfNotExists(ref b);

                        break;
                    case Browsers.ChromeBeta:
                        b = @"C:\Program Files\Google\Chrome Beta\Application\chrome.exe";
                        NullIfNotExists(ref b);
                        break;

                    case Browsers.EdgeStable:
                        b = @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe";

                        if (!FS.ExistsFile(b))
                        {
                            //C:\Users\Administrator\AppData\Local\Microsoft\WindowsApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe
                            b = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                            
                        }
                        NullIfNotExists(ref b);
                        break;
                    default:
                        ThrowEx.NotImplementedCase(prohlizec);
                        break;
                }

                if (b == null)
                {
                    b = string.Empty;
                }

                path.Add(prohlizec, b);

                return b;
            }
            return path[prohlizec];
        }

    private static void NullIfNotExists(ref string b)
    {
        if (!FS.ExistsFile(b))
        {
            b = null;
        }
    }

    /// <summary>
    /// A1 is chrome replacement
    /// </summary>
    /// <param name="array"></param>
    /// <param name="what"></param>
    public static void SearchInAll(IEnumerable array, string what)
        {
            var br = Browsers.Chrome;
            PHWin.AddBrowser(Browsers.Chrome);
            foreach (var item in array)
            {
                opened++;
                string uri = UriWebServices.FromChromeReplacement(item.ToString(), what);
                PHWin.OpenInBrowser(br, uri);
                if (opened % 10 == 0)
                {
                    Debugger.Break();
                }
            }
        }

        public static void AssignSearchInAll()
        {
            //AddBrowsers();
            UriWebServices.AssignSearchInAll(PHWin.SearchInAll);
        }




    }
