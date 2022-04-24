﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CAThreadShared
{
    #region ToList to use in different threads
    public static List<object> ToList(IEnumerable e, System.Windows.Threading.Dispatcher d)
    {
        List<object> ls = new List<object>(e.Count());

        d.Invoke(() =>
        {
            foreach (var item in e)
            {
                ls.Add(item);
            }
        }, System.Windows.Threading.DispatcherPriority.ContextIdle);

        return ls;
    }
    #endregion
}
