﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct LUID_AND_ATTRIBUTES
{
    public LUID Luid;
    public UInt32 Attributes;
}
