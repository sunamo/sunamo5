﻿using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public  class MainWindowSunamo_Ctor
{
    public static void FirstSection(string appName, Action WpfAppInit, IClipboardHelper ClipboardHelperWinInstance, Action checkForAlreadyRunning, Action applyCryptData)
    {
        ThisApp.Name = appName;
        WpfAppInit();
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