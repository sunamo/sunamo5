using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class IEnumerableExtensions
{
    

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
        return source.Skip(Math.Max(0, source.Count() - N));
    }

    public static IEnumerable<TSource> Where2<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        //source.ToList().Where(predicate); - StackOverflowExtension
        //return new List<TSource>(source).Where(predicate) ;
        return source.ToList().Where(predicate);
    }
}