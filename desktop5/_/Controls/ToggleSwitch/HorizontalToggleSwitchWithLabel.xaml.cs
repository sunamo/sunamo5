﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// desktopControlsToggleSwitch
namespace desktop.Controls.ToggleSwitch
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HorizontalToggleSwitchWithLabel : UserControl
    {
        public HorizontalToggleSwitchWithLabel()
        {
             InitializeComponent();
        }

        public string Label
        {
            set => tb.Text = value;
        }
    }
}