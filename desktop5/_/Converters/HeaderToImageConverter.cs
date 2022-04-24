﻿
using sunamo.Data;
using sunamo.PInvoke;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace desktop.Converters
{
    //TODO: Try this, in attr are types string and bool but Convert() return ImageSource

    //[ValueConversion(typeof(string), typeof(bool))]
    /// <summary>
    /// Zkoušel jsem ho dát do win.pi kvůli IconExtractor
    /// Bohužel pak se mi nepovedlo ani udělat import v xaml pro FolderContentsTreeView
    /// Proto jednoduše vše co se používá v desktop xamlu, musí být v desktop
    /// </summary>
    public class HeaderToImageConverter : IValueConverter
        {
            public static HeaderToImageConverter Instance = new HeaderToImageConverter();

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
               FileSystemEntry path = (value as FileSystemEntry);

                using (Icon i = IconExtractor.GetSmallIcon(path.path, path.file))
                {
                   if (i != null)
                   {
                       ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
                                               i.Handle,
                                               new Int32Rect(0, 0, i.Width, i.Height),
                                               BitmapSizeOptions.FromEmptyOptions());
                       return img;
                   }
                }
                return null;
            }

            static Type type = typeof(HeaderToImageConverter);

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.CannotConvertBack));
            return null;
            }
        }
    
}