using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Helpers.Controls
{
    public class SuMenuItemTemplates
    {
        public static SuMenuItem AvailableShortcut(Dictionary<string, string> dictionary2)
        {
            SuMenuItem miShowControls = new SuMenuItem();
            miShowControls.Click += delegate
            {
                WindowWithUserControl.AvailableShortcut(dictionary2);
            };
            miShowControls.Header = sess.i18n(XlfKeys.AvailableShortcuts) + "...";
            return miShowControls;
        }
    }
}