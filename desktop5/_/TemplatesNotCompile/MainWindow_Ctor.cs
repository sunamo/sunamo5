﻿using ConfigurableWindow.Shared;
using desktop;
using desktop.UserControls;
using sunamo.Essential;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

public partial class MainWindow_Ctor : Window, IEssentialMainWindow, IHideToTray, IConfigurableWindow
{
    static Type type = typeof(MainWindow_Ctor);
    Mode mode = Mode.Empty; public string ModeString { get => mode.ToString(); }
    EmptyUC emptyUC = null;
    LogUC logUC = null;
    UserControl settingsUC = null;
    Control _actual = new UserControl(); public Control actual { get => _actual; set => _actual = value; }
    IUserControl userControl = null;
    IUserControlWithSuMenuItemsList userControlWithSuMenuItems;
    IUserControlClosing userControlClosing;
    IKeysHandler keysHandler;
    List<SuMenuItem> previouslyRegisteredSuMenuItems = new List<SuMenuItem>();
    dynamic Instance = null;
    //public SunamoCzLoginManager sunamoCzLoginManager = new SunamoCzLoginManager();

    #region MyRegion
    SuMenuItem miGenerateScreenshot = null;
    SuMenuItem miAlwaysOnTop = null;
    Grid grid;
    SuMenuItem miUC;

    #region Implicitly in Window
    Dispatcher Dispatcher = null;
    TextBlock tbLastErrorOrWarning;
    TextBlock tbLastOtherMessage;
    string Title = null;
    #endregion
    #endregion

    public ApplicationDataContainer data { get; set; }
    public ConfigurableWindowWrapper configurableWindowWrapper { get; set; }
    public bool CancelClosing { get; set; }
    public WindowWithUserControl windowWithUserControl { get; set; }

    public MainWindow_Ctor()
    {
        // In ctor can be only InitializeComponent, all everything must be in Loaded. Use template as exists in MainWindow_Ctor

        Loaded += MainWindow_Loaded;
    }


    public void CheckIsAlreadyRunning()
    {
#if !DEBUG
              
                    if (PH.IsAlreadyRunning(ThisApp.Name))
                    {
                        SetCancelClosing(false);

                        MessageBox.Show(sess.i18n(XlfKeys.PleaseUseAppInTray));

                        Close();
                    }
                
#endif
    }

    public void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        /*
             * 1) Clipboard and Xlf
             * 2) Initialize logging
             * 3) Initialize base properties of every app
             * 4) Initialize helpers of app
             * 5) Set modes
             * 6) Attach handlers
             */

        #region 1) ThisApp.Name, Check for already running, required conditions, Clipboard, AppData and Xlf
        SunamoPageHelperSunamo.localizedString = SunamoPageHelper.LocalizedString_String;
        string appName = "";
        MainWindowSunamo_Ctor.FirstSection<Dispatcher>(appName, WpfApp.Init, ClipboardHelperWinStd.Instance, CheckIsAlreadyRunning, null, Dispatcher, ThisApp.async_);
        //CloudProvidersHelper.myStations = new DefaultAccountNames();
        #endregion
        // All initialization must be after #region Initialize base properties of every app 

        #region 2) Initialize logging
#if DEBUG
        InitApp.TemplateLogger = SunamoTemplateLogger.Instance;
        InitApp.Logger = SunamoLogger.Instance;
        InitApp.TypedLogger = TypedSunamoLogger.Instance;
#else
        //sunamo.Essential.InitApp.TemplateLogger = SunamoTemplateLogger.Instance;
        // sunamo.Essential.InitApp.Logger = SunamoLogger.Instance;
        // sunamo.Essential.InitApp.TypedLogger = TypedSunamoLogger.Instance;

        // For console always write only to console. When I need write also to event log, must do it separately
        //CmdApp.EnableConsoleLogging(true);
        // InitApp.Logger = ConsoleLogger.Instance;
        //     InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        //     InitApp.TypedLogger = TypedConsoleLogger.Instance;
#endif

        WpfApp.EnableDesktopLogging(true);

        // 1st LogUC must be before Empty
        SetMode(Mode.LogUC);
        WpfApp.SaveReferenceToTextBlockStatus(false, tbLastErrorOrWarning, tbLastOtherMessage);
        WpfApp.SaveReferenceToLogsStackPanel(logUC.lbLogsOthers.lbLogs, logUC.lbLogsErrors.lbLogs);
        #endregion

