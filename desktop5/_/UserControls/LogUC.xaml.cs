using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using sunamo;
using sunamo.Interfaces;

namespace desktop.UserControls
{
    public partial class LogUC : UserControl, IUserControl, IWindowOpener, IUserControlShared, IKeysHandler, ISaveWithoutArg, IUserControlClosing
    {
        #region Rewrite to pure cs. With xaml is often problems without building
        public LogUC()
        {
            InitializeComponent();

            uc_Loaded(null, null);
        }

        public string Title => sess.i18n(XlfKeys.Logs);
        bool initialized = false;
        public WindowWithUserControl windowWithUserControl { get => windowOpenerMain.windowWithUserControl; set => windowOpenerMain.windowWithUserControl = value; }

        IKeysHandler keyHandlerMain = null;
        IEssentialMainWindow mainControl = null;
        IWindowOpener windowOpenerMain = null;

        public IEssentialMainWindow MainControl
        {
            get { return mainControl; }
            set
            {
                mainControl = value;

                if (value is IKeysHandler)
                {
                    keyHandlerMain = (IKeysHandler)value;
                }
                if (value is IWindowOpener)
                {
                    windowOpenerMain = (IWindowOpener)value;
                }
            }
        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (keyHandlerMain != null)
            {
                if (keyHandlerMain.HandleKey(e))
                {
                    //return true;
                }
            }

            return false;
        }

        public void Init()
        {
            if (!initialized)
            {
                initialized = true;


            }
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Save()
        {
            lbLogsErrors.Save();
            lbLogsOthers.Save();
        }

        public void OpenInCode()
        {
            PHWin.Code(lbLogsErrors.fileToSave);
            PHWin.Code(lbLogsOthers.fileToSave);

        }

        public void OnClosing()
        {

        }
        #endregion
    }
}