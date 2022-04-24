using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;
public static partial class WriterEventLog
{
    /// <summary>
    /// Potřebuje vždy admin práva pro běh programu
    /// </summary>
    public static void DeleteMainAppLog(string name = null)
    {
        if (name == null)
        {
            name = ThisApp.Name;
        }
        if (EventLog.Exists(name))
        {
            EventLog.Delete(name);
        }
    }
}