        #region 3) Initialize base properties of app
        Instance = this;

        // Not Dispatcher od DispatcherObject but Application.Current.Dispatcher - only then is working in all cases
        WpfApp.cd = Application.Current.Dispatcher; 
ThrowExceptions.showExceptionWindow = WindowHelper.ShowExceptionWindow2;

        // Important to shut down app
        WpfApp.mp = this; WpfApp.htt = this; // delete htt when is not derived, its was mass replaced due to shutdown app after hide to tray
        // Must be due to shutdown after hide to tray
        WpfApp.htt = this;

        try
        {
            WriterEventLog.CreateMainAppLog(EventLogNames.Dummy);
        }
        catch (Exception)
        {
            //'Could not load file or assembly 'System.Diagnostics.EventLog, Version=0.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.'
        }

        // Must be initialize right after data set
        // ApplicationDataContainer Must be before configurableWindowWrapper
        data = new ApplicationDataContainer();
        Name = ThisApp.Name;



        data.Add(this);



#if !DEBUG
            if (PH.IsAlreadyRunning(ThisApp.Name))
            {
                SetCancelClosing(false);
                //MessageBox.Show(sess.i18n(XlfKeys.PleaseUseAppInTray));
                Close();
            }
#endif

        configurableWindowWrapper = new ConfigurableWindowWrapper(this, miAlwaysOnTop);


        Title = appName;
        #endregion

        #region 4) Initialize helpers, SQL of app
        //PS.Init();
        TF.isUsed = PHWin.IsUsed;
        //sunamoCzLoginManager.DoWebRequest = DoWebRequest;
        // Assign JavascriptSerialization.utf8json
        new JavascriptSerialization(SerializationLibrary.Utf8Json);

        FS.DeleteFileMaybeLocked = FSWin.DeleteFileMaybeLocked;
        #endregion

        #region 5) Set modes
        // 2nd Edit only in #if
        SetMode(Mode.Empty);

#if DEBUG
        //3rd in debug show uc
        SetMode("Dummy");
#endif
        #endregion

        #region 6) Attach handlers
        PreviewKeyDown += MainWindow_PreviewKeyDown;
        #endregion

        #region 7) Notify icon
        //SetCancelClosing(true);
        //// .ico must be set up to Resource
        ///Dictionary<string, Action> contextSuMenuItems = new Dictionary<string, Action>();
        //NotifyIconHelper.Create(SetCancelClosing, ResourcesH.ci.GetStream(ThisApp.Name + ".ico"), delegate (object sen, EventArgs args)
        //{
        //    this.Show();
        //    this.BringIntoView();
        //    var beforeTopMost = this.Topmost;
        //    this.Topmost = true;
        //    this.Topmost = beforeTopMost;

        //    // WindowState should be loaded from configuration
        //    //this.WindowState = WindowState.Normal;
        //}, forms.ContextMenuHelper.Get(WpfApp.Shutdown), null);
        #endregion

        #region 8) App-specific testing

        #endregion

        #region 9) Set up UI of app
        Icon = EmbeddedResourcesHShared.ciShared.GetAppIcon(".ico");

        miGenerateScreenshot.Header = sess.i18n(XlfKeys.GenerateScreenshot);
        miGenerateScreenshot.Click += FrameworkElementHelper.CreateBitmapFromVisual; if (!RuntimeHelper.IsAdminUser())
        {
            miGenerateScreenshot.Visibility = Visibility.Collapsed;
        }

        SetAwesomeIcons();
        #endregion

        #region 10) Login, Load data

