using sunamo.Essential;
using System;


public class MainWindowSunamo_Ctor
{
    public static void FirstSection<Dispatcher>(string appName, Action<Dispatcher> WpfAppInit, IClipboardHelper ClipboardHelperWinInstance, Action checkForAlreadyRunning, Action applyCryptData, Dispatcher d, bool async_)
    {
        ThisApp.Name = appName;
        ThisApp.async_ = async_;

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