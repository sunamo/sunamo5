using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

[StructLayout(LayoutKind.Sequential)]
public struct LUID
{
    public uint LowPart;
    public int HighPart;
}
