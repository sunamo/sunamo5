using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public partial class UriWebServices
{
    public static string GoogleImFeelingLucky(string v)
    {
        return FromChromeReplacement("https://www.google.com/search?btnI&q=%s", v);
    }

    public static string MapyCz(string v)
    {
        return FromChromeReplacement("https://mapy.cz/?q=%s&sourceid=Searchmodule_1", v);
    }

    public static string TopRecepty(string what)
    {
        return FromChromeReplacement("https://www.toprecepty.cz/vyhledavani.php?hledam=%s&kategorie=&autor=&razeni=", WebUtility.UrlEncode(what));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    private static string GoogleMaps(string item)
    {
        return FromChromeReplacement("https://www.google.com/maps/place/%", item);
    }

    public static void GoogleMaps(List<string> list)
    {
        foreach (var item in list)
        {
            Process.Start(GoogleMaps(item));
        }
    }

    public static string KmoAll(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=247&field=0", item);
    }

    public static string KmoAV(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=238&field=0", item);
    }

    public static string KmoMP(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=242&field=0", item);
    }


}