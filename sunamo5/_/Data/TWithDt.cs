using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TWithDt<T> : ITWithDt<T>
{
    public T t { get; set; } = default(T);
    public DateTime Dt { get; set; }
}