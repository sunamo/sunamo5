using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoComparerICompare
{
    public class IEnumerableCharCountAsc<T> : IComparer<T> where T : IEnumerable<char>
    {
        public int Compare(T x, T y)
        {
            int a = 0;
            int b = 0;

            foreach (var item in x)
            {
                a++;
            }

            foreach (var item in y)
            {
                b++;
            }


            return a.CompareTo(b);
        }
    }

    public class TWithDtComparer
    {
        public class Desc<T> : IComparer<TWithDt<T>>
        {
            private ISunamoComparer<TWithDt<T>> _sc = null;

            public Desc(ISunamoComparer<TWithDt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(TWithDt<T> x, TWithDt<T> y)
            {
                return _sc.Desc(x, y);
            }
        }

        public class Asc<T> : IComparer<ITWithDt<T>>
        {
            private ISunamoComparer<ITWithDt<T>> _sc = null;

            public Asc(ISunamoComparer<ITWithDt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(ITWithDt<T> x, ITWithDt<T> y)
            {
                return _sc.Asc(x, y);
            }
        }
    }

    /// <summary>
    /// Usage:vr.Sort(new SunamoComparerICompare.TWithIntComparer.Desc<string>(new SunamoComparer.TWithIntSunamoComparer<string>()));
    /// </summary>
    public class TWithIntComparer
    {
        public class Desc<T> : IComparer<TWithInt<T>>
        {
            private ISunamoComparer<TWithInt<T>> _sc = null;

            public Desc(ISunamoComparer<TWithInt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(TWithInt<T> x, TWithInt<T> y)
            {
                return _sc.Desc(x, y);
            }
        }

        public class Asc<T> : IComparer<TWithInt<T>>
        {
            private ISunamoComparer<TWithInt<T>> _sc = null;

            public Asc(ISunamoComparer<TWithInt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(TWithInt<T> x, TWithInt<T> y)
            {
                return _sc.Asc(x, y);
            }
        }
    }
}