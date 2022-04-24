using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public struct TOKEN_PRIVILEGES
{
    public UInt32 PrivilegeCount;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public LUID_AND_ATTRIBUTES[] Privileges;
}
