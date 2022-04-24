using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class W32Base
{
    public delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);
    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
}