using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;



public class ResourcesH : IResourceHelper
    {
        public static ResourcesH ci = new ResourcesH();

        private ResourcesH()
        {

        }

        public Uri GetRelativeUri(string name)
        {
        if (name.EndsWith(".ico"))
        {

        }
        var name2 = SH.PrefixIfNotStartedWith( name, "/");
            return new Uri(name2, UriKind.Relative);
        }

        public BitmapImage GetBitmapImageSource(string name)
        {
            return new BitmapImage(GetRelativeUri(name));
        }

        public string GetString(string name)
        {
            return Encoding.UTF8.GetString(FS.StreamToArrayBytes(GetStream(name)));
        }

    public Stream GetStream(string name)
    {
        var v = GetRelativeUri(name);
        StreamResourceInfo info = Application.GetResourceStream(v);
        return info.Stream;
    }
}