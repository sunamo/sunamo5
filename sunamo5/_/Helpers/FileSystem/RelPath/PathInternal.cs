using System;
using System.Collections.Generic;
using System.Text;

#region For easy copy
public partial class PathInternal
{
    public static bool EndsInDirectorySeparator(ReadOnlySpan<char> path) => PathInternal.EndsInDirectorySeparator2(path);

    internal static bool EndsInDirectorySeparator2(ReadOnlySpan<char> path) =>
           path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]);

    /// <summary>
    /// Get the common path length from the start of the string.
    /// </summary>
    internal static int GetCommonPathLength(string first, string second, bool ignoreCase)
    {
        int commonChars = EqualStartingCharacterCount(first, second, ignoreCase: ignoreCase);

        // If nothing matches
        if (commonChars == 0)
            return commonChars;

        // Or we're a full string and equal length or match to a separator
        if (commonChars == first.Length
            && (commonChars == second.Length || IsDirectorySeparator(second[commonChars])))
            return commonChars;

        if (commonChars == second.Length && IsDirectorySeparator(first[commonChars]))
            return commonChars;

        // It's possible we matched somewhere in the middle of a segment e.g. C:\Foodie and C:\Foobar.
        while (commonChars > 0 && !IsDirectorySeparator(first[commonChars - 1]))
            commonChars--;

        return commonChars;
    }

    public static int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
    {
        if (ignoreCase)
        {
            first = first.ToLower();
            second = second.ToLower();


        }

        var to = Math.Min(first.Length, second.Length);
        for (int i = 0; i < to; i++)
        {
            if (first[i] != second[i])
            {
                return i;
            }
        }

        return 0;
    }

    /// <summary>
    /// Returns true if the two paths have the same root
    /// </summary>
    internal static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
    {
        int firstRootLength = GetRootLength(first.AsSpan());
        int secondRootLength = GetRootLength(second.AsSpan());

        return firstRootLength == secondRootLength
            && string.Compare(
                strA: first,
                indexA: 0,
                strB: second,
                indexB: 0,
                length: firstRootLength,
                comparisonType: comparisonType) == 0;
    }

    /// <summary>
    /// Returns true if the path uses the canonical form of extended syntax ("\\?\" or "\??\"). If the
    /// path matches exactly (cannot use alternate directory separators) Windows will skip normalization
    /// and path length checks.
    /// </summary>
    internal static bool IsExtended(ReadOnlySpan<char> path)
    {
        // While paths like "//?/C:/" will work, they're treated the same as "\\.\" paths.
        // Skipping of normalization will *only* occur if back slashes ('\') are used.
        return path.Length >= DevicePrefixLength
            && path[0] == '\\'
            && (path[1] == '\\' || path[1] == '?')
            && path[2] == '?'
            && path[3] == '\\';
    }

    /// <summary>
    /// Returns true if the path uses any of the DOS device path syntaxes. ("\\.\", "\\?\", or "\??\")
    /// </summary>
    internal static bool IsDevice(ReadOnlySpan<char> path)
    {
        // If the path begins with any two separators is will be recognized and normalized and prepped with
        // "\??\" for internal usage correctly. "\??\" is recognized and handled, "/??/" is not.
        return IsExtended(path)
            ||
            (
                path.Length >= DevicePrefixLength
                && IsDirectorySeparator(path[0])
                && IsDirectorySeparator(path[1])
                && (path[2] == '.' || path[2] == '?')
                && IsDirectorySeparator(path[3])
            );
    }

    /// <summary>
    /// Returns true if the path is a device UNC (\\?\UNC\, \\.\UNC\)
    /// </summary>
    internal static bool IsDeviceUNC(ReadOnlySpan<char> path)
    {
        return path.Length >= UncExtendedPrefixLength
            && IsDevice(path)
            && IsDirectorySeparator(path[7])
            && path[4] == 'U'
            && path[5] == 'N'
            && path[6] == 'C';
    }

    /// <summary>
    /// True if the given character is a directory separator.
    /// </summary>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsDirectorySeparator(char c)
    {
        return c == DirectorySeparatorChar || c == AltDirectorySeparatorChar;
    }

    internal const char DirectorySeparatorChar = '\\';
    internal const char AltDirectorySeparatorChar = '/';
    // \\
    internal const int UncPrefixLength = 2;
    // \\?\UNC\, \\.\UNC\
    internal const int UncExtendedPrefixLength = 8;

    /// <summary>
    /// Gets the length of the root of the path (drive, share, etc.).
    /// </summary>
    internal static int GetRootLength(ReadOnlySpan<char> path)
    {
        int pathLength = path.Length;
        int i = 0;

        bool deviceSyntax = IsDevice(path);
        bool deviceUnc = deviceSyntax && IsDeviceUNC(path);

        if ((!deviceSyntax || deviceUnc) && pathLength > 0 && IsDirectorySeparator(path[0]))
        {
            // UNC or simple rooted path (e.g. "\foo", NOT "\\?\C:\foo")
            if (deviceUnc || (pathLength > 1 && IsDirectorySeparator(path[1])))
            {
                // UNC (\\?\UNC\ or \\), scan past server\share

                // Start past the prefix ("\\" or "\\?\UNC\")
                i = deviceUnc ? UncExtendedPrefixLength : UncPrefixLength;

                // Skip two separators at most
                int n = 2;
                while (i < pathLength && (!IsDirectorySeparator(path[i]) || --n > 0))
                    i++;
            }
            else
            {
                // Current drive rooted (e.g. "\foo")
                i = 1;
            }
        }
        else if (deviceSyntax)
        {
            // Device path (e.g. "\\?\.", "\\.\")
            // Skip any characters following the prefix that aren't a separator
            i = DevicePrefixLength;
            while (i < pathLength && !IsDirectorySeparator(path[i]))
                i++;

            // If there is another separator take it, as long as we have had at least one
            // non-separator after the prefix (e.g. don't take "\\?\\", but take "\\?\a\")
            if (i < pathLength && i > DevicePrefixLength && IsDirectorySeparator(path[i]))
                i++;
        }
        else if (pathLength >= 2
            && path[1] == VolumeSeparatorChar
            && IsValidDriveChar(path[0]))
        {
            // Valid drive specified path ("C:", "D:", etc.)
            i = 2;

            // If the colon is followed by a directory separator, move past it (e.g "C:\")
            if (pathLength > 2 && IsDirectorySeparator(path[2]))
                i++;
        }

        return i;
    }

    /// <summary>
    /// Returns true if the given character is a valid drive letter
    /// </summary>
    internal static bool IsValidDriveChar(char value)
    {
        return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
    }

    internal const char VolumeSeparatorChar = ':';
    internal const int DevicePrefixLength = 4;
} 
#endregion