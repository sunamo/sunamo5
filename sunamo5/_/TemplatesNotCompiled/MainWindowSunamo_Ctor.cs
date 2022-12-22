using sunamo.Essential;
using System;


public class MainWindowSunamo_Ctor
{
    public static void FirstSection<Dispatcher>(string appName, Action<Dispatcher> WpfAppInit, IClipboardHelper ClipboardHelperWinStdInstance, Action checkForAlreadyRunning, Action applyCryptData, Dispatcher d, bool async_,Func<char, bool> bitLockerHelperIsFolderLockedByBitLocker, Action bitLockerHelperInit)
    {
        ThisApp.Name = appName;
        ThisApp.async_ = async_;

        if(bitLockerHelperInit != null)
        {
        bitLockerHelperInit();
        ThrowEx.IsLockedByBitLocker = bitLockerHelperIsFolderLockedByBitLocker;
        SunamoExceptions.ThrowEx.IsLockedByBitLocker = bitLockerHelperIsFolderLockedByBitLocker;
        }

        WpfAppInit(d);
        if (checkForAlreadyRunning != null)
        {
            checkForAlreadyRunning();
        }

        ClipboardHelper.Instance = ClipboardHelperWinStdInstance;
        AppData.ci.CreateAppFoldersIfDontExists();
        applyCryptData();

        XlfResourcesHSunamo.SaveResouresToRLSunamo();
    }
}