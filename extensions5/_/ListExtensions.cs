using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static partial class ListExtensions
{
    

    public static void RemoveMany<T>(this IList<T> list, List<T> l)
    {
        foreach (var item in l)
        {
            list.Remove(item);
        }
    }

    /// <summary>
    /// Nepoužívat toto na přidávání js, vloží se v špatném pořadí, pak to dělá function is not defined!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    private static List<T> Add2<T>(this IList<T> list, T item)
    {
        list.Add(item);
        return (List<T>)list;
    }

    
    

    

    


}