using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TextMemoryStream
{
    public List<string> lines = null;

    public TextMemoryStream(string t)
    {
        lines = TF.ReadAllLines(t);
    }

    public string LineStartingWith(string date)
    {
        foreach (var item in lines)
        {
            if (item.StartsWith(date))
            {
                return item;
            }
        }
        return null;
    }
}
