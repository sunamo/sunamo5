﻿using sunamo.Essential;
using System.Configuration;
using System.Windows;

namespace ConfigurableWindow.Shared
{
    /// <summary>
    /// Represents a type that can be used to save and 
    /// retrieve window settings. This contract is used 
    /// by the ConfigurableWindow to describe a backing 
    /// store for its persisted configuration settings.
    /// </summary>
    public interface IConfigurableWindowSettings
    {
        /// <summary>
        /// Returns true if the application has never 
        /// been run before by the current user.  If
        /// this returns true, the Window's initial
        /// location is determined by the operating
        /// system, not the WindowLocation property.
        /// </summary>
        bool IsFirstRun { get; }

        /// <summary>
        /// Gets/sets the Window's desktop coordinate.
        /// </summary>
        Point WindowLocation { get; set; }

        /// <summary>
        /// Gets/sets the size of the Window.
        /// </summary>
        Size WindowSize { get; set; }

        /// <summary>
        /// Gets/sets the WindowState of the Window.
        /// </summary>
        WindowState WindowState { get; set; }

        bool AlwaysOnTop { get; set; }
    }

    /// <summary>
    /// Provides a convenient implementation 
    /// of IConfigurableWindowSettings that  
    /// uses the ApplicationSettingsBase class 
    /// as a backing store for setting values.
    /// </summary>
    public class ConfigurableWindowSettings
        : IConfigurableWindowSettings
    {
        #region Data

        readonly ApplicationDataContainer data;

        readonly string _isFirstRunProp;
        readonly string _windowLocationProp;
        readonly string _windowSizeProp;
        readonly string _windowStateProp;
        readonly string _alwaysOnTopProp;

        #endregion // Data

        #region Constructor

        public ConfigurableWindowSettings(
            ApplicationDataContainer settings2,
            string isFirstRunProp,
            string windowLocationProp,
            string windowSizeProp,
            string windowStateProp,
            string alwaysOnTopProp)
        {
            data = settings2;

            _isFirstRunProp = isFirstRunProp;
            _windowLocationProp = windowLocationProp;
            _windowSizeProp = windowSizeProp;
            _windowStateProp = windowStateProp;
            _alwaysOnTopProp = alwaysOnTopProp;
        }

        #endregion // Constructor

        #region GetValue / SetValue

        protected T GetValue<T>(string propName)
        {
            var c = data.Get<T>(WpfApp.window, propName);
            return c;
        }

        protected void SetValue(string propName, object value)
        {

            data.Set(WpfApp.window, propName, value);
            data.SaveControl(WpfApp.window);
            //data[propName] = value;
            //data.Save();
        }

        #endregion // GetValue / SetValue

        #region IConfigurableWindowSettings Members

        public bool IsFirstRun
        {
            get { return GetValue<bool>(_isFirstRunProp); }
            protected set { SetValue(_isFirstRunProp, value); }
        }

        public Point WindowLocation
        {
            get { return GetValue<Point>(_windowLocationProp); }
            set { SetValue(_windowLocationProp, value); }
        }

        public Size WindowSize
        {
            get { return GetValue<Size>(_windowSizeProp); }
            set { SetValue(_windowSizeProp, value); }
        }

        public WindowState WindowState
        {
            get { return GetValue<WindowState>(_windowStateProp); }
            set { SetValue(_windowStateProp, value); }
        }

        public bool AlwaysOnTop
        {
            get { return GetValue<bool>(_alwaysOnTopProp); }
            set { SetValue(_alwaysOnTopProp, value); }
        }

        #endregion // IConfigurableWindowSettings Members
    }
}