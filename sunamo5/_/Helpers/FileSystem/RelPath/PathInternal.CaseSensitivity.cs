// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;

#region For easy copy
public partial class PathInternal
{
    /// <summary>Returns a comparison that can be used to compare file and directory names for equality.</summary>
    public static StringComparison StringComparison
    {
        get
        {
            return IsCaseSensitive ?
                StringComparison.Ordinal :
                StringComparison.OrdinalIgnoreCase;
        }
    }

    /// <summary>Gets whether the system is case-sensitive.</summary>
    public static bool IsCaseSensitive
    {
        get
        {
            return true;
            //return !(OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS());
        }
    }
} 
#endregion
