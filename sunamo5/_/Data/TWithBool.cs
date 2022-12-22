using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TWithBool<T> //: ITWithDt<T>
{
    public T t { get; set; } = default(T);
    public bool b { get; set; }
}
