using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TextMemoryStream
{
    //public List<string> lines = null;

    public StringBuilder line = new StringBuilder();
    string fn = null;

    public TextMemoryStream(string t)
    {
        //lines = TF.ReadAllLines(t);
        fn = t;

        string line2 = string.Empty;
        if (FS.ExistsFile(fn))
        {
            line2 = TF.ReadAllText(t).Trim();
        }
        
        line.Append(line2);
    }

    public void Save()
    {
        TF.SaveFile(line.ToString(), fn);
    }

    //public string LineStartingWith(string date)
    //{
    //    foreach (var item in lines)
    //    {
    //        if (item.StartsWith(date))
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}
}
