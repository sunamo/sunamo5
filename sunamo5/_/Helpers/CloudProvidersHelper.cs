using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace sunamo.Helpers.FileSystem
{
    public class CloudProvidersHelper
    {
        //private Dictionary<string, string> folders = null;
        public static string OneDriveFolder0 = null;
        public static string OneDriveFolder1 = null;
        public static string GDriveFolder = null;
        public static string OneDriveExe = null;
        public static string GDriveExe = null;
        
        public static string OneDriveFn = null;
        public static string GDriveFn = null;

        public static IMyStations myStations = null;

        private static CloudProvidersHelper Instance = null;
        
        public CloudProvidersHelper()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            var fCloudProviders = AppData.ci.GetFileCommonSettings("CloudProviders.txt");
            List<string> header = null;
            var l = SF.GetAllElementsFileAdvanced(fCloudProviders, out header);
            
            //folders = SF.ToDictionary<string, string>(l);
            var OneDriveFolders = SH.Split(header[0], AllStrings.ast);
            OneDriveFolder0 = OneDriveFolders[0];
            OneDriveFolder1 = OneDriveFolders[1];
            GDriveFolder = l[0][0];
            
            string oneDriveExe = header[1];

            if (myStations != null)
            {
                oneDriveExe = oneDriveExe.Replace(SH.WrapWithBs(myStations.Vps), SH.WrapWithBs(myStations.Mb));
            }

            if (!VpsHelperSunamo.IsVps)
            {
                OneDriveExe = oneDriveExe;
            }
            
            GDriveExe = l[0][1];

            OneDriveFn = FS.GetFileNameWithoutExtension(OneDriveExe);
            GDriveFn = FS.GetFileNameWithoutExtension(GDriveExe);
        }

        public static Action<string> RunAsDesktopUserNoAdmin;
        
        public static void OpenSyncAppIfNotRunning(string ss2)
        {
            if(OneDriveExe == null)
                return;
            
            if (ss2.StartsWith(OneDriveFolder0) || ss2.StartsWith(OneDriveFolder1))
            {
                if (!PH.IsAlreadyRunning(OneDriveFn))
                {
                    RunAsDesktopUserNoAdmin(OneDriveExe);
                    Thread.Sleep(5000);
                }
            }
            else if (ss2.StartsWith(GDriveFolder))
            {
                if (!PH.IsAlreadyRunning(GDriveFn))
                {
                    Process.Start(GDriveExe);
                    Thread.Sleep(5000);
                }
            }
            
        }
    }
}