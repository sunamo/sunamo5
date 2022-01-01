using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public  class MainWindowSunamo_Ctor
{
    public static void FirstSection<Dispatcher>(string appName, Action<Dispatcher> WpfAppInit, IClipboardHelper ClipboardHelperWinInstance, Action checkForAlreadyRunning, Action applyCryptData, Dispatcher d)
    {
        ThisApp.Name = appName;

        BitLockerHelper.Init();
        ThrowEx.IsLockedByBitLocker = BitLockerHelper.IsFolderLockedByBitLocker;
        SunamoExceptions.ThrowEx.IsLockedByBitLocker = BitLockerHelper.IsFolderLockedByBitLocker;

        WpfAppInit(d);
        if (checkForAlreadyRunning != null)
        {
            checkForAlreadyRunning();
        }

        ClipboardHelper.Instance = ClipboardHelperWinInstance;
        AppData.ci.CreateAppFoldersIfDontExists();
        applyCryptData();

        XlfResourcesHSunamo.SaveResouresToRLSunamo();
    }
}