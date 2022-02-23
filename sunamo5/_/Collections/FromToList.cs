using System;
using System.Collections.Generic;
using System.Text;

public class FromToList
{
    public List<FromTo> c = new List<FromTo>();

    public bool IsInRange(int i)
    {
        foreach (var item in c)
        {
            if (i < item.to && i > item.from)
            {
                return true;
            }
        }
        return false;
    }
}