        #endregion
    }



    #region MyRegion
    // Only for working with notify, but always insert block with userControlClosing
    //    protected override void OnClosing(CancelEventArgs e)
    //    {
    //#if !DEBUG
    //        e.Cancel = GetCancelClosing();
    //        WindowState = WindowState.Minimized;
    //#endif
    // Must check before - during shutdowning down is miAlwaysOnTop null
    //if (!e.Cancel)
    //{
    //        CheckSuMenuItemTopMost();
    //}
    //if (userControlClosing != null)
    //    {
    //        userControlClosing.OnClosing();
    //    }

    //        base.OnClosing(e);
    //    } 
    #endregion

    string DoWebRequest(string uri)
    {
        //return HttpClientHelperHttp.GetResponseText(uri, HttpMethod.Get, new HttpRequestDataHttp());
        return HttpClientHelper.GetResponseText(uri, HttpMethod.Get, new HttpRequestData());
    }
    private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (keysHandler != null)
        {
            if (keysHandler.HandleKey(e))
            {
                e.Handled = true;
            }
            else if (e.Key == Key.PrintScreen)
            {
                actual.MakeScreenshot();
            }
            else
            {

            }
        }
    }

    void MiAlwaysOnTop_Click(object sender, RoutedEventArgs e)
    {
        Topmost = miAlwaysOnTop.IsChecked;
        CheckSuMenuItemTopMost();
    }

    private void CheckSuMenuItemTopMost(bool? topMost = null)
    {
        if (topMost.HasValue)
        {
            Topmost = topMost.Value;
        }

        if (miAlwaysOnTop != null)
        {
            //  miAlwaysOnTop is null when is calling from OnClosing / Closing
            miAlwaysOnTop.IsChecked = Topmost;
        }
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        ConfigurableWindowHelper.SourceInitialized(configurableWindowWrapper);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        base.OnRenderSizeChanged(info);

        ConfigurableWindowHelper.RenderSizeChanged(configurableWindowWrapper);
    }

    public const string awesomeFontPath = "/Fonts/FontAwesome.otf#FontAwesome";

    void SetAwesomeIcons()
    {
        //AwesomeFontControls.SetAwesomeFontSymbol(tbAwesome, "\uf133");
    }

    public IConfigurableWindowSettings CreateSettings()
    {
        return new ConfigurableWindow.Shared.WindowConfigSettings(this, data);
    }

    public void SetMode(object mode2)
    {
        var mode = EnumHelper.Parse<Mode>(mode2.ToString(), Mode.Empty);
        if (userControlClosing != null)
        {
            userControlClosing.OnClosing();
        }

        this.Topmost = false;
        #region After arrange I have to newly unregister
        //if (result != null)
        //{
        //    result.Finished -= Result_Finished;
        //}

        //if (userControlInWindow != null)
        //{
        //    userControlInWindow.ChangeDialogResult -= UserControlInWindow_ChangeDialogResult;
        //}
        #endregion

        this.mode = mode;
        grid.Children.Remove(actual);

        switch (mode)
        {
            #region Shared UC
            case Mode.Empty:
                if (emptyUC == null)
                {
                    emptyUC = new EmptyUC();
                }
                actual = emptyUC;
                break;
            case Mode.LogUC:
                if (logUC == null)
                {
                    logUC = new LogUC();
                }
                actual = logUC;
                break;
            case Mode.Settings:
                if (settingsUC == null)
                {
                    //settingsUC = ne
                }
                actual = settingsUC;
                break;
            #endregion
            default:
                break;
        }

        // Here I can use (IUserControl) because every have to be IUserControl
        userControl = (IUserControl)actual;
        userControl.Init();

        userControlWithSuMenuItems = actual as IUserControlWithSuMenuItemsList;
        userControlClosing = actual as IUserControlClosing;
        keysHandler = actual as IKeysHandler;
        ThrowExceptions.WasNotKeysHandler(Exc.GetStackTrace(), type, Exc.CallingMethod(), userControl.Title, keysHandler);

        #region On start I have to unregister
        previouslyRegisteredSuMenuItems.ForEach(SuMenuItem => miUC.Items.Remove(SuMenuItem));

        var pMode = "userControlWithSuMenuItems " + mode;

        if (userControlWithSuMenuItems != null)
        {
            // keep long name due to copy to new selling apps
            miUC.Visibility = System.Windows.Visibility.Visible;
            miUC.Header = userControl.Title;
            previouslyRegisteredSuMenuItems = userControlWithSuMenuItems.SuMenuItems();
            foreach (var item in previouslyRegisteredSuMenuItems)
            {
                if (item.Parent != null)
                {
                    ((Menu)item.Parent).Items.Remove(item);
                }
                miUC.Items.Add(item);
            }
            miUC.UpdateLayout();
        }
        else
        {
            miUC.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        grid.Children.Add(actual);
        Grid.SetRow(actual, 1);

        MainWindow_SizeChanged(null, null);
    }

    private void MainWindow_SizeChanged(object p1, object p2)
    {

    }

    public bool GetCancelClosing()
    {
        if (CancelClosing)
        {
            Hide();
        }
        return CancelClosing;
    }

    public void SetCancelClosing(bool b)
    {
        CancelClosing = b;
    }


}