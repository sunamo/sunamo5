using System;
using System.Collections.Generic;
using System.Text;


namespace Xlf
{

    #region For easy copy
    public partial class PathInternal
    {
        /// <summary>Returns a comparison that can be used to compare file and directory names for equality.</summary>
        internal static StringComparison StringComparison
        {
            get
            {
                return IsCaseSensitive ?
                    StringComparison.Ordinal :
                    StringComparison.OrdinalIgnoreCase;
            }
        }

        /// <summary>Gets whether the system is case-sensitive.</summary>
        internal static bool IsCaseSensitive
        {
            get
            {
                return true;
                //return !(OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS());
            }
        }
    }
    #endregion

}