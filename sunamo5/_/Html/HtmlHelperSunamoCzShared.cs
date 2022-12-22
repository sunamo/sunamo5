using sunamo;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class HtmlHelperSunamoCz{
    static Type type = typeof(HtmlHelperSunamoCz);



    private static void AddToDict(Dictionary<int, string> italic2, List<int> italic, string v)
    {
        foreach (var item in italic)
        {
            italic2.Add(item, v);
        }
    }
}