﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITWithDt<T>
{
    T t { get; set; }
    DateTime Dt { get; set; }
}