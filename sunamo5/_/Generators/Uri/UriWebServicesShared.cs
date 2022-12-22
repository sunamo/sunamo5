using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class UriWebServices
{
    public const string chromeSearchstringReplacement = "%s";
    public static Action<IEnumerable, string> SearchInAll;

    public static string YouTubeProfile(string nick)
    {
        return "https://www.youtube.com/c/" + nick;
    }

    public static string TwitterProfile(string nick)
    {
        return "https://www.twitter.com/" + nick;
    }

    public static void AssignSearchInAll(Action<IEnumerable, string> assignSearchInAll)
    {
        SearchInAll = assignSearchInAll;
    }

    public static string FromChromeReplacement(string uri, string term)
    {
        // UrlEncode is not needed because not encode space to %20
        term = Uri.EscapeUriString(term);
        //term = UH.UrlEncode(term);
        return uri.Replace(chromeSearchstringReplacement, term);
    }
}