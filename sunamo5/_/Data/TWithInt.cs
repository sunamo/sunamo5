using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class TWithInt<T>
    {
        public T t = default(T);
        public int count = 0;
    }
}