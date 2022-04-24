using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class ListExtensions
{
    public static void AddRangeIfNotContain<T>(this IList<T> list, List<T> l)
    {
        foreach (var item in l)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
    }

    /// <summary>
    /// Js() With Leading should be always the last in code
    /// Js() With Add should be first in code
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    public static List<string> Leading(this List<string> list, string item)
    {
        list.Insert(0, item);
        return list;
    }

    public static List<string> LeadingRange(this List<string> list, IEnumerable< string> items)
    {
        foreach (var item in items)
        {
            list.Insert(0, item);
        }
        
        return list;
    }

    public static List<T> Insert<T>(this IList<T> list, int dx, T item)
    {
        list.Insert(dx, item);
        return (List<T>)list;
    }
}