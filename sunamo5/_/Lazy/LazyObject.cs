using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LazyT<T>
{
    private Func<string, bool, T> getCommonSettings;
    private string arg;
    T value = default(T);

    public T Value {
        get {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                value = getCommonSettings(arg, true);
            }
            return value;
                }
    }

    public LazyT(Func<string, bool, T> getCommonSettings, string pwUsersScz)
    {
        this.getCommonSettings = getCommonSettings;
        this.arg = pwUsersScz;
    }
}