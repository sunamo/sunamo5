using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PsInvokeArgs
{
    public static readonly PsInvokeArgs Def = new PsInvokeArgs();
    public bool writePb = false;
    /// <summary>
    /// earlier false
    /// </summary>
    public bool immediatelyToStatus = false;
    public List<string> addBeforeEveryCommand = null;
}