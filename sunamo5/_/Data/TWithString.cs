using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TWithString<T>
{
    public TWithString()
    {

    }

    public TWithString(T t, string path)
    {
        this.t = t;
        this.path = path;
    }

        public T t = default(T);
        public string path = null;

    public override string ToString()
    {
        return path;
    }
}