using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public class SuMenuItem : MenuItem
    {
        public bool onClick = false;

        public new event RoutedEventHandler Click
        {
            add
            {
                base.Click += value;
                onClick = true;
            }
            remove
            {
                base.Click -= value;
                onClick = false;
            }
        }

        public static void CollapseMaybeNotReferenced(params SuMenuItem[] mis)
        {
            foreach (var mi in mis)
            {
                mi.Visibility = Visibility.Collapsed;
            }
        }
    }
}

